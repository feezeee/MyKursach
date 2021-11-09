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
        public ViewResult List(PaymentMethod pay)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from paym in _context.PaymentMethods
                      select new PaymentMethod
                      {
                          Id = paym.Id,
                          PaymentMethodName = paym.PaymentMethodName
                      };

            if (pay?.Id > 0)
            {
                res = res.Where(i => i.Id == pay.Id).Select(i => i);
            }
            if (pay?.PaymentMethodName != null)
            {
                res = res.Where(fn => fn.PaymentMethodName.ToUpper().Contains(pay.PaymentMethodName.ToUpper())).Select(fn => fn);
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
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckPaymentMethodName(int? Id, string PaymentMethodName)
        {
            if (Id != null)
            {
                var res1 = _context.PaymentMethods.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.PaymentMethods.Where(t => t.PaymentMethodName == PaymentMethodName).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.PaymentMethods.Where(t => t.PaymentMethodName == PaymentMethodName).Select(t => t).FirstOrDefault();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }
            PaymentMethod pay = _context.PaymentMethods.Find(id);
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
            return View(pay);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            PaymentMethod payment = _context.PaymentMethods.Find(id);
            if (payment == null)
            {
                //return HttpNotFound();
            }

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            PaymentMethod payment = _context.PaymentMethods.Find(id);
            if (payment == null)
            {
                //return HttpNotFound();
            }
            _context.PaymentMethods.Remove(payment);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
