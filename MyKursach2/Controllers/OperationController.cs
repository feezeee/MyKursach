using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public ViewResult List()
        {
            return View();
        }
    }
}
