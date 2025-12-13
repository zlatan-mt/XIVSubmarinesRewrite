# Phase 13: Notion 通知機能削除計画 (Final)

## 1. 概要と目的
Notion 通知機能が正常に動作しておらず、利用頻度も低いと判断されたため、プロジェクトから関連機能を完全に削除する。これによりコードベースを軽量化し、メンテナンスコストを削減する。

## 2. 影響範囲分析

### 2.1 削除対象コンポーネント (Integrations/Application)
*   **`src/Integrations/Notifications/NotionWebhookClient.cs`**: Notion API と通信するクライアント実装。**ファイル削除**。
*   **`src/Integrations/Notifications/INotionClient.cs`**: クライアントインターフェース。**ファイル削除**。
*   **`src/Application/Notifications/VoyageNotificationFormatter.cs`**:
    *   `CreateNotionPayload` メソッド
    *   `NotionNotificationPayload` レコード
*   **`src/Application/Services/NotificationCoordinator.cs`**:
    *   `INotionClient` への依存
    *   `RecordVoyageCompletionAsync` 内の呼び出し
    *   `RecordAlarmAsync` 内の呼び出し

### 2.2 設定 (Infrastructure)
*   **`src/Infrastructure/Configuration/NotificationSettings.cs`**:
    *   `EnableNotion` プロパティ
    *   `NotionWebhookUrl` プロパティ
    *   *注: `DalamudJsonSettingsProvider` は `JsonUnmappedMemberHandling.Skip` 相当の挙動をするため、既存の JSON 設定ファイルに Notion 項目が残っていてもエラーにはならない。*
*   **`src/Infrastructure/Composition/PluginBootstrapper.cs`**:
    *   `NotionWebhookClient` のインスタンス化と DI コンテナへの登録処理。

### 2.3 UI (Presentation)
*   **`src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs`** (および partial):
    *   Notion 関連のフィールド (`notionUrlBuffer`, `notionUrlValid`, `notionUrlError`)
    *   `RevalidateChannelUrls` 内の Notion バリデーション呼び出し
*   **`src/Presentation/Rendering/NotificationMonitorWindowRenderer.ChannelCards.cs`**:
    *   `NotificationChannel` enum から `Notion` を削除。
    *   `RenderChannelCards` メソッド内のループロジック（`totalChannels` を 1 に変更、ループ除去）。
    *   `NotificationWebhookValidator.ValidateNotion` メソッド削除。
*   **`src/Presentation/Rendering/NotificationMonitorWindowRenderer.Queue.cs`**:
    *   設定の読み込み・保存ロジック内の Notion 関連記述。
*   **`src/Presentation/Rendering/NotificationMonitorWindowRenderer.SavePanel.cs`**:
    *   バリデーション状態の参照ロジック修正。

### 2.4 テスト (Tests)
*   **`tests/XIVSubmarinesRewrite.Tests/NotionWebhookContractTests.cs`**: **ファイル削除**。
*   **`tests/XIVSubmarinesRewrite.Tests/NotificationCoordinatorForceImmediateTests.cs`**:
    *   `RecordingNotionClient` モッククラス削除。
    *   `NotificationCoordinator` コンストラクタ修正。
*   **`tests/XIVSubmarinesRewrite.Tests/NotificationWebhookValidatorTests.cs`**:
    *   `NotionValidator_AllowsHttpsAnyDomain` テストメソッド削除。
*   **`tests/Playwright/notification-layout.spec.ts`**:
    *   Notion 設定フォームに関連するテストケース削除。
    *   `FormState` 型定義修正。
*   **`tests/Playwright/release-smoke.spec.ts`**:
    *   Notion 入力欄の存在確認ステップ削除。

### 2.5 ドキュメント
*   **Steering Documents**:
    *   `.kiro/steering/tech.md`: NotionWebhookClient への言及を削除。
    *   `.kiro/steering/structure.md`: ディレクトリ構造から Notion 関連ファイルを削除。
*   **Guides**:
    *   `README.md`: Notion 通知に関する記述削除。
    *   `release-package/README.md`: Notion 通知に関する記述削除。
    *   `docs/ai-development/CLAUDE.md`: NotionWebhookClient への言及を削除。
*   **Design/Analysis**:
    *   `.kiro/specs/design/discord-message-optimization.md`: 設計記述から Notion を削除（該当する場合）。
    *   `docs/analysis/log_analysis_report_v1.2.4.md`: ログ分析対象から Notion を削除。

## 3. リスク軽減策と方針
*   **ブランチ戦略**: `feature/remove-notion-v1-3-0` ブランチを作成して作業する。
*   **コミット戦略**: 各 Step 完了ごとにコミットを行い、作業の区切りを明確にする。最終的に PR を経由して `main` ブランチへ Squash Merge する。
*   **バージョン統合**: 現在のバージョン不整合（1.2.3 / 1.2.4 / 1.2.5）を解消し、`1.3.0` に統一する。

## 4. 作業手順

### Step 0: 準備
1.  Git ブランチ作成: `git checkout -b feature/remove-notion-v1-3-0`

### Step 1: Notion 専用テストの削除
1.  `tests/XIVSubmarinesRewrite.Tests/NotionWebhookContractTests.cs` を削除。
2.  `tests/XIVSubmarinesRewrite.Tests/NotificationWebhookValidatorTests.cs` から `NotionValidator_AllowsHttpsAnyDomain` を削除。
    *   *注: `NotificationCoordinatorForceImmediateTests` の修正は Step 4 で行う（コンストラクタ変更と同期させるため）。*
3.  **Commit**: `git commit -am "test: remove notion specific tests"`

### Step 2: UIコードの修正
UIコードから Notion 設定プロパティへの参照を除去する（Step 3 の準備）。
1.  `src/Presentation/Rendering/NotificationMonitorWindowRenderer.ChannelCards.cs`:
    *   `NotificationChannel` enum から `Notion` 削除。
    *   `RenderChannelCards` の `totalChannels` を 1 に変更し、ループ構造を簡素化。
    *   `RenderChannelCard` から Notion 分岐を削除。
    *   `NotificationWebhookValidator.ValidateNotion` を削除。
2.  `src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs`:
    *   `notionUrlBuffer`, `notionUrlValid` 等のフィールド削除。
    *   `RevalidateChannelUrls` 修正。
3.  `src/Presentation/Rendering/NotificationMonitorWindowRenderer.Queue.cs`:
    *   `settings.EnableNotion` 等への参照削除。
4.  `src/Presentation/Rendering/NotificationMonitorWindowRenderer.SavePanel.cs`:
    *   `notionUrlError` 等への参照削除。
5.  **Commit**: `git commit -am "ui: remove notion settings from notification window"`

### Step 3: 設定とDIの修正
設定クラスからプロパティを削除する。Step 2 で参照が消えているためビルドは通るはずである。
1.  `src/Infrastructure/Configuration/NotificationSettings.cs` から `EnableNotion`, `NotionWebhookUrl` 削除。
2.  `src/Infrastructure/Composition/PluginBootstrapper.cs` から `NotionWebhookClient` 生成・登録処理削除。
3.  **検証**: `dotnet build` を実行し、成功することを確認。
4.  **Commit**: `git commit -am "config: remove notion settings and dependency injection"`

### Step 4: 実装コードの削除と依存関係の解消
1.  **`src/Application/Services/NotificationCoordinator.cs` 修正**:
    *   `INotionClient` フィールド削除。
    *   コンストラクタ引数から `INotionClient` を削除。
    *   `PublishAlarmAsync`, `PublishVoyageCompletionAsync` から Notion 呼び出しを削除。
2.  **`tests/XIVSubmarinesRewrite.Tests/NotificationCoordinatorForceImmediateTests.cs` 修正**:
    *   `RecordingNotionClient` クラス削除。
    *   `NotificationCoordinator` コンストラクタ呼び出しから Notion モックを削除。
3.  **`src/Application/Notifications/VoyageNotificationFormatter.cs` 修正**:
    *   `CreateNotionPayload` メソッド削除。
    *   `NotionNotificationPayload` レコード削除。
4.  **`src/Integrations/Notifications/INotionClient.cs` 削除**。
5.  **`src/Integrations/Notifications/NotionWebhookClient.cs` 削除**。
6.  **検証**: `dotnet build` を実行し、成功することを確認。
7.  **Commit**: `git commit -am "feat: remove notion client implementation"`

### Step 5: Playwright テストの修正
1.  **`tests/Playwright/notification-layout.spec.ts`**:
    *   `FormState` 型から `enableNotion`, `notionUrl` を削除。
    *   `isFormValid` 関数から Notion チェックを削除。
    *   `@notification validation` ブロック内の `notion url requires https` テストを削除。
    *   その他、Notion プロパティを含むテストデータから Notion 項目を削除。
2.  **`tests/Playwright/release-smoke.spec.ts`**:
    *   `@release configuration structure validation` テスト内の Notion HTML 定義と `.notion-url` の `expect` を削除。
3.  **Commit**: `git commit -am "test: update playwright specs for notion removal"`

### Step 6: ドキュメント更新とバージョン管理
1.  **バージョン統一 (1.3.0)**:
    *   *現状: manifest.json(1.2.5), release-package/manifest.json(1.2.3), csproj(1.2.4)*
    *   `manifest.json`: AssemblyVersion を `1.3.0` に。
    *   `XIVSubmarinesRewrite.csproj`:
        *   Version: `1.3.0`
        *   FileVersion: `1.3.0.0`
        *   AssemblyVersion: `1.3.0.0`
        *   InformationalVersion: `1.3.0+build...` (日付部分は維持または更新)
    *   `release-package/manifest.json`: AssemblyVersion を `1.3.0` に。
2.  **CHANGELOG.md 更新**:
    *   `[1.3.0]` セクションを追加。
    *   `### Removed` サブセクションを追加し、以下のように記述:
        > - **Notion 通知機能の削除**: 利用頻度が低く正常に動作していなかったため、Notion 通知機能を完全に削除しました。
        >   - `NotionWebhookClient`, `INotionClient` を削除
        >   - UI から Notion 設定カードを削除
        >   - 関連テスト・ドキュメントを整理
3.  **ドキュメント更新**:
    *   `README.md`: Notion 通知に関する記述削除。
    *   `release-package/README.md`: Notion 通知に関する記述削除。
    *   `.kiro/steering/tech.md`: NotionWebhookClient への言及削除。
    *   `.kiro/steering/structure.md`: Notion 関連ファイル削除。
    *   `docs/ai-development/CLAUDE.md`: NotionWebhookClient への言及削除。
    *   `docs/analysis/log_analysis_report_v1.2.4.md`: Notion 関連の記述削除。
    *   `.kiro/specs/design/discord-message-optimization.md`: Notion 関連の記述削除。
4.  **Commit**: `git commit -am "docs: update version to 1.3.0 and remove notion references"`

### Step 7: 検証
1.  **ビルド**: `dotnet build` 成功確認。
2.  **クリーンアップ確認**:
    *   PowerShell: `Get-ChildItem -Recurse src -Filter *.cs | Select-String "Notion"` が 0 件であること。
    *   PowerShell: `Get-ChildItem -Recurse tests -Filter *.cs | Select-String "Notion"` が 0 件であること（意図的なコメントを除く）。
3.  **単体テスト**: `dotnet test` 成功確認。
4.  **UIテスト**: `npx playwright test` 成功確認。
5.  **手動検証**:
    *   ゲーム内でプラグインを起動（エラーログがないこと確認）。
    *   設定画面を開き、Notion 設定カードが表示されず、Discord カードのみであることを確認。
    *   (オプション) Discord Webhook URL を設定し、通知テストを実施。

### Step 8: リリース
1.  **PR 作成**: `gh pr create --base main --title "feat: remove notion notification (v1.3.0)" --body "Notion通知機能を削除し、バージョンを1.3.0へ更新します。"`
    *   *CLI が利用できない場合は、GitHub Web UI から作成するか、ローカルでマージする。*
2.  **マージ**: PR が承認されたら Squash Merge を行う。
    *   (Local fallback):
        ```powershell
        git checkout main
        git merge --squash feature/remove-notion-v1-3-0
        git commit -m "feat: remove notion notification (v1.3.0)"
        ```
3.  **タグ作成**: `git tag -a v1.3.0 -m "Release v1.3.0: Remove Notion notification feature"`
4.  **リリース作成**: `gh release create v1.3.0 --generate-notes`

## 5. 完了条件
*   [ ] Notion 関連コードが完全に削除されている（PowerShell grep check = 0）。
*   [ ] `dotnet build` が警告 0 で成功する。
*   [ ] `dotnet test` が全件パスする。
*   [ ] `npx playwright test` が全件パスする。
*   [ ] CHANGELOG.md に Removed セクションがあり、バージョン 1.3.0 が記載されている。
*   [ ] manifest.json, csproj, release-package/manifest.json のバージョンが全て `1.3.0` に統一されている。
