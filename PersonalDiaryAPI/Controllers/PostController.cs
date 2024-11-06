using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDiaryAPI.Models;

namespace PersonalDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        PersonalDiaryContext _context = new PersonalDiaryContext();
        [HttpGet("GetAllPost")]
        public IActionResult GetPost()
        {
            var post = _context.Posts
                .Include(p => p.Likes)
                .Include(p => p.Reports)
                .Include(p => p.Shares)
                .Include(p => p.User)
                .Select(x => new PostDTO
                {
                     Content = x.Content,
                     CreatedAt = x.CreatedAt,
                     Id = x.Id,
                     Privacy = x.Privacy,
                     UserId = x.UserId,
                     Tag = x.Tag,
                    Likes = x.Likes.Select(l => new LikeDTO
                    {
                        Id = l.Id,
                        CreatedAt = l.CreatedAt,
                        UserId = l.UserId,
                        PostId = l.PostId,
                    }).ToList(),
                    Shares = x.Shares.Select(s => new ShareDTO
                    {
                        Id = s.Id,
                        CreatedAt = s.CreatedAt,
                        UserId = s.UserId,
                        PostId = s.PostId
                    }).ToList(),
                    Reports = x.Reports.Select(r => new ReportDTO
                    {
                        Id = r.Id,
                        CreatedAt = r.CreatedAt,
                        UserId = r.UserId,
                        PostId = r.PostId,
                        Reason = r.Reason
                    }).ToList(),

                }).ToList();
            return Ok(post);
        }
        [HttpGet("FindPostById/{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _context.Posts
                .Include(p => p.User)
                .FirstOrDefault(x => x.Id == id);

            if (post == null)
            {
                return NotFound("Post not found.");
            }

            var postDto = new PostDTO
            {
                Id = post.Id,
                UserId = post.UserId,
                Content = post.Content,
                Tag = post.Tag,
                CreatedAt = post.CreatedAt,
                Privacy = post.Privacy,
                User = new UserDTO
                {
                    Fullname = post.User.Fullname,
                }
            };


            return Ok(postDto);
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] NewPostDTO newPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var post = new Post
                {
                    UserId = newPostDto.UserId,
                    Content = newPostDto.Content,
                    Privacy = newPostDto.Privacy,
                    CreatedAt = DateTime.UtcNow,
                    Tag = newPostDto.Tag,
                };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut("EditPost/{id}")]
        public IActionResult EditPost(int id, [FromBody] EditPostDTO editPostDTO)
        {
            if (editPostDTO == null || string.IsNullOrEmpty(editPostDTO.Content) || string.IsNullOrEmpty(editPostDTO.Privacy))
            {
                return BadRequest("Content and Privacy fields are required.");
            }

            // Tìm bài post cần cập nhật
            var postEntity = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (postEntity == null)
            {
                return NotFound("Post not found.");
            }
            
            postEntity.Content = editPostDTO.Content;
            postEntity.Privacy = editPostDTO.Privacy;
            postEntity.Tag = editPostDTO.Tag;
            _context.SaveChanges();

            return Ok("Update successfully");
       
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            // Tìm bài post theo Id
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound("Post not found.");
            }

            _context.Posts.Remove(post);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occurred while deleting the post: {ex.InnerException?.Message}");
            }

            return Ok("Post deleted successfully.");
        }
        [HttpPut("EditPostPrivate/{id}")]
        public IActionResult EditPostPrivate(int id, [FromBody] EditPostDTO editPostDTO)
        {
            if (editPostDTO == null || string.IsNullOrEmpty(editPostDTO.Content) || string.IsNullOrEmpty(editPostDTO.Privacy))
            {
                return BadRequest("Content and Privacy fields are required.");
            }

            // Tìm bài post cần cập nhật
            var postEntity = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (postEntity == null)
            {
                return NotFound("Post not found.");
            }

            postEntity.Content = editPostDTO.Content;
            postEntity.Privacy = editPostDTO.Privacy;
            postEntity.Tag = editPostDTO.Tag;
            _context.SaveChanges();

            return Ok("Update successfully");

        }
        [HttpDelete("DeletePostPrivate/{id}")]
        public IActionResult DeletePostPrivate(int id)
        {
            // Tìm bài post theo Id
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound("Post not found.");
            }

            _context.Posts.Remove(post);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occurred while deleting the post: {ex.InnerException?.Message}");
            }

            return Ok("Post deleted successfully.");
        }
        [HttpGet("SearchByTag/{tag}")]
        public async Task<ActionResult<IEnumerable<Post>>> SearchByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return BadRequest("Tag cannot be empty");
            }

            var posts = await _context.Posts
                .Where(p => p.Tag.Contains(tag) && p.Privacy == "public") 
                .ToListAsync();

            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found with the specified tag");
            }

            return Ok(posts);
        }
        [HttpGet("SearchByTagPrivate/{tag}")]
        public async Task<ActionResult<IEnumerable<Post>>> SearchByTagPrivate(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return BadRequest("Tag cannot be empty");
            }

            var posts = await _context.Posts
                .Where(p => p.Tag.Contains(tag) && p.Privacy == "private")
                .ToListAsync();

            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found with the specified tag");
            }

            return Ok(posts);
        }
        //[HttpPost("ReportPost")]
        //public IActionResult ReportPost([FromForm] ReportDTO reportDto)
        //{
        //    if (reportDto == null || reportDto.UserId <= 0 || reportDto.PostId <= 0 || string.IsNullOrEmpty(reportDto.Reason))
        //    {
        //        return BadRequest("UserId, PostId, and Reason fields are required.");
        //    }

        //    var post = _context.Posts.FirstOrDefault(p => p.Id == reportDto.PostId);
        //    if (post == null)
        //    {
        //        return NotFound("Post not found.");
        //    }

        //    var user = _context.Users.FirstOrDefault(u => u.Id == reportDto.UserId);
        //    if (user == null)
        //    {
        //        return NotFound("User not found.");
        //    }

        //    var report = new Report
        //    {
        //        UserId = reportDto.UserId,
        //        PostId = reportDto.PostId,
        //        Reason = reportDto.Reason,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    _context.Reports.Add(report);

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return StatusCode(500, $"An error occurred while reporting the post: {ex.InnerException?.Message}");
        //    }

        //    // Trả về phản hồi thành công
        //    return Ok("Post reported successfully.");
        //}
    }

}

