namespace XIVSubmarinesRewrite.Application.Services;

using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Messaging;

/// <summary>Coordinates the acquisition gateway and propagates snapshot events.</summary>
public sealed class SnapshotOrchestrator
{
    private readonly DataAcquisitionGateway gateway;
    private readonly IEventBus eventBus;

    public SnapshotOrchestrator(DataAcquisitionGateway gateway, IEventBus eventBus)
    {
        this.gateway = gateway;
        this.eventBus = eventBus;
    }

    public async ValueTask ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var snapshot = await this.gateway.RefreshAsync(cancellationToken).ConfigureAwait(false);
        if (snapshot is null)
        {
            return;
        }

        await this.eventBus.PublishAsync(snapshot, cancellationToken).ConfigureAwait(false);
    }
}
