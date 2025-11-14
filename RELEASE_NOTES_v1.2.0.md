# XIV Submarines Rewrite v1.2.0 リリースノート

**リリース日**: 2025年11月14日  
**対応API**: Dalamud API Level 13  
**フレームワーク**: .NET 9.0-windows

---

## 🎯 このリリースについて

- 探索完了状態の潜水艦が「探索完了」などの状態テキストを含む名称を持つ場合、Discord通知や一覧表示にステータス文字列が残り続けていました。
- 本リリースでは行解析ロジックと GUID 生成を強化して、名前抽出の精度と通知の信頼性を高めています。

## Fixed

- **Status テキストの除去精度を向上**: `StripStatusSuffix` で単語境界を考慮し、`IsOnlyStatusText` でステータス単体を排除するので、`MyReturn` や `Theready` といった本来の名前が誤って削除されません。
- **Completed 状態の名前抽出**: `StatusCompletedKeywords` を拡張して `探索完了` などの複合キーワードも含め、Discord通知とUI上に正しい表示を実現します。
- **Voyage GUID の一意性強化**: `ComputeVoyageId` がキャラクターIDとスロット番号も混ぜて GUID を構成するようになり、複数キャラクターや同名の潜水艦でも衝突しません。

## Technical Details

- `StripStatusSuffix` でステータス語の前提条件に単語境界と空白チェックを追加し、名前とステータスの曖昧さを除去。
- `IsOnlyStatusText` がステータス単語だけの行を除外して、空行や文字列だけの状態を無視。
- `ExtractNameCandidate` がステータスと角括弧のトリミングを行ったあとに名前文字列を返すので、UI の SelectString から余計な語を削除できます。
- `ComputeVoyageId` は `submarineId.CharacterId` と `submarineId.Slot` を投入し、同一の登録/帰港時刻でもキャラクターやスロットの違いを識別します。

