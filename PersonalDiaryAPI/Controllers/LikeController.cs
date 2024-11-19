//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PersonalDiaryAPI.Models;

//namespace PersonalDiaryAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LikeController : ControllerBase
//    {
//        PersonalDiaryContext _context = new PersonalDiaryContext();
//        [HttpGet("GetAllLike")]
//        public IActionResult GetAllLike() {
//            var like = _context.Likes.ToList();
//            return Ok(like);


//        }
//        [HttpPost("CreateLike")]
//        public IActionResult CreateLike([FromBody] LikeDTO likeDTO)
//        {
//            if (likeDTO == null || likeDTO.UserId == 0) // Kiểm tra dữ liệu hợp lệ
//            {
//                return BadRequest("Invalid like data.");
//            }

//            var post = _context.Posts.FirstOrDefault(p => p.Id == p.Id);
//            if (post == null)
//            {
//                return NotFound("Post not found.");
//            }

//            var like = new Like
//            {
//                PostId = likeDTO.Id,      
//                UserId = likeDTO.UserId,  
//                CreatedAt = DateTime.UtcNow 
//            };

//            _context.Likes.Add(like);

//            try
//            {
//                _context.SaveChanges();
//            }
//            catch (DbUpdateException ex)
//            {
//                return StatusCode(500, $"An error occurred while saving the like: {ex.InnerException?.Message}");
//            }

//            return Ok("Like successfully created.");
//        }

//        [HttpDelete("DeleteLike")]
//        public IActionResult DeleteLike(int userId, int postId)
//        {
//            var like = _context.Likes.FirstOrDefault(l => l.UserId == userId && l.PostId == postId);

//            if (like == null)
//            {
//                return NotFound("Like not found.");
//            }

//            _context.Likes.Remove(like);

//            try
//            {
//                _context.SaveChanges();
//            }
//            catch (DbUpdateException ex)
//            {
//                return StatusCode(500, $"An error occurred while deleting the like: {ex.InnerException?.Message}");
//            }

//            return Ok("Like deleted successfully.");
//        }


//    }
//}
