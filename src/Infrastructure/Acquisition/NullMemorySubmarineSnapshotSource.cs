namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Placeholder memory reader that yields no data until wired to Dalamud.</summary>
public sealed class NullMemorySubmarineSnapshotSource : IMemorySubmarineSnapshotSource
{
    public ValueTask<IReadOnlyList<Submarine>?> TryReadAsync(CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
    }
}
