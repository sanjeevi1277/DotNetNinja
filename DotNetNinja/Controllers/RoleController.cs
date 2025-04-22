using DotNetNinja.Interfaces;
using DotNetNinja.Services;
using Microsoft.AspNetCore.Authorization;
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
        public readonly IRole _roleservices;
        public RoleController(ApplicationDbContext ap,IRole rs)
        {
            _applicationDbContext = ap;
            _roleservices = rs;

        }
        
        [HttpPost("AddingRoles")]
        public IActionResult AddingRoles(string? role)
        {
            if (string.IsNullOrWhiteSpace(role))
                return BadRequest("Role cannot be null");

            bool success = _roleservices.CreateRoles(role);

            if (success)
                return Ok("Role created successfully");

            return BadRequest("Failed to create role");
        }
    }
}
