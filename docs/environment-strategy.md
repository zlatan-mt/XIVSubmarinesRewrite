# Dalamud 依存導入と開発環境整備案

## 目的
- Data Acquisition Gateway / SnapshotCache を実装するための Dalamud 参照とテレメトリ基盤を安定運用する。
- 開発者・CI 間で環境差分を抑え、負荷削減ロジックと自動テストを再現可能にする。
- 秘匿情報や設定を安全に扱いつつ、自動バグフィックスパイプラインを回せるようにする。

## 依存取得フロー
1. **Hooks 配置**
   - 既定は `DALAMUD_LIB_PATH` 環境変数で Dalamud dev hooks を指す。
   - `Local.props` で `DalamudLibPath` と `DevPluginsDir` を上書き可能。複数環境では `Directory.Build.props` で共通値を定義。
2. **自動検証**
   - `tools/verify_dalamud_refs.ps1` を拡張し、参照 DLL・バージョン・ハッシュをチェック。CI でも必須実行。
   - API レベル更新時は `repo.json` / `manifest.json` / `csproj` の整合性チェックを CI に組み込む。
3. **SDK 取得スクリプト**
   - Windows/WSL 向けに `docs/setup/dalamud-sdk.md` を参照。Zip ダウンロード -> キャッシュ展開 -> `DALAMUD_LIB_PATH` 設定のスクリプトを追加予定。

## プロジェクト構成指針
- `src/Domain`, `src/Acquisition`, `src/Application`, `src/Infrastructure`, `src/Presentation`, `src/Integrations` などへ namespace 分割。必要に応じてサブプロジェクト化。
- Dependency Injection は `Infrastructure` で構成。Dalamud からのエントリポイントは Composition Root のみ最小限にとどめる。
- Snapshot やテレメトリは `Infrastructure/Diagnostics` 配下で一元管理し、各層へインターフェース提供。

## テレメトリ・パフォーマンス計測
- `PerformanceMonitor` を DI で注入し、CPU/GPU/GC 指標・スキャン所要時間を収集。
- 「高負荷モード/低負荷モード」移行イベントをテレメトリに記録し、後続の自動分析に利用。
- テストと本番で計測の粒度を切り替えるため、`IDiagnosticsConfig` でサンプリング設定を注入。

## 自動テスト・CI 戦略
1. **ローカル**
   - `dotnet test` に加え、メモリスナップショットを利用した統合テスト (`MemorySnapshotFixture`) を実行。
   - `just` もしくは `scripts/` に統一タスクを用意し、開発者はコマンド一発でビルド/テスト/パッケージ。
2. **CI (GitHub Actions 想定)**
   - キャッシュ済み Dalamud SDK を展開 → `tools/verify_dalamud_refs.ps1` で検証 → `dotnet test` + 静的解析 (Roslyn Analyzer, StyleCop)。
   - テレメトリ閾値テスト (CPU 使用率, スキャン時間) を自動で実行し、逸脱した場合は失敗扱い。
   - Regression Harness (再現スナップショット + 自動生成テスト) を nightly / バグ報告トリガで走らせる。
3. **Artifact 配布**
   - Release ビルドは DevPluginsDir へコピー後、`repo.json` を更新し Dalamud 用パッケージを生成。
   - CI ではパッケージ前に Secrets (Webhook/Token) が参照されないことを検証する。

## 設定・Secrets 運用
- `ISettingsProvider` (JSON) と `ISecretStore` (暗号化) を DI に登録。ローカルでは plaintext + DPAPI/Windows Credential Guard を想定。
- CI では Secrets を使用せず、モック値で統合テストを実施。外部通知の本番検証はリリース前の手動チェックとする。
- 設定スキーマ変更時は Migration スクリプトを用意し、旧版との互換を確保。

## 今後の TODO
- [ ] SDK 取得スクリプトとキャッシュ戦略を `tools/` 配下に追加。
- [ ] テレメトリ設定 (`IDiagnosticsConfig`) の仕様をドキュメント化し、開発/本番/テストのプリセットを整備。
- [ ] GitHub Actions ワークフロー (build/test/lint/package/telemetry-check) の叩き台を作成。
- [ ] Regression Harness で利用するスナップショット形式と保存先ポリシーを決定。
