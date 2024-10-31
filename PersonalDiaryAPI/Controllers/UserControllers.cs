using Microsoft.AspNetCore.Mvc;

namespace PersonalDiaryAPI.Controllers
{
    public class UserControllers : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
