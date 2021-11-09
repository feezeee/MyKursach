using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyKursach2.Data;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class AvailablePaymentController : Controller
    {
        private ApplicationDbContext _context;
        public AvailablePaymentController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор, ")]
        public ViewResult List(AvailablePayment availablePayment)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from availPay in _context.AvailablePayments
                      select new AvailablePayment
                      {
                          Id = availPay.Id,
                          AvailablePaymentName = availPay.AvailablePaymentName
                      };

            if (availablePayment?.Id > 0)
            {
                res = res.Where(i => i.Id == availablePayment.Id).Select(i => i);
            }
            if (availablePayment?.AvailablePaymentName != null)
            {
                res = res.Where(fn => fn.AvailablePaymentName.ToUpper().Contains(availablePayment.AvailablePaymentName.ToUpper())).Select(fn => fn);
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
        public async Task<IActionResult> Create(AvailablePayment availablePayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availablePayment);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckAvailablePaymentName(int? Id, string AvailablePaymentName)
        {
            if (Id != null)
            {
                var res1 = _context.AvailablePayments.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.AvailablePayments.Where(t => t.AvailablePaymentName == AvailablePaymentName).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.AvailablePayments.Where(t => t.AvailablePaymentName == AvailablePaymentName).Select(t => t).FirstOrDefault();
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
            AvailablePayment availablePayment = _context.AvailablePayments.Find(id);
            if (availablePayment != null)
            {
                return View(availablePayment);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(AvailablePayment availablePayment)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(availablePayment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(availablePayment);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            AvailablePayment availablePayment = _context.AvailablePayments.Find(id);
            if (availablePayment == null)
            {
                //return HttpNotFound();
            }

            return View(availablePayment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            AvailablePayment availablePayment = _context.AvailablePayments.Find(id);
            if (availablePayment == null)
            {
                //return HttpNotFound();
            }
            _context.AvailablePayments.Remove(availablePayment);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
