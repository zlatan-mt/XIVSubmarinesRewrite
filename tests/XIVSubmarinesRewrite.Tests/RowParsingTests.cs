// tests/XIVSubmarinesRewrite.Tests/RowParsingTests.cs
// DalamudUiSubmarineSnapshotSource.RowParsing における名前抽出ロジックのテスト
// 探索完了状態や様々なステータステキストから正しく潜水艦名が抽出できることを検証します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs

using XIVSubmarinesRewrite.Infrastructure.Acquisition;
using Xunit;

public sealed class RowParsingTests
{
    [Theory]
    [InlineData("SM1", "SM1")]                                      // 名前のみ
    [InlineData("SMTwo", "SMTwo")]                                  // 名前のみ（複数文字）
    [InlineData("NAME3", "NAME3")]                                  // 名前のみ（大文字+数字）
    [InlineData("FF4", "FF4")]                                      // 名前のみ（短い）
    [InlineData("SMTwo     探索完了", "SMTwo")]                     // 探索完了（日本語）
    [InlineData("NAME3     帰港", "NAME3")]                         // 帰港
    [InlineData("FF4     完了", "FF4")]                             // 完了
    [InlineData("SM1  [Rank98]", "SM1")]                            // Rank付き
    [InlineData("SMTwo  [Rank97]  探索完了", "SMTwo")]              // Rank+探索完了
    [InlineData("FF4  [Rank96]  完了", "FF4")]                      // Rank+完了
    [InlineData("TestSub     到着", "TestSub")]                     // 到着
    [InlineData("MySub ready", "MySub")]                            // ready（英語）
    [InlineData("SubName completed", "SubName")]                    // completed（英語）
    [InlineData("Vessel underway", "Vessel")]                       // underway（英語）
    [InlineData("探索完了", null)]                                  // 名前なし（ステータスのみ）
    [InlineData("完了", null)]                                      // 名前なし（完了のみ）
    [InlineData("Rank98", null)]                                    // Rankのみ
    [InlineData("", null)]                                          // 空文字列
    [InlineData("   ", null)]                                       // 空白のみ
    [InlineData("MyReturn", "MyReturn")]                            // "return"を含むが単語境界で区切られていない
    [InlineData("Theready", "Theready")]                            // "ready"を含むが単語境界で区切られていない
    public void ExtractNameCandidate_ShouldExtractCorrectly(string input, string? expected)
    {
        // Act
        var actual = DalamudUiSubmarineSnapshotSource.ExtractNameCandidateForTest(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("SM1  探索中", "SM1")]                              // 探索中（単語境界あり）
    [InlineData("SMTwo  航行", "SMTwo")]                            // 航行
    [InlineData("NAME3  出航", "NAME3")]                            // 出航
    [InlineData("FF4  航海中", "FF4")]                              // 航海中
    [InlineData("TestSub  準備中", "TestSub")]                      // 準備中
    [InlineData("MySub  scheduled", "MySub")]                       // scheduled（英語）
    [InlineData("Vessel  deploy", "Vessel")]                        // deploy（英語）
    public void ExtractNameCandidate_ShouldStripUnderwayAndScheduledStatus(string input, string? expected)
    {
        // Act
        var actual = DalamudUiSubmarineSnapshotSource.ExtractNameCandidateForTest(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("A->B->C", null)]                                   // ルート（矢印）
    [InlineData("A→B→C", null)]                                     // ルート（日本語矢印）
    [InlineData("21時間7分", null)]                                 // ETA（時間分）
    [InlineData("5h 30m", null)]                                    // ETA（英語）
    [InlineData("Rank99", null)]                                    // Rank
    public void ExtractNameCandidate_ShouldRejectNonNameTexts(string input, string? expected)
    {
        // Act
        var actual = DalamudUiSubmarineSnapshotSource.ExtractNameCandidateForTest(input);

        // Assert
        Assert.Equal(expected, actual);
    }
}
