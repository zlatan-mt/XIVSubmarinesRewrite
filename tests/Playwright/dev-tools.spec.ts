// apps/XIVSubmarinesRewrite/tests/Playwright/dev-tools.spec.ts
// DEV タブの操作ログモデルを簡易シミュレーションする Playwright テストです
// 手動発火ログが 10 件で打ち切られることとサマリ生成が安定することを確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/IForceNotifyDiagnostics.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs

import { test, expect } from '@playwright/test';

type ManualTrigger = {
  triggeredAtUtc: Date;
  characterId: number;
  notificationsEnqueued: number;
};

function trimManualLog(entries: ManualTrigger[], limit = 10): ManualTrigger[] {
  if (entries.length <= limit) {
    return entries;
  }

  return entries.slice(-limit);
}

function buildSummary(entry: ManualTrigger): string {
  const formatted = entry.triggeredAtUtc.toISOString();
  return entry.notificationsEnqueued > 0
    ? `${formatted}: ${entry.notificationsEnqueued} 件を再送`
    : `${formatted}: 再送対象なし`;
}

test.describe('@dev manual trigger log', () => {
  test('@dev keeps only the latest ten entries', () => {
    const entries = Array.from({ length: 18 }, (_, index) => ({
      triggeredAtUtc: new Date(1_600_000_000_000 + index * 1_000),
      characterId: index,
      notificationsEnqueued: index % 3,
    }));

    const trimmed = trimManualLog(entries);
    expect(trimmed).toHaveLength(10);
    expect(trimmed[0].characterId).toBe(8);
    expect(trimmed[trimmed.length - 1].characterId).toBe(17);
  });

  test('@dev summary string reflects enqueue count', () => {
    const zero = buildSummary({
      triggeredAtUtc: new Date('2025-10-01T12:34:56Z'),
      characterId: 1,
      notificationsEnqueued: 0,
    });
    expect(zero).toContain('再送対象なし');

    const multiple = buildSummary({
      triggeredAtUtc: new Date('2025-10-01T12:34:56Z'),
      characterId: 1,
      notificationsEnqueued: 3,
    });
    expect(multiple).toContain('3 件を再送');
  });
});
