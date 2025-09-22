namespace XIVSubmarinesRewrite.Application.Notifications;

using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Services;

/// <summary>Bridges voyage notifications to the existing notification coordinator.</summary>
public sealed class NotificationCoordinatorDispatcher : IVoyageNotificationDispatcher
{
    private readonly NotificationCoordinator coordinator;

    public NotificationCoordinatorDispatcher(NotificationCoordinator coordinator)
    {
        this.coordinator = coordinator;
    }

    public ValueTask DispatchAsync(NotificationEnvelope envelope, CancellationToken cancellationToken = default)
    {
        return this.coordinator.PublishVoyageCompletionAsync(envelope, cancellationToken);
    }
}
