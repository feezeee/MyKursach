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
    public class DeliveryCountryController : Controller
    {
        private ApplicationDbContext _context;
        public DeliveryCountryController(ApplicationDbContext context)
        {
            _context = context;
        }


        const string AdminKassir = "Администратор, Кассир";
        const string Admin = "Администратор";

        [Authorize(Roles = AdminKassir)]
        public async Task<IActionResult> List(DeliveryCountry deliveryCountry)
        {
            var res = await _context.DeliveryCountries.FromSqlRaw("get_countries_delivery").ToListAsync();

            if (deliveryCountry?.Id > 0)
            {
                res = res.Where(i => i.Id == deliveryCountry.Id).ToList();
            }
            if (deliveryCountry?.DeliveryCountryName != null)
            {
                res = res.Where(fn => fn.DeliveryCountryName.ToUpper().Contains(deliveryCountry.DeliveryCountryName.ToUpper())).Select(fn => fn).ToList();
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
        public async Task<IActionResult> Create(DeliveryCountry deliveryCountry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryCountry);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            deliveryCountry.DeliveryGoods = await _context.DeliveryGoods.Where(t => t.DeliveryCountryId == deliveryCountry.Id).ToListAsync();
            return View(deliveryCountry);
        }

        [Authorize(Roles = Admin)]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckDeliveryCountryName(int? Id, string DeliveryCountryName)
        {
            if (Id != null)
            {
                var res1 = await _context.DeliveryCountries.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.DeliveryCountries.Where(t => t.DeliveryCountryName == DeliveryCountryName).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.DeliveryCountries.Where(t => t.DeliveryCountryName == DeliveryCountryName).Select(t => t).FirstOrDefaultAsync();
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
            DeliveryCountry deliveryCountry = await _context.DeliveryCountries.Include(t => t.DeliveryGoods).Where(t=>t.Id == id).FirstOrDefaultAsync();
            if (deliveryCountry != null)
            {
                return View(deliveryCountry);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(DeliveryCountry deliveryCountry)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(deliveryCountry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            deliveryCountry.DeliveryGoods = await _context.DeliveryGoods.Where(t => t.DeliveryCountryId == deliveryCountry.Id).ToListAsync();

            return View(deliveryCountry);
        }

        [Authorize(Roles = Admin)]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            DeliveryCountry deliveryCountry = await _context.DeliveryCountries.Include(t => t.DeliveryGoods).Where(t=>t.Id == id).FirstOrDefaultAsync();
            if (deliveryCountry == null || deliveryCountry.DeliveryGoods?.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }         

            return View(deliveryCountry);
        }


        [Authorize(Roles = Admin)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            DeliveryCountry deliveryCountry = await _context.DeliveryCountries.Include(t => t.DeliveryGoods).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (deliveryCountry == null || deliveryCountry.DeliveryGoods?.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            _context.DeliveryCountries.Remove(deliveryCountry);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

    }

}

