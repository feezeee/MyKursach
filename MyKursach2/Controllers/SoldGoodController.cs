using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyKursach2.Data;
using MyKursach2.Models;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class SoldGoodController : Controller
    {
        private ApplicationDbContext _context;
        public SoldGoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Create(int operationId)
        {
            SoldGood soldGood = new SoldGood();

            soldGood.OperationId = operationId;

            ViewBag.GoodsForSale = new SelectList(_context.GoodsForSale, "Id", "Name");

            
            return View(soldGood);
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Create(SoldGood soldGood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldGood);

                await _context.SaveChangesAsync();
                return RedirectToRoute("default", new { controller = "Operation", action = "Create", operationId = soldGood.OperationId });
            }
            ViewBag.GoodsForSale = new SelectList(_context.GoodsForSale, "Id", "Name");

            return View(soldGood);
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
    }
}
