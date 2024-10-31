using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonalDiaryAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PersonalDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private PersonalDiaryContext _context;
        public LoginController(IConfiguration configuration, PersonalDiaryContext context)
        {
            _context = context;
            _config = configuration;
        }
        private User AuthenticateUser(string username, string password)
        {
            User user = _context.Users.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));
            return user; 
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Login(string username, string password)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(username, password);
            if (user != null)
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}
