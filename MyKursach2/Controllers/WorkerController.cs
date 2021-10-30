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
        public ViewResult List()
        {

            var res = repository.Workers;
            return View(res);
        }
    }
}
