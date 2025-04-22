using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;

namespace DotNetNinja.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        [HttpPost]
        public IActionResult RegisterCustomer([FromBody] Customer cs)
        {
            double date = (cs.DefualtCheckOutDate - cs.CheckInDate).TotalDays;
            return Ok();
        }
    }
}
