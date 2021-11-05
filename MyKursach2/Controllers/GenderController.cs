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
    public class GenderController : Controller
    {
        private ApplicationDbContext _context;
        public GenderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор")]
        public ViewResult List(Gender gender)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from gend in _context.Gender
                      select new Gender
                      {
                          Id = gend.Id,
                          GenderName = gend.GenderName
                      };

            if (gender?.Id > 0)
            {
                res = res.Where(i => i.Id == gender.Id).Select(i => i);
            }
            if (gender?.GenderName != null)
            {
                res = res.Where(fn => fn.GenderName.ToUpper().Contains(gender.GenderName.ToUpper())).Select(fn => fn);
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
        public async Task<IActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }            
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckGenderName(int? Id, string GenderName)
        {
            if (Id != null)
            {
                var res1 = _context.Gender.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.Gender.Where(t => t.GenderName == GenderName).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.Gender.Where(t => t.GenderName == GenderName).Select(t => t).FirstOrDefault();
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
            Gender gender = _context.Gender.Find(id);
            if (gender != null)
            {                
                return View(gender);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(Gender gender)
        {
            if (ModelState.IsValid)
            {
                //if (worker.Id == AuthorizedUser.GetInstance().GetWorker().Id)
                //{
                //    worker.Position = _context.Position.Find(worker.PositionId);
                //    worker.Gender = _context.Gender.Find(worker.GenderId);
                //    AuthorizedUser.GetInstance().ClearUser();
                //    AuthorizedUser.GetInstance().SetUser(worker);
                //}
                _context.Entry(gender).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            
            return View(gender);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Gender gender = _context.Gender.Find(id);
            if (gender == null)
            {
                //return HttpNotFound();
            }
            //else if (gender?.Id == AuthorizedUser.GetInstance().GetWorker().Id)
            //{
            //    return RedirectToRoute("default", new { controller = "Worker", action = "Edit", id = id });
            //}

            return View(gender);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Gender gender = _context.Gender.Find(id);
            if (gender == null)
            {
                //return HttpNotFound();
            }
            _context.Gender.Remove(gender);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
