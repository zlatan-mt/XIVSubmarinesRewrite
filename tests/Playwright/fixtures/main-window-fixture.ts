// apps/XIVSubmarinesRewrite/tests/Playwright/fixtures/main-window-fixture.ts
// メインウィンドウを模した軽量ハーネスを提供する Playwright フィクスチャです
// slash コマンドと DEV トグルの切替を自動テストで検証するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tests/Playwright/main-window.spec.ts, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs

import { expect, test as base } from '@playwright/test';
import type { Page } from '@playwright/test';

export type MainWindowFixtures = {
  app: MainWindowHarness;
};

export const test = base.extend<MainWindowFixtures>({
  app: async ({ page }, use) => {
    const harness = new MainWindowHarness(page);
    await harness.initialize();
    await use(harness);
    await harness.dispose();
  },
});

export { expect };

class MainWindowHarness {
  constructor(private readonly page: Page) {}

  async initialize(): Promise<void> {
    await this.page.setContent(this.buildDocument(), { waitUntil: 'domcontentloaded' });
    await this.reset();
  }

  async dispose(): Promise<void> {
    await this.page.evaluate(() => {
      const app = (window as any).mainWindowApp;
      if (app && typeof app.reset === 'function') {
        app.reset();
      }
    });
  }

  async reset(): Promise<void> {
    await this.page.evaluate(() => {
      const app = (window as any).mainWindowApp;
      if (app && typeof app.reset === 'function') {
        app.reset();
      }
    });
  }

  async executeCommand(command: string): Promise<void> {
    await this.page.evaluate((cmd) => {
      const app = (window as any).mainWindowApp;
      app.runCommand(cmd);
    }, command);
  }

  async activeTab(): Promise<string> {
    return this.page.evaluate(() => {
      const app = (window as any).mainWindowApp;
      return app.activeTab();
    });
  }

  async setDevVisible(visible: boolean): Promise<void> {
    await this.page.evaluate((value) => {
      const app = (window as any).mainWindowApp;
      app.setDevVisible(value);
    }, visible);
  }

  async devVisible(): Promise<boolean> {
    return this.page.evaluate(() => {
      const app = (window as any).mainWindowApp;
      return app.isDevVisible();
    });
  }

  async isTabVisible(tabId: string): Promise<boolean> {
    return this.page.evaluate((id) => {
      const element = document.getElementById(`tab-${id}`);
      if (!element) {
        return false;
      }
      return element.dataset.visible === 'true';
    }, tabId);
  }

  async setWindowSize(width: number, height: number): Promise<void> {
    await this.page.evaluate(({ w, h }) => {
      const app = (window as any).mainWindowApp;
      app.setWindowSize(w, h);
    }, { w: width, h: height });
  }

  async windowSize(): Promise<{ width: number; height: number }> {
    return this.page.evaluate(() => {
      const app = (window as any).mainWindowApp;
      return app.getWindowSize();
    });
  }

  async reopen(): Promise<void> {
    await this.page.evaluate(() => {
      const app = (window as any).mainWindowApp;
      app.reopen();
    });
  }

  private buildDocument(): string {
    return `<!DOCTYPE html>
<html lang="ja">
<head>
  <meta charset="utf-8" />
  <title>XSR Main Window Harness</title>
  <style>
    body { font-family: 'Segoe UI', sans-serif; margin: 0; background: #111; color: #f5f5f5; }
    #frame { margin: 24px auto; border-radius: 16px; padding: 16px; background: #18181f; box-shadow: 0 12px 24px rgba(0, 0, 0, 0.4); }
    #tabs { display: flex; gap: 8px; margin-bottom: 16px; }
    button[data-role="tab"] { padding: 8px 16px; border-radius: 999px; border: none; cursor: pointer; background: rgba(255,255,255,0.08); color: inherit; }
    button[data-role="tab"][data-active="true"] { background: rgba(64,128,255,0.65); }
    button[data-role="tab"][data-visible="false"] { display: none; }
    #content { padding: 16px; border-radius: 12px; background: rgba(255,255,255,0.05); }
    #meta { font-size: 12px; opacity: 0.75; margin-top: 12px; }
  </style>
</head>
<body>
  <section id="frame" data-width="960" data-height="640">
    <nav id="tabs"></nav>
    <main id="content"></main>
    <p id="meta"></p>
  </section>
  <script>
    (function () {
      const TAB_STORAGE_KEY = 'xsr.window.size';
      const DEFAULT_SIZE = { width: 960, height: 640 };
      const memoryStorage = new Map();

      function readStorage(key) {
        try {
          return window.localStorage.getItem(key);
        } catch (error) {
          console.warn('[Harness] localStorage unavailable, using in-memory storage', error);
          return memoryStorage.has(key) ? memoryStorage.get(key) : null;
        }
      }

      function writeStorage(key, value) {
        try {
          window.localStorage.setItem(key, value);
        } catch (error) {
          console.warn('[Harness] localStorage unavailable, using in-memory storage', error);
          memoryStorage.set(key, value);
        }
      }

      function clearStorage(key) {
        try {
          window.localStorage.removeItem(key);
        } catch (error) {
          console.warn('[Harness] localStorage unavailable, using in-memory storage', error);
          memoryStorage.delete(key);
        }
      }
      const state = {
        activeTab: 'overview',
        devVisible: false,
        size: loadSize(),
      };
      const tabs = [
        { id: 'overview', label: 'Overview', command: '/xsr' },
        { id: 'notifications', label: 'Notifications', command: '/xsr notify' },
        { id: 'dev', label: 'Dev Tools', command: null, devOnly: true }
      ];
      const frame = document.getElementById('frame');
      const tabsContainer = document.getElementById('tabs');
      const content = document.getElementById('content');
      const meta = document.getElementById('meta');

      function loadSize() {
        const raw = readStorage(TAB_STORAGE_KEY);
        if (!raw) return { ...DEFAULT_SIZE };
        try {
          const parsed = JSON.parse(raw);
          if (typeof parsed.width === 'number' && typeof parsed.height === 'number') {
            return { width: parsed.width, height: parsed.height };
          }
        } catch (error) {
          console.warn('[Harness] Failed to parse stored size', error);
        }
        return { ...DEFAULT_SIZE };
      }

      function saveSize() {
        writeStorage(TAB_STORAGE_KEY, JSON.stringify(state.size));
      }

      function render() {
        frame.dataset.width = String(state.size.width);
        frame.dataset.height = String(state.size.height);
        frame.style.width = state.size.width + 'px';
        frame.style.height = state.size.height + 'px';

        tabsContainer.innerHTML = '';
        tabs.forEach((tab) => {
          const button = document.createElement('button');
          button.id = 'tab-' + tab.id;
          button.dataset.role = 'tab';
          button.dataset.tab = tab.id;
          button.dataset.active = (state.activeTab === tab.id).toString();
          const visible = !tab.devOnly || state.devVisible;
          button.dataset.visible = visible.toString();
          if (!visible) {
            button.style.display = 'none';
          } else {
            button.style.display = 'inline-flex';
          }
          button.textContent = tab.label;
          button.addEventListener('click', () => {
            if (tab.command) {
              window.mainWindowApp.runCommand(tab.command);
            } else {
              state.activeTab = tab.id;
              render();
            }
          });
          tabsContainer.appendChild(button);
        });

        content.textContent = 'Active tab: ' + state.activeTab;
        meta.textContent = 'Window ' + state.size.width + '×' + state.size.height + (state.devVisible ? ' (DEV visible)' : '');
      }

      window.mainWindowApp = {
        runCommand(command) {
          if (command === '/xsr notify') {
            state.activeTab = 'notifications';
          } else {
            state.activeTab = 'overview';
          }
          render();
        },
        setDevVisible(value) {
          state.devVisible = Boolean(value);
          render();
        },
        toggleDev() {
          state.devVisible = !state.devVisible;
          render();
        },
        isDevVisible() {
          return state.devVisible;
        },
        activeTab() {
          return state.activeTab;
        },
        setWindowSize(width, height) {
          const nextWidth = Number(width);
          const nextHeight = Number(height);
          state.size = {
            width: Number.isFinite(nextWidth) && nextWidth > 0 ? nextWidth : state.size.width,
            height: Number.isFinite(nextHeight) && nextHeight > 0 ? nextHeight : state.size.height,
          };
          saveSize();
          render();
        },
        reopen() {
          state.size = loadSize();
          render();
        },
        getWindowSize() {
          return { ...state.size };
        },
        hasDevTab() {
          const element = document.getElementById('tab-dev');
          return !!element && element.dataset.visible === 'true';
        },
        reset() {
          clearStorage(TAB_STORAGE_KEY);
          state.activeTab = 'overview';
          state.devVisible = false;
          state.size = { ...DEFAULT_SIZE };
          render();
        },
      };

      render();
    })();
  </script>
</body>
</html>`;
  }
}
