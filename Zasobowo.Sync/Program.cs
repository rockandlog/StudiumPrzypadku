using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zasobowo.Sync.Services;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddLogging(config => config.AddConsole());
        services.AddSingleton<RemoteSyncService>();
        services.AddSingleton<SyncService>();
    });

var host = builder.Build();
var syncService = host.Services.GetRequiredService<SyncService>();
await syncService.StartAsync();

Console.WriteLine("Naciœnij dowolny klawisz, aby zakoñczyæ...");
Console.ReadKey();
