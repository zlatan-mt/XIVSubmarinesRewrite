namespace XIVSubmarinesRewrite.Presentation.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Application.Notifications;

/// <summary>View-model exposing notification queue status for UI diagnostics.</summary>
public sealed class NotificationQueueViewModel
{
    private readonly INotificationQueue queue;
    private IReadOnlyList<NotificationWorkItemSnapshot> pending = Array.Empty<NotificationWorkItemSnapshot>();
    private IReadOnlyList<NotificationWorkItemSnapshot> deadLetters = Array.Empty<NotificationWorkItemSnapshot>();

    public NotificationQueueViewModel(INotificationQueue queue)
    {
        this.queue = queue;
    }

    public IReadOnlyList<NotificationWorkItemSnapshot> Pending => this.pending;

    public IReadOnlyList<NotificationWorkItemSnapshot> DeadLetters => this.deadLetters;

    public void Refresh()
    {
        this.pending = this.queue.GetPending()
            .OrderBy(item => item.NextAttemptAtUtc)
            .ToArray();

        this.deadLetters = this.queue.GetDeadLetters()
            .OrderByDescending(item => item.LastAttemptAtUtc ?? item.NextAttemptAtUtc)
            .ToArray();
    }

    public bool TryRequeue(string hashKey)
    {
        var result = this.queue.TryRequeueDeadLetter(hashKey);
        if (result)
        {
            this.Refresh();
        }

        return result;
    }
}
