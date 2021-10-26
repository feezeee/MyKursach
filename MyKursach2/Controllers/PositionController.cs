using Microsoft.AspNetCore.Mvc;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class PositionController : Controller
    {
        private IPositionRepository repository;
        public PositionController (IPositionRepository repos)
        {
            repository = repos;
        }

        public ViewResult List()
        {
            return View(repository.Positions);
        }
    }
}
