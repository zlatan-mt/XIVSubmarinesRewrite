// apps/XIVSubmarinesRewrite/src/Application/Notifications/IForceNotifyDiagnostics.cs
// ForceNotifyUnderway の内部状態を UI へ公開する診断用インターフェースです
// 通知ウィンドウでクールダウンや最終送信時刻を確認できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Provides visibility into ForceNotifyUnderway diagnostic information.</summary>
public interface IForceNotifyDiagnostics
{
    /// <summary>Gets a snapshot of current ForceNotifyUnderway states.</summary>
    IReadOnlyList<ForceNotifyStateSnapshot> GetForceNotifySnapshot();

    /// <summary>Returns true when ForceNotifyUnderway is currently enabled.</summary>
    bool IsForceNotifyEnabled { get; }

    /// <summary>Records a manual trigger action for diagnostic purposes.</summary>
    void RecordManualTrigger(ForceNotifyManualTrigger entry);

    /// <summary>Gets recent manual trigger log entries.</summary>
    IReadOnlyList<ForceNotifyManualTrigger> GetManualTriggerLog();
}

/// <summary>Immutable snapshot of ForceNotifyUnderway cooldown state per submarine.</summary>
public sealed record ForceNotifyStateSnapshot(
    SubmarineId SubmarineId,
    DateTime LastTriggerUtc,
    DateTime CooldownUntilUtc,
    DateTime? LastArrivalUtc,
    TimeSpan Remaining,
    string Reason);


/// <summary>Represents a manual ForceNotify trigger recorded for diagnostics.</summary>
public sealed record ForceNotifyManualTrigger(
    DateTime TriggeredAtUtc,
    ulong CharacterId,
    string? CharacterName,
    string? World,
    bool IncludeUnderway,
    int NotificationsEnqueued);
