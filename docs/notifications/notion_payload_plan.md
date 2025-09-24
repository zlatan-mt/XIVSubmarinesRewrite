<!-- apps/XIVSubmarinesRewrite/docs/notifications/notion_payload_plan.md -->
<!-- Notion 通知ペイロード拡張の検討メモ -->
<!-- 互換性を崩さずに追加フィールドを定義する案を整理するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs, apps/XIVSubmarinesRewrite/src/Integrations/Notifications/NotionWebhookClient.cs -->

# Notion Payload Extension Plan

## Current Fields (2025-09-23)
- `Character`: `<Name>@<World>`
- `Submarine`: Friendly name.
- `Route`: Raw route identifier string.
- `DepartureUtc`: ISO8601 UTC timestamp.
- `ArrivalUtc`: ISO8601 UTC timestamp.
- `Duration`: Human readable duration (JA locale).
- `Status`: `Completed` / `Underway` / `Failed`.
- `Confidence`: Acquisition confidence label.
- `Hash`: Deduplication key.

## Implemented Additions (2025-09-23)
- `RouteDisplay`: Humanized route label resolved through `RouteCatalog`.
- `ArrivalLocal`: Local time string (ISO8601) for user-friendly logs.
- `Remaining`: Friendly remaining/elapsed text mirroring Discord 表示。

## Deferred Consideration
- `CompletedAt`: May duplicate `ArrivalUtc`; revisit if downstream tools require separate semantics.

## Compatibility Notes
- Existing Zap integrations expect the current casing; new fields will be additive and optional.
- Payload size remains small (<1 KB). JSON casing will stay camelCase when serialized via `JsonSerializerOptions(JsonSerializerDefaults.Web)`.
- Backwards compatibility ensured by avoiding removal/renaming of existing keys.

## Next Steps (Phase 2)
1. Done (2025-09-23): `VoyageNotificationFormatter.CreateNotionPayload` populates `RouteDisplay` / `ArrivalLocal` / `Remaining`。
2. Extend `docs/notifications/notion_payload.json` with additional edge-case samples (例: `NotifyVoyageCompleted=false`).
3. Manually verify with staging Zap to confirm no breakage。

## 2025-09-24 残タスクメモ
- ステージング Zap のページデータベースで `Remaining` カラムをテンポラリ表示させ、Webhook 受信直後の値をスクリーンショット取得する。
- Zap 履歴の raw JSON をダウンロードし、`ArrivalLocal` と `Remaining` のフォーマットが ISO8601 / `PT#H#M` 表記かを確認し、差異があればここに記録する。
- 互換性問題が無い場合は `docs/notifications/notion_payload.json` にサンプルを追記し、sessions ノートへ検証完了を記録する。
