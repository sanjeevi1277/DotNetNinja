using DotNetNinja.DTO;
using DotNetNinja.Enum;
using DotNetNinja.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(rd.Password),
                //PasswordServices.HashPassword(rd.Password),
                Email = rd.Email,
                EmailVerificationToken = Guid.NewGuid().ToString(),
                RoleId = 1,
            };
            _dbcontext.Users.Add(User);
            _dbcontext.SaveChanges();
            return Ok("User Created Successfully!!!");
        }

        [HttpPost("user/login")]
        public IActionResult UserLogin([FromBody] LoginDTO loginModel)
        {
            //var user = _dbcontext.Users
            //    .Where(u => u.Name == loginModel.Name)
            //    .Select(u => new { u.Name, u.PasswordHash })
            //    .FirstOrDefault();
            //if (user != null && PasswordServices.VerifyPassword(user.PasswordHash, loginModel.Password))
            //{
            //    return "Login Successfully!!!!";
            //}
            //return "Login Failed";
            var user = _dbcontext.Users.FirstOrDefault(u => u.Name == loginModel.Name);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            return Ok("Login succsess");
            // Generate token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes("pBv8UjHs1f8s1yqxLhZG5U+mFZAPrsw5xn33gq26S5w=");

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //    new Claim(ClaimTypes.NameIdentifier, user.GUID.ToString()),
        //    new Claim(ClaimTypes.Name, user.Name)
        //}),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var jwt = tokenHandler.WriteToken(token);
        //    return Ok(new { token = jwt });

        }
    }
}
