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
        private readonly string _baseApiUrl = "https://localhost:7031/api/device"; // Upewnij się, że port pasuje do API

        public DeviceServiceClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Device>>(_baseApiUrl);
            return response ?? new List<Device>();
        }

        public async Task<bool> AddDeviceAsync(Device device)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseApiUrl, device);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
