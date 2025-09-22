namespace XIVSubmarinesRewrite.Infrastructure.Repositories;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;

/// <summary>In-memory repository used for development and tests.</summary>
public sealed class InMemorySubmarineRepository : ISubmarineRepository
{
    private readonly ConcurrentDictionary<SubmarineId, Submarine> store = new();

    public ValueTask<Submarine?> GetAsync(SubmarineId submarineId, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        this.store.TryGetValue(submarineId, out var submarine);
        return ValueTask.FromResult(submarine);
    }

    public async IAsyncEnumerable<Submarine> ListAsync(ulong characterId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var entry in this.store)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (entry.Key.CharacterId == characterId)
            {
                yield return entry.Value;
            }
        }

        await Task.CompletedTask;
    }

    public ValueTask SaveAsync(Submarine submarine, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        foreach (var entry in this.store)
        {
            if (entry.Key.CharacterId == submarine.Id.CharacterId
                && !entry.Key.Equals(submarine.Id)
                && string.Equals(entry.Value.Name, submarine.Name, StringComparison.OrdinalIgnoreCase))
            {
                this.store.TryRemove(entry.Key, out _);
            }
        }

        this.store[submarine.Id] = submarine;
        return ValueTask.CompletedTask;
    }
}
