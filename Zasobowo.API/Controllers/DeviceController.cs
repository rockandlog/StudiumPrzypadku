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

<<<<<<< HEAD
            var result = devices.Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                Type = d.Type,
                Status = d.Status,
                AssignedTo = d.AssignedUser?.Username
            });

            return Ok(result);
=======
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
                return NotFound();

            return device;
        }

        [HttpPost]
        public async Task<ActionResult<Device>> AddDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, Device device)
        {
            var existing = await _context.Devices.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = device.Name;
            existing.Status = device.Status;
            existing.AssignedTo = device.AssignedTo;
            existing.Type = device.Type;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
                return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return NoContent();
>>>>>>> develop
        }
    }
}
