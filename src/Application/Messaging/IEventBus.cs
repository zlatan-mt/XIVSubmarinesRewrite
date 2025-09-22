namespace XIVSubmarinesRewrite.Application.Messaging;

using System.Threading;
using System.Threading.Tasks;

/// <summary>Simplified application-level event bus abstraction.</summary>
public interface IEventBus
{
    ValueTask PublishAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default) where TEvent : class;
}
