namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Falls back to UI scraping when memory inspection fails.</summary>
public sealed class UiDataSource : IDataSource
{
    private readonly IUiSubmarineSnapshotSource source;

    public UiDataSource(IUiSubmarineSnapshotSource source)
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
            AcquisitionSourceKind.Ui,
            characterId,
            null,
            null,
            SnapshotConfidence.Direct);
    }
}
