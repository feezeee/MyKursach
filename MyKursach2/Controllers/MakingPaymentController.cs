//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MyKursach2.Data;
//using MyKursach2.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MyKursach2.Controllers
//{
//    public class MakingPaymentController : Controller
//    {
//        private ApplicationDbContext _context;
//        public MakingPaymentController(ApplicationDbContext context)
//        {
//            _context = context;
//        }        

//        [Authorize(Roles = "Директор, Администратор")]
//        [HttpGet]
//        public IActionResult Create(int operationId)
//        {
//            CompletedPayment makingPayment = new CompletedPayment();
//            makingPayment.OperationId = operationId;
//            ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName"); 
//            ViewBag.PaymentMethods = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName"); 
//            return View(makingPayment);
//        }

//        [Authorize(Roles = "Директор, Администратор")]
//        [HttpPost]
//        public async Task<IActionResult> Create(CompletedPayment makingPayment)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(makingPayment);

//                await _context.SaveChangesAsync();
//                return Redirect($"/Operation/Create?operationId={makingPayment.OperationId}");
//            }
//            ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
//            ViewBag.PaymentMethods = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName");
//            return View(makingPayment);
//        }   

//        [Authorize(Roles = "Директор, Администратор")]
//        [HttpGet]
//        public IActionResult Edit(int? id)
//        {
//            if (id == 0)
//            {
//                return RedirectToAction("/Operation/List");
//            }
//            CompletedPayment makingPayment = _context.MakingPayments.Find(id);
//            if (makingPayment != null)
//            {                
//                ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
//                ViewBag.PaymentMethods = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName");
//                return View(makingPayment);
//            }
//            return RedirectToAction("List");
        
//        }

//        [Authorize(Roles = "Директор, Администратор")]
//        [HttpPost]
//        public async Task<IActionResult> Edit(CompletedPayment makingPayment)
//        {
//            if (ModelState.IsValid)
//            {
                
//                _context.Entry(makingPayment).State = EntityState.Modified;
//                await _context.SaveChangesAsync();
//                return RedirectToAction("List");
//            }

//            ViewBag.AvailablePayments = new SelectList(_context.AvailablePayments, "Id", "AvailablePaymentName");
//            ViewBag.PaymentMethods = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName");
//            return View(makingPayment);
//        }

//        [HttpGet]
//        public IActionResult Delete(int? id)
//        {
//            CompletedPayment makingPayment = _context.MakingPayments.Find(id);
//            if (makingPayment == null)
//            {
//                return RedirectToAction("/Operation/List");
//            }
            
//            return View(makingPayment);
               
//        }

//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeleteConfirmed(int? id)
//        {
//            CompletedPayment makingPayment = _context.MakingPayments.Find(id);
//            if (makingPayment == null)
//            {
//                return RedirectToAction("/Operation/List");
//            }
//            _context.MakingPayments.Remove(makingPayment);
//            _context.SaveChanges();
//            return RedirectToAction("List");
//        }

//    }
//}
