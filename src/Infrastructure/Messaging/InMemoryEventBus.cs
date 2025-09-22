namespace XIVSubmarinesRewrite.Infrastructure.Messaging;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Messaging;

/// <summary>Simple synchronous event bus suitable for unit tests.</summary>
public sealed class InMemoryEventBus : IEventBus
{
    private readonly List<Func<object, CancellationToken, ValueTask>> handlers = new ();

    public void Subscribe<TEvent>(Func<TEvent, CancellationToken, ValueTask> handler) where TEvent : class
    {
        this.handlers.Add(async (evt, token) =>
        {
            if (evt is TEvent typed)
            {
                await handler(typed, token).ConfigureAwait(false);
            }
        });
    }

    public async ValueTask PublishAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default) where TEvent : class
    {
        foreach (var handler in this.handlers)
        {
            await handler(eventData!, cancellationToken).ConfigureAwait(false);
        }
    }
}
