namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Determines whether a new snapshot is materially different from the previous one.</summary>
public sealed class SnapshotDiffer
{
    public bool HasMeaningfulChange(AcquisitionSnapshot? previous, AcquisitionSnapshot next)
    {
        if (previous is null)
        {
            return true;
        }

        if (previous.Submarines.Count != next.Submarines.Count)
        {
            return true;
        }

        var previousLookup = previous.Submarines.ToDictionary(s => s.Id);
        foreach (var submarine in next.Submarines)
        {
            if (!previousLookup.TryGetValue(submarine.Id, out var oldSubmarine))
            {
                return true;
            }

            if (!string.Equals(oldSubmarine.Name, submarine.Name, StringComparison.Ordinal))
            {
                return true;
            }

            if (!string.Equals(oldSubmarine.ProfileId, submarine.ProfileId, StringComparison.Ordinal))
            {
                return true;
            }

            if (HasVoyageChanges(oldSubmarine.Voyages, submarine.Voyages))
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasVoyageChanges(IReadOnlyList<Voyage> previous, IReadOnlyList<Voyage> next)
    {
        if (previous.Count != next.Count)
        {
            return true;
        }

        for (var i = 0; i < next.Count; i++)
        {
            var oldVoyage = previous[i];
            var newVoyage = next[i];
            if (oldVoyage.Id != newVoyage.Id)
            {
                return true;
            }

            if (!string.Equals(oldVoyage.RouteId, newVoyage.RouteId, StringComparison.Ordinal))
            {
                return true;
            }

            if (oldVoyage.Departure != newVoyage.Departure || oldVoyage.Arrival != newVoyage.Arrival)
            {
                return true;
            }

            if (oldVoyage.Status != newVoyage.Status)
            {
                return true;
            }
        }

        return false;
    }
}
