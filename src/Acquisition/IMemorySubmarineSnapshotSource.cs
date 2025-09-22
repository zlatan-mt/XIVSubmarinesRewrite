namespace XIVSubmarinesRewrite.Acquisition;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Provides the latest submarine snapshot via memory inspection.</summary>
public interface IMemorySubmarineSnapshotSource
{
    ValueTask<IReadOnlyList<Submarine>?> TryReadAsync(CancellationToken cancellationToken = default);
}
