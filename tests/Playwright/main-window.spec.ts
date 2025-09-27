// apps/XIVSubmarinesRewrite/tests/Playwright/main-window.spec.ts
// メインウィンドウの主要フローを自動検証する Playwright テストです
// slash コマンドと DEV トグル、ウィンドウサイズ復元が退行しないことを確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tests/Playwright/fixtures/main-window-fixture.ts, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs

import { expect, test } from './fixtures/main-window-fixture';

test.describe('@main-window slash commands', () => {
  test('@main-window /xsr keeps overview active and /xsr notify opens notifications', async ({ app }) => {
    await app.reset();
    expect(await app.activeTab()).toBe('overview');

    await app.executeCommand('/xsr');
    expect(await app.activeTab()).toBe('overview');

    await app.executeCommand('/xsr notify');
    expect(await app.activeTab()).toBe('notifications');
  });
});

test.describe('@main-window dev tab', () => {
  test('@main-window DEV toggle shows and hides developer tab', async ({ app }) => {
    await app.reset();
    expect(await app.devVisible()).toBe(false);
    expect(await app.isTabVisible('dev')).toBe(false);

    await app.setDevVisible(true);
    expect(await app.devVisible()).toBe(true);
    expect(await app.isTabVisible('dev')).toBe(true);

    await app.setDevVisible(false);
    expect(await app.devVisible()).toBe(false);
    expect(await app.isTabVisible('dev')).toBe(false);
  });
});

test.describe('@main-window window persistence', () => {
  test('@main-window window size persists across reopen', async ({ app }) => {
    await app.reset();
    await app.setWindowSize(820, 540);

    const firstSize = await app.windowSize();
    expect(firstSize).toEqual({ width: 820, height: 540 });

    await app.reopen();
    const reopened = await app.windowSize();
    expect(reopened).toEqual({ width: 820, height: 540 });
  });
});
