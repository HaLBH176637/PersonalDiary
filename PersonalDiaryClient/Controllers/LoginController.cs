using Microsoft.AspNetCore.Mvc;
using PersonalDiaryClient.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace PersonalDiaryClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        public LoginController()
        {
            _client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var loginDTO = new
            {
                Username = user.Username,
                Password = user.Password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginDTO), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("https://localhost:7108/api/Login/Authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic responseData = JsonConvert.DeserializeObject(jsonResponse);

                string token = responseData.token;
                int userId = responseData.userId;

                HttpContext.Session.SetInt32("UserId", userId);
                HttpContext.Session.SetString("AuthToken", token);

                return RedirectToAction("Index", "Home");
            }

            TempData["mess"] = "Vui lòng kiểm tra mật khẩu rồi đăng nhập lại đi!!!";
            return RedirectToAction("Index", "Login");
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
