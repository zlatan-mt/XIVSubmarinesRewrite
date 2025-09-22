namespace XIVSubmarinesRewrite.Presentation.ViewModels;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Supplies ImGui overview tab with current submarine summaries.</summary>
public interface IOverviewViewModel
{
    IReadOnlyList<SubmarineOverviewEntry> Submarines { get; }
    System.DateTime? LastUpdatedUtc { get; }
    ValueTask RefreshAsync(ulong characterId, CancellationToken cancellationToken = default);
}

public sealed record SubmarineOverviewEntry(
    SubmarineId SubmarineId,
    ulong CharacterId,
    string Name,
    string ProfileId,
    string? RouteId,
    VoyageStatus Status,
    System.DateTime? Departure,
    System.DateTime? Arrival,
    System.TimeSpan? Remaining
);
