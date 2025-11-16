// tests/XIVSubmarinesRewrite.Tests/RowConfidenceTests.cs
// UI候補スコアリングのしきい値をユニットテストで検証します
// 帰港中や英語クライアントの行が除外されないことを保証するために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs,tests/XIVSubmarinesRewrite.Tests/RowParsingTests.cs

using System.Collections.Generic;
using XIVSubmarinesRewrite.Infrastructure.Acquisition;
using Xunit;

public sealed class RowConfidenceTests
{
    public static IEnumerable<object[]> StatusOnlyRows()
    {
        yield return new object[] { new[] { "SM4", "帰港" } };
        yield return new object[] { new[] { "FF4", "探索完了" } };
        yield return new object[] { new[] { "SM1", "Return from voyage" } };
    }

    [Theory]
    [MemberData(nameof(StatusOnlyRows))]
    public void ComputeConfidenceScore_ShouldAcceptStatusOnlyRows(string[] texts)
    {
        var score = DalamudUiSubmarineSnapshotSource.ComputeConfidenceScoreForTest(texts);
        Assert.True(score >= 3, $"Expected score >= 3 but got {score} for '{string.Join(" | ", texts)}'");
    }

    [Fact]
    public void ComputeConfidenceScore_ShouldStillPenalizeRetainerRows()
    {
        var score = DalamudUiSubmarineSnapshotSource.ComputeConfidenceScoreForTest(new[] { "Retainer", "Venture Delivery" });
        Assert.True(score < 0, $"Expected negative score for retainer rows but got {score}");
    }
}
