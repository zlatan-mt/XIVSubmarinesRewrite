namespace XIVSubmarinesRewrite.Acquisition;

using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;
using XIVSubmarinesRewrite.Infrastructure.Persistence;

/// <summary>Persists snapshot contents to repositories for later queries.</summary>
public sealed class SnapshotPersister
{
    private readonly ISubmarineRepository submarineRepository;
    private readonly IVoyageRepository voyageRepository;
    private readonly SnapshotStorageService storageService;

    public SnapshotPersister(ISubmarineRepository submarineRepository, IVoyageRepository voyageRepository, SnapshotStorageService storageService)
    {
        this.submarineRepository = submarineRepository;
        this.voyageRepository = voyageRepository;
        this.storageService = storageService;
    }

    public async ValueTask PersistAsync(AcquisitionSnapshot snapshot, CancellationToken cancellationToken = default)
    {
        foreach (var submarine in snapshot.Submarines)
        {
            await this.submarineRepository.SaveAsync(submarine, cancellationToken).ConfigureAwait(false);
            foreach (var voyage in submarine.Voyages)
            {
                await this.voyageRepository.SaveAsync(voyage, cancellationToken).ConfigureAwait(false);
            }
        }

        this.storageService.UpdateFromSnapshot(snapshot);
    }
}
