using Microsoft.AspNetCore.Mvc;
using PersonalDiaryClient.Models;
using System.Diagnostics;

namespace PersonalDiaryClient.Controllers
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
            return View();
        }

        public IActionResult Logout()
        {
            return Redirect("/Login/Index");
        }


    }
}
