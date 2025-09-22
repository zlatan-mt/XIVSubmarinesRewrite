namespace XIVSubmarinesRewrite.Infrastructure.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;
using XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>Provides high-level access to persisted snapshot data.</summary>
public sealed class SnapshotStorageService
{
    private readonly ISettingsProvider settings;
    private readonly SnapshotStorageMigrator migrator;
    private readonly object sync = new ();
    private SnapshotStorageEnvelope envelope;

    public SnapshotStorageService(ISettingsProvider settings, SnapshotStorageMigrator migrator)
    {
        this.settings = settings;
        this.migrator = migrator;
        this.envelope = migrator.Load();
    }

    public IReadOnlyList<AcquisitionSnapshot> LoadSnapshots()
    {
        var results = new List<AcquisitionSnapshot>();
        foreach (var (cid, character) in this.envelope.Data.Characters)
        {
            if (character.Submarines.Count == 0)
            {
                continue;
            }

            var submarines = new List<Submarine>();
            foreach (var submarineRecord in character.Submarines.Values)
            {
                var subId = new SubmarineId(cid, submarineRecord.Slot);
                var voyages = submarineRecord.Voyages
                    .Select(v => new Voyage(VoyageId.Create(subId, v.VoyageGuid == Guid.Empty ? Guid.NewGuid() : v.VoyageGuid), v.RouteId, v.Departure, v.Arrival, v.Status))
                    .Cast<Voyage>()
                    .ToList();
                submarines.Add(new Submarine(subId, submarineRecord.Name, submarineRecord.ProfileId, voyages));
            }

            results.Add(new AcquisitionSnapshot(
                DateTime.UtcNow,
                submarines,
                AcquisitionSourceKind.Unknown,
                cid,
                character.CharacterName,
                character.WorldName,
                SnapshotConfidence.Direct));
        }

        return results;
    }

    public async ValueTask HydrateAsync(ISubmarineRepository submarineRepository, IVoyageRepository voyageRepository, CancellationToken cancellationToken = default)
    {
        var snapshots = this.LoadSnapshots();
        foreach (var snapshot in snapshots)
        {
            foreach (var submarine in snapshot.Submarines)
            {
                await submarineRepository.SaveAsync(submarine, cancellationToken).ConfigureAwait(false);
                foreach (var voyage in submarine.Voyages)
                {
                    await voyageRepository.SaveAsync(voyage, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }

    public void UpdateFromSnapshot(AcquisitionSnapshot snapshot)
    {
        lock (this.sync)
        {
            var data = this.envelope.Data;
            if (!data.Characters.TryGetValue(snapshot.CharacterId, out var characterRecord))
            {
                characterRecord = new CharacterSnapshotRecord
                {
                    CharacterId = snapshot.CharacterId,
                };
                data.Characters[snapshot.CharacterId] = characterRecord;
            }

            if (!string.IsNullOrWhiteSpace(snapshot.CharacterName))
            {
                characterRecord.CharacterName = snapshot.CharacterName;
            }

            if (!string.IsNullOrWhiteSpace(snapshot.WorldName))
            {
                characterRecord.WorldName = snapshot.WorldName;
            }

            foreach (var submarine in snapshot.Submarines)
            {
                var slot = submarine.Id.Slot;
                characterRecord.Submarines[slot] = new SubmarineRecord
                {
                    Slot = slot,
                    Name = submarine.Name,
                    ProfileId = submarine.ProfileId,
                    Voyages = submarine.Voyages.Select(v => new VoyageRecord
                    {
                        VoyageGuid = v.Id.Value,
                        RouteId = v.RouteId,
                        Departure = v.Departure,
                        Arrival = v.Arrival,
                        Status = v.Status,
                    }).ToList(),
                };
            }

            this.envelope.Version = SnapshotStorageModel.CurrentVersion;
            this.settings.SaveAsync(this.envelope).GetAwaiter().GetResult();
        }
    }
}
