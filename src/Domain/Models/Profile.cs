namespace XIVSubmarinesRewrite.Domain.Models;

using System.Collections.Generic;

/// <summary>Represents operator preferences and thresholds applied across submarines.</summary>
public sealed record Profile(string ProfileId, string Name, IReadOnlyDictionary<string, string> Preferences);
