using Microsoft.Extensions.Logging;

namespace OrleansDemo.Grains;

internal interface IScriptGrain : IGrainWithGuidKey
{
    Task Initialize();
    Task Process();
}

internal class ScriptGrain : GrainBase<ScriptGrain>, IScriptGrain
{
    public ScriptGrain(ILogger<ScriptGrain> logger) : base(logger) { }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public Task Process()
    {
        return Task.CompletedTask;
    }
}