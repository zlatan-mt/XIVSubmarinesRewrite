namespace XIVSubmarinesRewrite.Infrastructure.Repositories;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;

/// <summary>Temporary store for route metadata.</summary>
public sealed class InMemoryRouteRepository : IRouteRepository
{
    private readonly ConcurrentDictionary<string, Route> store = new();

    public ValueTask<Route?> GetAsync(string routeId, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        this.store.TryGetValue(routeId, out var route);
        return ValueTask.FromResult(route);
    }

    public async IAsyncEnumerable<Route> ListAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var route in this.store.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return route;
        }

        await Task.CompletedTask;
    }
}
