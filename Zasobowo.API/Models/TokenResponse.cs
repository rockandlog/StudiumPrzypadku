namespace Zasobowo.API.Models.Auth
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
