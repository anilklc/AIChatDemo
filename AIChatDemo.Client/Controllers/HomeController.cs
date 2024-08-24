using AIChatDemo.Client.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AIChatDemo.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notfyService;
        public HomeController(ILogger<HomeController> logger, INotyfService notfyService)
        {
            _logger = logger;
            _notfyService = notfyService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            var userId = Request.Cookies["UserId"];
            var fullName = Request.Cookies["FullName"];

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(fullName))
            {
                _notfyService.Error("Bu alana giriþ için lütfen giriþ yapýnýz");
                return RedirectToAction("Login", "User");
            }

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
}
