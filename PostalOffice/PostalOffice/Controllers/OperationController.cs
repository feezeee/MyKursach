using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostalOffice.Data;
using PostalOffice.Models;

namespace PostalOffice.Controllers
{
    public class OperationController : Controller
    {
        private ApplicationDbContext _context;
        public OperationController(ApplicationDbContext context)
        {
            _context = context;
        }


        const string AdminKassir = "Администратор, Кассир";
        const string Kassir = "Кассир";

        [Authorize(Roles = AdminKassir)]
        public async Task<IActionResult> List(Operation operation)
        {
            var res = await _context.Operations.Include(c => c.Worker).Include(t => t.SoldGoods).Include(t => t.DeliveryGoods).Include(t => t.CompletedPayments).Select(t => t).ToListAsync();

            if (operation?.Id > 0)
            {
                res = res.Where(i => i.Id == operation.Id).Select(i => i).ToList();
            }
            if (operation?.Worker?.LastName != null)
            {
                res = res.Where(fn => fn.Worker.LastName.ToUpper().Contains(operation.Worker.LastName.ToUpper())).Select(fn => fn).ToList();
            }
            if (operation?.DateTime != null)
            {
                res = res.Where(t => t.DateTime.Value.Date == operation.DateTime.Value.Date).Select(fn => fn).ToList();
            }
            return View(res);
        }


        [Authorize(Roles = AdminKassir)]
        [HttpGet]
        public async Task<IActionResult> Create(int? operationId)
        {
            Operation operation = new Operation();
            if (operationId == null)
            {
                operation.WorkerId = AuthorizedUser.GetInstance().GetWorker().Id;
                operation.DateTime = DateTime.Now;
                var lastOperation = await _context.Operations.Include(t => t.CompletedPayments).Include(t => t.SoldGoods).Include(t => t.DeliveryGoods).Where(t => t.WorkerId == AuthorizedUser.GetInstance().GetWorker().Id).OrderBy(t=>t.Id).LastOrDefaultAsync();
                if (lastOperation?.CompletedPayments?.Count == 0 && lastOperation?.SoldGoods?.Count == 0 && lastOperation?.DeliveryGoods?.Count == 0)
                {
                    _context.Operations.Remove(lastOperation);
                    await _context.SaveChangesAsync();
                }
                _context.Add(operation);
                await _context.SaveChangesAsync();
                operation = await _context.Operations
                .Include(t => t.CompletedPayments)
                    .ThenInclude(t => t.AvailablePayment)
                .Include(t => t.SoldGoods)
                    .ThenInclude(t => t.GoodForSale)
                .Include(t => t.DeliveryGoods)
                    .ThenInclude(t => t.DeliveryCountry)
                .Include(t => t.PaymentMethods)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.Operation)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.PaymentMethod)
                .Where(t => t.Worker.Id == AuthorizedUser.GetInstance().GetWorker().Id).OrderBy(t => t.Id).LastOrDefaultAsync();
            }
            else
            {
                operation = _context.Operations
                 .Include(t => t.CompletedPayments)
                    .ThenInclude(t => t.AvailablePayment)
                .Include(t => t.SoldGoods)
                    .ThenInclude(t => t.GoodForSale)
                .Include(t => t.DeliveryGoods)
                    .ThenInclude(t => t.DeliveryCountry)
                .Include(t => t.PaymentMethods)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.Operation)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.PaymentMethod)
                .Where(t => t.Id == operationId).Select(t => t).FirstOrDefault();
            }           
            return View(operation);
        }


        [Authorize(Roles = AdminKassir)]
        [HttpPost]
        public async Task<IActionResult> Create(Operation operation)
        {
            operation = _context.Operations
                 .Include(t => t.CompletedPayments)
                    .ThenInclude(t => t.AvailablePayment)
                .Include(t => t.SoldGoods)
                    .ThenInclude(t => t.GoodForSale)
                .Include(t => t.DeliveryGoods)
                    .ThenInclude(t => t.DeliveryCountry)
                .Include(t => t.PaymentMethods)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.Operation)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.PaymentMethod)
                .Where(t => t.Id == operation.Id).Select(t => t).FirstOrDefault();
            if (operation?.SoldGoods?.Count == 0 && operation?.CompletedPayments?.Count == 0 && operation?.DeliveryGoods?.Count == 0 && operation?.PaymentMethods?.Count == 0)
            {
                if ((await _context.Operations.Where(t => t.Id == operation.Id).OrderBy(t => t.Id).LastOrDefaultAsync()) != null)
                {
                    _context.Operations.Remove(operation);
                    await _context.SaveChangesAsync();
                } 
            }
            if(operation?.PaymentMethods.Count == 0 && operation?.TotalPrice != 0)
            {
                return View(operation);
            }
            int sum = 0;
            foreach(var count in operation?.Operations_PaymentMethods)
            {
                sum += count.Sum;
            }
            if(sum != operation?.TotalPrice)
            {
                return View(operation);
            }
            return RedirectToAction("List","Operation");
        }


        //[Authorize(Roles = "Директор, Администратор")]
        //[HttpPost]
        //public async Task<IActionResult> Create(Worker worker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(worker);

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("List");
        //    }
        //    ViewBag.Positions = new SelectList(_context.Positions, "Id", "PositionName");
        //    return View();
        //}
        //[Authorize(Roles = "Директор, Администратор")]
        //[AcceptVerbs("Get", "Post")]
        //public IActionResult CheckPhoneNumber(int? Id, string PhoneNumber)
        //{
        //    if (Id != null)
        //    {
        //        var res1 = _context.Workers.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();
        //        var res2 = _context.Workers.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
        //        if (res2 == null || res1.Id == res2?.Id)
        //        {
        //            return Json(true);
        //        }
        //        return Json(false);
        //    }
        //    else
        //    {
        //        var res3 = _context.Workers.Where(t => t.PhoneNumber == PhoneNumber).Select(t => t).FirstOrDefault();
        //        if (res3 != null)
        //            return Json(false);
        //        return Json(true);
        //    }
        //}

        [Authorize(Roles = Kassir)]
        [HttpGet]
        public IActionResult Check(int operationId)
        {
            var operation = _context.Operations
                 .Include(t => t.CompletedPayments)
                    .ThenInclude(t => t.AvailablePayment)
                .Include(t => t.SoldGoods)
                    .ThenInclude(t => t.GoodForSale)
                .Include(t => t.DeliveryGoods)
                    .ThenInclude(t => t.DeliveryCountry)
                .Include(t => t.PaymentMethods)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.Operation)
                .Include(t => t.Operations_PaymentMethods)
                .ThenInclude(t => t.PaymentMethod)
                .Where(t => t.Id == operationId).Select(t => t).FirstOrDefault();
            if (operation == null)
            {
                return RedirectToAction("List");
            }
            return View(operation);         

        }

        //[Authorize(Roles = "Директор, Администратор")]
        //[HttpPost]
        //public async Task<IActionResult> Edit(Worker worker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (worker.Id == AuthorizedUser.GetInstance().GetWorker().Id)
        //        {
        //            worker.Position = _context.Positions.Find(worker.PositionId);
        //            worker.Gender = _context.Genders.Find(worker.GenderId);
        //            AuthorizedUser.GetInstance().ClearUser();
        //            AuthorizedUser.GetInstance().SetUser(worker);
        //        }
        //        _context.Entry(worker).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("List");
        //    }

        //    var gend = _context.Genders;
        //    var pos = _context.Positions;
        //    ViewBag.Genders = new SelectList(gend, "Id", "GenderName");
        //    ViewBag.Positions = new SelectList(pos, "Id", "PositionName");
        //    return View(worker);
        //}

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    Worker worker = _context.Workers.Find(id);
        //    if (worker == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    else if (worker?.Id == AuthorizedUser.GetInstance().GetWorker().Id)
        //    {
        //        return RedirectToRoute("default", new { controller = "Worker", action = "Edit", id = id });
        //    }

        //    return View(worker);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int? id)
        //{
        //    Worker worker = _context.Workers.Find(id);
        //    if (worker == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    _context.Workers.Remove(worker);
        //    _context.SaveChanges();
        //    return RedirectToAction("List");
        //}
    }
}
