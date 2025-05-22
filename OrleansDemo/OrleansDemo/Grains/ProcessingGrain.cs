using Microsoft.Extensions.Logging;

namespace OrleansDemo.Grains;

internal interface IProcessingGrain : IGrainWithStringKey
{
    Task Ping();
}

internal class ProcessingGrain : GrainBase<ProcessingGrain>, IProcessingGrain
{
    public ProcessingGrain(ILogger<ProcessingGrain> logger) : base(logger) { }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        this.RegisterGrainTimer(ProcessData, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

        return base.OnActivateAsync(cancellationToken);
    }

    public Task Ping()
    {
        _logger.LogInformation("Ping received.");

        return Task.CompletedTask;
    }

    public async Task ProcessData()
    {
        _logger.LogInformation("Processing data...");

        var arenaGrain = GrainFactory.GetGrain<IArenaGrain>(this.GetPrimaryKeyString());
        var robotIds = await arenaGrain.GetRobotIds();

        foreach (var robotId in robotIds)
        {
            var scriptGrain = GrainFactory.GetGrain<IScriptGrain>(robotId);
            await scriptGrain.Process();
        }
    }
}