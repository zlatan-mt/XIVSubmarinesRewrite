namespace XIVSubmarinesRewrite.Domain.Repositories;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Retrieves voyage history and current voyage state.</summary>
public interface IVoyageRepository
{
    ValueTask<Voyage?> GetAsync(VoyageId voyageId, CancellationToken cancellationToken = default);
    ValueTask SaveAsync(Voyage voyage, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Voyage> ListActiveAsync(ulong characterId, CancellationToken cancellationToken = default);
}
