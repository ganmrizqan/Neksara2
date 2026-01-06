using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    [Route("admin/testimoni")]
    public class TestimoniController : Controller
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

        [HttpPost("approve/{id}")]
        public IActionResult Approve(int id)
        {
            _testimoniService.Approve(id);
            return RedirectToAction("Index");
        }

        [HttpPost("reject/{id}")]
        public IActionResult Reject(int id)
        {
            _testimoniService.Reject(id);
            return RedirectToAction("Index");
        }
    }
}