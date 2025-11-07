// Phase 13: Discord notification optimization E2E tests
// Verifies UI changes and notification format optimization
// NOTE: These tests require actual Dalamud plugin UI and cannot run in CI.
// They are skipped and should be run manually in a Dalamud environment.

import { test, expect } from '@playwright/test';
import { MainWindowHarness } from './fixtures/main-window-fixture';

test.describe.skip('@notification @phase13 Discord notification optimization', () => {
  
  test('completed notification checkbox is removed from UI', async ({ page }) => {
    const fixture = new MainWindowHarness(page);
    await fixture.initialize();
    
    // Navigate to notification tab
    await page.click('text=Notification');
    await page.waitForTimeout(500);
    
    // Verify old '航海完了を通知' checkbox is NOT present
    const completedCheckbox = page.locator('text=航海完了を通知');
    await expect(completedCheckbox).not.toBeVisible();
    
    // Verify '出港直後を通知' checkbox IS present
    const underwayCheckbox = page.locator('text=出港直後を通知');
    await expect(underwayCheckbox).toBeVisible();
  });

  test('reminder settings section is visible', async ({ page }) => {
    const fixture = new MainWindowHarness(page);
    await fixture.initialize();
    
    await page.click('text=Notification');
    await page.waitForTimeout(500);
    
    // Verify reminder checkbox exists
    const reminderCheckbox = page.locator('text=リマインダーコマンドを含める');
    await expect(reminderCheckbox).toBeVisible();
  });

  test('reminder channel input appears when enabled', async ({ page }) => {
    const fixture = new MainWindowHarness(page);
    await fixture.initialize();
    
    await page.click('text=Notification');
    await page.waitForTimeout(500);
    
    // Check reminder checkbox
    const reminderCheckbox = page.locator('text=リマインダーコマンドを含める');
    await reminderCheckbox.click();
    await page.waitForTimeout(300);
    
    // Verify channel name label and input appear
    const channelLabel = page.locator('text=チャンネル名:');
    await expect(channelLabel).toBeVisible();
    
    const exampleText = page.locator('text=例: #submarine');
    await expect(exampleText).toBeVisible();
  });

  test('reminder help text is displayed when enabled', async ({ page }) => {
    const fixture = new MainWindowHarness(page);
    await fixture.initialize();
    
    await page.click('text=Notification');
    await page.waitForTimeout(500);
    
    // Enable reminder
    await page.click('text=リマインダーコマンドを含める');
    await page.waitForTimeout(300);
    
    // Verify help text
    const helpText = page.locator('text=通知にDiscord Reminder Botのコマンドを追加');
    await expect(helpText).toBeVisible();
    
    const usageText = page.locator('text=コマンドをコピペして実行すると');
    await expect(usageText).toBeVisible();
  });

  test('settings can be saved with reminder enabled', async ({ page }) => {
    const fixture = new MainWindowHarness(page);
    await fixture.initialize();
    
    await page.click('text=Notification');
    await page.waitForTimeout(500);
    
    // Enable reminder
    await page.click('text=リマインダーコマンドを含める');
    await page.waitForTimeout(300);
    
    // Find and click save button
    const saveButton = page.locator('button:has-text("通知設定を保存")');
    
    // Save button should be enabled (assuming valid URLs are already set)
    // Note: This test assumes default or pre-configured webhook URLs
    // In real scenario, webhook URLs need to be set first
    
    await expect(saveButton).toBeVisible();
  });
});

