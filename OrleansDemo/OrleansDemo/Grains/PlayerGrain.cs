using Microsoft.Extensions.Logging;

namespace OrleansDemo.Grains;

internal interface IPlayerGrain : IGrainWithStringKey
{
    Task<Guid> DeployRobot(string arena);
}

internal class PlayerGrain : GrainBase<PlayerGrain>, IPlayerGrain
{
    public PlayerGrain(ILogger<PlayerGrain> logger) : base(logger) { }

    public async Task<Guid> DeployRobot(string arena)
    {
        var arenaGrain = GrainFactory.GetGrain<IArenaGrain>(arena);
        await arenaGrain.StartGame();

        return await arenaGrain.DeployRobot();
    }
}