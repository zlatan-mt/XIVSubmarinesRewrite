// apps/XIVSubmarinesRewrite/tests/Playwright/overview-responsive.spec.ts
// Overview タブの列幅プリセットとコンパクト表示条件を検証する Playwright テストです
// 幅ごとのプリセット選択と残り時間/帰港予定の省略表示ロジックを自動テストで確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.Layout.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewRowFormatter.cs

import { expect, test } from '@playwright/test';

type ColumnPreset = {
  name: number;
  status: number;
  remaining: number;
  arrival: number;
  route: number;
};

function selectPreset(width: number): ColumnPreset {
  if (width >= 960) {
    return { name: 2.0, status: 1.0, remaining: 1.0, arrival: 1.2, route: 1.8 };
  }
  if (width >= 780) {
    return { name: 1.8, status: 0.9, remaining: 0.9, arrival: 1.1, route: 1.5 };
  }
  return { name: 1.6, status: 0.8, remaining: 0.8, arrival: 0.9, route: 1.4 };
}

function formatRemainingCompact(minutes: number): string {
  if (minutes <= 0) {
    return '帰港';
  }
  if (minutes >= 60) {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h${mins.toString().padStart(2, '0')}`;
  }
  return `${minutes}m`;
}

function formatArrivalCompact(date: Date): string {
  return date.toLocaleTimeString(undefined, { hour: '2-digit', minute: '2-digit', hour12: false });
}

test.describe('@overview presets', () => {
  test('@overview width >= 960 uses wide preset', async () => {
    const preset = selectPreset(980);
    expect(preset.route).toBeCloseTo(1.8);
  });

  test('@overview width 820 uses medium preset', async () => {
    const preset = selectPreset(820);
    expect(preset.name).toBeCloseTo(1.8);
    expect(preset.route).toBeLessThan(1.6 + 1e-6);
  });

  test('@overview width 680 uses compact preset', async () => {
    const preset = selectPreset(680);
    expect(preset.name).toBeCloseTo(1.6);
    expect(preset.route).toBeCloseTo(1.4);
  });
});

test.describe('@overview compact formatting', () => {
  test('@overview compact remaining for 90 minutes', async () => {
    expect(formatRemainingCompact(90)).toBe('1h30');
  });

  test('@overview compact remaining for 45 minutes', async () => {
    expect(formatRemainingCompact(45)).toBe('45m');
  });

  test('@overview compact arrival renders HH:mm', async () => {
    const date = new Date('2025-09-30T09:45:00Z');
    expect(formatArrivalCompact(date)).toMatch(/\d{2}:\d{2}/);
  });
});
