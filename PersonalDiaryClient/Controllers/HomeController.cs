using DotnetGeminiSDK.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalDiaryClient.Models;
using System.Text;
using System.Text.Json;

namespace PersonalDiaryClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly PersonalDiaryContext context = new PersonalDiaryContext();

        public HomeController( IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiBaseUrl = configuration["API:BaseUrl"] ?? throw new ArgumentNullException("API:BaseUrl");
        }

        public IActionResult Index()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View();
        }
        public IActionResult AI()
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


        [HttpPost]
        public async Task<IActionResult> AskAI(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                ViewBag.AIResponse = "No content provided.";
                return View("AI");
            }

            var client = _httpClientFactory.CreateClient();
            var url = $"{_apiBaseUrl}/api/AI/prompttext?text={Uri.EscapeDataString("Gợi ý viết nhật ký" + userInput)}";

            var response = await client.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                try
                {
                    var jsonResponse = JsonSerializer.Deserialize<JsonElement>(result);
                    var aiResponse = jsonResponse
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    ViewBag.AIResponse = aiResponse?.Replace("*", string.Empty) ?? "No response content.";
                }
                catch (Exception)
                {
                    ViewBag.AIResponse = "Error parsing AI response.";
                }
            }
            else
            {
                ViewBag.AIResponse = "AI không thể trả lời yêu cầu của bạn.";
            }

            return View("AI");
        }

        [HttpGet]
        public async Task<IActionResult> SendAI(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                ViewBag.AIResponse = "No content provided.";
                return View("AI");
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId.Value);

                if (user?.Dob.HasValue == true)
                {
                    var age = CalculateAge(user.Dob.Value);

                    var client = _httpClientFactory.CreateClient();
                    var url = $"{_apiBaseUrl}/api/AI/prompttext?text={Uri.EscapeDataString("Gợi ý viết nhật ký " + userInput + " dưới góc độ " + age + " tuổi")}";

                    var response = await client.PostAsync(url, null);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(result);
                            var aiResponse = jsonResponse
                                .GetProperty("candidates")[0]
                                .GetProperty("content")
                                .GetProperty("parts")[0]
                                .GetProperty("text")
                                .GetString();

                            ViewBag.AIResponse = aiResponse?.Replace("*", string.Empty) ?? "No response content.";
                        }
                        catch (Exception)
                        {
                            ViewBag.AIResponse = "Error parsing AI response.";
                        }
                    }
                    else
                    {
                        ViewBag.AIResponse = "AI không thể trả lời yêu cầu của bạn.";
                    }
                }
                else
                {
                    ViewBag.AIResponse = "User's date of birth is missing.";
                }
            }
            else
            {
                ViewBag.AIResponse = "User ID is missing.";
            }

            return View("AI");
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}