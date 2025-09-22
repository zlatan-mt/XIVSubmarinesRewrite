namespace XIVSubmarinesRewrite.Infrastructure.Notifications;

using System;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;

/// <summary>Fallback dispatcher that executes callbacks synchronously on the current thread.</summary>
public sealed class ImmediateMainThreadDispatcher : IMainThreadDispatcher
{
    public ValueTask InvokeAsync(Func<ValueTask> action, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return action();
    }
}
