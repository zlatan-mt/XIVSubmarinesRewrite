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

## Deferred Consideration
- `CompletedAt`: May duplicate `ArrivalUtc`; revisit if downstream tools require separate semantics.

## Compatibility Notes
- Existing Zap integrations expect the current casing; new fields will be additive and optional.
- Payload size remains small (<1 KB). JSON casing will stay camelCase when serialized via `JsonSerializerOptions(JsonSerializerDefaults.Web)`.
- Backwards compatibility ensured by avoiding removal/renaming of existing keys.

## Next Steps (Phase 2)
1. Update `VoyageNotificationFormatter.CreateNotionPayload` to populate new keys.
2. Extend `docs/notifications/notion_payload.json` with sample output for validation.
3. Manually verify with staging Zap to confirm no breakage.
