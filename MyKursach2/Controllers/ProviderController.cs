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
    public class ProviderController : Controller
    {
        private ApplicationDbContext _context;
        public ProviderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор, ")]
        public ViewResult List(Provider provider)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from prv in _context.Providers
                      select new Provider
                      {
                          Id = prv.Id,
                          Name = prv.Name,
                          PhoneNumber = prv.PhoneNumber,
                          Email = prv.Email,
                          GoodsForSale = prv.GoodsForSale
                      };

            if (provider?.Id > 0)
            {
                res = res.Where(i => i.Id == provider.Id).Select(i => i);
            }
            if (provider?.Name != null)
            {
                res = res.Where(fn => fn.Name.ToUpper().Contains(provider.Name.ToUpper())).Select(fn => fn);
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
        public async Task<IActionResult> Create(Provider provider)
        {
            if (ModelState.IsValid)
            {

                _context.Add(provider);

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
                var res1 = _context.Providers.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.Providers.Where(t => t.Name == Name).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.Providers.Where(t => t.Name == Name).Select(t => t).FirstOrDefault();
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
            

            var res = from prv in _context.Providers
                      where prv.Id == id
                      select new Provider
                      {
                          Id = prv.Id,
                          Name = prv.Name,
                          PhoneNumber = prv.PhoneNumber,
                          Email = prv.Email,
                          GoodsForSale = prv.GoodsForSale
                      };

            Provider provider = res.FirstOrDefault();

            if (provider != null)
            {
                return View(provider);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(Provider provider)
        {
            if (ModelState.IsValid)
            {
                var res = from prv in _context.Providers
                          where prv.Id == provider.Id
                          select new Provider
                          {
                              Id = prv.Id,
                              Name = prv.Name,
                              PhoneNumber = prv.PhoneNumber,
                              Email = prv.Email,
                              GoodsForSale = prv.GoodsForSale
                          };

                Provider newProvider = res.FirstOrDefault();
                newProvider.Name = provider.Name;
                newProvider.PhoneNumber = provider.PhoneNumber;
                newProvider.Email = provider.Email;
                _context.Entry(newProvider).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(provider);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Provider provider = _context.Providers.Find(id);
            if (provider == null)
            {
                //return HttpNotFound();
            }

            return View(provider);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Provider provider = _context.Providers.Find(id);
            if (provider == null)
            {
                //return HttpNotFound();
            }

            provider.GoodsForSale.Clear();
            _context.Entry(provider).State = EntityState.Modified;

            _context.Entry(provider).Collection(u => u.GoodsForSale).Load();
            provider.GoodsForSale.Clear();
            _context.Entry(provider).State = EntityState.Modified;
            _context.Providers.Remove(provider);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
