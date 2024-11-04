using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalDiaryClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace PersonalDiaryClient.Controllers
{
    public class DiaryController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private string ProductApiUrl = "https://localhost:7108/api/Post";

        public DiaryController()
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(NewPostDTO newPostDto)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized(); 
            }

            newPostDto.UserId = userId.Value;

            try
            {
                var json = JsonConvert.SerializeObject(newPostDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{ProductApiUrl}/CreatePost", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Bài viết đã được tạo thành công!";
                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    ViewBag.Error = "Unable to create post.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

             return RedirectToAction("Index", "Home"); 
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Login/Index");
        }

        public async Task<IActionResult> Public()
        {
            List<Post> posts = new List<Post>();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/GetAllPost"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Post>>(json)
                                 .Where(post => post.Privacy == "public")
                                 .ToList();
                    }
                    else
                    {
                        ViewBag.Error = "Unable to retrieve posts from the API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            ViewBag.Posts = posts;
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            Post post = null;
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/FindPostById/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        post = JsonConvert.DeserializeObject<Post>(json);
                    }
                    else
                    {
                        ViewBag.Error = "Unable to retrieve the post from the API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            if (post == null)
            {
                ViewBag.Error = "Post not found.";
                return NotFound();
            }

            return View(post);
        }
        public async Task<IActionResult> DetailsPrivate(int id)
        {
            Post post = null;
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/FindPostById/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        post = JsonConvert.DeserializeObject<Post>(json);
                    }
                    else
                    {
                        ViewBag.Error = "Unable to retrieve the post from the API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            if (post == null)
            {
                ViewBag.Error = "Post not found.";
                return NotFound();
            }

            return View(post);
        }
        public async Task<IActionResult> SearchByTag(string tag)
        {
            List<Post> posts = new List<Post>();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/SearchByTag/{tag}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Post>>(json)
                                 .Where(post => post.Privacy == "public")
                                 .ToList();
                    }
                    else
                    {
                        ViewBag.Error = "Unable to retrieve posts from the API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            ViewBag.Posts = posts;
            return View("Public");
        }
        public async Task<IActionResult> SearchByTagPrivate(string tag)
        {
            List<Post> posts = new List<Post>();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/SearchByTagPrivate/{tag}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Post>>(json)
                                 .Where(post => post.Privacy == "private")
                                 .ToList();
                    }
                    else
                    {
                        ViewBag.Error = "Unable to retrieve posts from the API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            ViewBag.Posts = posts;
            return View("Public");
        }
        public async Task<IActionResult> Private()
        {
            List<Post> posts = new List<Post>();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/GetAllPost"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Post>>(json)
                                 .Where(post => post.Privacy == "private")
                                 .ToList();
                    }
                    else
                    {
                        ViewBag.Error = "Unable to retrieve posts from the API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            ViewBag.Posts = posts;
            return View();
        }
    }
}
