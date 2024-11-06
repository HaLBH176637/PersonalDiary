using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly PersonalDiaryContext _context = new PersonalDiaryContext();
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

        public async Task<IActionResult> Public(int page = 1, int pageSize = 5)
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

            var totalPosts = posts.Count;
            var totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);
            var paginatedPosts = posts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Posts = paginatedPosts;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PostLikes = paginatedPosts.ToDictionary(post => post.Id, post => post.Likes.Count);

            // Lấy các bài viết đã like từ user
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId != 0)
            {
                var likedPosts = _context.Likes
                                          .Where(l => l.UserId == userId)
                                          .Select(l => l.PostId)
                                          .ToList();
                ViewBag.LikedPosts = likedPosts;
            }

            return View();
        }

        public async Task<IActionResult> ToggleLike(int postId)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Lấy ID người dùng từ session
            if (userId == 0) return Unauthorized();

            // Kiểm tra xem người dùng đã like bài viết này chưa
            var existingLike = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

            if (existingLike != null)
            {
                // Nếu đã like, bỏ like
                _context.Likes.Remove(existingLike);
            }
            else
            {
                // Nếu chưa like, thêm like
                var newLike = new Like
                {
                    UserId = userId,
                    PostId = postId,
                    CreatedAt = DateTime.Now
                };
                _context.Likes.Add(newLike);
            }

            await _context.SaveChangesAsync();

            // Cập nhật lại ViewBag.LikedPosts sau khi thay đổi trạng thái like
            var likedPosts = _context.Likes
                                      .Where(l => l.UserId == userId)
                                      .Select(l => l.PostId)
                                      .ToList();

            ViewBag.LikedPosts = likedPosts;

            return RedirectToAction("Public");
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
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
                        var userId = HttpContext.Session.GetInt32("UserId");

                        // Lọc bài viết theo quyền riêng tư và userId
                        posts = JsonConvert.DeserializeObject<List<Post>>(json)
                                    .Where(post => post.Privacy == "private" && post.UserId == userId)
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
            return View("Private");
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
                                 .Where(post => post.Privacy == "private" && HttpContext.Session.GetInt32("UserId") == post.UserId || post.Privacy == "public" && HttpContext.Session.GetInt32("UserId") == post.UserId)
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            Post posts = new Post();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/FindPostById/{id}"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<Post>(json);
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditPost1(int id, string Content, string Tag, string Privacy)
        {
            Post posts = new Post();
            posts.Content = Content;
            posts.Tag = Tag;
            posts.Privacy = Privacy;
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                posts.UserId = (int)HttpContext.Session.GetInt32("UserId");
            }
            try
            {
                using (HttpResponseMessage response = await client.PutAsJsonAsync($"{ProductApiUrl}/EditPost/{id}", posts))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<Post>(json);
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
            return RedirectToAction("Private");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Post posts = new Post();
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                posts.UserId = (int)HttpContext.Session.GetInt32("UserId");
            }
            try
            {
                using (HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/Delete/{id}"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Bài viết đã được xóa thành công!";
                    }
                    else
                    {
                        ViewData["Error"] = "Không thể xóa bài viết từ API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }
            return RedirectToAction("Public");
        }
        [HttpGet]
        public async Task<IActionResult> EditPostPrivate(int id)
        {
            Post posts = new Post();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/FindPostById/{id}"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<Post>(json);
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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditPostPrivate1(int id, string Content, string Tag, string Privacy)
        {
            Post posts = new Post();
            posts.Content = Content;
            posts.Tag = Tag;
            posts.Privacy = Privacy;
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                posts.UserId = (int)HttpContext.Session.GetInt32("UserId");
            }
            try
            {
                using (HttpResponseMessage response = await client.PutAsJsonAsync($"{ProductApiUrl}/EditPost/{id}", posts))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<Post>(json);
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
            return RedirectToAction("Private");
        }

        public async Task<IActionResult> DeletePrivate(int id)
        {
            Post posts = new Post();
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                posts.UserId = (int)HttpContext.Session.GetInt32("UserId");
            }
            try
            {
                using (HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/Delete/{id}"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Bài viết đã được xóa thành công!";
                    }
                    else
                    {
                        ViewData["Error"] = "Không thể xóa bài viết từ API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }
            return RedirectToAction("Private");
        }
        public async Task<IActionResult> FilterPosts(string sortOrder, string privacy)
        {
            List<Post> posts = new List<Post>();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/GetAllPost"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Post>>(json);

                        var userId = HttpContext.Session.GetInt32("UserId");

                        if (privacy == "public")
                        {
                            posts = posts.Where(post => post.Privacy == "public" && post.UserId == userId).ToList();
                        }
                        else if (privacy == "private")
                        {
                            posts = posts.Where(post => post.Privacy == "private" && post.UserId == userId).ToList();
                        }
                        else
                        {
                            posts = posts.Where(post => post.UserId == userId).ToList();
                        }

                        posts = sortOrder switch
                        {
                            "newest" => posts.OrderByDescending(post => post.CreatedAt).ToList(),
                            "oldest" => posts.OrderBy(post => post.CreatedAt).ToList(),
                            _ => posts.ToList()
                        };
                    }
                    else
                    {
                        ViewBag.Error = "Không thể lấy bài viết từ API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Đã xảy ra lỗi: {ex.Message}";
            }

            ViewBag.Posts = posts;
            return View("Private");
        }
        public async Task<IActionResult> FilterPostsPublic(string sortOrder, string privacy)
        {
            List<Post> posts = new List<Post>();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/GetAllPost"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<Post>>(json);

                        var userId = HttpContext.Session.GetInt32("UserId");

                        if (privacy == "public")
                        {
                            posts = posts.Where(post => post.Privacy == "public" && post.UserId == userId).ToList();
                        }
                        else if (privacy == "private")
                        {
                            posts = posts.Where(post => post.Privacy == "private" && post.UserId == userId).ToList();
                        }
                        else
                        {
                            posts = posts.Where(post => post.UserId == userId).ToList();
                        }

                        posts = sortOrder switch
                        {
                            "newest" => posts.OrderByDescending(post => post.CreatedAt).ToList(),
                            "oldest" => posts.OrderBy(post => post.CreatedAt).ToList(),
                            _ => posts.ToList()
                        };
                    }
                    else
                    {
                        ViewBag.Error = "Không thể lấy bài viết từ API.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Đã xảy ra lỗi: {ex.Message}";
            }

            ViewBag.Posts = posts;
            return View("Public");
        }
        //[HttpPost]
        //public async Task<IActionResult> ReportPost(int postId, string reason)
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");

        //    if (userId == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var reportPostDto = new ReportDTO
        //    {
        //        PostId = postId,
        //        UserId = userId.Value,
        //        Reason = reason
        //    };

        //    try
        //    {
        //        var json = JsonConvert.SerializeObject(reportPostDto);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PostAsync("https://localhost:7108/api/Post/ReportPost", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = "Bài viết đã được báo cáo thành công!";
        //            return RedirectToAction("Details", new { id = postId });
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Không thể báo cáo bài viết.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = $"Đã xảy ra lỗi: {ex.Message}";
        //    }

        //    return RedirectToAction("Details", new { id = postId });
        //}
        [HttpPost]
        public JsonResult ValidatePassword(string password)
        {
            // Get UserId from session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Ensure the userId is not null or invalid
            if (userId == null)
            {
                return Json(new { isValid = false, message = "User is not logged in." });
            }

            // Fetch the stored private password from the database
            var storedPassword = _context.Users
                                          .Where(u => u.Id == userId)  // Use session userId to find the correct user
                                          .Select(u => u.PrivatePassword)  // Adjust based on your database schema
                                          .FirstOrDefault();

            // Compare the entered password with the stored one
            if (storedPassword != null && storedPassword == password)
            {
                return Json(new { isValid = true });
            }
            else
            {
                return Json(new { isValid = false });
            }
        }

    }
}
