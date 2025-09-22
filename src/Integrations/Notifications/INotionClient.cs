namespace XIVSubmarinesRewrite.Integrations.Notifications;

using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Provides access to Notion automation endpoints.</summary>
public interface INotionClient
{
    ValueTask RecordAlarmAsync(Alarm alarm, CancellationToken cancellationToken = default);

    ValueTask RecordVoyageCompletionAsync(VoyageNotification notification, NotionNotificationPayload payload, CancellationToken cancellationToken = default);
}
