using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKursach2.Data;
using MyKursach2.Models;

namespace MyKursach2.Controllers
{
    public class OperationController : Controller
    {
        private ApplicationDbContext _context;
        public OperationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List(Operation operation)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = _context.Operations.Include(c => c.Worker).Select(t => t);

            if (operation?.Id > 0)
            {
                res = res.Where(i => i.Id == operation.Id).Select(i => i);
            }
            if (operation?.Worker?.LastName != null)
            {
                res = res.Where(fn => fn.Worker.LastName.ToUpper().Contains(operation.Worker.LastName.ToUpper())).Select(fn => fn);
            }
            if (operation?.DateTime != null)
            {
                res = res.Where(t => t.DateTime.Value.Date == operation.DateTime.Value.Date).Select(fn => fn);
            }
            return View(res);
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public async Task<IActionResult> Create(int? operationId)
        {
            Operation operation = new Operation();
            if (operationId == null)
            {
                operation.WorkerId = AuthorizedUser.GetInstance().GetWorker().Id;
                operation.DateTime = DateTime.Now;
                _context.Add(operation);
                await _context.SaveChangesAsync();
                operation = _context.Operations
                    .Include(t => t.MakingPayments)
                        .ThenInclude(t => t.AvailablePayment)
                    .Include(t => t.MakingPayments)
                        .ThenInclude(t => t.PaymentMethod)
                    .Where(t => t.Worker.Id == AuthorizedUser.GetInstance().GetWorker().Id).OrderBy(t => t.Id).Last();
            }
            else
            {
                operation = _context.Operations
                    .Include(t => t.MakingPayments)
                        .ThenInclude(t => t.AvailablePayment)
                    .Include(t => t.MakingPayments)
                        .ThenInclude(t => t.PaymentMethod)
                    .Where(t => t.Id == operationId).Select(t => t).FirstOrDefault();
            }
            
            //operation.MakingPayments = _context.MakingPayments.Where(t => t.OperationId == operation.Id).ToList();
            return View(operation);
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
            ViewBag.Genders = new SelectList(_context.Genders, "Id", "GenderName");
            ViewBag.Positions = new SelectList(_context.Positions, "Id", "PositionName");
            return View();
        }
        [Authorize(Roles = "Директор, Администратор")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckPhoneNumber(int? Id, string PhoneNumber)
        {
            if (Id != null)
            {
                var res1 = _context.Workers.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
                var res2 = _context.Workers.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = _context.Workers.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
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
            Worker worker = _context.Workers.Find(id);
            if (worker != null)
            {
                var gend = _context.Genders;
                var pos = _context.Positions;
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
                if (worker.Id == AuthorizedUser.GetInstance().GetWorker().Id)
                {
                    worker.Position = _context.Positions.Find(worker.PositionId);
                    worker.Gender = _context.Genders.Find(worker.GenderId);
                    AuthorizedUser.GetInstance().ClearUser();
                    AuthorizedUser.GetInstance().SetUser(worker);
                }
                _context.Entry(worker).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            var gend = _context.Genders;
            var pos = _context.Positions;
            ViewBag.Genders = new SelectList(gend, "Id", "GenderName");
            ViewBag.Positions = new SelectList(pos, "Id", "PositionName");
            return View(worker);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Worker worker = _context.Workers.Find(id);
            if (worker == null)
            {
                //return HttpNotFound();
            }
            else if (worker?.Id == AuthorizedUser.GetInstance().GetWorker().Id)
            {
                return RedirectToRoute("default", new { controller = "Worker", action = "Edit", id = id });
            }

            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Worker worker = _context.Workers.Find(id);
            if (worker == null)
            {
                //return HttpNotFound();
            }
            _context.Workers.Remove(worker);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
