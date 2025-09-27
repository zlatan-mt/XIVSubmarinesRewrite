// apps/XIVSubmarinesRewrite/tests/Playwright/playwright.config.ts
// Playwright テストの共通設定です
// UiTheme プレビューの検証を Node 環境で安定して実行するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tests/Playwright/ui-theme.spec.ts, apps/XIVSubmarinesRewrite/tools/RendererPreview/Program.cs

import { defineConfig } from '@playwright/test';

export default defineConfig({
  testDir: './',
  timeout: 30_000,
  retries: 0,
  use: {
    viewport: { width: 1280, height: 720 },
    colorScheme: 'dark',
  },
});
