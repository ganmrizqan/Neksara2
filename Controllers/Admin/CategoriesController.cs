using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    [Route("admin/categories")]
    public class CategoriesController : BaseAuthenticatedController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(string search, string sort)
        {
            return View(_categoryService.GetAll(search, sort));
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id, string search, string sort, int? rating)
        {
            _categoryService.IncrementView(id);
            return View(_categoryService.GetDetail(id, search, sort, rating));
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Category category, IFormFile CoverImage)
        {
            if (CoverImage == null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(CoverImage.FileName);
                var filePath = Path.Combine("wwwroot/uploads/categories", fileName);

                Directory.CreateDirectory("wwwroot/uploads/categories");
                using var stream = new FileStream(filePath, FileMode.Create);
                CoverImage.CopyTo(stream);

                category.CoverImage = "/uploads/categories/" + fileName;
            }

            _categoryService.Create(category);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View(_categoryService.GetById(id));
        }

        [HttpPost("edit")]
        public IActionResult Edit(Category category, IFormFile CoverImage)
        {
            if (CoverImage != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(CoverImage.FileName);
                var filePath = Path.Combine("wwwroot/uploads/categories", fileName);

                Directory.CreateDirectory("wwwroot/uploads/categories");
                using var stream = new FileStream(filePath, FileMode.Create);
                CoverImage.CopyTo(stream);

                category.CoverImage = "/uploads/categories/" + fileName;
            }

            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.SoftDelete(id);
            return RedirectToAction("Index");
        }
    }
}