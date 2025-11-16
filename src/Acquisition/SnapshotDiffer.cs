// apps/XIVSubmarinesRewrite/src/Acquisition/SnapshotDiffer.cs
// 2つのスナップショット間で意味のある変化を検出します
// 無駄な永続化や通知を避けるためのフィルタ係として存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Acquisition/DataAcquisitionGateway.cs, apps/XIVSubmarinesRewrite/src/Acquisition/CharacterSnapshotAggregator.cs

namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Determines whether a new snapshot is materially different from the previous one.</summary>
public sealed class SnapshotDiffer
{
    public bool HasMeaningfulChange(AcquisitionSnapshot? previous, AcquisitionSnapshot next)
        => this.TryDescribeChange(previous, next, out _);

    public bool TryDescribeChange(AcquisitionSnapshot? previous, AcquisitionSnapshot next, out string reason)
    {
        if (previous is null)
        {
            reason = "no-previous";
            return true;
        }

        if (previous.Submarines.Count != next.Submarines.Count)
        {
            reason = "submarine-count";
            return true;
        }

        var previousLookup = previous.Submarines.ToDictionary(s => s.Id);
        foreach (var submarine in next.Submarines)
        {
            if (!previousLookup.TryGetValue(submarine.Id, out var oldSubmarine))
            {
                reason = "submarine-missing";
                return true;
            }

            if (!string.Equals(oldSubmarine.Name, submarine.Name, StringComparison.Ordinal))
            {
                reason = "submarine-name";
                return true;
            }

            if (!string.Equals(oldSubmarine.ProfileId, submarine.ProfileId, StringComparison.Ordinal))
            {
                reason = "submarine-profile";
                return true;
            }

            var voyageReason = DescribeVoyageChange(oldSubmarine.Voyages, submarine.Voyages);
            if (voyageReason is not null)
            {
                reason = voyageReason;
                return true;
            }
        }

        reason = "unchanged";
        return false;
    }

    private static string? DescribeVoyageChange(IReadOnlyList<Voyage> previous, IReadOnlyList<Voyage> next)
    {
        if (previous.Count != next.Count)
        {
            return "voyage-count";
        }

        for (var i = 0; i < next.Count; i++)
        {
            var oldVoyage = previous[i];
            var newVoyage = next[i];
            if (oldVoyage.Id != newVoyage.Id)
            {
                return "voyage-id";
            }

            if (!string.Equals(oldVoyage.RouteId, newVoyage.RouteId, StringComparison.Ordinal))
            {
                return "voyage-route";
            }

            if (oldVoyage.Departure != newVoyage.Departure || oldVoyage.Arrival != newVoyage.Arrival)
            {
                return "voyage-timing";
            }

            if (oldVoyage.Status != newVoyage.Status)
            {
                return "voyage-status";
            }
        }

        return null;
    }
}
