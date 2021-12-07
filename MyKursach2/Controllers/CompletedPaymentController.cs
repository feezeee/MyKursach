using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKursach2.Data;
using MyKursach2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class CompletedPaymentController : Controller
    {
        private ApplicationDbContext _context;
        public CompletedPaymentController(ApplicationDbContext context)
        {
            _context = context;
        }


        const string DirectorAdminKassir = "Директор, Администратор, Кассир";
        const string DirectorAdmin = "Директор, Администратор";


        [Authorize(Roles = DirectorAdminKassir)]
        [HttpGet]
        public IActionResult Create(int operationId)
        {
            CompletedPayment makingPayment = new CompletedPayment();
            makingPayment.OperationId = operationId;
            ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
           
            return View(makingPayment);
        }

        [Authorize(Roles = DirectorAdminKassir)]
        [HttpPost]
        public async Task<IActionResult> Create(CompletedPayment makingPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(makingPayment);

                await _context.SaveChangesAsync();
                return RedirectToRoute("default", new { controller = "Operation", action = "Create", operationId = makingPayment.OperationId });
            }
            ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
           
            return View(makingPayment);
        }


        [Authorize(Roles = DirectorAdminKassir)]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List", "Operation");
            }
            CompletedPayment completedPayment = await _context.CompletedPayments.Include(t => t.AvailablePayment).Include(t => t.Operation).Where(t => t.Id == id.Value).FirstOrDefaultAsync();
            if (completedPayment != null)
            {
                _context.CompletedPayments.Remove(completedPayment);
                await _context.SaveChangesAsync();
                completedPayment.Id = 0;
                ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
                return View("Create", completedPayment);
            }
            return RedirectToAction("List", "Operation");
        }

        //[Authorize(Roles = "Директор, Администратор")]
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id == 0)
        //    {
        //        return RedirectToAction("/Operation/List");
        //    }
        //    CompletedPayment makingPayment = _context.MakingPayments.Find(id);
        //    if (makingPayment != null)
        //    {
        //        ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
        //        ViewBag.PaymentMethods = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName");
        //        return View(makingPayment);
        //    }
        //    return RedirectToAction("List");

        //}

        //[Authorize(Roles = "Директор, Администратор")]
        //[HttpPost]
        //public async Task<IActionResult> Edit(CompletedPayment makingPayment)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _context.Entry(makingPayment).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("List");
        //    }

        //    ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
        //    return View(makingPayment);
        //}

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    CompletedPayment makingPayment = _context.MakingPayments.Find(id);
        //    if (makingPayment == null)
        //    {
        //        return RedirectToAction("/Operation/List");
        //    }

        //    return View(makingPayment);

        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int? id)
        //{
        //    CompletedPayment makingPayment = _context.MakingPayments.Find(id);
        //    if (makingPayment == null)
        //    {
        //        return RedirectToAction("/Operation/List");
        //    }
        //    _context.MakingPayments.Remove(makingPayment);
        //    _context.SaveChanges();
        //    return RedirectToAction("List");
        //}

    }
}
