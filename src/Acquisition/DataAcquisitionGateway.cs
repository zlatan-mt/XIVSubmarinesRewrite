namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Services;

/// <summary>Coordinates data sources, aggregates snapshots per character, and handles persistence/telemetry.</summary>
public sealed class DataAcquisitionGateway
{
    private readonly IReadOnlyList<IDataSource> dataSources;
    private readonly SnapshotCache cache;
    private readonly IAcquisitionTelemetry telemetry;
    private readonly SnapshotPersister persister;
    private readonly CharacterSnapshotAggregator aggregator;
    private readonly SnapshotDiffer differ;
    private readonly ICharacterRegistry characterRegistry;

    public DataAcquisitionGateway(
        IEnumerable<IDataSource> dataSources,
        SnapshotCache cache,
        IAcquisitionTelemetry telemetry,
        SnapshotPersister persister,
        CharacterSnapshotAggregator aggregator,
        SnapshotDiffer differ,
        ICharacterRegistry characterRegistry)
    {
        this.dataSources = dataSources.ToList();
        this.cache = cache;
        this.telemetry = telemetry;
        this.persister = persister;
        this.aggregator = aggregator;
        this.differ = differ;
        this.characterRegistry = characterRegistry;
    }

    public async ValueTask<AcquisitionSnapshot?> RefreshAsync(CancellationToken cancellationToken = default)
    {
        foreach (var source in this.dataSources)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var candidate = await source.TryAcquireAsync(cancellationToken).ConfigureAwait(false);
                if (candidate is null)
                {
                    continue;
                }

                if (candidate.CharacterId == 0)
                {
                    continue;
                }

                stopwatch.Stop();
                this.telemetry.RecordSuccess(stopwatch.Elapsed, candidate.Source);

                var processed = await this.ProcessCandidateAsync(candidate, cancellationToken).ConfigureAwait(false);
                if (processed is not null)
                {
                    return processed;
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                this.telemetry.RecordFailure(ex);
            }
        }

        return null;
    }

    private async ValueTask<AcquisitionSnapshot?> ProcessCandidateAsync(AcquisitionSnapshot candidate, CancellationToken cancellationToken)
    {
        var snapshot = this.aggregator.Integrate(candidate);
        var previous = this.cache.GetSnapshot(snapshot.CharacterId);
        if (!this.differ.HasMeaningfulChange(previous, snapshot))
        {
            this.telemetry.RecordSkip(candidate.Source);
            return previous;
        }

        await this.persister.PersistAsync(snapshot, cancellationToken).ConfigureAwait(false);
        this.cache.Update(snapshot, snapshot.CharacterId);
        this.characterRegistry.RegisterSnapshot(snapshot);
        return snapshot;
    }
}
