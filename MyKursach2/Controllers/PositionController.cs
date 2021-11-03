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
    public class PositionController : Controller
    {
        private ApplicationDbContext _context;
        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор")]
        public ViewResult List(Position position)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from pos in _context.Position                      
                      select new Position
                      {
                          Id = pos.Id,
                          PositionName = pos.PositionName
                      };

            if (position?.Id > 0)
            {
                res = res.Where(i => i.Id == position.Id).Select(i => i);
            }
            if (position?.PositionName != null)
            {
                res = res.Where(fn => fn.PositionName.ToUpper().Contains(position.PositionName.ToUpper())).Select(fn => fn);
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
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }           
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckPositionName(int? Id, string PositionName)
        {
            if (Id != null)
            {
                var res1 = _context.Position.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.Position.Where(t => t.PositionName == PositionName).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.Position.Where(t => t.PositionName == PositionName).Select(t => t).FirstOrDefault();
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
            Position position = _context.Position.Find(id);
            if (position != null)
            {                
                return View(position);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                if (position.Id == AuthorizedUser.GetInstance().GetWorker().PositionId)
                {
                    AuthorizedUser.GetInstance().GetWorker().Position.PositionName = position.PositionName;                    
                }
                _context.Entry(position).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(position);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Position position = _context.Position.Find(id);
            if (position == null)
            {
                //return HttpNotFound();
            }
            else if (position?.Id == AuthorizedUser.GetInstance().GetWorker().PositionId)
            {
                return RedirectToRoute("default", new { controller = "Position", action = "Edit", id = id });
            }

            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Position position = _context.Position.Find(id);
            if (position == null)
            {
                //return HttpNotFound();
            }
            _context.Position.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("List");
        }


    }
}
