using DotNetNinja.DTO;
using DotNetNinja.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;

namespace DotNetNinja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public readonly ApplicationDbContext _dbcontext;

        public RegisterController(ApplicationDbContext db)
        {
            _dbcontext = db;

        }
        [HttpPost("user/registration")]
        public IActionResult UserCreation(RegisterDTO rd)
        {
            var User = new User
            {
                Name = rd.Name,
                PasswordHash = PasswordServices.HashPassword(rd.Password),
                Email = rd.Email,
                EmailVerificationToken = Guid.NewGuid().ToString(),
            };
            _dbcontext.Users.Add(User);
            _dbcontext.SaveChanges();
            return Ok("User Created Successfully!!!");
        }

        [HttpPost("user/login")]
        public string UserLogin([FromBody] LoginDTO loginModel)
        {
            var user = _dbcontext.Users
                .Where(u => u.Name == loginModel.Name)
                .Select(u => new { u.Name, u.PasswordHash })
                .FirstOrDefault();
            if (user != null && PasswordServices.VerifyPassword(user.PasswordHash, loginModel.Password))
            {
                return "Login Successfully!!!!";
            }
            return "Login Failed";

        }
    }
}
