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

            // ��� ����������� �� ����������, �������� ��������� �� ����� ���������
            if (string.IsNullOrEmpty(lastVisit))
            {
                ViewBag.LastVisit = "���� � ������ ����� ���������!";
            }
            else
            {
                // ��������� ���������� ���������
                ViewBag.LastVisit = "�������� ���������: " + lastVisit;
            }

            // ��������� �������� ���� � ��� � ���������
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Response.Cookies.Append("LastVisit", currentTime, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), // ����������� �� ������ ���� 30 ���
                HttpOnly = true, // �� ���������
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
