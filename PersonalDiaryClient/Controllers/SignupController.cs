using Microsoft.AspNetCore.Mvc;

namespace PersonalDiaryClient.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
