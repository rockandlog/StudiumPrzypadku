using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Data;
using Zasobowo.API.Models;

namespace Zasobowo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly ZasobowoContext _context;

        public DevicesController(ZasobowoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();
            return device;
        }

        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.Id) return BadRequest();
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
