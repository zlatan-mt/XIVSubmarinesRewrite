// apps/XIVSubmarinesRewrite/tests/Playwright/utils/renderer-preview.ts
// RendererPreview CLI を呼び出して Playwright で利用するアーティファクトを生成します
// UI テーマ検証を安定させるため、成果物の作成とパス解決を共通化する目的で存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tests/Playwright/ui-theme.spec.ts, apps/XIVSubmarinesRewrite/tools/RendererPreview/Program.cs

import { spawnSync } from 'node:child_process';
import fs from 'node:fs';
import path from 'node:path';

export type RendererPreviewArtifacts = {
  readonly runRoot: string;
  readonly htmlPath: string;
  readonly jsonPath: string;
};

export type RendererPreviewOptions = {
  readonly runName?: string;
  readonly outputRoot?: string;
  readonly repoRoot?: string;
};

export function generateRendererPreviewArtifacts(options: RendererPreviewOptions = {}): RendererPreviewArtifacts {
  const repoRoot = options.repoRoot ?? path.resolve(__dirname, '..', '..', '..');
  const outputRoot = options.outputRoot ?? path.resolve(__dirname, '..', '.artifacts');
  const runName = options.runName ?? `playwright-${Date.now()}`;
  const runRoot = path.join(outputRoot, runName);

  fs.mkdirSync(outputRoot, { recursive: true });
  fs.rmSync(runRoot, { recursive: true, force: true });

  const args = [
    'run',
    '--project',
    'tools/RendererPreview/RendererPreview.csproj',
    '--',
    `--output=${outputRoot}`,
    `--run=${runName}`,
  ];

  const start = process.hrtime.bigint();
  const result = spawnSync('dotnet', args, { cwd: repoRoot, stdio: 'inherit' });
  const elapsedMs = Number(process.hrtime.bigint() - start) / 1_000_000;

  if (result.status !== 0) {
    throw new Error(`RendererPreview CLI failed with status ${result.status}`);
  }

  const htmlPath = path.join(runRoot, 'report.html');
  const jsonPath = path.join(runRoot, 'swatches.json');

  if (!fs.existsSync(htmlPath) || !fs.existsSync(jsonPath)) {
    throw new Error(`[RendererPreview] Expected artifacts not found under ${runRoot}`);
  }

  console.info(`[RendererPreview] artifacts generated at ${runRoot} (${elapsedMs.toFixed(1)}ms)`);

  return { runRoot, htmlPath, jsonPath };
}
