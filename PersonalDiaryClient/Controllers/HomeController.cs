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
        private readonly ILogger<HomeController> _logger;
        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<HomeController> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiBaseUrl = configuration["API:BaseUrl"] ?? throw new ArgumentNullException("API:BaseUrl");
            _logger = logger;
            context = new PersonalDiaryContext();
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
        [HttpPost]
        public async Task<IActionResult> SummarizePost(int postId)
        {
            var post = context.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                _logger.LogError($"Post with ID {postId} not found.");
                return Json(new { summary = "Post not found." });
            }

            var postContent = post.Content;
            if (string.IsNullOrWhiteSpace(postContent))
            {
                _logger.LogWarning($"Post with ID {postId} has empty content.");
                return Json(new { summary = "Post content is empty or invalid." });
            }

            // Log the original content for debugging
            _logger.LogInformation("Summarizing post content: " + postContent);

            try
            {
                // Build the API URL
                var url = $"{_apiBaseUrl}/api/AI/summarize?text={Uri.EscapeDataString(postContent)}";
                _logger.LogInformation("Calling AI API at URL: " + url);

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync(url, null);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    // Log the raw response from AI API
                    _logger.LogInformation("API response: " + result);

                    try
                    {
                        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(result);
                        if (jsonResponse.TryGetProperty("summary", out var summaryProperty))
                        {
                            var summary = summaryProperty.GetString();
                            if (string.IsNullOrWhiteSpace(summary))
                            {
                                _logger.LogWarning("AI returned an empty summary.");
                                return Json(new { summary = "AI could not generate the summary." });
                            }

                            return Json(new { summary = summary });
                        }
                        else
                        {
                            _logger.LogWarning("AI response does not contain 'summary' property.");
                            return Json(new { summary = "AI response does not contain a valid summary." });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error parsing AI API response: " + ex.Message);
                        return Json(new { summary = "Error parsing AI response." });
                    }
                }
                else
                {
                    // Log the error status code if the response is not successful
                    _logger.LogError($"AI API call failed with status code: {response.StatusCode}");
                    return Json(new { summary = "AI could not generate the summary due to an error." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during AI API call: " + ex.Message);
                return Json(new { summary = "An error occurred while trying to summarize the post." });
            }
        }



    }
}