namespace XIVSubmarinesRewrite.Infrastructure.Logging;

using System;

/// <summary>Minimal abstraction for structured logging.</summary>
public interface ILogSink
{
    void Log(LogLevel level, string message, Exception? exception = null);
}

public enum LogLevel
{
    Trace,
    Debug,
    Information,
    Warning,
    Error,
    Critical
}
