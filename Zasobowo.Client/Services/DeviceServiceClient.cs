using System.Net.Http;
using System.Net.Http.Json;
using Zasobowo.Client.Models;

namespace Zasobowo.Client.Services
{
    public class DeviceServiceClient
    {
        private readonly HttpClient _http = new()
        {
            BaseAddress = new Uri("https://localhost:7031/") // Zmień port jeśli używasz innego
        };

        public async Task<List<DeviceDto>> GetAllDevicesAsync()
        {
            return await _http.GetFromJsonAsync<List<DeviceDto>>("api/device") ?? new();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDto>>("api/user") ?? new();
        }

        public async Task<string> CreateDeviceAsync(DeviceDto device)
        {
            var response = await _http.PostAsJsonAsync("api/device", device);
            if (response.IsSuccessStatusCode)
                return "✅ Urządzenie dodane pomyślnie.";

            var error = await response.Content.ReadAsStringAsync();
            return $"❌ Błąd: Dodawanie nie powiodło się: {error}";
        }

        public async Task<string> UpdateDeviceAsync(int id, DeviceDto device)
        {
            var response = await _http.PutAsJsonAsync($"api/device/{id}", device);
            if (response.IsSuccessStatusCode)
                return "✅ Urządzenie zaktualizowane.";

            var error = await response.Content.ReadAsStringAsync();
            return $"❌ Błąd: Aktualizacja nie powiodła się: {error}";
        }

        public async Task<string> DeleteDeviceAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/device/{id}");
            if (response.IsSuccessStatusCode)
                return "✅ Urządzenie usunięte.";

            var error = await response.Content.ReadAsStringAsync();
            return $"❌ Błąd: Usuwanie nie powiodło się: {error}";
        }
    }
}
