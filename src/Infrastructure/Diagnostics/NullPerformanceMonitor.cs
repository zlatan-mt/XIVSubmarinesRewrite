namespace XIVSubmarinesRewrite.Infrastructure.Diagnostics;

using System;

/// <summary>Stub implementation used until telemetry wiring is complete.</summary>
public sealed class NullPerformanceMonitor : IPerformanceMonitor
{
    public PerformanceSample Capture() => new (DateTime.UtcNow, 0, 0, 0);
}
