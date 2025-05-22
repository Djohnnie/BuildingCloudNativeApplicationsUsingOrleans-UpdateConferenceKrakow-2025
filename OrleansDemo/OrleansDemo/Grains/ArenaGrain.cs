using Microsoft.Extensions.Logging;

namespace OrleansDemo.Grains;

internal interface IArenaGrain : IGrainWithStringKey
{
    Task StartGame();
    Task<Guid> DeployRobot();
    Task<Guid[]> GetRobotIds();
}

internal class ArenaGrain : GrainBase<ArenaGrain>, IArenaGrain
{
    private readonly IList<Guid> _robotIds = new List<Guid>();
    private bool isActive = false;

    public string Name => this.GetPrimaryKeyString();

    public ArenaGrain(ILogger<ArenaGrain> logger) : base(logger) { }

    public async Task StartGame()
    {
        if (!isActive)
        {
            var processingGrain = GrainFactory.GetGrain<IProcessingGrain>(Name);
            await processingGrain.Ping();
            isActive = true;
        }
    }

    public async Task<Guid> DeployRobot()
    {
        var robotId = Guid.NewGuid();

        var robotGrain = GrainFactory.GetGrain<IRobotGrain>(robotId);
        await robotGrain.CreateRobot();
        _robotIds.Add(robotId);

        return robotId;
    }

    public Task<Guid[]> GetRobotIds()
    {
        return Task.FromResult(_robotIds.ToArray());
    }
}