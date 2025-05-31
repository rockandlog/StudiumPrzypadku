using Microsoft.AspNetCore.Mvc;
using Zasobowo.API.Data;
using System.Linq;

namespace Zasobowo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ZasobowoContext _context;

        public UserController(ZasobowoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
