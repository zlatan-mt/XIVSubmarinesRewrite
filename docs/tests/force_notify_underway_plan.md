<!-- apps/XIVSubmarinesRewrite/docs/tests/force_notify_underway_plan.md -->
<!-- ForceNotifyUnderway のユニットテスト設計メモ -->
<!-- 再発防止のために想定ケースを整理する目的で存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs -->

# ForceNotifyUnderway Test Scenarios

## Goals
- Guard against duplicate notifications when cooldown is active.
- Ensure arrival ETA changes trigger immediate resend.
- Verify cooldown resets after voyage completion.

## Key Scenarios
1. **First Detection**
   - Underway voyage enters tracking.
   - Expect `TryEnqueue` invoked once and `forceNotifyStates` populated.

2. **Cooldown Skip**
   - Subsequent snapshot within 5 minutes of previous notify.
   - Expect no queue enqueue and trace log with remaining minutes.

3. **ETA Change**
   - Arrival UTC changes while cooldown active.
   - Expect immediate enqueue and cooldown reset.

4. **Cooldown Expiry**
   - Advance time past `ForceNotifyCooldownWindow` (30 min).
   - Expect enqueue and state refreshed.

5. **Completion Reset**
   - Voyage status transitions to `Completed`.
   - ForceNotify state cleared; next Underway should trigger again.

## Implementation Notes
- Use `TestClock` abstraction or inject `DateTimeProvider` via constructor (Phase 2 change).
- Mock `INotificationQueue` and assert `TryEnqueue` call counts per scenario.
- Leverage existing `SnapshotBuilder` helpers (if unavailable, add lightweight factory in tests).

## Open Questions
- How to simulate logging for assertions without coupling? (Consider `TestLogSink`).
- Do we need to simulate multiple submarines concurrently? (Potential additional scenario.)

## Next Actions
- Add clock abstraction to `VoyageCompletionProjection` constructor (Phase 2).
- Draft xUnit parameterized tests covering scenarios 1–4, optional 5.
- Extend test data builders for `AcquisitionSnapshot` to simplify setup.
