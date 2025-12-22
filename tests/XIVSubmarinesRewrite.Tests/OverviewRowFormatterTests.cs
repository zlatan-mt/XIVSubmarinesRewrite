// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/OverviewRowFormatterTests.cs
// OverviewRowFormatter の文字列生成ロジックを検証するユニットテストです
// UI の折り返し表示とコピー内容が期待通りか自動で確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewRowFormatter.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

using System;
using System.Globalization;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Presentation.Rendering;
using XIVSubmarinesRewrite.Presentation.ViewModels;
using Xunit;

public sealed class OverviewRowFormatterTests
{
    [Fact]
    public void BuildCopyLine_ReturnsConciseSummary()
    {
        var entry = new SubmarineOverviewEntry(
            new SubmarineId(0xDEADBEEF, 1),
            CharacterId: 0xDEADBEEF,
            Name: "Aquila",
            ProfileId: "profile-1",
            RouteId: "12-34-56",
            Status: VoyageStatus.Underway,
            Departure: DateTime.SpecifyKind(new DateTime(2025, 9, 28, 8, 0, 0), DateTimeKind.Local),
            Arrival: DateTime.SpecifyKind(new DateTime(2025, 9, 28, 15, 30, 0), DateTimeKind.Local),
            Remaining: TimeSpan.FromMinutes(45));

        var originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            var result = OverviewRowFormatter.BuildCopyLine(entry, "A-B-C");
            Assert.Equal("Aquila | 航行中 | 45m | 9/28 15:30 | A-B-C", result);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void BuildWrappedRouteLabel_InsertsZeroWidthSpaceAfterSeparators()
    {
        var wrapped = OverviewRowFormatter.BuildWrappedRouteLabel("A>B>C");
        Assert.Contains("\u200B", wrapped);
        Assert.Equal('A', wrapped[0]);
        Assert.Equal('>', wrapped[1]);
        Assert.Equal('\u200B', wrapped[2]);
    }

    [Theory]
    [InlineData(90, "01h30")]
    [InlineData(45, "45m")]
    public void FormatRemainingCompact_ReturnsShortNotation(int minutes, string expected)
    {
        var remaining = TimeSpan.FromMinutes(minutes);
        Assert.Equal(expected, OverviewRowFormatter.FormatRemainingCompact(remaining));
    }

    [Fact]
    public void FormatArrivalCompact_ReturnsHourMinute()
    {
        var local = DateTime.SpecifyKind(new DateTime(2025, 9, 30, 18, 45, 0), DateTimeKind.Local);
        Assert.Equal(local.ToString("HH:mm", CultureInfo.CurrentCulture), OverviewRowFormatter.FormatArrivalCompact(local));
    }

    [Fact]
    public void BuildCopyLine_StripsZeroWidthCharacters()
    {
        var entry = new SubmarineOverviewEntry(
            new SubmarineId(0x1, 1),
            CharacterId: 0x1,
            Name: "Hydra",
            ProfileId: "profile",
            RouteId: "12-34",
            Status: VoyageStatus.Completed,
            Departure: DateTime.UtcNow,
            Arrival: DateTime.UtcNow,
            Remaining: TimeSpan.Zero);

        var wrapped = "A\u200BB\u200BC";
        var copy = OverviewRowFormatter.BuildCopyLine(entry, wrapped);
        Assert.DoesNotContain('\u200B', copy);
    }
}
