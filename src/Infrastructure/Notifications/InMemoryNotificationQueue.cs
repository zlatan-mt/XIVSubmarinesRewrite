namespace XIVSubmarinesRewrite.Infrastructure.Notifications;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;

/// <summary>Priority-based in-memory queue with retry/backoff semantics.</summary>
public sealed class InMemoryNotificationQueue : INotificationQueue, IDisposable
{
    private readonly object gate = new ();
    private readonly PriorityQueue<NotificationWorkItem, DateTime> queue = new ();
    private readonly Dictionary<string, NotificationWorkItem> pendingByHash = new (StringComparer.Ordinal);
    private readonly Dictionary<string, NotificationDeliveryRecord> deliveryRecords = new (StringComparer.Ordinal);
    private readonly List<NotificationWorkItem> deadLetters = new ();
    private readonly SemaphoreSlim signal = new (0);
    private readonly NotificationQueueOptions options;
    private bool disposed;

    public InMemoryNotificationQueue(NotificationQueueOptions? options = null)
    {
        this.options = options ?? new NotificationQueueOptions();
    }

    public void Dispose()
    {
        if (this.disposed)
        {
            return;
        }

        this.disposed = true;
        this.signal.Dispose();
    }

    public bool TryEnqueue(NotificationEnvelope envelope, bool forceDuplicate = false)
    {
        var now = DateTime.UtcNow;
        var hash = envelope.HashKey;

        lock (this.gate)
        {
            this.CleanupExpiredRecords(now);

            // ForceDuplicate の場合は既存の DeliveryRecord を無視して再キュー
            if (!forceDuplicate && this.deliveryRecords.TryGetValue(hash, out var record))
            {
                if (record.State == NotificationDeliveryState.Delivered && !record.IsExpired(now))
                {
                    return false;
                }

                if (record.State is NotificationDeliveryState.Pending or NotificationDeliveryState.Delivering)
                {
                    return false;
                }

                if (record.State == NotificationDeliveryState.DeadLetter && !record.IsExpired(now))
                {
                    return false;
                }
            }

            var workItem = new NotificationWorkItem(envelope, now);
            this.EnqueueInternal(workItem);

            // ForceDuplicate の場合は既存の DeliveryRecord を Pending にリセット
            var expires = now + this.options.DeliveryRecordTtl;
            if (forceDuplicate && this.deliveryRecords.TryGetValue(hash, out var existingRecord))
            {
                existingRecord.State = NotificationDeliveryState.Pending;
                existingRecord.ExpiresAtUtc = expires;
                existingRecord.LastError = null;
                existingRecord.DeliveredAtUtc = null;
                existingRecord.DeadLetteredAtUtc = null;
            }
            else
            {
                this.deliveryRecords[hash] = new NotificationDeliveryRecord(hash, NotificationDeliveryState.Pending, expires);
            }
        }

        this.signal.Release();
        return true;
    }

    public async ValueTask<NotificationWorkItem?> DequeueAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            NotificationWorkItem? ready = null;
            TimeSpan wait = Timeout.InfiniteTimeSpan;

            lock (this.gate)
            {
                if (this.queue.TryPeek(out var candidate, out var nextAt))
                {
                    var now = DateTime.UtcNow;
                    if (nextAt <= now)
                    {
                        ready = this.queue.Dequeue();
                        var hash = ready.Envelope.HashKey;
                        this.pendingByHash.Remove(hash);
                        ready.MarkDelivering(now);

                        if (this.deliveryRecords.TryGetValue(hash, out var record))
                        {
                            record.State = NotificationDeliveryState.Delivering;
                            record.ExpiresAtUtc = now + this.options.DeliveryRecordTtl;
                        }
                    }
                    else
                    {
                        wait = nextAt - now;
                    }
                }
            }

            if (ready is not null)
            {
                return ready;
            }

            if (wait == Timeout.InfiniteTimeSpan)
            {
                await this.signal.WaitAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await this.signal.WaitAsync(wait, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    public void ReportSuccess(NotificationWorkItem item)
    {
        var now = DateTime.UtcNow;
        var hash = item.Envelope.HashKey;

        lock (this.gate)
        {
            item.MarkDelivered();
            if (this.deliveryRecords.TryGetValue(hash, out var record))
            {
                record.State = NotificationDeliveryState.Delivered;
                record.DeliveredAtUtc = now;
                record.LastError = null;
                record.ExpiresAtUtc = now + this.options.DeliveryRecordTtl;
            }
        }
    }

    public NotificationDeliveryState ReportFailure(NotificationWorkItem item, Exception error, TimeSpan? overrideDelay = null)
    {
        var now = DateTime.UtcNow;
        var hash = item.Envelope.HashKey;
        var errorMessage = error.Message;

        lock (this.gate)
        {
            var nextAttempt = item.AttemptCount + 1;
            if (nextAttempt >= this.options.MaxAttempts)
            {
                item.MarkDeadLetter(now, errorMessage);
                this.deadLetters.Add(item);
                this.TrimDeadLetters();

                if (this.deliveryRecords.TryGetValue(hash, out var record))
                {
                    record.State = NotificationDeliveryState.DeadLetter;
                    record.DeadLetteredAtUtc = now;
                    record.LastError = errorMessage;
                    record.ExpiresAtUtc = now + this.options.DeliveryRecordTtl;
                }

                return NotificationDeliveryState.DeadLetter;
            }

            var delay = overrideDelay ?? this.options.RetrySchedule[Math.Min(item.AttemptCount, this.options.RetrySchedule.Length - 1)];
            item.ScheduleRetry(now, delay, errorMessage);
            this.EnqueueInternal(item);

            if (this.deliveryRecords.TryGetValue(hash, out var retryRecord))
            {
                retryRecord.State = NotificationDeliveryState.Pending;
                retryRecord.LastError = errorMessage;
                retryRecord.ExpiresAtUtc = now + this.options.DeliveryRecordTtl;
            }
        }

        this.signal.Release();
        return NotificationDeliveryState.Pending;
    }

    public IReadOnlyCollection<NotificationWorkItemSnapshot> GetPending()
    {
        lock (this.gate)
        {
            return this.pendingByHash.Values
                .Select(CreateSnapshot)
                .ToArray();
        }
    }

    public IReadOnlyCollection<NotificationWorkItemSnapshot> GetDeadLetters()
    {
        lock (this.gate)
        {
            return this.deadLetters.Select(CreateSnapshot).ToArray();
        }
    }

    public bool TryRequeueDeadLetter(string hashKey)
    {
        NotificationWorkItem? item = null;
        lock (this.gate)
        {
            var index = this.deadLetters.FindIndex(w => string.Equals(w.Envelope.HashKey, hashKey, StringComparison.Ordinal));
            if (index < 0)
            {
                return false;
            }

            var deadLetter = this.deadLetters[index];
            this.deadLetters.RemoveAt(index);

            var now = DateTime.UtcNow;
            item = new NotificationWorkItem(deadLetter.Envelope, now);
            this.EnqueueInternal(item);

            if (this.deliveryRecords.TryGetValue(hashKey, out var record))
            {
                record.State = NotificationDeliveryState.Pending;
                record.LastError = null;
                record.DeadLetteredAtUtc = null;
                record.ExpiresAtUtc = now + this.options.DeliveryRecordTtl;
            }
        }

        if (item is not null)
        {
            this.signal.Release();
            return true;
        }

        return false;
    }

    private void EnqueueInternal(NotificationWorkItem item)
    {
        this.queue.Enqueue(item, item.NextAttemptAtUtc);
        this.pendingByHash[item.Envelope.HashKey] = item;
    }

    private void CleanupExpiredRecords(DateTime now)
    {
        var expired = this.deliveryRecords.Where(kvp => kvp.Value.IsExpired(now)).Select(kvp => kvp.Key).ToList();
        foreach (var key in expired)
        {
            this.deliveryRecords.Remove(key);
        }
    }

    private void TrimDeadLetters()
    {
        var capacity = this.options.DeadLetterCapacity;
        if (capacity <= 0)
        {
            return;
        }

        if (this.deadLetters.Count <= capacity)
        {
            return;
        }

        var excess = this.deadLetters.Count - capacity;
        this.deadLetters.RemoveRange(0, excess);
    }

    private static NotificationWorkItemSnapshot CreateSnapshot(NotificationWorkItem item)
        => new (
            item.Envelope,
            item.State,
            item.AttemptCount,
            item.NextAttemptAtUtc,
            item.LastAttemptAtUtc,
            item.LastError);
}
