using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Implementations;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            var admin = _authService.Login(email, password);
            if (admin == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            HttpContext.Session.SetString("Email", admin.Email);
            HttpContext.Session.SetString("Role", admin.Role);
            HttpContext.Session.SetString("Name", admin.Name);

            return RedirectToAction(nameof(Index), "AdminDashboard");

            // return Content("Login berhasil");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}