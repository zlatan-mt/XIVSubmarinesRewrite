// apps/XIVSubmarinesRewrite/src/Acquisition/CharacterSnapshotAggregator.cs
// キャラクター単位でスナップショット候補を集約し、マージ処理を適用します
// データソース間の差異を吸収し、差分検知の安定性を保つために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Acquisition/DataAcquisitionGateway.cs, apps/XIVSubmarinesRewrite/src/Acquisition/SnapshotDiffer.cs

namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Aggregates snapshot candidates per character and applies merge policies.</summary>
public sealed class CharacterSnapshotAggregator
{
    private readonly Dictionary<ulong, AggregatedState> states = new ();
    private readonly ILogSink log;

    public CharacterSnapshotAggregator(ILogSink? log = null)
    {
        this.log = log ?? new NullLogSink();
    }

    public AcquisitionSnapshot Integrate(AcquisitionSnapshot candidate)
    {
        if (candidate.CharacterId == 0)
        {
            throw new InvalidOperationException("Candidate snapshot must include CharacterId.");
        }

        if (!this.states.TryGetValue(candidate.CharacterId, out var state))
        {
            state = new AggregatedState(candidate.CharacterId, this.log);
            this.states.Add(candidate.CharacterId, state);
        }

        state.Update(candidate);
        return state.ToSnapshot();
    }

    private sealed class AggregatedState
    {
        private readonly ulong characterId;
        private readonly Dictionary<SubmarineId, Submarine> submarines = new ();
        private readonly ILogSink log;
        private string? name;
        private string? world;
        private DateTime lastUpdated;
        private AcquisitionSourceKind lastSource = AcquisitionSourceKind.Unknown;
        private bool sawMemory;
        private bool sawUi;

        public AggregatedState(ulong characterId, ILogSink log)
        {
            this.characterId = characterId;
            this.log = log;
        }

        public void Update(AcquisitionSnapshot snapshot)
        {
            this.name ??= snapshot.CharacterName;
            this.world ??= snapshot.WorldName;
            this.lastUpdated = snapshot.Timestamp;
            this.lastSource = snapshot.Source;

            switch (snapshot.Source)
            {
                case AcquisitionSourceKind.Memory:
                    this.sawMemory = true;
                    break;
                case AcquisitionSourceKind.Ui:
                    this.sawUi = true;
                    break;
                case AcquisitionSourceKind.Composite:
                    this.sawMemory = true;
                    this.sawUi = true;
                    break;
            }

            foreach (var submarine in snapshot.Submarines)
            {
                var normalized = NormalizeSubmarine(this.characterId, submarine);
                this.UpsertSubmarine(normalized);
            }
        }

        public AcquisitionSnapshot ToSnapshot()
        {
            var current = new List<Submarine>(this.submarines.Values);
            var confidence = this.sawMemory && this.sawUi ? SnapshotConfidence.Merged : SnapshotConfidence.Direct;
            return new AcquisitionSnapshot(
                this.lastUpdated,
                current,
                this.lastSource,
                this.characterId,
                this.name,
                this.world,
                confidence);
        }

        private void UpsertSubmarine(Submarine incoming)
        {
            this.LogUpsertEntry(incoming);
            try
            {
                if (this.submarines.TryGetValue(incoming.Id, out var existing))
                {
                    this.submarines[incoming.Id] = MergeSubmarine(existing, incoming);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(incoming.Name))
                {
                    foreach (var kvp in this.submarines)
                    {
                        if (string.Equals(kvp.Value.Name, incoming.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            var targetId = incoming.Id.IsPending ? kvp.Key : incoming.Id;
                            var merged = MergeSubmarine(kvp.Value, incoming with { Id = targetId });
                            this.submarines.Remove(kvp.Key);
                            this.submarines[targetId] = merged with { Id = targetId };
                            return;
                        }
                    }
                }

                this.submarines[incoming.Id] = incoming;
            }
            finally
            {
                this.LogSubmarineState();
            }
        }

        private void LogUpsertEntry(Submarine incoming)
        {
            var name = string.IsNullOrWhiteSpace(incoming.Name) ? "<unknown>" : incoming.Name;
            this.log.Log(LogLevel.Trace, $"[Acquisition] UpsertSubmarine incoming char={this.characterId} id={incoming.Id} pending={incoming.Id.IsPending} name={name} source={this.lastSource} currentCount={this.submarines.Count}");
        }

        private void LogSubmarineState()
        {
            var entries = this.submarines.Count == 0
                ? "<empty>"
                : string.Join(", ", this.submarines.Select(kvp => $"{kvp.Key}:{kvp.Value.Name ?? "<unknown>"}"));
            this.log.Log(LogLevel.Trace, $"[Acquisition] UpsertSubmarine state char={this.characterId} total={this.submarines.Count} entries=[{entries}] source={this.lastSource}");
        }

        private static Submarine MergeSubmarine(Submarine baseline, Submarine incoming)
        {
            var name = !string.IsNullOrWhiteSpace(incoming.Name) ? incoming.Name : baseline.Name;
            var profile = !string.IsNullOrWhiteSpace(incoming.ProfileId) ? incoming.ProfileId : baseline.ProfileId;
            var voyages = MergeVoyages(baseline.Voyages, incoming.Voyages);
            return baseline with { Name = name, ProfileId = profile, Voyages = voyages };
        }

        private static Submarine NormalizeSubmarine(ulong characterId, Submarine submarine)
        {
            var id = submarine.Id;
            if (id.CharacterId == 0 || id.CharacterId != characterId)
            {
                id = new SubmarineId(characterId, id.Slot);
            }

            return submarine with { Id = id };
        }

        private static IReadOnlyList<Voyage> MergeVoyages(IReadOnlyList<Voyage> baseline, IReadOnlyList<Voyage> incoming)
        {
            if (incoming.Count == 0)
            {
                return baseline;
            }

            if (baseline.Count == 0)
            {
                return incoming;
            }

            var maxCount = Math.Max(baseline.Count, incoming.Count);
            var voyages = new List<Voyage>(maxCount);

            for (var i = 0; i < maxCount; i++)
            {
                var baseVoyage = i < baseline.Count ? baseline[i] : null;
                var incomingVoyage = i < incoming.Count ? incoming[i] : null;

                if (baseVoyage is null && incomingVoyage is not null)
                {
                    voyages.Add(incomingVoyage);
                    continue;
                }

                if (baseVoyage is not null && incomingVoyage is null)
                {
                    voyages.Add(baseVoyage);
                    continue;
                }

                if (baseVoyage is null || incomingVoyage is null)
                {
                    continue;
                }

                var route = !string.IsNullOrWhiteSpace(incomingVoyage.RouteId) ? incomingVoyage.RouteId : baseVoyage.RouteId;
                var arrivalChanged = incomingVoyage.Arrival.HasValue && incomingVoyage.Arrival != baseVoyage.Arrival;
                if (arrivalChanged && string.IsNullOrWhiteSpace(incomingVoyage.RouteId))
                {
                    route = string.Empty;
                }
                var arrival = incomingVoyage.Arrival ?? baseVoyage.Arrival;
                var status = incomingVoyage.Status != VoyageStatus.Unknown ? incomingVoyage.Status : baseVoyage.Status;
                var departure = incomingVoyage.Departure != default ? incomingVoyage.Departure : baseVoyage.Departure;
                var voyageGuid = incomingVoyage.Id.Value != Guid.Empty ? incomingVoyage.Id.Value : baseVoyage.Id.Value;
                var subId = incomingVoyage.Id.SubmarineId.IsPending ? baseVoyage.Id.SubmarineId : incomingVoyage.Id.SubmarineId;
                voyages.Add(new Voyage(VoyageId.Create(subId, voyageGuid), route, departure, arrival, status));
            }

            return voyages;
        }
    }
}
