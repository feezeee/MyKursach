using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKursach2.Data;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class PaymentMethodController : Controller
    {
        private ApplicationDbContext _context;
        public PaymentMethodController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор")]
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

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Директор, Администратор")]
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
                operation_PaymentMethod.Sum = sum.Value;
                ViewBag.PaymentMethods = new SelectList(await _context.PaymentMethods.ToListAsync(), "Id", "PaymentMethodName");
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
            ViewBag.PaymentMethods = new SelectList(await _context.PaymentMethods.ToListAsync(), "Id", "Name");
            return View(operation_PaymentMethod);

        }


        [Authorize(Roles = "Директор, Администратор")]
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

        [Authorize(Roles = "Директор, Администратор")]
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

        [Authorize(Roles = "Директор, Администратор")]
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
