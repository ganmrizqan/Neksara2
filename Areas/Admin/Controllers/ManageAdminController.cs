using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Service.Implementations;
using NeksaraArief.Models;
using NeksaraArief.Controllers.Base;

namespace NeksaraArief.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageAdminController : BaseAuthenticatedController
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
            return View(_authService.GetAdminUsers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AdminUser user)
        {
            _authService.CreateAdminUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_authService.GetAdminUserById(id));
        }

        [HttpPost]
        public IActionResult Edit(AdminUser user)
        {
            _authService.UpdateAdminUser(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _authService.DeleteAdminUser(id);
            return RedirectToAction("Index");
        }
    }
}