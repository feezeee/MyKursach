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


        [Authorize(Roles = "Директор, Администратор")]
        public ViewResult List(Gender gender)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from gend in _context.Gender
                      select new Gender
                      {
                          Id = gend.Id,
                          GenderName = gend.GenderName
                      };

            if (gender?.Id > 0)
            {
                res = res.Where(i => i.Id == gender.Id).Select(i => i);
            }
            if (gender?.GenderName != null)
            {
                res = res.Where(fn => fn.GenderName.ToUpper().Contains(gender.GenderName.ToUpper())).Select(fn => fn);
            }
            return View(res);
        }
    }
}
