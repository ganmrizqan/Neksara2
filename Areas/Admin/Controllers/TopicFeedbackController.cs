using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;
using NeksaraArief.Controllers.Base;

namespace NeksaraArief.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicFeedbackController : BaseAuthenticatedController
    {
        private readonly ITopicFeedbackService _topicFeedbackService;

        public TopicFeedbackController(ITopicFeedbackService topicFeedbackService)
        {
            _topicFeedbackService = topicFeedbackService;
        }

        public IActionResult Index(string search)
        {
            return View(_topicFeedbackService.GetPending());
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            _topicFeedbackService.Approve(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            _topicFeedbackService.Reject(id);
            return RedirectToAction("Index");
        }
    }
}