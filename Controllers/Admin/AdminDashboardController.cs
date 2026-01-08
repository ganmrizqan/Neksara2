using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Service.Implementations;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    public class AdminDashboardController : BaseAuthenticatedController
    {
        private readonly ICategoryService _categoryService;
        private readonly ITopicService _topicService;
        private readonly ITopicFeedbackService _feedbackService;
        private readonly ITestimoniService _testimoniService;

        public AdminDashboardController(
            ICategoryService categoryService,
            ITopicService topicService,
            ITopicFeedbackService feedbackService,
            ITestimoniService testimoniService)
        {
            _categoryService = categoryService;
            _topicService = topicService;
            _feedbackService = feedbackService;
            _testimoniService = testimoniService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalCategory = _categoryService.GetAll(null, null).Count;
            ViewBag.TotalTopic = _topicService.GetAll(null, null, null).Count;
            ViewBag.PendingFeedback = _feedbackService.GetPending().Count;
            ViewBag.PendingTestimoni = _testimoniService.GetPending(null, null, null).Count;

            return View();
        }
    }
}
