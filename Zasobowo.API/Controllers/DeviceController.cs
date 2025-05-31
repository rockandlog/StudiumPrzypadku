using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Data;
using Zasobowo.API.Models;
using System.Linq;

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
        public IActionResult GetAll()
        {
            var devices = _context.Devices
                .Include(d => d.AssignedUser) // <-- to było brakujące
                .ToList();

            return Ok(devices);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Device device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Devices.Any(d => d.Name == device.Name))
                return Conflict(new { message = "Urządzenie o takiej nazwie już istnieje." });

            if (device.Status == "Przydzielony" && device.AssignedUserId == null)
                return BadRequest(new { message = "Przydzielone urządzenie musi mieć przypisanego użytkownika." });

            _context.Devices.Add(device);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _context.Devices.FirstOrDefault(d => d.Id == id);
            if (existing == null)
                return NotFound();

            if (_context.Devices.Any(d => d.Id != id && d.Name == device.Name))
                return Conflict(new { message = "Inne urządzenie o tej nazwie już istnieje." });

            if (device.Status == "Przydzielony" && device.AssignedUserId == null)
                return BadRequest(new { message = "Przydzielone urządzenie musi mieć przypisanego użytkownika." });

            existing.Name = device.Name;
            existing.Type = device.Type;
            existing.Status = device.Status;
            existing.AssignedUserId = device.AssignedUserId;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var device = _context.Devices.FirstOrDefault(d => d.Id == id);
            if (device == null)
                return NotFound();

            _context.Devices.Remove(device);
            _context.SaveChanges();

            return Ok();
        }
    }
}
