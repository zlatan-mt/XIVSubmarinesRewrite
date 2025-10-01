// apps/XIVSubmarinesRewrite/tests/Playwright/ui-theme.spec.ts
// UiTheme プレビューの HTML を検証する Playwright テストです
// RendererPreview が出力した色情報がテーマと一致することを自動で確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tests/Playwright/utils/renderer-preview.ts, apps/XIVSubmarinesRewrite/tools/RendererPreview/Program.cs

import { test, expect } from '@playwright/test';
import fs from 'node:fs';
import { generateRendererPreviewArtifacts, type RendererPreviewArtifacts } from './utils/renderer-preview';

let artifacts: RendererPreviewArtifacts;
type SwatchRecord = {
  Token?: string;
  token?: string;
  Hex?: string;
  hex?: string;
  ReferenceHex?: string | null;
  referenceHex?: string | null;
  ReferenceRgba?: string | null;
  referenceRgba?: string | null;
};

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
  artifacts = generateRendererPreviewArtifacts({ runName: 'ui-theme' });
});

test('@theme UiTheme colors match RendererPreview swatches', async ({ page }) => {
  expect(fs.existsSync(artifacts.htmlPath)).toBe(true);
  expect(fs.existsSync(artifacts.jsonPath)).toBe(true);

  const artifact = JSON.parse(fs.readFileSync(artifacts.jsonPath, 'utf-8')) as { Swatches?: SwatchRecord[]; swatches?: SwatchRecord[] };
  const swatches = artifact.Swatches ?? artifact.swatches;
  expect(Array.isArray(swatches)).toBe(true);

  const referenceByToken = new Map<string, SwatchRecord>();
  for (const entry of swatches ?? []) {
    const token = (entry.Token ?? entry.token) ?? '';
    referenceByToken.set(token, entry);
  }

  await page.goto(`file://${artifacts.htmlPath}`);

  const handles = await page.$$('[data-token]');
  expect(handles.length).toBe(referenceByToken.size);

  for (const handle of handles) {
    const token = await handle.getAttribute('data-token');
    expect(token).not.toBeNull();
    const reference = referenceByToken.get(token!);
    expect(reference).toBeDefined();

    const expected = harmonizeColor(normalizeColor(await handle.getAttribute('data-expected')));
    const role = await handle.getAttribute('data-role');
    const cssProperty = role === 'background' ? 'backgroundColor' : 'color';
    const computed = harmonizeColor(normalizeColor(await handle.evaluate((node, propertyName) => {
      const style = window.getComputedStyle(node as HTMLElement);
      return (style as any)[propertyName as string] as string;
    }, cssProperty)));

    expect(expected).not.toBe('');
    expect(computed).toBe(expected);

    const figmaRgba = harmonizeColor(normalizeColor(await handle.getAttribute('data-figma-rgba')));
    if (figmaRgba) {
      expect(expected).toBe(figmaRgba);
    }

    const referenceHex = normalizeColor((reference?.ReferenceHex ?? reference?.referenceHex) ?? null);
    if (referenceHex) {
      const expectedHex = normalizeColor((reference?.Hex ?? reference?.hex) ?? null);
      if (expectedHex) {
        expect(referenceHex.toUpperCase()).toBe(expectedHex.toUpperCase());
      }
    }
  }
});
