using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zasobowo.Sync.Models;

namespace Zasobowo.Sync.Services
{
    public class SyncService
    {
        private readonly ILogger<SyncService> _logger;
        private readonly RemoteSyncService _remoteSyncService;

        public SyncService(ILogger<SyncService> logger, RemoteSyncService remoteSyncService)
        {
            _logger = logger;
            _remoteSyncService = remoteSyncService;
        }

        public async Task StartAsync()
        {
            _logger.LogInformation("=== [SYNC SERVICE STARTED] ===");

            var device = new DeviceDto
            {
                Id = 99,
                Name = "Router testowy",
                Status = "Dostępny",
                AssignedTo = null,
                Type = "Sieciowy"
            };

            await _remoteSyncService.SyncDevice(device);
            await _remoteSyncService.SyncDeleteDevice(device.Id);

            _logger.LogInformation("=== [SYNC SERVICE FINISHED] ===");
        }

        public async Task<List<DeviceDto>> GetDevicesAsync()
        {
            return await _remoteSyncService.GetDevicesAsync();
        }
    }
}
