using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyKursach2.Models;

namespace MyKursach2.Controllers
{
    public class OperationController : Controller
    {
        private IOperationRepository repository;
        public OperationController(IOperationRepository repos)
        {
            repository = repos;
        }

        public ViewResult List()
        {
            return View(repository.Operations);
        }
    }
}
