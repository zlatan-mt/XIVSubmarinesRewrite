namespace XIVSubmarinesRewrite.Acquisition;

using System;

/// <summary>Records acquisition metrics to drive adaptive sampling decisions.</summary>
public interface IAcquisitionTelemetry
{
    void RecordSuccess(TimeSpan duration, AcquisitionSourceKind source);
    void RecordFailure(Exception exception);
    void RecordSkip(AcquisitionSourceKind source);
    AcquisitionTelemetrySnapshot Snapshot { get; }
}

public sealed record AcquisitionTelemetrySnapshot(
    int SuccessCount,
    int FailureCount,
    TimeSpan AverageDuration,
    int MemorySuccessCount,
    int UiSuccessCount,
    int SkipCount,
    int MemorySkipCount,
    int UiSkipCount);
