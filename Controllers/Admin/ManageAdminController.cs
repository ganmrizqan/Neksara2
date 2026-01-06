using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    public class ManageAdminController : BaseAdminController
    {
        private readonly IAuthService _authService;

        public ManageAdminController(IAuthService authService)
        {
            _authService = authService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("AdminRole");
            if (role != "SuperAdmin")
            {
                context.Result = new RedirectToActionResult("Index", "Categories", null);
            }

            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            return View(_authService.GetAdmins());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AdminUser user)
        {
            _authService.CreateAdmin(user);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_authService.GetAdminById(id));
        }

        [HttpPost]
        public IActionResult Edit(AdminUser user)
        {
            _authService.UpdateAdmin(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _authService.SoftDeleteAdmin(id);
            return RedirectToAction("Index");
        }
    }
}