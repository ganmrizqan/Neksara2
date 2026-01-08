using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;
using NeksaraArief.ViewModels.Admin;
using NeksaraArief.Controllers.Base;
using Microsoft.AspNetCore.Hosting;

namespace NeksaraArief.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicController : BaseAuthenticatedController
    {
        private readonly ITopicService _topicService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;

        public TopicController(ITopicService topicService, ICategoryService categoryService, IWebHostEnvironment env)
        {
            _topicService = topicService;
            _categoryService = categoryService;
            _env = env;
        }

        public IActionResult Index(string? search, string? sort)
        {
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            ViewBag.Categories = _categoryService.GetAll(null, null);

            var topics = _topicService.GetAll(search, sort);

            return View(topics);
        }

        [HttpPost]
        public IActionResult Create(Topic topic, IFormFile pictTopic)
        {
            _topicService.Create(topic, pictTopic);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _categoryService.GetAll(null, null);

            return View(_topicService.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Topic topic, IFormFile pictTopic)
        {
            _topicService.Update(topic, pictTopic);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _topicService.SoftDelete(id);
            return RedirectToAction("Index");
        }

    }
}