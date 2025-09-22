namespace XIVSubmarinesRewrite.Acquisition;

using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>Caches the latest snapshot per character and notifies observers on change.</summary>
public sealed class SnapshotCache
{
    private readonly ReaderWriterLockSlim sync = new ();
    private readonly Dictionary<ulong, AcquisitionSnapshot> snapshots = new ();

    public event EventHandler<SnapshotUpdatedEventArgs>? SnapshotUpdated;

    public AcquisitionSnapshot? GetSnapshot(ulong characterId)
    {
        this.sync.EnterReadLock();
        try
        {
            return this.snapshots.TryGetValue(characterId, out var snapshot) ? snapshot : null;
        }
        finally
        {
            this.sync.ExitReadLock();
        }
    }

    public IReadOnlyDictionary<ulong, AcquisitionSnapshot> GetAll()
    {
        this.sync.EnterReadLock();
        try
        {
            return new Dictionary<ulong, AcquisitionSnapshot>(this.snapshots);
        }
        finally
        {
            this.sync.ExitReadLock();
        }
    }

    public void Update(AcquisitionSnapshot snapshot, ulong characterId)
    {
        this.sync.EnterWriteLock();
        try
        {
            this.snapshots[characterId] = snapshot;
        }
        finally
        {
            this.sync.ExitWriteLock();
        }

        this.SnapshotUpdated?.Invoke(this, new SnapshotUpdatedEventArgs(characterId, snapshot));
    }
}

public sealed class SnapshotUpdatedEventArgs : EventArgs
{
    public SnapshotUpdatedEventArgs(ulong characterId, AcquisitionSnapshot snapshot)
    {
        this.CharacterId = characterId;
        this.Snapshot = snapshot;
    }

    public ulong CharacterId { get; }

    public AcquisitionSnapshot Snapshot { get; }
}
