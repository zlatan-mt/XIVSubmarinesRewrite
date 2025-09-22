namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Placeholder UI reader that yields no data until UI scraping is implemented.</summary>
public sealed class NullUiSubmarineSnapshotSource : IUiSubmarineSnapshotSource
{
    public ValueTask<IReadOnlyList<Submarine>?> TryReadAsync(CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
    }
}
