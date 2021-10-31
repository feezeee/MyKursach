using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Genders = new SelectList(_context.Gender, "Id", "GenderName"); 
            ViewBag.Positions = new SelectList(_context.Position, "Id", "PositionName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Genders = new SelectList(_context.Gender, "Id", "GenderName");
            ViewBag.Positions = new SelectList(_context.Position, "Id", "PositionName");
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckPhoneNumber(string PhoneNumber)
        {
            var res = _context.Worker.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
            if(res != null)
                return Json(false);
            return Json(true);
        }

    }
}
