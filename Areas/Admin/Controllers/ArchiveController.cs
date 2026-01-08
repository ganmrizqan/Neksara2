using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Models;
using NeksaraArief.Controllers.Base;

namespace NeksaraArief.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : BaseAuthenticatedController
    {
        private readonly IArchiveService _archiveService;

        public ArchiveController(IArchiveService archiveService)
        {
            _archiveService = archiveService;
        }

        public IActionResult Index(string search)
        {
            ViewBag.Category = _archiveService.GetDeletedCategories(search);
            ViewBag.Topic = _archiveService.GetDeletedTopics(search);
            ViewBag.Testimoni = _archiveService.GetDeletedTestimoni(search);
            ViewBag.TopicFeedback = _archiveService.GetDeletedTopicFeedback(search);

            return View();
        }

        [HttpPost]
        public IActionResult Restore(string entity, int id)
        {
            _archiveService.Restore(entity, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePermanent(string entity, int id)
        {
            _archiveService.DeletePermanent(entity, id);
            return RedirectToAction("Index");
        }
    }
}