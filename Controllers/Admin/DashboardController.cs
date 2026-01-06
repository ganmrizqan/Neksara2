using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Models;
using NeksaraArief.Data

namespace NeksaraArief.Controllers.Admin
{
    public class DashboardController : Controller
    {
        private readonly NeksaraDbContext _context;

        public DashboardController(NeksaraDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _context.Categories.Count(x => !x.ISDeleted);
            ViewBag.Topics = _context.Topics.Count(x => !x.ISDeleted);
            ViewBag.Feedbacks = _context.Feedbacks.Count(x => !x.ISDeleted);
            return View();
        }
    }
}
