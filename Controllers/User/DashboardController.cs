using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.User
{
    public class DashboardController : Controller
    {
        private readonly ITestimoniService _testimoniService;

        public DashboardController(ITestimoniService testimoniService)
        {
            _testimoniService = testimoniService;
        }

        public IActionResult Index()
        {
            ViewBag.Testimonis = _testimoniService.GetApproved();
            
            return View();
        }
    }
}