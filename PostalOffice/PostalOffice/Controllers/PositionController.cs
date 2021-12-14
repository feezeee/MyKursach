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
    public class PositionController : Controller
    {
        private ApplicationDbContext _context;
        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        const string Admin = "Администратор";


        [Authorize(Roles = Admin)]
        public async Task<IActionResult> List(Position position)
        {
            var res = await _context.Positions.Include(t => t.Workers).OrderBy(t => t.Id).ToListAsync();


            if (position?.Id > 0)
            {
                res = res.Where(i => i.Id == position.Id).ToList();
            }
            if (position?.PositionName != null)
            {
                res = res.Where(fn => fn.PositionName.ToUpper().Contains(position.PositionName.ToUpper())).Select(fn => fn).ToList();
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
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            position.Workers = await _context.Workers.Where(t => t.PositionId == position.Id).ToListAsync();
            return View(position);
        }

        [Authorize(Roles = Admin)]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckPositionName(int? Id, string PositionName)
        {
            if (Id != null)
            {
                var res1 = await _context.Positions.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.Positions.Where(t => t.PositionName == PositionName).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.Positions.Where(t => t.PositionName == PositionName).Select(t => t).FirstOrDefaultAsync();
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
            Position position = await _context.Positions.Include(t => t.Workers).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (position != null)
            {
                return View(position);
            }
            return RedirectToAction("List");

        }

        [Authorize(Roles = Admin)]
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
            position.Workers = await _context.Workers.Where(t => t.PositionId == position.Id).ToListAsync();
            return View(position);
        }

        [Authorize(Roles = Admin)]

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Position position = await _context.Positions.Include(t => t.Workers).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (position == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (position?.Id == AuthorizedUser.GetInstance().GetWorker().PositionId || position.Workers?.Count != 0)
            {
                return RedirectToRoute("default", new { controller = "Position", action = "Edit", id = id });
            }            

            return View(position);
        }


        [Authorize(Roles = Admin)]

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Position position = await _context.Positions.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (position == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (position?.Id == AuthorizedUser.GetInstance().GetWorker().PositionId || position.Workers?.Count != 0)
            {
                return RedirectToRoute("default", new { controller = "Position", action = "Edit", id = id });
            }
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }


    }
}
