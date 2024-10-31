using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDiaryAPI.Models;

namespace PersonalDiaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
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
            var user = _context.Users.ToList();
            return Ok(user);
        }
    }
}
