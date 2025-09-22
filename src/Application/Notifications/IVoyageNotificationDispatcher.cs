namespace XIVSubmarinesRewrite.Application.Notifications;

using System.Threading;
using System.Threading.Tasks;

/// <summary>Abstracts delivery of voyage completion notifications to downstream channels.</summary>
public interface IVoyageNotificationDispatcher
{
    ValueTask DispatchAsync(NotificationEnvelope envelope, CancellationToken cancellationToken = default);
}
