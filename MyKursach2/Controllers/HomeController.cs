using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyKursach.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Positions()
        {
            return View();
        }

        public ViewResult Workers()
        {
            return View();
        }

        public ViewResult Operations()
        {
            return View();
        }
    }
}
