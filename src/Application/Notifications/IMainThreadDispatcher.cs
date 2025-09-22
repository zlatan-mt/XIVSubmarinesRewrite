namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Abstraction to execute work on Dalamud's main thread.</summary>
public interface IMainThreadDispatcher
{
    ValueTask InvokeAsync(Func<ValueTask> action, CancellationToken cancellationToken = default);
}
