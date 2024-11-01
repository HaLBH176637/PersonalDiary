﻿using Microsoft.AspNetCore.Http;
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
            var post = _context.Posts.ToList();
            return Ok(post);
        }

        [HttpGet("FindPostById/{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }


        [HttpPost("CreatePost")]
        public IActionResult CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            if (createPostDTO == null || string.IsNullOrEmpty(createPostDTO.Content) || string.IsNullOrEmpty(createPostDTO.Privacy))
            {
                return BadRequest("Content and Privacy fields are required.");
            }
            var data = new Post
            {
                UserId = createPostDTO.UserId,
                Content = createPostDTO.Content,
                Privacy = createPostDTO.Privacy,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Posts.Add(data); 
            _context.SaveChanges();       

            return CreatedAtAction(nameof(GetPostById), new { id = createPostDTO.Id }, createPostDTO);
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
            _context.SaveChanges();

            return Ok("Edit successfully");
       
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

    }
}
