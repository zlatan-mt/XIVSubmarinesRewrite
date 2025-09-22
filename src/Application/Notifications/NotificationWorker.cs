namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Background worker that drains the notification queue and forwards events to the dispatcher with retry semantics.</summary>
public sealed class NotificationWorker : IDisposable
{
    private readonly INotificationQueue queue;
    private readonly IVoyageNotificationDispatcher dispatcher;
    private readonly IMainThreadDispatcher mainThreadDispatcher;
    private readonly ILogSink log;
    private readonly CancellationTokenSource cancellation = new ();
    private readonly Task loopTask;
    private bool disposed;

    public NotificationWorker(
        INotificationQueue queue,
        IVoyageNotificationDispatcher dispatcher,
        IMainThreadDispatcher mainThreadDispatcher,
        ILogSink log)
    {
        this.queue = queue;
        this.dispatcher = dispatcher;
        this.mainThreadDispatcher = mainThreadDispatcher;
        this.log = log;
        this.loopTask = Task.Run(() => this.RunAsync(this.cancellation.Token));
    }

    public void Dispose()
    {
        if (this.disposed)
        {
            return;
        }

        this.disposed = true;
        this.cancellation.Cancel();
        try
        {
            this.loopTask.Wait(TimeSpan.FromSeconds(5));
        }
        catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
        {
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Warning, "[Notifications] Notification worker failed to stop cleanly.", ex);
        }
        finally
        {
            this.cancellation.Dispose();
        }
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            NotificationWorkItem? item = null;
            try
            {
                item = await this.queue.DequeueAsync(cancellationToken).ConfigureAwait(false);
                if (item is null)
                {
                    continue;
                }

                await this.DispatchAsync(item, cancellationToken).ConfigureAwait(false);
                this.queue.ReportSuccess(item);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                if (item is null)
                {
                    this.log.Log(LogLevel.Error, "[Notifications] Worker loop encountered an unexpected error.", ex);
                    continue;
                }

                TimeSpan? overrideDelay = null;
                if (ex is NotificationRateLimitException rateLimit && rateLimit.RetryAfter > TimeSpan.Zero)
                {
                    overrideDelay = rateLimit.RetryAfter;
                }

                var state = this.queue.ReportFailure(item, ex, overrideDelay);
                if (state == NotificationDeliveryState.Pending)
                {
                    this.log.Log(LogLevel.Warning, $"[Notifications] Dispatch failed (attempt {item.AttemptCount}) for {item.Envelope.HashKey}.", ex);
                }
                else
                {
                    this.log.Log(LogLevel.Error, $"[Notifications] Gave up on {item.Envelope.HashKey} after {item.AttemptCount} attempts.", ex);
                }
            }
        }
    }

    private async ValueTask DispatchAsync(NotificationWorkItem item, CancellationToken cancellationToken)
    {
        await this.mainThreadDispatcher.InvokeAsync(
            () => this.dispatcher.DispatchAsync(item.Envelope, cancellationToken),
            cancellationToken).ConfigureAwait(false);
    }
}
