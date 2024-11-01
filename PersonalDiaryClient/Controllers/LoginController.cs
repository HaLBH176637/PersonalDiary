using Microsoft.AspNetCore.Mvc;
using PersonalDiaryClient.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalDiaryClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly PersonalDiaryContext context = new PersonalDiaryContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var m = context.Users.FirstOrDefault(s => s.Email.Contains(user.Email) && s.Password.Contains(user.Password));
            if (m != null)
            {
                return Redirect("/Home/Index");
            }
            TempData["mess"] = "Vui lòng kiểm tra mật khẩu rồi đăng nhập lại đi!!!";
            return Redirect("/Login/Index");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }
    }
}
