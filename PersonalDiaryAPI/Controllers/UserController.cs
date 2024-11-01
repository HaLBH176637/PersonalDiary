using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDiaryAPI.Models;

namespace PersonalDiaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        //Bearer 
        private IConfiguration _config;
        private PersonalDiaryContext _context;
        public UserController(IConfiguration configuration, PersonalDiaryContext context)
        {
            _context = context;
            _config = configuration;
        }
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser()
        {
            var user = _context.Users
                .Include(user => user.Role)
                .Include(user => user.Likes)               
                .Include(user => user.Reports)
                .Include(user => user.Shares)
                .Include(user => user.Posts)
                .ThenInclude(post => post.Tags)
                .Select(x => new UserDTO
                {
                    Username = x.Username,
                    Email = x.Email,
                    Dob = x.Dob,
                    Fullname = x.Fullname,
                    Number = x.Number,
                    Password = x.Password
                }).ToList();
            return Ok(user);
        }
        [HttpGet]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users
                .Include(user => user.Role)
                .Include(user => user.Likes)
                .Include(user => user.Reports)
                .Include(user => user.Shares)
                .Include(user => user.Posts)
                .ThenInclude(post => post.Tags)
                .Where(i => i.Id == id).Select(x => new UserDTO
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Dob = x.Dob,
                    Fullname = x.Fullname,
                    Number = x.Number,
                    Password = x.Password,
                    RoleId = x.RoleId
                });
            return Ok(user);
        }
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest();
            }
            if(userDTO != null)
            {
                var user = new User
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Dob = userDTO.Dob,
                    Fullname = userDTO.Fullname,
                    Number = userDTO.Number,
                    Password = userDTO.Password,
                    RoleId = userDTO.RoleId
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return Ok("Da add User moi " + userDTO.Username);
        }
        [HttpPut("EditUser")]
        public IActionResult EditUser(UserDTO userDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userDTO.Id);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng với ID: " + userDTO.Id);
            }

            user.Username = userDTO.Username;
            user.Email = userDTO.Email;
            user.Dob = userDTO.Dob;
            user.Fullname = userDTO.Fullname;
            user.Number = userDTO.Number;
            user.Password = userDTO.Password;
            user.RoleId = userDTO.RoleId;

            _context.SaveChanges();

            return Ok("Cập nhật thành công người dùng với ID: " + userDTO.Id);
        }
    }
}
