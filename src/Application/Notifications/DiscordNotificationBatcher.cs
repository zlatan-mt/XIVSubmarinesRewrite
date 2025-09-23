// apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs
// Discord 通知を短時間で集約し、艦隊単位のメッセージとして送信します
// 同時帰港時に通知スパムを防ぎつつ情報量を維持するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs, apps/XIVSubmarinesRewrite/src/Integrations/Notifications/IDiscordClient.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Integrations.Notifications;

/// <summary>Batches voyage notifications for Discord delivery.</summary>
public sealed class DiscordNotificationBatcher : IDisposable
{
    private readonly IDiscordClient discordClient;
    private readonly VoyageNotificationFormatter formatter;
    private readonly ILogSink log;
    private TimeSpan batchWindow;
    private readonly object gate = new ();
    private readonly Dictionary<ulong, BatchState> batches = new ();
    private bool disposed;

    public DiscordNotificationBatcher(IDiscordClient discordClient, VoyageNotificationFormatter formatter, ILogSink log, TimeSpan? batchWindow = null)
    {
        this.discordClient = discordClient;
        this.formatter = formatter;
        this.log = log;
        this.batchWindow = ValidateWindow(batchWindow ?? TimeSpan.FromSeconds(2));
    }

    public void UpdateWindow(TimeSpan window)
    {
        lock (this.gate)
        {
            this.batchWindow = ValidateWindow(window);
        }
    }

    public ValueTask EnqueueAsync(VoyageNotification notification, DiscordNotificationPayload payload, CancellationToken cancellationToken)
    {
        BatchState? flushTarget = null;
        lock (this.gate)
        {
            this.ThrowIfDisposed();
            var key = notification.CharacterId;
            if (!this.batches.TryGetValue(key, out var state))
            {
                state = BatchState.Create(notification, payload);
                this.batches[key] = state;
                this.log.Log(
                    LogLevel.Debug,
                    $"[Notifications] Discord batch started character={state.CharacterLabel} window={this.batchWindow.TotalMilliseconds:F0}ms arrival={notification.ArrivalUtc:O}.");
                this.ScheduleFlush(key, state);
                return ValueTask.CompletedTask;
            }

            state.Items.Add(new BatchItem(notification, payload));
            if (state.Items.Count >= 4)
            {
                this.batches.Remove(key);
                state.CancelTimer();
                this.log.Log(
                    LogLevel.Debug,
                    $"[Notifications] Discord batch reached immediate flush threshold character={state.CharacterLabel} count={state.Items.Count}.");
                flushTarget = state;
            }
        }

        if (flushTarget is not null)
        {
            return new ValueTask(this.FlushAsync(flushTarget, cancellationToken));
        }

        return ValueTask.CompletedTask;
    }

    public void Dispose()
    {
        if (this.disposed)
        {
            return;
        }

        this.disposed = true;
        List<BatchState> pending;
        lock (this.gate)
        {
            pending = this.batches.Values.ToList();
            this.batches.Clear();
        }

        foreach (var state in pending)
        {
            state.CancelTimer();
            try
            {
                this.FlushAsync(state, CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.log.Log(LogLevel.Warning, "[Notifications] Discord batch flush during dispose failed.", ex);
            }
            finally
            {
                state.Dispose();
            }
        }
    }

    private async Task FlushAsync(BatchState state, CancellationToken cancellationToken)
    {
        try
        {
            if (state.Items.Count == 0)
            {
                return;
            }

            if (state.Items.Count == 1)
            {
                var item = state.Items[0];
                this.log.Log(
                    LogLevel.Debug,
                    $"[Notifications] Discord batch single dispatch character={state.CharacterLabel} ageMs={(DateTime.UtcNow - state.CreatedUtc).TotalMilliseconds:F0}.");
                await this.discordClient.SendVoyageCompletionAsync(item.Notification, item.Payload, cancellationToken).ConfigureAwait(false);
                return;
            }

            var ordered = state.Items
                .Select(i => i.Notification)
                .OrderBy(n => n.ArrivalUtc)
                .ToArray();
            var latestArrival = ordered[^1].ArrivalUtc;
            var payload = this.formatter.CreateDiscordBatchPayload(ordered[0].CharacterLabel, ordered);
            this.log.Log(
                LogLevel.Information,
                $"[Notifications] Discord batch flush character={state.CharacterLabel} count={state.Items.Count} ageMs={(DateTime.UtcNow - state.CreatedUtc).TotalMilliseconds:F0} scheduledWindowMs={state.ScheduledWindow.TotalMilliseconds:F0}.");
            await this.discordClient.SendVoyageBatchAsync(ordered[0].CharacterLabel, payload, latestArrival, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Warning, "[Notifications] Discord batch dispatch failed.", ex);
        }
        finally
        {
            state.Dispose();
        }
    }

    private void ScheduleFlush(ulong key, BatchState state)
    {
        var window = GetCurrentWindow();
        state.ScheduledWindow = window;
        state.Schedule(Task.Run(async () =>
        {
            try
            {
                await Task.Delay(window, state.Cancellation.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            BatchState? flushTarget = null;
            lock (this.gate)
            {
                if (this.batches.TryGetValue(key, out var existing) && ReferenceEquals(existing, state))
                {
                    this.batches.Remove(key);
                    flushTarget = existing;
                }
            }

            if (flushTarget is null)
            {
                state.Dispose();
                return;
            }

            try
            {
                await this.FlushAsync(flushTarget, CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.log.Log(LogLevel.Warning, "[Notifications] Discord batch scheduled flush failed.", ex);
            }
        }));
    }

    private void ThrowIfDisposed()
    {
        if (this.disposed)
        {
            throw new ObjectDisposedException(nameof(DiscordNotificationBatcher));
        }
    }

    private TimeSpan GetCurrentWindow()
    {
        lock (this.gate)
        {
            return this.batchWindow;
        }
    }

    private static TimeSpan ValidateWindow(TimeSpan window)
    {
        if (window <= TimeSpan.Zero)
        {
            return TimeSpan.FromSeconds(0.5);
        }

        return window < TimeSpan.FromSeconds(0.5)
            ? TimeSpan.FromSeconds(0.5)
            : window;
    }

    private readonly record struct BatchItem(VoyageNotification Notification, DiscordNotificationPayload Payload);

    private sealed class BatchState : IDisposable
    {
        private BatchState()
        {
            this.Cancellation = new CancellationTokenSource();
            this.Items = new List<BatchItem>();
            this.CreatedUtc = DateTime.UtcNow;
        }

        public List<BatchItem> Items { get; }

        public CancellationTokenSource Cancellation { get; }

        public string CharacterLabel { get; private set; } = string.Empty;

        public ulong CharacterId { get; private set; }

        public DateTime CreatedUtc { get; }

        public TimeSpan ScheduledWindow { get; set; }

        public static BatchState Create(VoyageNotification notification, DiscordNotificationPayload payload)
        {
            var state = new BatchState();
            state.Items.Add(new BatchItem(notification, payload));
            state.CharacterLabel = notification.CharacterLabel;
            state.CharacterId = notification.CharacterId;
            return state;
        }

        public void Schedule(Task task)
        {
            _ = task;
        }

        public void CancelTimer()
        {
            try
            {
                this.Cancellation.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        public void Dispose()
        {
            this.Cancellation.Dispose();
        }
    }
}
