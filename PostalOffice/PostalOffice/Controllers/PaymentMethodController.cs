using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostalOffice.Data;
using PostalOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalOffice.Controllers
{
    public class PaymentMethodController : Controller
    {
        private ApplicationDbContext _context;
        public PaymentMethodController(ApplicationDbContext context)
        {
            _context = context;
        }

        const string AdminKassir = "Администратор, Кассир";
        const string Admin = "Администратор";

        [Authorize(Roles = AdminKassir)]
        public async Task<IActionResult> List(PaymentMethod pay)
        {
            var res = await _context.PaymentMethods.Include(t => t.Operations).Include(t => t.Operations_PaymentMethods).OrderBy(t => t.Id).ToListAsync();

            if (pay?.Id > 0)
            {
                res = res.Where(i => i.Id == pay.Id).ToList();
            }
            if (pay?.PaymentMethodName != null)
            {
                res = res.Where(fn => fn.PaymentMethodName.ToUpper().Contains(pay.PaymentMethodName.ToUpper())).Select(fn => fn).ToList();
            }
            return View(res);
        }

        [Authorize(Roles = Admin)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(PaymentMethod pay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pay);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(pay);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToOperation(int? operationId, int? sum)
        {
            if (operationId != null && sum != null)
            {
                Operation_PaymentMethod operation_PaymentMethod = new Operation_PaymentMethod();
                operation_PaymentMethod.OperationId = operationId.Value;

                int _sum = 0;
                foreach (var allsum in _context.Operation_PaymentMethods.Where(t => t.OperationId == operationId.Value).ToList())
                {
                    _sum += allsum.Sum;
                }
                operation_PaymentMethod.Sum = sum.Value - _sum;
                var op = await _context.Operations.Include(t => t.PaymentMethods).Where(t => t.Id == operationId.Value).FirstOrDefaultAsync();
                ViewBag.PaymentMethods = new SelectList((await _context.PaymentMethods.ToListAsync()).Except(op.PaymentMethods), "Id", "PaymentMethodName");
                return View(operation_PaymentMethod);
            }
            return RedirectToAction("Create", "Operation",new { id = operationId });

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToOperation(Operation_PaymentMethod operation_PaymentMethod)
        {

            if (ModelState.IsValid)
            {
                _context.Add(operation_PaymentMethod);

                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Operation", new { operationId = operation_PaymentMethod.OperationId });
            }
            var op = await _context.Operations.Include(t => t.PaymentMethods).Where(t => t.Id == operation_PaymentMethod.OperationId).FirstOrDefaultAsync();
            ViewBag.PaymentMethods = new SelectList((await _context.PaymentMethods.ToListAsync()).Except(op.PaymentMethods), "Id", "PaymentMethodName");

            return View(operation_PaymentMethod);

        }

        [Authorize(Roles = AdminKassir)]
        [HttpGet]
        public async Task<IActionResult> DeleteInOperation(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return RedirectToAction("List", "Operation");
            }

            string[] new_id = id.Split('_');
            if(new_id.Length != 2)
            {
                return RedirectToAction("List", "Operation");
            }
            int operation_id = int.Parse(new_id[0]);
            int payment_id = int.Parse(new_id[1]);

            Operation operation = await _context.Operations.Include(t => t.PaymentMethods).Include(t => t.Operations_PaymentMethods).Where(t => t.Id == operation_id).FirstOrDefaultAsync();
            PaymentMethod paymentMethod = await _context.PaymentMethods.Include(t => t.Operations).Include(t => t.Operations_PaymentMethods).Where(t => t.Id == payment_id).FirstOrDefaultAsync();
            if (operation != null && paymentMethod != null)
            {
                var perem = operation.Operations_PaymentMethods.FirstOrDefault(t => t.PaymentMethodId == paymentMethod.Id);
                operation.Operations_PaymentMethods.Remove(perem);
                await _context.SaveChangesAsync();
            }
            return RedirectToRoute("default", new { controller = "Operation", action = "Create", operationId = operation_id });
        }

        [Authorize(Roles = Admin)]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckPaymentMethodName(int? Id, string PaymentMethodName)
        {
            if (Id != null)
            {
                var res1 = await _context.PaymentMethods.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.PaymentMethods.Where(t => t.PaymentMethodName == PaymentMethodName).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.PaymentMethods.Where(t => t.PaymentMethodName == PaymentMethodName).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }

        [Authorize(Roles = Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }
            PaymentMethod pay = await _context.PaymentMethods.Include(t => t.Operations_PaymentMethods).Include(t => t.Operations).Where(t=>t.Id == id).FirstOrDefaultAsync();
            if (pay != null)
            {
                return View(pay);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(PaymentMethod pay)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(pay).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            pay.Operations_PaymentMethods = await _context.Operation_PaymentMethods.Where(t => t.PaymentMethodId == pay.Id).ToListAsync();
            return View(pay);
        }


        [Authorize(Roles = Admin)]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            PaymentMethod payment = await _context.PaymentMethods.Include(t => t.Operations).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (payment == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if(payment.Operations?.Count == 0)
            {
                return View(payment);
            }

            return RedirectToAction("Edit", new { id = id });
        }


        [Authorize(Roles = Admin)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            PaymentMethod payment = await _context.PaymentMethods.Include(t => t.Operations).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (payment == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (payment.Operations?.Count == 0)
            {
                _context.PaymentMethods.Remove(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = id });

        }

    }
}
