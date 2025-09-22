namespace XIVSubmarinesRewrite.Infrastructure.Repositories;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;

/// <summary>In-memory persistence for voyage states.</summary>
public sealed class InMemoryVoyageRepository : IVoyageRepository
{
    private readonly ConcurrentDictionary<VoyageId, Voyage> store = new();

    public ValueTask<Voyage?> GetAsync(VoyageId voyageId, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        this.store.TryGetValue(voyageId, out var voyage);
        return ValueTask.FromResult(voyage);
    }

    public async IAsyncEnumerable<Voyage> ListActiveAsync(ulong characterId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var entry in this.store)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (entry.Key.SubmarineId.CharacterId == characterId && entry.Value.Status is VoyageStatus.Underway or VoyageStatus.Scheduled)
            {
                yield return entry.Value;
            }
        }

        await Task.CompletedTask;
    }

    public ValueTask SaveAsync(Voyage voyage, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        this.store[voyage.Id] = voyage;
        return ValueTask.CompletedTask;
    }
}
