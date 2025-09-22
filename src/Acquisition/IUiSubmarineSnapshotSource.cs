namespace XIVSubmarinesRewrite.Acquisition;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Fallback snapshot provider that leverages UI scraping.</summary>
public interface IUiSubmarineSnapshotSource
{
    ValueTask<IReadOnlyList<Submarine>?> TryReadAsync(CancellationToken cancellationToken = default);
}
