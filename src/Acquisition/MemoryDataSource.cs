namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Attempts to capture submarine data directly from process memory.</summary>
public sealed class MemoryDataSource : IDataSource
{
    private readonly IMemorySubmarineSnapshotSource source;

    public MemoryDataSource(IMemorySubmarineSnapshotSource source)
    {
        this.source = source;
    }

    public async ValueTask<AcquisitionSnapshot?> TryAcquireAsync(CancellationToken cancellationToken = default)
    {
        var submarines = await this.source.TryReadAsync(cancellationToken).ConfigureAwait(false);
        if (submarines is null || submarines.Count == 0)
        {
            return null;
        }

        var characterId = submarines[0].Id.CharacterId;
        if (characterId == 0)
        {
            return null;
        }

        return new AcquisitionSnapshot(
            DateTime.UtcNow,
            submarines,
            AcquisitionSourceKind.Memory,
            characterId,
            null,
            null,
            SnapshotConfidence.Direct);
    }
}
