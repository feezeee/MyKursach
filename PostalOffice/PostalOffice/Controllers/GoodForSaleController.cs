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
    public class GoodForSaleController : Controller
    {
        private ApplicationDbContext _context;
        public GoodForSaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        const string DirectorAdminKassir = "Директор, Администратор, Кассир";
        const string DirectorAdmin = "Директор, Администратор";

        [Authorize(Roles = DirectorAdminKassir)]
        public async Task<IActionResult> List(GoodForSale goodForSale)
        {
            var res = await _context.GoodsForSale.Include(t=>t.Providers).Include(t=>t.SoldGoods).Include(t=>t.GoodForSale_Providers).OrderBy(t => t.Id).ToListAsync();



            if (goodForSale?.Id > 0)
            {
                res = res.Where(i => i.Id == goodForSale.Id).Select(t => t).ToList();
            }
            if (goodForSale?.Name != null)
            {
                res = res.Where(fn => fn.Name.ToUpper().Contains(goodForSale.Name.ToUpper())).Select(fn => fn).ToList();
            }
            if (goodForSale?.GoodAmount > 0)
            {
                res = res.Where(fn => fn.GoodAmount == goodForSale.GoodAmount).Select(fn => fn).ToList();
            }
            if (goodForSale?.GoodPrice > 0)
            {
                res = res.Where(fn => fn.GoodPrice == goodForSale.GoodPrice).Select(fn => fn).ToList();
            }
            return View(res);
        }

        [Authorize(Roles = DirectorAdmin)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View();
        }

        [Authorize(Roles = DirectorAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create(GoodForSale goodForSale, int[] selectedProviders)
        {
            if (ModelState.IsValid)
            {
                if (selectedProviders != null)
                {
                    
                    foreach (var p in _context.Providers.Where(t => selectedProviders.Contains(t.Id)))
                    {
                        goodForSale.Providers.Add(p);
                    }
                }

                _context.Add(goodForSale);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(goodForSale);
        }
        [Authorize(Roles = DirectorAdmin)]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = await _context.GoodsForSale.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.GoodsForSale.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.GoodsForSale.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }

        [Authorize(Roles = DirectorAdmin)]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }

            GoodForSale goodForSale = await _context.GoodsForSale.Where(t => t.Id == id).Include(t=>t.Providers).Include(t=>t.SoldGoods).Include(t=>t.GoodForSale_Providers).Select(t => t).FirstOrDefaultAsync();

            if (goodForSale != null)
            {
                ViewBag.Provider = await _context.Providers.ToListAsync();
                return View(goodForSale);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = DirectorAdmin)]
        [HttpPost]
        public async Task<IActionResult> Edit(GoodForSale goodForSale, int[] selectedProviders)
        {
            if (ModelState.IsValid)
            {
                GoodForSale newgoodForSale = await _context.GoodsForSale.Where(t => t.Id == goodForSale.Id).Select(t => t).FirstOrDefaultAsync();
                newgoodForSale.Name = goodForSale.Name;
                newgoodForSale.GoodAmount = goodForSale.GoodAmount;
                newgoodForSale.GoodPrice = goodForSale.GoodPrice;

                newgoodForSale.Providers.Clear();
                _context.Entry(newgoodForSale).State = EntityState.Modified;

                _context.Entry(newgoodForSale).Collection(u => u.Providers).Load();
                newgoodForSale.Providers.Clear();
                await _context.SaveChangesAsync();
                if (selectedProviders != null)
                {
                    foreach (var p in _context.Providers.Where(t => selectedProviders.Contains(t.Id)))
                    {
                        newgoodForSale.Providers.Add(p);
                    }
                }
                _context.Entry(newgoodForSale).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(goodForSale);
        }

        [Authorize(Roles = DirectorAdmin)]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            GoodForSale goodForSale = await _context.GoodsForSale.Where(t=>t.Id == id).Include(t=>t.SoldGoods).FirstOrDefaultAsync();
            if (goodForSale == null || goodForSale.SoldGoods.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            return View(goodForSale);
        }

        [Authorize(Roles = DirectorAdmin)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            GoodForSale goodForSale = await _context.GoodsForSale.Where(t => t.Id == id).Include(t => t.SoldGoods).Include(t=>t.Providers).Include(t=>t.GoodForSale_Providers).FirstOrDefaultAsync();

            if (goodForSale == null || goodForSale.SoldGoods.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            goodForSale.Providers.Clear();
            _context.Entry(goodForSale).State = EntityState.Modified;

            _context.Entry(goodForSale).Collection(u => u.Providers).Load();
            goodForSale.Providers.Clear();
            _context.Entry(goodForSale).State = EntityState.Modified;
            _context.GoodsForSale.Remove(goodForSale);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
