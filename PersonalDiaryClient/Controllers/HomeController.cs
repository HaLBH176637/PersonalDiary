using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("AuthToken");
            return RedirectToAction("Index", "Login");
        }
    }
}