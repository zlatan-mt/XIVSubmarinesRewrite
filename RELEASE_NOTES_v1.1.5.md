# XIV Submarines Rewrite v1.1.5 リリースノート

**リリース日**: 2025年10月25日  
**対応API**: Dalamud API Level 13  
**フレームワーク**: .NET 9.0-windows

---

## 🎯 このリリースについて

v1.1.5では、Discord通知の使いやすさを大幅に改善しました。ユーザーフィードバックに基づき、通知の簡素化とリマインダー機能の追加を行いました。

**主な改善点**:
- 航海完了通知の廃止（出航時の通知で帰還予定が分かります）
- 通知フォーマットの最適化（メッセージサイズ67%削減）
- Discord Reminder Bot連携（オプション）

---

## ✨ 新機能

### Discord Reminder Bot 連携

通知設定に「リマインダーコマンドを含める」オプションを追加しました。

**使い方**:

1. **設定を有効化**  
   Notification タブの設定で「リマインダーコマンドを含める」をオンにします。

2. **チャンネル名を設定**  
   リマインダーを送信するDiscordチャンネル名を入力します（例: `#submarine`）。

3. **コマンドをコピー**  
   Discord通知を受け取ったら、「リマインダー設定」フィールドに表示されるコマンドをコピーします。

4. **Discordで実行**  
   コピーしたコマンドをDiscordで貼り付けて実行します。

**例**:
```
/remind #submarine 10/26 14:30 Sub-1が帰還
```

これにより、[Discord Reminder Bot](https://reminder-bot.com/) が帰還予定時刻に自動でリマインダーを送信します。

**注意**: Reminder Botがサーバーに導入されている必要があります。

---

## 🔄 変更内容

### 航海完了通知の廃止

「航海完了を通知」機能は廃止されました。

**理由**:
- ユーザーは帰還したことではなく、**いつ帰還するか**を知りたい
- 出航時の通知に帰還予定時刻が含まれるため、完了通知は冗長
- 通知量が50%削減され、Discordチャンネルの可読性が向上

**移行ガイド**:
- 既存設定の `NotifyVoyageCompleted` は無視されます（エラーは発生しません）
- 設定UIから「航海完了を通知」チェックボックスは削除されています
- 「出港直後を通知」のみが有効な設定です

---

### 通知フォーマットの最適化

Discord通知のフォーマットを大幅に簡素化しました。

#### 単体通知（変更前）
```
タイトル: Submarine Voyage - Completed
フィールド:
  潜水艦: Sub-1
  状態: 完了
  航路: Sea of Ash 1
  帰還時刻: 2025/10/26(土) 14:30
  出航時刻: 2025/10/25(金) 02:30
  所要時間: 12時間
```
**6フィールド、約120文字**

#### 単体通知（変更後）
```
タイトル: Sub-1 出航
説明: Sea of Ash 1
フィールド:
  帰還予定: 10/26(土) 14:30 (12h)
```
**1フィールド、約40文字（67%削減）**

---

#### バッチ通知（4隻、変更前）
```
タイトル: Character - 4 Submarines Underway
フィールド:
  Sub-1: Sea of Ash 1
  → 帰還: 10/26 14:30
  Sub-2: Route-B
  → 帰還: 10/26 15:00
  Sub-3: Route-C
  → 帰還: 10/26 16:30
  Sub-4: Route-D
  → 帰還: 10/26 18:00
```
**12行**

#### バッチ通知（4隻、変更後）
```
タイトル: Mona - 4隻出航
フィールド:
  Sub-1: 10/26 14:30 (12h)
  Sub-2: 10/26 15:00 (12.5h)
  Sub-3: 10/26 16:30 (14h)
  Sub-4: 10/26 18:00 (15.5h)
```
**4行（67%削減）**

---

### 時刻表示の改善

残り時間の表示を簡潔にしました。

| 残り時間 | 旧フォーマット | 新フォーマット |
|---------|--------------|--------------|
| 12時間 | `12:00:00` | `12h` |
| 30分 | `00:30:00` | `30m` |
| 12時間30分 | `12:30:00` | `12.5h` |
| 14日以上 | `14 days+` | `14d+` |

---

## 🗑️ 廃止機能

### NotifyVoyageCompleted 設定

`NotificationSettings.NotifyVoyageCompleted` プロパティは `[Obsolete]` としてマークされました。

**影響**:
- 設定ファイルに残っていても、読み込み時にエラーは発生しません
- UIから設定を変更する手段は削除されています
- 内部的には常に無視されます

**推奨**:
- 新規インストールでは影響ありません
- 既存ユーザーは「出港直後を通知」の設定を確認してください

---

## 🧪 テスト強化

Phase 13向けに16個のテストケースを追加しました。

### 単体テスト（8ケース）
- `VoyageNotificationFormatterTests.cs`
  - Underway通知のフォーマット検証
  - Completed通知の拒否検証
  - リマインダーフィールドの有効/無効
  - バッチ通知のフォーマット検証

### 統合テスト（3ケース）
- `VoyageCompletionProjectionPhase13Tests.cs`
  - Completed航海の通知スキップ
  - Underway航海の正常通知
  - 状態遷移の正常動作

### E2Eテスト（5ケース）
- `discord-notification-phase13.spec.ts`
  - UI要素の表示/非表示
  - リマインダー設定フロー
  - 設定の保存と復元

---

## 📦 インストール・アップグレード

### 新規インストール

Dalamud Plugin Installer から直接インストールできます。

1. カスタムリポジトリを追加:
   ```
   https://raw.githubusercontent.com/zlatan-mt/XIVSubmarinesRewrite/release/repo.json
   ```

2. プラグインインストーラーで「XIV Submarines Rewrite」を検索してインストール

### アップグレード

既存ユーザーは自動的にv1.1.5に更新されます。

**確認手順**:
1. Dalamudのプラグインリストで「XIV Submarines Rewrite」のバージョンを確認
2. v1.1.5になっていればOK
3. 設定画面で「航海完了を通知」チェックボックスが削除されていることを確認

---

## ⚠️ 既知の問題

- なし（v1.1.5リリース時点）

---

## 🔗 関連リンク

- **リポジトリ**: https://github.com/zlatan-mt/XIVSubmarinesRewrite
- **Discord Reminder Bot**: https://reminder-bot.com/
- **前回リリース**: v1.1.4 (2025-10-04)

---

## 📝 開発者向け情報

### Phase 13実装サマリー

**変更ファイル**:
- `src/Application/Notifications/VoyageCompletionProjection.cs`
- `src/Application/Notifications/VoyageNotificationFormatter.cs`
- `src/Infrastructure/Configuration/NotificationSettings.cs`
- `src/Presentation/Rendering/NotificationMonitorWindowRenderer.*.cs`

**コミット数**: 8コミット  
**追加テスト**: 16ケース  
**開発期間**: 1日

詳細は `CLAUDE.md` の "Phase 13: Discord Notification Optimization" セクションを参照してください。

---

## 💬 フィードバック

バグ報告や機能要望は、GitHubのIssuesまたはDiscussionsでお寄せください。

**改善事例**:  
> "帰還したことは通知する必要はない。出向させたときに、「次に帰還するのはいつか？」が分かれば良い"

このユーザーフィードバックがPhase 13の設計を導きました。皆様のご意見をお待ちしています。

---

**Enjoy your submarine voyages!** 🚢

