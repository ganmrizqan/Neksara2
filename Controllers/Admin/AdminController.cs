using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    [Route("admin/admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public bool IsSuperAdmin()
        {
            return HttpContext.Session.GetString("Role") == "SuperAdmin";
        }

        public IActionResult Index(string search, string role)
        {
            if (!IsSuperAdmin())
            {
                return Unauthorized();
            }

            return View(_adminService.Get(search, role));
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            if (!IsSuperAdmin())
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Admin admin)
        {
            if (!IsSuperAdmin())
            {
                return Unauthorized();
            }

            _adminService.Create(admin);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (!IsSuperAdmin())
            {
                return Unauthorized();
            }

            return View(_adminService.GetById(id));
        }

        [HttpPost("edit")]
        public IActionResult Edit(Admin admin)
        {
            if (!IsSuperAdmin())
            {
                return Unauthorized();
            }

            _adminService.Update(admin);
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsSuperAdmin())
            {
                return Unauthorized();
            }

            _adminService.SoftDelete(id);
            return RedirectToAction("Index");
        }
    }
}