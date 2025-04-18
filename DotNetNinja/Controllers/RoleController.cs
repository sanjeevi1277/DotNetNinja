using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;

namespace DotNetNinja.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public RoleController(ApplicationDbContext ap)
        {
            _applicationDbContext = ap;
        }
        [HttpPost("AddingRoles")]
        public IActionResult AddingRoles(string? role)
        {
            if (role == null)
            {
                return BadRequest("Role cannot be null");
            }
            var Roles = new Role
            {
                RoleName = role,
            };
            _applicationDbContext.Roles.Add(Roles);
            _applicationDbContext.SaveChanges();
            return Ok("");
        }
    }
}
