# 4隻目の潜水艦ステータス問題 - 修正計画

## 1. 問題の再定義

### 誤った仮説（元レポート）
「4隻目の潜水艦だけが特別扱いされている」

### 正しい根本原因（レビュー後）
**スコアリングロジックの矛盾とペナルティバランスの問題**により、情報量が少ない行（特に帰港中や英語クライアントの"return"を含む行）がスコア閾値を下回り、UI候補から除外される。

## 2. レビュー結果の反映

### ⚠️ 重要: 問題2の分析誤りについて
**レビュー1の指摘**: 「ステータスのみの行の低スコア」の分析は論理的に誤りです。

**誤った分析**:
```csharp
if (!hasRoute && !(hasStatus && !hasEta))  // この条件の評価を誤解していた
```

**正しい評価** (hasStatus=true, hasEta=false の場合):
```
!hasRoute && !(hasStatus && !hasEta)
= true && !(true && true)
= true && false
= false  ← ペナルティは適用されない！
```

したがって、帰港中の潜水艦（名前+ステータスのみ）のスコアは:
- 名前(+1) + ステータス(+2) = **3点** → 閾値クリアのはず

**結論**: 問題2の原因は未特定。実機デバッグログが必要。

## 3. 確認された問題点

### 問題1: "return" キーワードの矛盾（✅ 確定）
**場所**:
- `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs:78-90`
- `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.Helpers.cs:21-29`

**矛盾**:
- `NegativeRowKeywords` に "return" が含まれる（-3点ペナルティ）
- `StatusUnderwayKeywords` に "return" が含まれる（+2点ボーナス）

**影響**:
英語クライアントで "Return from voyage" などのテキストを持つ行は、+2（ステータス）-3（ネガティブ）= -1点の純減となり、他の加点があっても閾値3点を下回りやすい。

### 問題2: 帰港中の行のスコア不足（❓ 原因未特定）
**場所**: `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs:447-528`

**報告された現象**:
帰港中の潜水艦（特に4隻目）がUI候補から除外される

**理論上のスコア** (名前+ステータスのみ):
- hasValidName: +1
- hasStatus: +2
- ペナルティ: なし（条件評価により）
- **合計: 3点 → 閾値3で通過するはず**

**しかし実際には除外されている** → 原因は以下のいずれか:

1. **ステータステキストが認識されていない**
   - `IsStatusText("帰港")` が false を返している？
   - 4隻目だけテキスト形式が異なる？

2. **名前抽出が失敗している**
   - `ExtractNameCandidate` が null を返している？
   - 4隻目の名前パターンが特殊？

3. **想定外のネガティブキーワードに引っかかっている**
   - `ContainsNegativeKeyword` が true になっている？

**結論**: 実機のデバッグログがないと真の原因を特定できない

### 問題3: 4隻目だけが影響を受ける理由
4隻目が特別扱いされているわけではなく、以下の条件が揃った時に**どのスロットでも**問題が発生する：

1. 帰港中（航路情報なし、ETA情報なし）
2. Rank表示がない（または読み取れない）
3. 英語クライアントで "return" キーワードを含む

ユーザー報告で「4隻目だけ」となったのは、おそらく：
- 他の3隻は航海中（ルート+ETA情報あり）
- 4隻目のみ帰港中（情報が少ない）

## 4. 修正方針（レビュー後の更新）

### 修正A: "return" キーワードの矛盾解消（✅ 承認 - 実装推奨）

#### オプションA1: NegativeRowKeywords から "return" を削除
**理由**: "return" は潜水艦の正当なステータステキスト（航海中の意味）であり、ネガティブキーワードとして扱うのは誤り。

**変更箇所**: `DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs:78-90`
```csharp
private static readonly string[] NegativeRowKeywords =
{
    "retainer",
    "リテイナー",
    "ベンチャー",
    "収集品",
    "返却",
    // "return", ← 削除
    "venture",
    "delivery",
    "supply",
    "squadron",
};
```

**影響**: 英語クライアントで "Return from voyage" を含む行が -3点ペナルティを受けなくなる。

#### オプションA2: コンテキスト依存の判定（より厳密）
"return" が単独で現れる場合のみネガティブ判定し、ステータステキストとして認識された場合は除外する。

**複雑度**: 高（優先度低）

### 修正B: 帰港中の行のスコア不足対策（❌ 却下 - 要再調査）

**レビュー1の指摘**: 修正B1の提案は論理的に誤りです。

**誤った提案**:
```csharp
// 修正前
if (!hasRoute && !(hasStatus && !hasEta))

// 修正後（誤り）
if (!hasRoute && !hasStatus)
```

**問題点**: hasStatus=true の場合、両方とも false と評価されるため、修正しても何も変わらない。

**推奨アクション**:
1. **デバッグログの追加** (LogLevel.Trace、環境変数で制御)
   ```csharp
   if (FilterToggles.StrictFilter && score < FilterToggles.MinimumScore)
   {
       this.log.Log(LogLevel.Trace,
           $"[UI Inspector] Row rejected: score={score} threshold={FilterToggles.MinimumScore}\n" +
           $"  hasValidName={hasValidName} hasRoute={hasRoute} hasEta={hasEta}\n" +
           $"  hasStatus={hasStatus} hasRank={hasRank} hasNegative={hasNegative}\n" +
           $"  texts={string.Join(" | ", row.Texts)}");
   }
   ```

2. **実機テストで4隻目のテキストを取得**
   - 4隻目の `row.Texts` の実際の内容を確認
   - どの要素が false になっているかを特定

3. **真の原因特定後に修正案を再立案**

### 修正C: 最小スコア閾値の調整（オプション）

**環境変数**: `XIVSR_UI_MIN_SCORE` (デフォルト: 3)

**提案**: デフォルトを 3 → 2 に引き下げ

**理由**: 名前+ステータスのみの行（2点）が通過できる

**リスク**: ノイズ行が候補に混入する可能性が増加

**推奨**: 修正A+Bを適用後、必要に応じて検討

## 5. 推奨される修正手順（レビュー後の更新）

### フェーズ1: 確実な修正の実装（即時）
1. **修正A1のみ実装**: "return" を `NegativeRowKeywords` から削除
   - これで英語クライアントの問題は解決

### フェーズ2: 問題2の原因特定（デバッグ）
**⚠️ レビュー2の懸念**: v1.2.3でログ静穏化を実施済み。デバッグログは条件付きで実装。

1. **環境変数による制御**
   ```csharp
   // 環境変数 XIVSR_DEBUG_SCORING=1 の場合のみログ出力
   private static readonly bool DebugScoring =
       Environment.GetEnvironmentVariable("XIVSR_DEBUG_SCORING") == "1";
   ```

2. **TryCreateCandidate に詳細ログ追加**
   ```csharp
   if (DebugScoring || FilterToggles.StrictFilter && score < FilterToggles.MinimumScore)
   {
       this.log.Log(LogLevel.Trace,
           $"[UI Debug] score={score} name={hasValidName} route={hasRoute}\n" +
           $"  eta={hasEta} status={hasStatus} rank={hasRank} neg={hasNegative}\n" +
           $"  texts=[{string.Join(" | ", row.Texts)}]");
   }
   ```

3. **実機テストで4隻目のログを取得**
   - 環境変数を設定して実行
   - 4隻目の実際のテキストとスコア計算を確認

### フェーズ3: テスト
1. 日本語クライアント: 帰港中の潜水艦が正しく認識されるか
2. 英語クライアント: "Return from voyage" を含む行が正しく認識されるか
3. 4隻全て異なる状態（航海中、帰港、完了、準備中）で動作確認

### フェーズ4: ドキュメント更新
1. 元レポート（`report_4th_submarine_status_issue.md`）に「修正済み」の注釈を追加
2. このレビューと修正内容を `docs/analysis/` に記録

## 5. コード変更の詳細

### 変更1: NegativeRowKeywords から "return" を削除

**ファイル**: `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs`

**Before (78-90行目)**:
```csharp
private static readonly string[] NegativeRowKeywords =
{
    "retainer",
    "リテイナー",
    "ベンチャー",
    "収集品",
    "返却",
    "return",
    "venture",
    "delivery",
    "supply",
    "squadron",
};
```

**After**:
```csharp
private static readonly string[] NegativeRowKeywords =
{
    "retainer",
    "リテイナー",
    "ベンチャー",
    "収集品",
    "返却",
    // "return" removed: 潜水艦の正当なステータステキストであるため、
    // ネガティブキーワードとして扱わない
    "venture",
    "delivery",
    "supply",
    "squadron",
};
```

### ~~変更2: ステータスありの場合のルート不在ペナルティ免除~~（❌ 却下）

**レビュー1の指摘により却下**: この変更は論理的に効果がない。

**理由**: hasStatus=true の場合、元のコードも提案されたコードも両方とも false と評価されるため、スコア計算に変化なし。

## 6. 期待される効果

### Before（現状）
| 状態 | 要素 | スコア計算 | 結果 |
|------|------|------------|------|
| 帰港中（日本語） | 名前+ステータス | 1+2-1 = **2点** | ❌ 除外 |
| Return from voyage（英語） | 名前+ステータス+ネガティブ | 1+2-3-1 = **-1点** | ❌ 除外 |
| 航海中（ルート+ETA） | 名前+ルート+ETA | 1+2+2 = **5点** | ✅ 通過 |

### After（修正後）
| 状態 | 要素 | スコア計算 | 結果 |
|------|------|------------|------|
| 帰港中（日本語） | 名前+ステータス | 1+2 = **3点** | ✅ 通過 |
| Return from voyage（英語） | 名前+ステータス | 1+2 = **3点** | ✅ 通過 |
| 航海中（ルート+ETA） | 名前+ルート+ETA | 1+2+2 = **5点** | ✅ 通過 |

## 7. リスク評価

### 低リスク
- 変更範囲が明確（2箇所のみ）
- ロジックが単純化される
- 既存の正常動作に悪影響なし

### 検証が必要な点
- 他のUIメニュー（リテイナーなど）の誤検出が増えないか
  → ヘッダーキーワードチェックとテリトリーゲートで保護されているため、リスクは低い

### 推奨テスト
1. 全スロット帰港中の状態で動作確認
2. 英語クライアントでの動作確認
3. リテイナーメニューが誤検出されないことを確認

## 8. レビュー対応と次のアクション

### レビュー1の対応
- ✅ 修正A1: 承認 → 実装推奨
- ❌ 修正B1: 却下 → 要再調査
- ✅ 論理誤りの指摘を反映し、計画書を更新

### レビュー2の対応
- ✅ デバッグログは環境変数による条件付き実装に変更
- ✅ XIVSR_UI_MIN_SCORE の変更は優先度を最低に設定
- ✅ ユニットテストのケースを具体化（下記参照）

### ユニットテストケース（ComputeConfidenceScoreForTest使用）
```csharp
[Theory]
[InlineData(new[] { "SM1", "帰港" }, 3)]  // 名前+ステータス
[InlineData(new[] { "SM1", "Return from voyage" }, 3)]  // 修正A1後
[InlineData(new[] { "SM1", "航路A->B->C", "12h 30m" }, 5)]  // フル情報
[InlineData(new[] { "SM1" }, 1)]  // 名前のみ
public void ComputeConfidence_VariousCases(string[] texts, int expected)
{
    var score = DalamudUiSubmarineSnapshotSource.ComputeConfidenceScoreForTest(texts);
    Assert.Equal(expected, score);
}
```

### 次のアクション
1. ✅ レビュー対応完了
2. ⬜ 修正A1のみ実装（"return"削除）
3. ⬜ デバッグログ追加（環境変数制御）
4. ⬜ 実機テストで4隻目のログ取得
5. ⬜ 真の原因特定後、修正B（新）を立案
6. ⬜ v1.2.4としてリリース

---

**作成日**: 2025-11-16
**レビュアー指摘の重要性**: ⭐⭐⭐⭐⭐（非常に優れた分析）
