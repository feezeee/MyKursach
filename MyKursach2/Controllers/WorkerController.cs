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
    public class WorkerController : Controller
    {
        private ApplicationDbContext _context;
        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Директор, Администратор")]

        public ViewResult List(Worker worker)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from work in _context.Worker
                      join position in _context.Position on work.PositionId equals position.Id
                      join gender in _context.Gender on work.GenderId equals gender.Id
                      select new Worker
                      {
                          Id = work.Id,
                          FirstName = work.FirstName,
                          LastName = work.LastName,
                          Email = work.Email,
                          DateOfBirth = work.DateOfBirth,
                          GenderId = work.GenderId,
                          Gender = gender,
                          PositionId = work.PositionId,
                          Position = position,
                          Password = work.Password,
                          PhoneNumber = work.PhoneNumber
                      };

            if (worker?.Id > 0)
            {
                res = res.Where(i => i.Id == worker.Id).Select(i => i);
            }
            if (worker?.FirstName != null)
            {
                res = res.Where(fn => fn.FirstName.ToUpper().Contains(worker.FirstName.ToUpper())).Select(fn => fn);
            }
            if (worker?.LastName != null)
            {
                res = res.Where(ln => ln.LastName.ToUpper().Contains(worker.LastName.ToUpper())).Select(ln => ln);
            }            
            if (worker?.Position?.PositionName != null)
            {
                res = res.Where(ln => ln.Position.PositionName.ToUpper().Contains(worker.Position.PositionName.ToUpper())).Select(ln => ln);
            }
            return View(res);
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Genders = new SelectList(_context.Gender, "Id", "GenderName"); 
            ViewBag.Positions = new SelectList(_context.Position, "Id", "PositionName");
            return View();
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.Genders = new SelectList(_context.Gender, "Id", "GenderName");
            ViewBag.Positions = new SelectList(_context.Position, "Id", "PositionName");
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckPhoneNumber(int? Id, string PhoneNumber)
        {
            if (Id != null)
            {
                var res1 = _context.Worker.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.Worker.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.Worker.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
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
            Worker worker = _context.Worker.Find(id);
            if (worker != null)
            {
                var gend = _context.Gender;
                var pos = _context.Position;
                worker.Gender = gend.Where(t => t.Id == worker.GenderId).Select(t => t).FirstOrDefault();
                worker.Position = pos.Where(t => t.Id == worker.PositionId).Select(t => t).FirstOrDefault();
                ViewBag.Genders = new SelectList(gend, "Id", "GenderName");
                ViewBag.Positions = new SelectList(pos, "Id", "PositionName");
                return View(worker);
            }
            return RedirectToAction("List");
        
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(worker).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            var gend = _context.Gender;
            var pos = _context.Position;
            ViewBag.Genders = new SelectList(gend, "Id", "GenderName");
            ViewBag.Positions = new SelectList(pos, "Id", "PositionName");
            return View(worker);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Worker worker = _context.Worker.Find(id);
            if (worker == null)
            {
                //return HttpNotFound();
            }
            if (worker?.Id == AuthorizedUser.GetInstance().GetWorker().Id)
            {
                //return View("Fail");
            }
            return View(worker);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Worker worker = _context.Worker.Find(id);
            if (worker == null)
            {
                //return HttpNotFound();
            }
            _context.Worker.Remove(worker);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
