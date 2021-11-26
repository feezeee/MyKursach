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
        public async Task<IActionResult> List(AvailablePayment availablePayment)
        {
            var res = await _context.AvailablePayments.Include(t => t.CompletedPayment).OrderBy(t => t.Id).ToListAsync();

            if (availablePayment?.Id > 0)
            {
                res = res.Where(i => i.Id == availablePayment.Id).ToList();
            }
            if (availablePayment?.AvailablePaymentName != null)
            {
                res = res.Where(fn => fn.AvailablePaymentName.ToUpper().Contains(availablePayment.AvailablePaymentName.ToUpper())).Select(fn => fn).ToList();
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
            availablePayment.CompletedPayment = await _context.CompletedPayments.Where(t => t.AvailablePaymentId == availablePayment.Id).ToListAsync();
            return View(availablePayment);
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckAvailablePaymentName(int? Id, string AvailablePaymentName)
        {
            if (Id != null)
            {
                var res1 = await _context.AvailablePayments.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.AvailablePayments.Where(t => t.AvailablePaymentName == AvailablePaymentName).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.AvailablePayments.Where(t => t.AvailablePaymentName == AvailablePaymentName).Select(t => t).FirstOrDefaultAsync();
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
            AvailablePayment availablePayment = await _context.AvailablePayments.Include(t => t.CompletedPayment).Where(t=>t.Id == id).FirstOrDefaultAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            AvailablePayment availablePayment = await _context.AvailablePayments.Where(t=>t.Id == id).Include(t=>t.CompletedPayment).FirstOrDefaultAsync();
            if (availablePayment == null || availablePayment.CompletedPayment.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            return View(availablePayment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            AvailablePayment availablePayment = await _context.AvailablePayments.Where(t => t.Id == id).Include(t => t.CompletedPayment).FirstOrDefaultAsync();

            if (availablePayment == null || availablePayment.CompletedPayment.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            _context.AvailablePayments.Remove(availablePayment);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
