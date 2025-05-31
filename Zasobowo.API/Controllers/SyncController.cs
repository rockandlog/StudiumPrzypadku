using Microsoft.AspNetCore.Mvc;
using Zasobowo.API.Data;
using Zasobowo.API.Models;

namespace Zasobowo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ZasobowoContext _context;

        public SyncController(ZasobowoContext context)
        {
            _context = context;
        }

        [HttpPost("device")]
        public async Task<IActionResult> SyncDevice([FromBody] Device device)
        {
            var existing = await _context.Devices.FindAsync(device.Id);
            if (existing != null)
            {
                existing.Name = device.Name;
                existing.Status = device.Status;
                existing.AssignedTo = device.AssignedTo;
            }
            else
            {
                await _context.Devices.AddAsync(device);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("device-delete")]
        public async Task<IActionResult> SyncDeleteDevice([FromBody] int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
