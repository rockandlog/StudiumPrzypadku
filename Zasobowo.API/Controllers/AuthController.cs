using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zasobowo.API.Data;
using Zasobowo.API.Models;
using Zasobowo.API.Models.Auth;

namespace Zasobowo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ZasobowoContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ZasobowoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return Conflict("Użytkownik o podanym adresie e-mail już istnieje.");
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Email.Split('@')[0],
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("Rejestracja zakończona sukcesem.");
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("Nieprawidłowy email lub hasło.");
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordValid)
            {
                return Unauthorized("Nieprawidłowy email lub hasło.");
            }

            var token = GenerateJwtToken(user);
            return Ok(new TokenResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
