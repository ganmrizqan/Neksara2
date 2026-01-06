using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    [Route("admin/topic")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly NeksaraDbContext _context;

        public TopicController(ITopicService topicService, NeksaraDbContext context)
        {
            _topicService = topicService;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index(string search, string sort, int rating)
        {
            var topics = _topicService.GetAll(search, sort, rating);

            foreach (var topic in topics)
            {
                topic.Rating = (int)Math.Round(_topicService.GetAverageRating(topic.TopicId));
            }

            return View(topics);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories
                .Where(c => !c.ISDeleted)
                .ToList();

            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Topic topic)
        {
            _topicService.Create(topic);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories
                .Where(c => !c.ISDeleted)
                .ToList();

            return View(_topicService.GetById(id));
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Topic topic)
        {
            _topicService.Update(topic);
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _topicService.SoftDelete(id);
            return RedirectToAction("Index");
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            _topicService.IncrementView(id);
            
            var topic = _topicService.GetDetail(id);
            ViewBag.AverageRating = _topicService.GetAverageRating(id);

            return View(topic);
        }
    }
}