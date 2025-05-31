using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Zasobowo.Client.Models;

namespace Zasobowo.Client.Services
{
    public class DeviceServiceClient
    {
        private readonly HttpClient _httpClient;

        public DeviceServiceClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7031/api/");
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Device>>("device");
        }

        public async Task AddDeviceAsync(Device device)
        {
            var response = await _httpClient.PostAsJsonAsync("device", device);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(ParseErrorMessage(content));
            }
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            var response = await _httpClient.PutAsJsonAsync($"device/{device.Id}", device);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(ParseErrorMessage(content));
            }
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"device/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Usuwanie nie powiodło się: {content}");
            }
        }

        private string ParseErrorMessage(string json)
        {
            if (json.Contains("already exists"))
                return "Urządzenie o tej nazwie już istnieje.";
            if (json.Contains("required"))
                return "Uzupełnij wszystkie wymagane pola.";
            if (json.Contains("przypisanego użytkownika"))
                return "Przydzielone urządzenie musi mieć przypisanego użytkownika.";
            return "Wystąpił błąd podczas operacji.";
        }
    }
}
