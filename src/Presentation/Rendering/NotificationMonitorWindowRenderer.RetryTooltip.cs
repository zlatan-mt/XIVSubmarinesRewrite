// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.RetryTooltip.cs
// リトライボタンのツールチップ描画ロジックを分離します
// 通知のリトライ状況を可視化し、ユーザー体験を向上させるため存在します
// RELEVANT FILES: NotificationMonitorWindowRenderer.Queue.cs, RetryTooltipFormatter.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using XIVSubmarinesRewrite.Application.Notifications;

/// <summary>
/// Retry tooltip rendering helpers for NotificationMonitorWindowRenderer
/// </summary>
public sealed partial class NotificationMonitorWindowRenderer
{
    /// <summary>
    /// リトライボタン描画（ツールチップ付き）
    /// </summary>
    /// <param name="workItem">通知ワークアイテムのスナップショット</param>
    /// <returns>ボタンがクリックされた場合はtrue</returns>
    private bool DrawRetryButtonWithTooltip(NotificationWorkItemSnapshot workItem)
    {
        var buttonClicked = ImGui.SmallButton("再送");

        // ツールチップ描画
        if (ImGui.IsItemHovered())
        {
            DrawRetryTooltip(workItem);
        }

        return buttonClicked;
    }

    /// <summary>
    /// リトライツールチップの内容を描画
    /// </summary>
    /// <param name="workItem">通知ワークアイテムのスナップショット</param>
    private void DrawRetryTooltip(NotificationWorkItemSnapshot workItem)
    {
        ImGui.BeginTooltip();

        try
        {
            var tooltipContent = RetryTooltipFormatter.Format(
                workItem,
                System.DateTime.UtcNow
            );

            foreach (var line in tooltipContent.Lines)
            {
                DrawTooltipLine(line);
            }
        }
        catch (System.Exception)
        {
            // フォールバック: エラー時は最小限の情報を表示
            ImGui.TextColored(UiTheme.ErrorText, "ツールチップの表示に失敗しました");
        }
        finally
        {
            ImGui.EndTooltip();
        }
    }

    /// <summary>
    /// ツールチップの1行を描画
    /// </summary>
    /// <param name="line">ツールチップ行</param>
    private static void DrawTooltipLine(TooltipLine line)
    {
        if (line.IsSeparator)
        {
            ImGui.Separator();
            return;
        }

        if (line.Color.HasValue)
        {
            ImGui.TextColored(line.Color.Value, line.Text);
        }
        else
        {
            ImGui.TextUnformatted(line.Text);
        }
    }
}

