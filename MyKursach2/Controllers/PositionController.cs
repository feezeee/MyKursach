using Microsoft.AspNetCore.Mvc;
using MyKursach2.Data;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class PositionController : Controller
    {
        private ApplicationDbContext _context;
        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List()
        {
            return View(_context.Position);
        }
    }
}
