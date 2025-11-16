// apps/XIVSubmarinesRewrite/src/Infrastructure/Acquisition/DalamudMemorySubmarineSnapshotSource.cs
// Dalamudのメモリから潜水艦スナップショットを読み出します
// メモリソース特有のノイズを抑えて安定したスナップショットを渡すために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Acquisition/DataAcquisitionGateway.cs, apps/XIVSubmarinesRewrite/src/Acquisition/CharacterSnapshotAggregator.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Reads submarine data directly from Housing workshop memory via Dalamud.</summary>
public sealed unsafe class DalamudMemorySubmarineSnapshotSource : IMemorySubmarineSnapshotSource
{
    private const int SubmersiblePointerOffset = 0x8C80;
    private const int NameOffset = 0x22;
    private const int RegisterOffset = 0x10;
    private const int ReturnOffset = 0x14;
    private const int RankOffset = 0x0E;
    private const int ExplorationPointsOffset = 0x42;
    private const int ExplorationPointsCount = 5;
    private static readonly TimeSpan NameJitterSuppressionWindow = TimeSpan.FromMilliseconds(750);

    private readonly IClientState clientState;
    private readonly ILogSink log;
    private readonly Dictionary<SubmarineId, NameSample> lastNameSamples = new ();
    private readonly object sampleGate = new ();

    public DalamudMemorySubmarineSnapshotSource(IClientState clientState, ILogSink log)
    {
        this.clientState = clientState;
        this.log = log;
    }

    public ValueTask<IReadOnlyList<Submarine>?> TryReadAsync(CancellationToken cancellationToken = default)
    {
        if (!this.clientState.IsLoggedIn || this.clientState.LocalPlayer is null)
        {
            return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
        }

        try
        {
            var characterId = this.clientState.LocalContentId;
            if (characterId == 0)
            {
                this.log.Log(LogLevel.Debug, "[Memory] LocalContentId unavailable; skipping memory snapshot.");
                return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
            }

            var manager = HousingManager.Instance();
            if (manager == null)
            {
                return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
            }

            var workshop = manager->WorkshopTerritory;
            if (workshop == null)
            {
                return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
            }

            var results = new List<Submarine>();
            var submersible = &workshop->Submersible;
            var pointerBase = (nint*)((byte*)submersible + SubmersiblePointerOffset);

            for (var i = 0; i < 4; i++) // FFXIVの潜水艦は最大4隻（スロット0-3）
            {
                cancellationToken.ThrowIfCancellationRequested();
                var vesselPtr = (HousingWorkshopSubmersibleSubData*)pointerBase[i];
                if (vesselPtr == null)
                {
                    continue;
                }

                var name = ReadName(vesselPtr);
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                var registerSeconds = *(uint*)((byte*)vesselPtr + RegisterOffset);
                var returnSeconds = *(uint*)((byte*)vesselPtr + ReturnOffset);
                var rank = *((byte*)vesselPtr + RankOffset);

                var departure = DateTime.UnixEpoch.AddSeconds(registerSeconds);
                DateTime? arrival = returnSeconds == 0 ? null : DateTime.UnixEpoch.AddSeconds(returnSeconds);
                var status = DetermineStatus(arrival);

                var routeId = ExtractRouteIdentifier((byte*)vesselPtr + ExplorationPointsOffset);
                // iは0-3の範囲なので、slotはそのまま使用可能
                var slot = (byte)i;
                var submarineId = new SubmarineId(characterId, slot);
                name = this.ApplyNameDebounce(submarineId, name, registerSeconds, returnSeconds);
                var voyageGuid = ComputeVoyageId(name, registerSeconds, returnSeconds, characterId, slot);
                var voyageId = VoyageId.Create(submarineId, voyageGuid);
                var voyage = new Voyage(voyageId, routeId ?? string.Empty, departure, arrival, status);

                var profileId = $"Rank{rank:D2}";
                var submarine = new Submarine(submarineId, name, profileId, new[] { voyage });
                results.Add(submarine);
            }

            return results.Count == 0
                ? ValueTask.FromResult<IReadOnlyList<Submarine>?>(null)
                : ValueTask.FromResult<IReadOnlyList<Submarine>?>(results);
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Error, "Failed to read submarine data from memory.", ex);
            return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
        }
    }

    private static VoyageStatus DetermineStatus(DateTime? arrival)
    {
        if (!arrival.HasValue)
        {
            return VoyageStatus.Completed;
        }

        return DateTime.UtcNow < arrival.Value ? VoyageStatus.Underway : VoyageStatus.Completed;
    }

    private static string ReadName(HousingWorkshopSubmersibleSubData* vessel)
    {
        var span = new ReadOnlySpan<byte>((byte*)vessel + NameOffset, 20);
        var terminator = span.IndexOf((byte)0);
        if (terminator >= 0)
        {
            span = span[..terminator];
        }

        return span.IsEmpty ? string.Empty : Encoding.UTF8.GetString(span);
    }

    private static string? ExtractRouteIdentifier(byte* start)
    {
        var span = new ReadOnlySpan<byte>(start, ExplorationPointsCount);
        var segments = new List<string>(ExplorationPointsCount);
        foreach (var point in span)
        {
            if (point == 0)
            {
                continue;
            }

            segments.Add(point.ToString(CultureInfo.InvariantCulture));
        }

        if (segments.Count == 0)
        {
            return null;
        }

        return string.Join("-", segments);
    }

    private static Guid ComputeVoyageId(string name, uint registerSeconds, uint returnSeconds, ulong characterId, byte slot)
    {
        Span<byte> data = stackalloc byte[sizeof(uint) * 2];
        BitConverter.TryWriteBytes(data[..sizeof(uint)], registerSeconds);
        BitConverter.TryWriteBytes(data[sizeof(uint)..], returnSeconds);
        var nameBytes = Encoding.UTF8.GetBytes(name ?? string.Empty);
        var length = Math.Min(nameBytes.Length, 32);
        Span<byte> combined = stackalloc byte[data.Length + sizeof(ulong) + sizeof(byte) + length];
        data.CopyTo(combined);
        BitConverter.TryWriteBytes(combined.Slice(data.Length, sizeof(ulong)), characterId);
        combined[data.Length + sizeof(ulong)] = slot;
        if (length > 0)
        {
            nameBytes.AsSpan(0, length).CopyTo(combined.Slice(data.Length + sizeof(ulong) + sizeof(byte)));
        }
        var hash = MD5.HashData(combined);
        return new Guid(hash);
    }

    private string ApplyNameDebounce(SubmarineId submarineId, string name, uint registerSeconds, uint returnSeconds)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return name;
        }

        var now = DateTime.UtcNow;
        lock (this.sampleGate)
        {
            if (this.lastNameSamples.TryGetValue(submarineId, out var sample))
            {
                var isSameVoyage = sample.RegisterSeconds == registerSeconds && sample.ReturnSeconds == returnSeconds;
                var withinWindow = (now - sample.TimestampUtc) <= NameJitterSuppressionWindow;
                if (isSameVoyage && withinWindow && !string.Equals(sample.Name, name, StringComparison.Ordinal))
                {
                    this.log.Log(LogLevel.Trace,
                        $"[Memory] Suppressed jitter for {submarineId}: keeping name={sample.Name} newName={name} register={registerSeconds} return={returnSeconds}");
                    return sample.Name;
                }
            }

            this.lastNameSamples[submarineId] = new NameSample(name, registerSeconds, returnSeconds, now);
        }

        return name;
    }

    private readonly record struct NameSample(string Name, uint RegisterSeconds, uint ReturnSeconds, DateTime TimestampUtc);
}
