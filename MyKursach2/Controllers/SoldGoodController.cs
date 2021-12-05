using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKursach2.Data;
using MyKursach2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class SoldGoodController : Controller
    {
        private ApplicationDbContext _context;
        public SoldGoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Директор, Администратор, Кассир")]
        [HttpGet]
        public IActionResult Create(int operationId)
        {
            SoldGood soldGood = new SoldGood();

            soldGood.OperationId = operationId;

            ViewBag.GoodsForSale = new SelectList(_context.GoodsForSale, "Id", "Name");

            
            return View(soldGood);
        }

        [Authorize(Roles = "Директор, Администратор, Кассир")]
        [HttpPost]
        public async Task<IActionResult> Create(SoldGood soldGood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldGood);

                await _context.SaveChangesAsync();
                return RedirectToRoute("default", new { controller = "Operation", action = "Create", operationId = soldGood.OperationId });
            }
            ViewBag.GoodsForSale = new SelectList(_context.GoodsForSale, "Id", "Name");

            return View(soldGood);
        }

        public async Task<IActionResult> CheckCount(int GoodForSaleId, int NumberSold)
        {
            int maxCount = (await _context.GoodsForSale.FindAsync(GoodForSaleId)).GoodAmount;
            if(NumberSold > maxCount)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }           
        }

        [Authorize(Roles = "Директор, Администратор, Кассир")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List", "Operation");
            }
            SoldGood soldGood = await _context.SoldGoods.Include(t=>t.GoodForSale).Include(t=>t.Operation).Where(t=>t.Id == id.Value).FirstOrDefaultAsync();
            if (soldGood != null)
            {
                _context.SoldGoods.Remove(soldGood);
                await _context.SaveChangesAsync();
                soldGood.Id = 0;
                ViewBag.GoodsForSale = new SelectList(_context.GoodsForSale, "Id", "Name");
                return View("Create",soldGood);
            }
            return RedirectToAction("List", "Operation");
        }

       
    }
}
