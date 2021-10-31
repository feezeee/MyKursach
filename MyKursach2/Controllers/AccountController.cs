using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyKursach2.Data;
using MyKursach2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyKursach2.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(Register model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Worker worker = await _context.Worker.FirstOrDefaultAsync(u => u.FirstName == model.FirstName && u.LastName == model.LastName && u.PhoneNumber == model.PhoneNumber);
        //        if (worker == null)
        //        {
        //            // добавляем пользователя в бд
        //            worker = new Worker { FirstName = worker.FirstName, LastName = worker.LastName, DateOfBirth = model.DateOfBirth, Email = model.Email, Password = model.Password, PhoneNumber=model.PhoneNumber };
        //            Position workerPosition = await _context.Position.FirstOrDefaultAsync(r => r.PositionName == "Директор");
        //            if (workerPosition != null)
        //                worker.Position = workerPosition;

        //            _context.Worker.Add(worker);
        //            await _context.SaveChangesAsync();

        //            await Authenticate(worker); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                Worker user = await _context.Worker.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber && u.Password == model.Password);
                if (user != null)
                {
                    user.Position = _context.Position.Where(p => p.Id == user.PositionId).FirstOrDefault();
                    user.Gender = _context.Gender.Where(p => p.Id == user.GenderId).FirstOrDefault();
                    await Authenticate(user); // аутентификация
                    AuthorizedUser.GetInstance().SetUser(user);
                    return RedirectToAction("Index", "Home");
                    
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await HttpContext.SignOutAsync();
            AuthorizedUser.GetInstance().ClearUser();
            return RedirectToAction("Index", "Home");
        }
        private async Task Authenticate(Worker worker)
        {
            // создаем один claim
            var claims = new List<Claim>
            {                
                new Claim(ClaimsIdentity.DefaultNameClaimType, worker.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, worker?.Position?.PositionName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
