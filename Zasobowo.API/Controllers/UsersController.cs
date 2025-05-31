using Microsoft.AspNetCore.Mvc;
using Zasobowo.API.Models;
using Zasobowo.API.Data;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ZasobowoContext _context;

    public UsersController(ZasobowoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
