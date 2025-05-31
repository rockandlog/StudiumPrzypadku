using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zasobowo.Sync.Models;

namespace Zasobowo.Sync.Services
{
    public class RemoteSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RemoteSyncService> _logger;

        public RemoteSyncService(ILogger<RemoteSyncService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient(); // prosty HttpClient, możesz zamienić na wstrzykiwany
        }

        public async Task SyncDevice(DeviceDto device)
        {
            _logger.LogInformation($"[SYNC] Wysyłanie urządzenia ID={device.Id}");
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7031/api/sync/device", device);
            _logger.LogInformation($"[SYNC] Odpowiedź: {response.StatusCode}");
        }

        public async Task SyncDeleteDevice(int id)
        {
            _logger.LogInformation($"[SYNC] Usuwanie urządzenia ID={id}");
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7031/api/sync/device-delete", id);
            _logger.LogInformation($"[SYNC] Odpowiedź: {response.StatusCode}");
        }

        public async Task<List<DeviceDto>> GetDevicesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<DeviceDto>>("https://localhost:7031/api/device");
            return response ?? new List<DeviceDto>();
        }
    }
}
