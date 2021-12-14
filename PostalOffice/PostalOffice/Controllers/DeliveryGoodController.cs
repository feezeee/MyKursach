using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostalOffice.Data;
using PostalOffice.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PostalOffice.Controllers
{
    public class DeliveryGoodController : Controller
    {
        private ApplicationDbContext _context;
        public DeliveryGoodController(ApplicationDbContext context)
        {
            _context = context;
        }



        const string AdminKassir = "Администратор, Кассир";
        const string Admin = "Администратор";


        [Authorize(Roles = AdminKassir)]
        [HttpGet]
        public IActionResult Create(int operationId)
        {
            DeliveryGood deliveryGood = new DeliveryGood();
            deliveryGood.OperationId = operationId;
            ViewBag.DeliveryCountries = new SelectList(_context.DeliveryCountries, "Id", "DeliveryCountryName");

            return View(deliveryGood);
        }

        [Authorize(Roles = AdminKassir)]
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

        [Authorize(Roles = AdminKassir)]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List", "Operation");
            }
            DeliveryGood deliveryGood = await _context.DeliveryGoods.Include(t => t.DeliveryCountry).Include(t => t.Operation).Where(t => t.Id == id.Value).FirstOrDefaultAsync();
            if (deliveryGood != null)
            {
                _context.DeliveryGoods.Remove(deliveryGood);
                await _context.SaveChangesAsync();
                deliveryGood.Id = 0;
                ViewBag.DeliveryCountries = new SelectList(_context.DeliveryCountries, "Id", "DeliveryCountryName");
                return View("Create", deliveryGood);
            }
            return RedirectToAction("List","Operation");
        }
    }
}
