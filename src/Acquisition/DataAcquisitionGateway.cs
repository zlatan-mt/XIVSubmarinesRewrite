// apps/XIVSubmarinesRewrite/src/Acquisition/DataAcquisitionGateway.cs
// 複数データソースからスナップショットを収集し、集約・永続化を調停します
// 差分の有無やテレメトリ記録を集中管理し、不要な処理を抑制するために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Acquisition/CharacterSnapshotAggregator.cs, apps/XIVSubmarinesRewrite/src/Acquisition/SnapshotDiffer.cs

namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Infrastructure.Logging;

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
    private readonly ILogSink log;

    public DataAcquisitionGateway(
        IEnumerable<IDataSource> dataSources,
        SnapshotCache cache,
        IAcquisitionTelemetry telemetry,
        SnapshotPersister persister,
        CharacterSnapshotAggregator aggregator,
        SnapshotDiffer differ,
        ICharacterRegistry characterRegistry,
        ILogSink log)
    {
        this.dataSources = dataSources.ToList();
        this.cache = cache;
        this.telemetry = telemetry;
        this.persister = persister;
        this.aggregator = aggregator;
        this.differ = differ;
        this.characterRegistry = characterRegistry;
        this.log = log;
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
        var changed = this.differ.TryDescribeChange(previous, snapshot, out var reason);
        var previousCount = previous?.Submarines.Count ?? 0;
        var currentCount = snapshot.Submarines.Count;
        this.log.Log(LogLevel.Trace, $"[Acquisition] SnapshotDiffer result char={snapshot.CharacterId} changed={changed} reason={reason} previousCount={previousCount} currentCount={currentCount}");
        if (!changed)
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
