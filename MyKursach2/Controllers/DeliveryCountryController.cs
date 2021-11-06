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
    public class DeliveryCountryController : Controller
    {
        private ApplicationDbContext _context;
        public DeliveryCountryController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор, Кассир, Почтальон")]
        public ViewResult List(DeliveryCountry deliveryCountry)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from delcount in _context.DeliveryCountry
                      select new DeliveryCountry
                      {
                          Id = delcount.Id,
                          DeliveryCountryName = delcount.DeliveryCountryName
                      };

            if (deliveryCountry?.Id > 0)
            {
                res = res.Where(i => i.Id == deliveryCountry.Id).Select(i => i);
            }
            if (deliveryCountry?.DeliveryCountryName != null)
            {
                res = res.Where(fn => fn.DeliveryCountryName.ToUpper().Contains(deliveryCountry.DeliveryCountryName.ToUpper())).Select(fn => fn);
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
        public async Task<IActionResult> Create(DeliveryCountry deliveryCountry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryCountry);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View();
        }

        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckDeliveryCountryName(int? Id, string DeliveryCountryName)
        {
            if (Id != null)
            {
                var res1 = _context.DeliveryCountry.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.DeliveryCountry.Where(t => t.DeliveryCountryName == DeliveryCountryName).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.DeliveryCountry.Where(t => t.DeliveryCountryName == DeliveryCountryName).Select(t => t).FirstOrDefault();
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
            DeliveryCountry deliveryCountry = _context.DeliveryCountry.Find(id);
            if (deliveryCountry != null)
            {
                return View(deliveryCountry);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(DeliveryCountry deliveryCountry)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(deliveryCountry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(deliveryCountry);
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            DeliveryCountry deliveryCountry = _context.DeliveryCountry.Find(id);
            if (deliveryCountry == null)
            {
                //return HttpNotFound();
            }

            return View(deliveryCountry);
        }


        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            DeliveryCountry deliveryCountry = _context.DeliveryCountry.Find(id);
            if (deliveryCountry == null)
            {
                //return HttpNotFound();
            }
            _context.DeliveryCountry.Remove(deliveryCountry);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }

}

