namespace XIVSubmarinesRewrite.Domain.Models;

using System;
using System.Collections.Generic;

/// <summary>Describes a planned or historical submarine route.</summary>
public sealed record Route(string RouteId, string Name, IReadOnlyList<RouteSegment> Segments);

public sealed record RouteSegment(int Index, string DestinationName, TimeSpan TravelTime);
