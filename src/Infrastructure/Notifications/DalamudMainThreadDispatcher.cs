namespace XIVSubmarinesRewrite.Infrastructure.Notifications;

using System;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Plugin.Services;
using XIVSubmarinesRewrite.Application.Notifications;

/// <summary>Dispatches work onto Dalamud's framework thread.</summary>
public sealed class DalamudMainThreadDispatcher : IMainThreadDispatcher
{
    private readonly IFramework framework;

    public DalamudMainThreadDispatcher(IFramework framework)
    {
        this.framework = framework;
    }

    public ValueTask InvokeAsync(Func<ValueTask> action, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled(cancellationToken);
        }

        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

        this.framework.RunOnFrameworkThread(() =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                tcs.TrySetCanceled(cancellationToken);
                return;
            }

            _ = ExecuteAsync(action, tcs, cancellationToken);
        });

        return new ValueTask(tcs.Task);
    }

    private static async Task ExecuteAsync(Func<ValueTask> action, TaskCompletionSource<object?> completion, CancellationToken cancellationToken)
    {
        try
        {
            await action().ConfigureAwait(false);
            completion.TrySetResult(null);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            completion.TrySetCanceled(cancellationToken);
        }
        catch (Exception ex)
        {
            completion.TrySetException(ex);
        }
    }
}
