using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.Admin
{
    [Route("admin/topic-feedback")]
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

        [HttpPost("approve/{id}")]
        public IActionResult Approve(int id)
        {
            _topicFeedbackService.Approve(id);
            return RedirectToAction("Index");
        }

        [HttpPost("reject/{id}")]
        public IActionResult Reject(int id)
        {
            _topicFeedbackService.Reject(id);
            return RedirectToAction("Index");
        }
    }
}