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
    public class WorkerController : Controller
    {
        private ApplicationDbContext _context;
        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        const string Admin = "Администратор";

        [Authorize(Roles = Admin)]
        public async Task<IActionResult> List(Worker worker)
        {
            var res = await _context.Workers.Include(t => t.Position).Include(t => t.GroupUser).OrderBy(t=>t.Id).ToListAsync();            

            if (worker?.Id > 0)
            {
                res = res.Where(i => i.Id == worker.Id).ToList();
            }
            if (worker?.FirstName != null)
            {
                res = res.Where(fn => fn.FirstName.ToUpper().Contains(worker.FirstName.ToUpper())).Select(fn => fn).ToList();
            }
            if (worker?.LastName != null)
            {
                res = res.Where(ln => ln.LastName.ToUpper().Contains(worker.LastName.ToUpper())).Select(ln => ln).ToList();
            }
            if (worker?.MiddleName != null)
            {
                res = res.Where(ln => ln.MiddleName.ToUpper().Contains(worker.MiddleName.ToUpper())).Select(ln => ln).ToList();
            }
            if (worker?.Position?.PositionName != null)
            {
                res = res.Where(ln => ln.Position.PositionName.ToUpper().Contains(worker.Position.PositionName.ToUpper())).Select(ln => ln).ToList();
            }
            if (worker?.GroupUser?.Name != null)
            {
                res = res.Where(ln => ln.GroupUser.Name.ToUpper().Contains(worker.GroupUser.Name.ToUpper())).Select(ln => ln).ToList();
            }

            return View(res);
        }

        [Authorize(Roles = Admin)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.GroupUsers = new SelectList(await _context.GroupUsers.ToListAsync(), "Id", "Name"); 
            ViewBag.Positions = new SelectList(await _context.Positions.ToListAsync(), "Id", "PositionName");
            return View();
        }

        [Authorize(Roles = Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.GroupUsers = new SelectList(await _context.GroupUsers.ToListAsync(), "Id", "Name");
            ViewBag.Positions = new SelectList(await _context.Positions.ToListAsync(), "Id", "PositionName");
            return View();
        }
        [Authorize(Roles = Admin)]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckPhoneNumber(int? Id, string PhoneNumber)
        {
            if (Id != null)
            {
                var res1 = await _context.Workers.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await _context.Workers.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await _context.Workers.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefaultAsync();
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
            Worker worker = await _context.Workers.Include(t=>t.GroupUser).Include(t=>t.Position).Include(t=>t.Operations).Where(t=>t.Id == id.Value).FirstOrDefaultAsync();
            if (worker != null)
            {
                ViewBag.GroupUsers = new SelectList(await _context.GroupUsers.ToListAsync(), "Id", "Name");
                ViewBag.Positions = new SelectList(await _context.Positions.ToListAsync(), "Id", "PositionName");
                return View(worker);
            }
            return RedirectToAction("List");
        
        }

        [Authorize(Roles = Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(Worker worker)
        {
            if (ModelState.IsValid)
            {
                if (worker.Id == AuthorizedUser.GetInstance().GetWorker().Id)
                {
                    worker.Position = await _context.Positions.Where(t=>t.Id == worker.PositionId).FirstOrDefaultAsync();
                    worker.GroupUser = await _context.GroupUsers.Where(t => t.Id == worker.GroupUserId).FirstOrDefaultAsync();
                    AuthorizedUser.GetInstance().ClearUser();
                    AuthorizedUser.GetInstance().SetUser(worker);
                }
                _context.Entry(worker).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            ViewBag.GroupUsers = new SelectList(await _context.GroupUsers.ToListAsync(), "Id", "Name");
            ViewBag.Positions = new SelectList(await _context.Positions.ToListAsync(), "Id", "PositionName");
            return View(worker);
        }

        [Authorize(Roles = Admin)]

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Worker worker = await _context.Workers.Include(t=>t.Position).Include(t=>t.GroupUser).Include(t=>t.Operations).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (worker == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (worker?.Id == AuthorizedUser.GetInstance().GetWorker().Id)
            {
                return RedirectToRoute("default", new { controller = "Worker", action = "Edit", id = id });
            }
            
            return View(worker);            
        }

        [Authorize(Roles = Admin)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Worker worker = await _context.Workers.Include(t => t.Position).Include(t => t.GroupUser).Include(t => t.Operations).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (worker == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (worker?.Id != AuthorizedUser.GetInstance().GetWorker().Id && worker?.Operations?.Count == 0)
            {
                _context.Workers.Remove(worker);
                _context.SaveChanges();                
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = id });
        }

    }
}
