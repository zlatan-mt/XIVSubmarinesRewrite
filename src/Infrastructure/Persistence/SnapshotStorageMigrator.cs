namespace XIVSubmarinesRewrite.Infrastructure.Persistence;

using System;
using System.Globalization;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Ensures snapshot storage is migrated to the latest schema.</summary>
public sealed class SnapshotStorageMigrator
{
    private readonly ISettingsProvider settings;
    private readonly ILogSink log;

    public SnapshotStorageMigrator(ISettingsProvider settings, ILogSink log)
    {
        this.settings = settings;
        this.log = log;
    }

    public SnapshotStorageEnvelope Load()
    {
        var envelope = this.settings.Get<SnapshotStorageEnvelope>();
        if (envelope.Version == SnapshotStorageModel.CurrentVersion)
        {
            return envelope;
        }

        if (envelope.Version == 0)
        {
            envelope = MigrateFromLegacy(envelope);
            this.settings.SaveAsync(envelope).GetAwaiter().GetResult();
            return envelope;
        }

        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported snapshot storage version: {0}", envelope.Version));
    }

    private SnapshotStorageEnvelope MigrateFromLegacy(SnapshotStorageEnvelope envelope)
    {
        if (envelope.LegacyData is null)
        {
            this.log.Log(LogLevel.Warning, "[Migration] No legacy data found; initializing empty snapshot storage.");
            envelope.Data = new SnapshotStorageModel();
            envelope.Version = SnapshotStorageModel.CurrentVersion;
            return envelope;
        }

        var model = new SnapshotStorageModel();
        var legacy = envelope.LegacyData;
        var record = new CharacterSnapshotRecord
        {
            CharacterId = 0,
        };
        model.Characters[0] = record;

        byte slot = 1;
        foreach (var legacySub in legacy.Submarines)
        {
            var subRecord = new SubmarineRecord
            {
                Slot = slot,
                Name = legacySub.Name,
                ProfileId = legacySub.ProfileId,
            };

            foreach (var legacyVoyage in legacySub.Voyages)
            {
                var voyageId = legacyVoyage.VoyageId == Guid.Empty ? Guid.NewGuid() : legacyVoyage.VoyageId;
                subRecord.Voyages.Add(new VoyageRecord
                {
                    VoyageGuid = voyageId,
                    RouteId = legacyVoyage.RouteId,
                    Departure = legacyVoyage.Departure,
                    Arrival = legacyVoyage.Arrival,
                    Status = legacyVoyage.Status,
                });
            }

            record.Submarines[subRecord.Slot] = subRecord;
            if (slot < byte.MaxValue - 1)
            {
                slot++;
            }
        }

        envelope.Data = model;
        envelope.LegacyData = null;
        envelope.Version = SnapshotStorageModel.CurrentVersion;
        this.log.Log(LogLevel.Information, $"[Migration] Snapshot storage migrated to version {SnapshotStorageModel.CurrentVersion}.");
        return envelope;
    }
}
