using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NeksaraArief.Models;
using NeksaraArief.Data;

namespace NeksaraArief.Controllers.Admin
{
    public class DashboardController : BaseAuthenticatedController
    {
        private readonly NeksaraDbContext _context;

        public DashboardController(NeksaraDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var email = context.HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _context.Categories.Count(x => !x.ISDeleted);
            ViewBag.Topics = _context.Topics.Count(x => !x.ISDeleted);
            ViewBag.Testimoni = _context.Testimoni.Count(x => !x.ISDeleted);
            return View();
        }
    }
}
