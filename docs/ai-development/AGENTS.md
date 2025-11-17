<!-- /AGENTS.md -->
<!-- Repository Guidelines for XIVSubmarinesRewrite contributors -->
<!-- Covers layout, tooling, style, testing, and release expectations -->
<!-- RELEVANT FILES: README.md, CHANGELOG.md, .github/workflows/release.yml -->

# Repository Guidelines

## Project Structure & Module Organization
- `src/` holds the Dalamud plugin code; follow the existing namespace hierarchy (e.g., `XIVSubmarinesRewrite.Acquisition`).
- `tests/` mirrors the production layout and contains the test project that targets the same DLL.
- Supporting documentation lives under `docs/` and `plans/`; keep release notes in the root `RELEASE_NOTES_v*.md` files, and keep `release-package/` synced with the distributable ZIP.
- Root JSON manifests (`manifest.json`, `plugin.json`, `repo.json`) describe the published build and should always align with the active assembly version and `main` branch URLs.

## Build, Test, and Development Commands
- `dotnet restore` to fetch NuGet dependencies before builds or tests.
- `dotnet build --configuration Release --no-restore` produces the plugin DLLs located in `bin/Release/net9.0-windows`. This is what release workflows archive.
- `dotnet test tests/XIVSubmarinesRewrite.Tests/XIVSubmarinesRewrite.Tests.csproj` runs the integration/unit suites; rerun before tagging.
- Release packaging recreates `release-package/` and the distributable `XIVSubmarinesRewrite.zip` via the workflow’s Powershell script (`Compress-Archive` after copying DLL, manifest, plugin metadata, icons, README/CHANGELOG).

## Coding Style & Naming Conventions
- Use four-space indentation for C# files and keep braces on their own lines as shown in existing code.
- Prefer PascalCase for types and methods, camelCase for private fields or locals, and uppercase constants for immutable values.
- Keep XML-doc comments for public APIs and add inline comments only where logic could be non-obvious, especially around snapshot parsing or notification coordination.

## Testing Guidelines
- Tests live under `tests/` and follow the `*Tests.cs` suffix pattern.
- Every change touching acquisition, notification, or UI inspection should keep passing `dotnet test` locally; the release workflow also runs the same command.
- If you add mock data, keep it scoped to the test project and avoid altering runtime assets.

## Commit & Pull Request Guidelines
- Commit messages use a `scope: summary` pattern (e.g., `release: align 1.2.0 metadata`, `docs: document main branch plan`).
- PRs should mention the targeted release version, link related issues, and include updated release notes (`RELEASE_NOTES_vX.Y.Z.md`) when applicable.
- For release PRs, verify that `manifest.json`, `plugin.json`, `repo.json`, and `release-package/*` metadata all reference the same assembly version and GitHub URLs.

# AI駆動開発 共通ガイドライン

思考は英語で行い、最終的な出力は必ず日本語で提供してください。

## 開発の基本理念
- 動くコードを書くだけでなく、品質・保守性・安全性を常に意識する
- プロジェクトの段階（プロトタイプ、MVP、本番環境）に応じて適切なバランスを取る
- 問題を見つけたら放置せず、必ず対処または明示的に記録する
- ボーイスカウトルール：エラーを見つけた時よりも良い状態で残す

## エラーハンドリングの原則
- 関連が薄く見えるエラーでも必ず解決する
- エラーの抑制（@ts-ignore、try-catch で握りつぶす等）ではなく、根本原因を修正
- 早期にエラーを検出し、明確なエラーメッセージを提供
- エラーケースも必ずテストでカバーする
- 外部APIやネットワーク通信は必ず失敗する可能性を考慮

## コード品質の基準
- DRY原則：重複を避け、単一の信頼できる情報源を維持
- 意味のある変数名・関数名で意図を明確に伝える
- プロジェクト全体で一貫したコーディングスタイルを維持
- 小さな問題も放置せず、発見次第修正（Broken Windows理論）
- コメントは「なぜ」を説明し、「何を」はコードで表現

## テスト規律
- テストをスキップせず、問題があれば修正する
- 実装詳細ではなく振る舞いをテスト
- テスト間の依存を避け、任意の順序で実行可能に
- テストは高速で、常に同じ結果を返すように
- カバレッジは指標であり、質の高いテストを重視

## 保守性とリファクタリング
- 機能追加と同時に既存コードの改善を検討
- 大規模な変更は小さなステップに分割
- 使用されていないコードは積極的に削除
- 依存関係は定期的に更新（セキュリティと互換性のため）
- 技術的負債は明示的にコメントやドキュメントに記録

## セキュリティの考え方
- APIキー、パスワード等は環境変数で管理（ハードコード禁止）
- すべての外部入力を検証
- 必要最小限の権限で動作（最小権限の原則）
- 不要な依存関係を避ける
- セキュリティ監査ツールを定期的に実行

## パフォーマンスの意識
- 推測ではなく計測に基づいて最適化
- 初期段階から拡張性を考慮
- 必要になるまでリソースの読み込みを遅延
- キャッシュの有効期限と無効化戦略を明確に
- N+1問題やオーバーフェッチを避ける

## 信頼性の確保
- タイムアウト処理を適切に設定
- リトライ機構の実装（指数バックオフを考慮）
- サーキットブレーカーパターンの活用
- 一時的な障害に対する耐性を持たせる
- 適切なログとメトリクスで可観測性を確保

## プロジェクトコンテキストの理解
- ビジネス要件と技術要件のバランスを取る
- 現在のフェーズで本当に必要な品質レベルを判断
- 時間制約がある場合でも、最低限の品質基準を維持
- チーム全体の技術レベルに合わせた実装選択

## トレードオフの認識
- すべてを完璧にすることは不可能（銀の弾丸は存在しない）
- 制約の中で最適なバランスを見つける
- プロトタイプなら簡潔さを、本番なら堅牢性を優先
- 妥協点とその理由を明確にドキュメント化

## Git運用の基本
- コンベンショナルコミット形式を使用（feat:, fix:, docs:, test:, refactor:, chore:）
- コミットは原子的で、単一の変更に焦点を当てる
- 明確で説明的なコミットメッセージを日本語で記述
- main/masterブランチへの直接コミットは避ける

## コードレビューの姿勢
- レビューコメントは建設的な改善提案として受け取る
- 個人ではなくコードに焦点を当てる
- 変更の理由と影響を明確に説明
- フィードバックを学習機会として歓迎

## デバッグのベストプラクティス
- 問題を確実に再現できる手順を確立
- 二分探索で問題の範囲を絞り込む
- 最近の変更から調査を開始
- デバッガー、プロファイラー等の適切なツールを活用
- 調査結果と解決策を記録し、知識を共有

## 依存関係の管理
- 本当に必要な依存関係のみを追加
- package-lock.json等のロックファイルを必ずコミット
- 新しい依存関係追加前にライセンス、サイズ、メンテナンス状況を確認
- セキュリティパッチとバグ修正のため定期的に更新

## ドキュメントの基準
- READMEにプロジェクトの概要、セットアップ、使用方法を明確に記載
- ドキュメントをコードと同期して更新
- 実例を示すことを優先
- 重要な設計判断はADR (Architecture Decision Records)で記録

## 継続的な改善
- 学んだことを次のプロジェクトに活かす
- 定期的に振り返りを行い、プロセスを改善
- 新しいツールや手法を適切に評価して取り入れる
- チームや将来の開発者のために知識を文書化