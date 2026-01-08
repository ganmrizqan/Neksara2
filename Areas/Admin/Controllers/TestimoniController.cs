using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;
using NeksaraArief.Controllers.Base;

namespace NeksaraArief.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimoniController : BaseAuthenticatedController
    {
        private readonly ITestimoniService _testimoniService;

        public TestimoniController(ITestimoniService testimoniService)
        {
            _testimoniService = testimoniService;
        }

        public IActionResult Index(string search, string sort, int? rating)
        {
            return View(_testimoniService.GetPending(search, sort, rating));
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            _testimoniService.Approve(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            _testimoniService.Reject(id);
            return RedirectToAction("Index");
        }
    }
}