// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/UiFallbackFilterTests.cs
// UI フォールバックのスコアリングと否定語フィルタの挙動を検証します
// 潜水艦候補の誤検知を防ぐためのユニットテストを提供する目的で存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs

using System.Collections.Generic;
using Xunit;
using XIVSubmarinesRewrite.Infrastructure.Acquisition;

public sealed class UiFallbackFilterTests
{
    [Fact]
    public void ComputeConfidenceScoreForTest_ReturnsLowScoreForRetainerText()
    {
        var texts = new List<string>
        {
            "リテイナーを帰す",
            "リテイナー任務",
            "返却"
        };

        var score = DalamudUiSubmarineSnapshotSource.ComputeConfidenceScoreForTest(texts);

        Assert.True(score <= 0, "Retainer related text should produce a non-positive score.");
    }

    [Fact]
    public void ComputeConfidenceScoreForTest_ReturnsHighScoreForValidSubmarineRow()
    {
        var texts = new List<string>
        {
            "潜水艦アルファ",
            "Route A -> B",
            "ETA 1h 20m",
            "Rank 20"
        };

        var score = DalamudUiSubmarineSnapshotSource.ComputeConfidenceScoreForTest(texts);

        Assert.True(score >= 3, "Valid submarine text should exceed the acceptance threshold.");
    }

    [Fact]
    public void ComputeConfidenceScoreForTest_ReturnsHighScoreForJapaneseUnderwayRow()
    {
        var texts = new List<string>
        {
            "Submarine-1",
            "Rank131",
            "探索中:残り時間 1日13時間18分",
        };

        var score = DalamudUiSubmarineSnapshotSource.ComputeConfidenceScoreForTest(texts);

        Assert.True(score >= 3, "Japanese underway row should exceed the acceptance threshold.");
    }
}
