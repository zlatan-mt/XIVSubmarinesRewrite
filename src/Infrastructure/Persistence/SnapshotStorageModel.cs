namespace XIVSubmarinesRewrite.Infrastructure.Persistence;

using System;
using System.Collections.Generic;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Current storage model for persisted snapshots.</summary>
public sealed class SnapshotStorageModel
{
    public const int CurrentVersion = 1;

    public Dictionary<ulong, CharacterSnapshotRecord> Characters { get; set; } = new();
}

public sealed class CharacterSnapshotRecord
{
    public ulong CharacterId { get; set; }
    public string? CharacterName { get; set; }
    public string? WorldName { get; set; }
    public Dictionary<byte, SubmarineRecord> Submarines { get; set; } = new();
}

public sealed class SubmarineRecord
{
    public byte Slot { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProfileId { get; set; } = string.Empty;
    public List<VoyageRecord> Voyages { get; set; } = new();
}

public sealed class VoyageRecord
{
    public Guid VoyageGuid { get; set; }
    public string RouteId { get; set; } = string.Empty;
    public DateTime Departure { get; set; }
    public DateTime? Arrival { get; set; }
    public VoyageStatus Status { get; set; }
}

public sealed class SnapshotStorageEnvelope
{
    public int Version { get; set; } = SnapshotStorageModel.CurrentVersion;
    public SnapshotStorageModel Data { get; set; } = new ();
    public LegacySnapshotStorageModel? LegacyData { get; set; }
}

public sealed class LegacySnapshotStorageModel
{
    public List<LegacySubmarineRecord> Submarines { get; set; } = new();
}

public sealed class LegacySubmarineRecord
{
    public string Identifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ProfileId { get; set; } = string.Empty;
    public List<LegacyVoyageRecord> Voyages { get; set; } = new();
}

public sealed class LegacyVoyageRecord
{
    public Guid VoyageId { get; set; }
    public string RouteId { get; set; } = string.Empty;
    public DateTime Departure { get; set; }
    public DateTime? Arrival { get; set; }
    public VoyageStatus Status { get; set; }
}
