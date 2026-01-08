using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.User
{
    public class TestimoniController : Controller
    {
        private readonly ITestimoniService _testimoniService;

        public TestimoniController(ITestimoniService testimoniService)
        {
            _testimoniService = testimoniService;
        }

        [HttpPost]
        public IActionResult Submit(Testimoni testimoni)
        {
            _testimoniService.Submit(testimoni);
            TempData["FeedbackMessage"] = "Tetimoni sesang ditinjau";
            return RedirectToAction("Index", "Home");
        }
    }
}