namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Represents the latest submarine-related data captured from the game client.</summary>
public sealed record AcquisitionSnapshot(
    DateTime Timestamp,
    IReadOnlyList<Submarine> Submarines,
    AcquisitionSourceKind Source,
    ulong CharacterId,
    string? CharacterName,
    string? WorldName,
    SnapshotConfidence Confidence);

public enum AcquisitionSourceKind
{
    Unknown = 0,
    Memory = 1,
    Ui = 2,
    Composite = 3,
}

public enum SnapshotConfidence
{
    Unknown = 0,
    Direct = 1,
    Merged = 2,
}
