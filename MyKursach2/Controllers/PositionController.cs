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
    public class PositionController : Controller
    {
        private ApplicationDbContext _context;
        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Директор, Администратор")]
        public ViewResult List(Position position)
        {
            //var res = _context.Workers.Join(_context.Positions);

            var res = from pos in _context.Position                      
                      select new Position
                      {
                          Id = pos.Id,
                          PositionName = pos.PositionName
                      };

            if (position?.Id > 0)
            {
                res = res.Where(i => i.Id == position.Id).Select(i => i);
            }
            if (position?.PositionName != null)
            {
                res = res.Where(fn => fn.PositionName.ToUpper().Contains(position.PositionName.ToUpper())).Select(fn => fn);
            }            
            return View(res);
        }
    }
}
