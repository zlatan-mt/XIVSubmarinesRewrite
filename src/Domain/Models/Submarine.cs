namespace XIVSubmarinesRewrite.Domain.Models;

using System.Collections.Generic;

/// <summary>Represents a single company submarine and its known voyages.</summary>
public sealed record Submarine(SubmarineId Id, string Name, string ProfileId, IReadOnlyList<Voyage> Voyages);
