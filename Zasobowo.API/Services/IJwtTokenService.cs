using Zasobowo.API.Models;

namespace Zasobowo.API.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
