using Microsoft.AspNetCore.Mvc;
using PersonalDiaryClient.Models;

namespace PersonalDiaryClient.Controllers
{
    public class SignupController : Controller
    {
        private readonly PersonalDiaryContext _context;

        public SignupController(PersonalDiaryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    PrivatePassword = userDto.PrivatePassword,
                    Fullname = userDto.Fullname,
                    Dob = userDto.Dob,
                    Number = userDto.Number,
                    CreatedAt = DateTime.Now,
                    IsBlock = false,
                    RoleId = 2
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index", "Login");
            }
            return View("Index", userDto);
        }
    }
}
