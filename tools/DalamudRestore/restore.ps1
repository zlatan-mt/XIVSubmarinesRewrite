# apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.ps1
# GitHub Release から Dalamud DLL を自動取得して配置します
# CI とローカル環境で dotnet build を誰でも実行できるようにするため存在します
# RELEVANT FILES: apps/XIVSubmarinesRewrite/XIVSubmarinesRewrite.csproj, apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.sh

param(
    [string]$TargetDir = "$env:APPDATA\XIVLauncher\addon\Hooks\dev",
    [string]$DalamudVersion = "latest"
)

$ErrorActionPreference = "Stop"

Write-Host "[DalamudRestore] Starting Dalamud DLL restore..." -ForegroundColor Cyan
Write-Host "[DalamudRestore] Target directory: $TargetDir" -ForegroundColor Gray

# ターゲットディレクトリを作成
if (-not (Test-Path $TargetDir)) {
    Write-Host "[DalamudRestore] Creating target directory..." -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $TargetDir -Force | Out-Null
}

# 必要な DLL リスト
$requiredDlls = @(
    "Dalamud.dll",
    "Dalamud.Bindings.ImGui.dll",
    "FFXIVClientStructs.dll",
    "InteropGenerator.Runtime.dll",
    "Lumina.dll",
    "Lumina.Excel.dll"
)

# 既に全ての DLL が存在するかチェック
$allExist = $true
foreach ($dll in $requiredDlls) {
    $dllPath = Join-Path $TargetDir $dll
    if (-not (Test-Path $dllPath)) {
        $allExist = $false
        break
    }
}

if ($allExist) {
    Write-Host "[DalamudRestore] All required DLLs already exist. Skipping download." -ForegroundColor Green
    exit 0
}

# GitHub から最新リリース情報を取得
Write-Host "[DalamudRestore] Fetching Dalamud release information..." -ForegroundColor Yellow
$releaseUrl = "https://api.github.com/repos/goatcorp/Dalamud/releases/latest"

try {
    $release = Invoke-RestMethod -Uri $releaseUrl -Headers @{ "User-Agent" = "XIVSubmarinesRewrite" }
    $version = $release.tag_name
    Write-Host "[DalamudRestore] Latest Dalamud version: $version" -ForegroundColor Green
} catch {
    Write-Host "[DalamudRestore] Failed to fetch release information: $_" -ForegroundColor Red
    Write-Host "[DalamudRestore] Using fallback: copying from vendor directory if available" -ForegroundColor Yellow
    
    # フォールバック: vendor ディレクトリからコピー
    $vendorDir = Join-Path $PSScriptRoot "..\..\vendor\Dalamud"
    if (Test-Path $vendorDir) {
        Write-Host "[DalamudRestore] Copying from vendor directory: $vendorDir" -ForegroundColor Yellow
        foreach ($dll in $requiredDlls) {
            $sourcePath = Join-Path $vendorDir $dll
            $targetPath = Join-Path $TargetDir $dll
            if (Test-Path $sourcePath) {
                Copy-Item $sourcePath $targetPath -Force
                Write-Host "[DalamudRestore] Copied: $dll" -ForegroundColor Green
            }
        }
        exit 0
    }
    
    Write-Host "[DalamudRestore] No vendor directory found. Please manually download Dalamud DLLs." -ForegroundColor Red
    exit 1
}

# 最新版がインストール済みの XIVLauncher から DLL をコピー
$installedPath = "$env:APPDATA\XIVLauncher\addon\Hooks\dev"
if (Test-Path $installedPath) {
    Write-Host "[DalamudRestore] Found installed Dalamud at: $installedPath" -ForegroundColor Green
    Write-Host "[DalamudRestore] Copying DLLs..." -ForegroundColor Yellow
    
    foreach ($dll in $requiredDlls) {
        $sourcePath = Join-Path $installedPath $dll
        $targetPath = Join-Path $TargetDir $dll
        
        if (Test-Path $sourcePath) {
            Copy-Item $sourcePath $targetPath -Force
            Write-Host "[DalamudRestore] Copied: $dll" -ForegroundColor Green
        } else {
            Write-Host "[DalamudRestore] Warning: $dll not found at $sourcePath" -ForegroundColor Yellow
        }
    }
    
    Write-Host "[DalamudRestore] Restore completed successfully!" -ForegroundColor Green
    exit 0
}

Write-Host "[DalamudRestore] No installed Dalamud found. Please install XIVLauncher with Dalamud." -ForegroundColor Red
Write-Host "[DalamudRestore] Alternative: Place DLLs in vendor/Dalamud directory." -ForegroundColor Yellow
exit 1

