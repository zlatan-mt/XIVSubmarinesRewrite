namespace XIVSubmarinesRewrite.Infrastructure.Diagnostics;

using System;
using System.Threading;
using XIVSubmarinesRewrite.Acquisition;

/// <summary>Thread-safe telemetry aggregator for acquisition performance.</summary>
public sealed class DefaultAcquisitionTelemetry : IAcquisitionTelemetry
{
    private long successCount;
    private long failureCount;
    private long totalTicks;
    private long memorySuccessCount;
    private long uiSuccessCount;
    private long skipCount;
    private long memorySkipCount;
    private long uiSkipCount;

    public AcquisitionTelemetrySnapshot Snapshot
    {
        get
        {
            var successes = Interlocked.Read(ref this.successCount);
            var failures = Interlocked.Read(ref this.failureCount);
            var ticks = Interlocked.Read(ref this.totalTicks);
            var totalEvents = Math.Max(successes, 1);
            var average = successes == 0 ? TimeSpan.Zero : TimeSpan.FromTicks(ticks / totalEvents);
            var memory = Interlocked.Read(ref this.memorySuccessCount);
            var ui = Interlocked.Read(ref this.uiSuccessCount);
            var skips = Interlocked.Read(ref this.skipCount);
            var memorySkips = Interlocked.Read(ref this.memorySkipCount);
            var uiSkips = Interlocked.Read(ref this.uiSkipCount);
            return new AcquisitionTelemetrySnapshot((int)successes, (int)failures, average, (int)memory, (int)ui, (int)skips, (int)memorySkips, (int)uiSkips);
        }
    }

    public void RecordFailure(Exception exception)
    {
        _ = exception;
        Interlocked.Increment(ref this.failureCount);
    }

    public void RecordSuccess(TimeSpan duration, AcquisitionSourceKind source)
    {
        Interlocked.Increment(ref this.successCount);
        Interlocked.Add(ref this.totalTicks, duration.Ticks);
        switch (source)
        {
            case AcquisitionSourceKind.Memory:
                Interlocked.Increment(ref this.memorySuccessCount);
                break;
            case AcquisitionSourceKind.Ui:
            case AcquisitionSourceKind.Composite:
                Interlocked.Increment(ref this.uiSuccessCount);
                break;
        }
    }

    public void RecordSkip(AcquisitionSourceKind source)
    {
        Interlocked.Increment(ref this.skipCount);
        switch (source)
        {
            case AcquisitionSourceKind.Memory:
                Interlocked.Increment(ref this.memorySkipCount);
                break;
            case AcquisitionSourceKind.Ui:
            case AcquisitionSourceKind.Composite:
                Interlocked.Increment(ref this.uiSkipCount);
                break;
        }
    }
}
