namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Provides deduplicated enqueue/dequeue operations for outbound notifications.</summary>
public interface INotificationQueue
{
    bool TryEnqueue(NotificationEnvelope envelope, bool forceDuplicate = false);

    ValueTask<NotificationWorkItem?> DequeueAsync(CancellationToken cancellationToken);

    void ReportSuccess(NotificationWorkItem item);

    NotificationDeliveryState ReportFailure(NotificationWorkItem item, Exception error, TimeSpan? overrideDelay = null);

    IReadOnlyCollection<NotificationWorkItemSnapshot> GetPending();

    IReadOnlyCollection<NotificationWorkItemSnapshot> GetDeadLetters();

    bool TryRequeueDeadLetter(string hashKey);
}
