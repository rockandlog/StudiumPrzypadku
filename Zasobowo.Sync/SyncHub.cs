using Microsoft.AspNetCore.SignalR;

namespace Zasobowo.Sync
{
    public class SyncHub : Hub
    {
        // Klient może zawołać tą metodę
        public async Task BroadcastDeviceUpdate(string message)
        {
            await Clients.Others.SendAsync("ReceiveDeviceUpdate", message);
        }
    }
}
