using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;
using NeksaraArief.ViewModels.Admin;
using NeksaraArief.Controllers.Base;
using Microsoft.AspNetCore.Hosting;

namespace NeksaraArief.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : BaseAuthenticatedController
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(ICategoryService categoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;
        }

        public IActionResult Index(string? search, string? sort)
        {
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;

            var categories = _categoryService.GetAll(search, sort);
            
            return View(categories);
}


        // public IActionResult Detail(int id, string search, string sort)
        // {
        //     var categories = _categoryService.GetAdminDetail(id);

        //     if (categories == null)
        //     {
        //         return NotFound();
        //     }

        //     var topics = _categoryService.GetAdminTopics(id, search, sort);

        //     var model = new CategoryDetailAdminViewModel
        //     {
        //         Category = categories,
        //         Topics = topics
        //     };

        //     return View(model);
        // }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category, IFormFile coverImage)
        {
            _categoryService.Create(category, coverImage);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View(_categoryService.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Category category, IFormFile coverImage)
        {
            _categoryService.Update(category, coverImage);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _categoryService.SoftDelete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            return View(_categoryService.GetAdminDetail(id));
        }
    }
}