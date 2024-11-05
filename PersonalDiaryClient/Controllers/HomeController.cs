using DotnetGeminiSDK.Model.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace PersonalDiaryClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;

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
            var client = _httpClientFactory.CreateClient();
            var url = $"{_apiBaseUrl}/api/AI/prompttext?text={Uri.EscapeDataString(userInput)}";

            var response = await client.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(result);
                var aiResponse = jsonResponse
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                if (aiResponse != null)
                {
                    aiResponse = aiResponse.Replace("*", string.Empty);
                }

                ViewBag.AIResponse = aiResponse ?? "No response content.";
            }
            else
            {
                ViewBag.AIResponse = "AI không thể trả lời yêu cầu của bạn.";
            }

            return View("AI");
        }


    }
}