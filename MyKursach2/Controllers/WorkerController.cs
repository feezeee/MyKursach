using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class WorkerController : Controller
    {
        private IWorkerRepository repository;
        public WorkerController(IWorkerRepository repos)
        {
            repository = repos;
        }

        [Authorize(Roles = "Директор, Администратор")]
        public ViewResult List(Worker worker)
        {
            var res = repository.Workers;
            if(worker?.Id > 0)
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
            ViewData["Positions"] = 
            return View(res);
        }

        [HttpPost]
        public void Create(Worker worker)
        {
            repository.Add(worker);
            repository.SaveChanges();
        }
    }
}
