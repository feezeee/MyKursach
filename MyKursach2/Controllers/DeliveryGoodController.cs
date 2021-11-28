using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyKursach2.Data;
using MyKursach2.Models;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class DeliveryGoodController : Controller
    {
        private ApplicationDbContext _context;
        public DeliveryGoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpGet]
        public IActionResult Create(int operationId)
        {
            DeliveryGood deliveryGood = new DeliveryGood();
            deliveryGood.OperationId = operationId;
            ViewBag.DeliveryCountries = new SelectList(_context.DeliveryCountries, "Id", "DeliveryCountryName");

            return View(deliveryGood);
        }

        [Authorize(Roles = "Директор, Администратор")]
        [HttpPost]
        public async Task<IActionResult> Create(DeliveryGood deliveryGood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryGood);

                await _context.SaveChangesAsync();
                return RedirectToRoute("default", new { controller = "Operation", action = "Create", operationId = deliveryGood.OperationId });
            }
            ViewBag.DeliveryCountries = new SelectList(_context.DeliveryCountries, "Id", "DeliveryCountryName");

            return View(deliveryGood);
        }
    }
}
