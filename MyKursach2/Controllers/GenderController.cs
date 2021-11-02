using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyKursach2.Data;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class GenderController : Controller
    {
        private ApplicationDbContext _context;
        public GenderController(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
