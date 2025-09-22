namespace XIVSubmarinesRewrite.Domain.Repositories;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Access to submarine aggregates across sessions.</summary>
public interface ISubmarineRepository
{
    ValueTask<Submarine?> GetAsync(SubmarineId submarineId, CancellationToken cancellationToken = default);
    ValueTask SaveAsync(Submarine submarine, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Submarine> ListAsync(ulong characterId, CancellationToken cancellationToken = default);
}
