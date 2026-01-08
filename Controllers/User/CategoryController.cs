using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.User
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetPublic());
        }

        public IActionResult Detail(int id)
        {
            return View(_service.GetDetailPublic(id));
        }
    }
}