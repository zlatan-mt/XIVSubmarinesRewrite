// apps/XIVSubmarinesRewrite/tests/Playwright/ui-theme.spec.ts
// UiTheme プレビューの HTML を検証する Playwright テストです
// RendererPreview が出力した色情報がテーマと一致することを自動で確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/RendererPreview/Program.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs

import { test, expect } from '@playwright/test';
import { execSync } from 'node:child_process';
import fs from 'node:fs';
import path from 'node:path';

const repoRoot = path.resolve(__dirname, '..', '..');
const artifactRoot = path.resolve(__dirname, '.artifacts');
const runRoot = path.join(artifactRoot, 'playwright');
const htmlPath = path.join(runRoot, 'report.html');
const jsonPath = path.join(runRoot, 'swatches.json');

function normalizeColor(value: string | null): string {
  if (!value) {
    return '';
  }
  return value.replace(/\s+/g, ' ').trim();
}

function harmonizeColor(value: string): string {
  const match = value.match(/^rgba\((\d+), (\d+), (\d+), ([0-9.]+)\)$/i);
  if (match) {
    const alpha = Number(match[4]);
    if (Math.abs(alpha - 1) < 1e-6) {
      return `rgb(${match[1]}, ${match[2]}, ${match[3]})`;
    }
  }
  return value;
}

test.beforeAll(() => {
  if (fs.existsSync(artifactRoot)) {
    fs.rmSync(artifactRoot, { recursive: true, force: true });
  }

  const command = `dotnet run --project tools/RendererPreview/RendererPreview.csproj -- --output=${artifactRoot} --run=playwright`;
  execSync(command, { stdio: 'inherit', cwd: repoRoot });
});

test('UiTheme colors match RendererPreview swatches', async ({ page }) => {
  expect(fs.existsSync(htmlPath)).toBe(true);
  expect(fs.existsSync(jsonPath)).toBe(true);

  const artifact = JSON.parse(fs.readFileSync(jsonPath, 'utf-8')) as { Swatches?: Array<{ token: string }>; swatches?: Array<{ token: string }> };
  const swatches = artifact.Swatches ?? artifact.swatches;
  expect(Array.isArray(swatches)).toBe(true);

  await page.goto(`file://${htmlPath}`);

  const handles = await page.$$('[data-token]');
  expect(handles.length).toBe(swatches!.length);

  for (const handle of handles) {
    const expected = harmonizeColor(normalizeColor(await handle.getAttribute('data-expected')));
    const role = await handle.getAttribute('data-role');
    const cssProperty = role === 'background' ? 'backgroundColor' : 'color';
    const computed = harmonizeColor(normalizeColor(await handle.evaluate((node, propertyName) => {
      const style = window.getComputedStyle(node as HTMLElement);
      return (style as any)[propertyName as string] as string;
    }, cssProperty)));

    expect(expected).not.toBe('');
    expect(computed).toBe(expected);
  }
});
