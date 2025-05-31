using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Data;
using Zasobowo.API.Models;
using Zasobowo.API.Models.Dtos;

namespace Zasobowo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ZasobowoContext _context;

        public DeviceController(ZasobowoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDevices()
        {
            var devices = await _context.Devices
                .Include(d => d.AssignedUser)
                .ToListAsync();

            var result = devices.Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                Type = d.Type,
                Status = d.Status,
                AssignedTo = d.AssignedUser?.Username
            });

            return Ok(result);
        }
    }
}
