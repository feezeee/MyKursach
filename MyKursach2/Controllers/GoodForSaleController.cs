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
    public class GoodForSaleController : Controller
    {
        private ApplicationDbContext _context;
        public GoodForSaleController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор, ")]
        public ViewResult List(GoodForSale goodForSale)
        {
            //var res = _context.Workers.Join(_context.Positions);

            //var res = from gfs in _context.GoodsForSale
            //          select new GoodForSale
            //          {
            //              Id = gfs.Id,
            //              Name = gfs.Name,
            //              QuantityInStock = gfs.QuantityInStock,
            //              Providers = gfs.Providers
            //          };
            //var res = _context.GoodsForSale;

            var res = _context.GoodsForSale.Include(c => c.Providers).Select(t=>t);


            if (goodForSale?.Id > 0)
            {
                res = res.Where(i => i.Id == goodForSale.Id).Select(i => i);
            }
            if (goodForSale?.Name != null)
            {
                res = res.Where(fn => fn.Name.ToUpper().Contains(goodForSale.Name.ToUpper())).Select(fn => fn);
            }
            if (goodForSale?.GoodAmount != null)
            {
                res = res.Where(fn => fn.GoodAmount == goodForSale.GoodAmount).Select(fn => fn);
            }
            return View(res);
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Provider = _context.Providers;
            return View();
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Create(GoodForSale goodForSale, int[] selectedProviders)
        {
            if (ModelState.IsValid)
            {
                if (selectedProviders != null)
                {

                    foreach(var p in _context.Providers.Where(t => selectedProviders.Contains(t.Id)))
                    {
                        goodForSale.Providers.Add(p);
                    }
                }

                _context.Add(goodForSale);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = _context.GoodsForSale.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.GoodsForSale.Where(t => t.Name == Name).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.GoodsForSale.Where(t => t.Name == Name).Select(t => t).FirstOrDefault();
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

            //var res = from gfs in _context.GoodsForSale
            //          where gfs.Id == id
            //          select new GoodForSale
            //          {
            //              Id = gfs.Id,
            //              Name = gfs.Name,
            //              QuantityInStock = gfs.QuantityInStock,
            //              Providers = gfs.Providers
            //          };
            var res = _context.GoodsForSale.Include(c => c.Providers).Where(t => t.Id == id).Select(t => t);

            GoodForSale goodForSale = res.FirstOrDefault();

            if (goodForSale != null)
            {
                ViewBag.Provider = _context.Providers;
                return View(goodForSale);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(GoodForSale goodForSale, int[] selectedProviders)
        {
            if (ModelState.IsValid)
            {
                //var res = from gfs in _context.GoodsForSale
                //          where gfs.Id == goodForSale.Id
                //          select new GoodForSale
                //          {
                //              Id = gfs.Id,
                //              Name = gfs.Name,
                //              QuantityInStock = gfs.QuantityInStock,
                //              Providers = gfs.Providers
                //          };

                var res = _context.GoodsForSale.Include(c => c.Providers).Where(t => t.Id == goodForSale.Id).Select(t => t);

                GoodForSale newgoodForSale = res.FirstOrDefault();
                newgoodForSale.Name = goodForSale.Name;
                newgoodForSale.GoodAmount = goodForSale.GoodAmount;

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

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            GoodForSale goodForSale = _context.GoodsForSale.Find(id);
            if (goodForSale == null)
            {
                return View("List");
            }

            return View(goodForSale);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            GoodForSale goodForSale = _context.GoodsForSale.Find(id);
            if (goodForSale == null)
            {
                return View("List");
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
