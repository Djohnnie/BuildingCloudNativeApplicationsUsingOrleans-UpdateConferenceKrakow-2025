using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrleansDemo.Grains;

namespace OrleansDemo.Workers;

internal class DemoWorker : BackgroundService
{
    private readonly IClusterClient _grainClient;
    private readonly ILogger<DemoWorker> _logger;

    public DemoWorker(
        IClusterClient grainClient,
        ILogger<DemoWorker> logger)
    {
        _grainClient = grainClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DemoWorker is doing background work.");

            IPlayerGrain player = _grainClient.GetGrain<IPlayerGrain>("new-player");
            var robotId = await player.DeployRobot("my-private-arena");

            await Task.Delay(10000, stoppingToken);
        }
    }
}