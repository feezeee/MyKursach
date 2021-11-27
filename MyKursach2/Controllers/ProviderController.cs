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
        public async Task<IActionResult> List(Provider provider)
        {
           
            var res = await _context.Providers.Include(c => c.GoodsForSale).Select(t => t).ToListAsync();

            if (provider?.Id > 0)
            {
                res = res.Where(i => i.Id == provider.Id).ToList();
            }
            if (provider?.Name != null)
            {
                res = res.Where(fn => fn.Name.ToUpper().Contains(provider.Name.ToUpper())).Select(fn => fn).ToList();
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
            return View(provider);
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = await _context.Providers.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.Providers.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.Providers.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
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

            Provider provider = await _context.Providers.Include(t=>t.GoodsForSale).Where(t => t.Id == id).Select(t => t).FirstOrDefaultAsync();

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
                Provider newProvider = await _context.Providers.Where(t => t.Id == provider.Id).Select(t => t).FirstOrDefaultAsync();

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
        public async Task<IActionResult> Delete(int? id)
        {
            Provider provider = await _context.Providers.Include(t => t.GoodsForSale).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (provider == null || provider.GoodsForSale?.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            return View(provider);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Provider provider = await _context.Providers.Include(t=>t.GoodsForSale).Where(t=>t.Id == id).FirstOrDefaultAsync();
            if (provider == null || provider.GoodsForSale?.Count != 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            provider.GoodsForSale_Providers.Clear();
            _context.Entry(provider).State = EntityState.Modified;

            _context.Entry(provider).Collection(u => u.GoodsForSale_Providers).Load();
            provider.GoodsForSale_Providers.Clear();
            _context.Entry(provider).State = EntityState.Modified;
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
