namespace XIVSubmarinesRewrite.Presentation.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;
using XIVSubmarinesRewrite.Application.Services;

/// <summary>Aggregates submarine information for the overview tab.</summary>
public sealed class OverviewViewModel : IOverviewViewModel
{
    private readonly ISubmarineRepository submarineRepository;
    private readonly ICharacterRegistry characterRegistry;
    private readonly object sync = new();
    private IReadOnlyList<SubmarineOverviewEntry> submarines = Array.Empty<SubmarineOverviewEntry>();
    private DateTime? lastUpdatedUtc;

    public OverviewViewModel(ISubmarineRepository submarineRepository, ICharacterRegistry characterRegistry)
    {
        this.submarineRepository = submarineRepository;
        this.characterRegistry = characterRegistry;
    }

    public IReadOnlyList<SubmarineOverviewEntry> Submarines
    {
        get
        {
            lock (this.sync)
            {
                return this.submarines;
            }
        }
    }

    public DateTime? LastUpdatedUtc
    {
        get
        {
            lock (this.sync)
            {
                return this.lastUpdatedUtc;
            }
        }
    }

    public async ValueTask RefreshAsync(ulong characterId, CancellationToken cancellationToken = default)
    {
        var list = new List<SubmarineOverviewEntry>();
        await foreach (var submarine in this.submarineRepository.ListAsync(characterId, cancellationToken))
        {
            var voyage = submarine.Voyages.LastOrDefault();
            var routeId = voyage?.RouteId;
            var departure = voyage?.Departure;
            var arrival = voyage?.Arrival;
            var status = voyage?.Status ?? VoyageStatus.Unknown;
            TimeSpan? remaining = null;
            if (arrival.HasValue)
            {
                var diff = arrival.Value - DateTime.UtcNow;
                remaining = diff < TimeSpan.Zero ? TimeSpan.Zero : diff;
            }

            list.Add(new SubmarineOverviewEntry(
                submarine.Id,
                submarine.Id.CharacterId,
                submarine.Name,
                submarine.ProfileId,
                routeId,
                status,
                departure,
                arrival,
                remaining));
        }

        lock (this.sync)
        {
            this.submarines = list;
            this.lastUpdatedUtc = this.characterRegistry.GetLastUpdatedUtc(characterId);
        }
    }
}
