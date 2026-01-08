using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;

namespace NeksaraArief.Controllers.User
{
    [Route("user/topic")]
    public class TopicController : Controller
    {
        private readonly ITopicFeedbackService _topicFeedbackService;
        private readonly ITopicService _topicService;

        public TopicController(ITopicFeedbackService topicFeedbackService, ITopicService topicService)
        {
            _topicFeedbackService = topicFeedbackService;
            _topicService = topicService;
        }

        public IActionResult Detail(int id)
        {
            _topicService.IncrementView(id);

            ViewBag.AverageRating = _topicService.GetAverageRating(id);

            ViewBag.Feedbacks = _topicFeedbackService.GetApprovedByTopic(id);

            return View(_topicService.GetDetail(id));
        }

        [HttpPost]
        public IActionResult Submit(int id, TopicFeedback topicFeedback)
        {
            topicFeedback.TopicId = id;
            _topicFeedbackService.SubmitFeedback(topicFeedback);

            TempData["FeedbackMessage"] = "Feedback sedang ditinjau";
            return RedirectToAction("Detail", new { id });
        }
    }
}