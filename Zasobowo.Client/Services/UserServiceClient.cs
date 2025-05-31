using System.Net.Http;
using System.Net.Http.Json;

public class UserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7031/api/");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>("user");
    }
}
