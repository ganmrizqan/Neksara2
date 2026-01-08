using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;
using NeksaraArief.Service.Implementations;

namespace NeksaraArief.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryService _categoryService;
    private readonly ITopicService _topicService;
    private readonly ITestimoniService _testimoniService;

    public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, ITopicService topicService, ITestimoniService testimoniService)
    {
        _logger = logger;
        _categoryService = categoryService;
        _topicService = topicService;
        _testimoniService = testimoniService;
    }

    public IActionResult Index()
    {
        ViewBag.Categories = _categoryService.GetAll(null, null);
        ViewBag.Topics = _topicService.GetAll(null, null);
        ViewBag.Testimonials = _testimoniService.GetApproved();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
