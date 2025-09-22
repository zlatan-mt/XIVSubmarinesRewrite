namespace XIVSubmarinesRewrite.Domain.Models;

using System;

/// <summary>Alarm configuration for notifying users before or after voyage transitions.</summary>
public sealed record Alarm(string AlarmId, VoyageId VoyageId, TimeSpan LeadTime, DateTime? NextTriggerAt);
