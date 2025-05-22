using Microsoft.Extensions.Logging;

namespace OrleansDemo.Grains;

internal class GrainBase<TGrain> : Grain where TGrain : Grain
{
    protected readonly ILogger<TGrain> _logger;

    public GrainBase(ILogger<TGrain> logger)
    {
        _logger = logger;
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"ACTIVATED {nameof(TGrain)} with ID '{this.GetGrainId()}'");

        return base.OnActivateAsync(cancellationToken);
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"DEACTIVATED {nameof(TGrain)} with ID '{this.GetGrainId()}' for reason '{reason}'");

        return base.OnDeactivateAsync(reason, cancellationToken);
    }
}