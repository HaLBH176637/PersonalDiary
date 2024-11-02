using Microsoft.AspNetCore.Mvc;

namespace PersonalDiaryClient.Controllers
{
    public class DiaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
