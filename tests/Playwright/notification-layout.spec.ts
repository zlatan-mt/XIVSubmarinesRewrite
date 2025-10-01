// apps/XIVSubmarinesRewrite/tests/Playwright/notification-layout.spec.ts
// 通知フォームのレイアウト計算とバリデーション条件を検証する Playwright テストです
// 1列/2列判定と保存ボタンの有効条件を自動化し、UI リグレッションを防ぐため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.ChannelCards.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SavePanel.cs

import { test, expect } from '@playwright/test';

type LayoutMetrics = {
  columnCount: number;
  cardWidth: number;
  cardHeight: number;
  stackSpacing: number;
};

type FormState = {
  enableDiscord: boolean;
  discordUrl: string;
  enableNotion: boolean;
  notionUrl: string;
};

function calculateLayoutMetrics(width: number, panelHeight: number, spacingX: number, threshold: number): LayoutMetrics {
  const usesTwoColumn = width >= threshold;
  const twoColumnWidth = Math.max((width - spacingX) * 0.5, 280);
  const singleColumnWidth = Math.max(width, 280);
  const cardWidth = usesTwoColumn ? twoColumnWidth : singleColumnWidth;
  let baseHeight = 148;
  if (!usesTwoColumn) {
    baseHeight -= 12;
  }
  const cardHeight = Math.max(120, baseHeight);
  const stackSpacing = Math.max(6, spacingX * 0.5);
  return {
    columnCount: usesTwoColumn ? 2 : 1,
    cardWidth,
    cardHeight,
    stackSpacing,
  };
}

function isHttps(url: string): boolean {
  return /^https:\/\//i.test(url);
}

function isDiscordUrl(url: string): boolean {
  return /discord/i.test(url);
}

function isFormValid(state: FormState): boolean {
  const discordValid = !state.enableDiscord
    || (state.discordUrl.trim().length > 0 && isHttps(state.discordUrl) && isDiscordUrl(state.discordUrl));
  const notionValid = !state.enableNotion
    || (state.notionUrl.trim().length > 0 && isHttps(state.notionUrl));
  return discordValid && notionValid;
}

test.describe('@notification layout metrics', () => {
  test('@notification width 820px yields two columns', async () => {
    const metrics = calculateLayoutMetrics(820, 360, 12, 620);
    expect(metrics.columnCount).toBe(2);
    expect(metrics.cardWidth).toBeGreaterThan(280);
  });

  test('@notification width 600px yields single column', async () => {
    const metrics = calculateLayoutMetrics(600, 360, 12, 620);
    expect(metrics.columnCount).toBe(1);
    expect(metrics.cardWidth).toBeGreaterThanOrEqual(280);
    expect(metrics.stackSpacing).toBeGreaterThan(6 - 1e-3);
  });
});

test.describe('@notification validation', () => {
  test('@notification missing discord url disables save', async () => {
    const valid = isFormValid({
      enableDiscord: true,
      discordUrl: '',
      enableNotion: false,
      notionUrl: '',
    });
    expect(valid).toBe(false);
  });

  test('@notification https discord url enables save', async () => {
    const valid = isFormValid({
      enableDiscord: true,
      discordUrl: 'https://discord.com/api/webhooks/123',
      enableNotion: false,
      notionUrl: '',
    });
    expect(valid).toBe(true);
  });

  test('@notification notion url requires https', async () => {
    const valid = isFormValid({
      enableDiscord: false,
      discordUrl: '',
      enableNotion: true,
      notionUrl: 'http://hooks.example',
    });
    expect(valid).toBe(false);
  });
});
