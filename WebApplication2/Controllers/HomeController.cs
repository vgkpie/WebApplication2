using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var lastVisit = Request.Cookies["LastVisit"];

            // Ако бисквитката не съществува, задаваме съобщение за първо посещение
            if (string.IsNullOrEmpty(lastVisit))
            {
                ViewBag.LastVisit = "Това е твоето първо посещение!";
            }
            else
            {
                // Показваме последното посещение
                ViewBag.LastVisit = "Последно посещение: " + lastVisit;
            }

            // Записваме текущата дата и час в бисквитка
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Response.Cookies.Append("LastVisit", currentTime, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), // Бисквитката ще изтече след 30 дни
                HttpOnly = true, // За сигурност
            });

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
