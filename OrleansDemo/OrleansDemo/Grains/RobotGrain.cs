using Microsoft.Extensions.Logging;

namespace OrleansDemo.Grains;

internal interface IRobotGrain : IGrainWithGuidKey
{
    Task CreateRobot();
}

internal class RobotGrain : GrainBase<RobotGrain>, IRobotGrain
{
    public RobotGrain(ILogger<RobotGrain> logger) : base(logger) { }

    public async Task CreateRobot()
    {
        var scriptGrain = GrainFactory.GetGrain<IScriptGrain>(this.GetPrimaryKey());
        await scriptGrain.Initialize();
    }
}