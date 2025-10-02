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

# Dalamud 配布情報の取得とダウンロード
$versionInfoUrl = "https://kamori.goats.dev/Dalamud/Release/VersionInfo"

function Resolve-DownloadUrl {
    try {
        $versionInfo = Invoke-RestMethod -Uri $versionInfoUrl -Headers @{ "User-Agent" = "XIVSubmarinesRewrite" }
        return $versionInfo.downloadUrl
    } catch {
        Write-Host "[DalamudRestore] Failed to fetch version info: $_" -ForegroundColor Red
        return $null
    }
}

function Download-And-Extract([string]$downloadUrl) {
    $tempPath = [System.IO.Path]::Combine([System.IO.Path]::GetTempPath(), "dalamud_" + [System.Guid]::NewGuid().ToString())
    $zipPath = "$tempPath.zip"
    $extractPath = "$tempPath"

    try {
        Write-Host "[DalamudRestore] Downloading Dalamud package..." -ForegroundColor Yellow
        Invoke-WebRequest -Uri $downloadUrl -Headers @{ "User-Agent" = "XIVSubmarinesRewrite" } -OutFile $zipPath

        Add-Type -AssemblyName System.IO.Compression.FileSystem
        [System.IO.Compression.ZipFile]::ExtractToDirectory($zipPath, $extractPath)

        foreach ($dll in $requiredDlls) {
            $sourcePath = Join-Path $extractPath $dll
            if (-not (Test-Path $sourcePath)) {
                throw "Required DLL $dll not found in downloaded package."
            }
            $targetPath = Join-Path $TargetDir $dll
            Copy-Item $sourcePath $targetPath -Force
            Write-Host "[DalamudRestore] Installed: $dll" -ForegroundColor Green
        }

        Write-Host "[DalamudRestore] Restore completed successfully!" -ForegroundColor Green
        return $true
    } catch {
        Write-Host "[DalamudRestore] Download or extraction failed: $_" -ForegroundColor Red
        return $false
    } finally {
        if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
        if (Test-Path $extractPath) { Remove-Item $extractPath -Recurse -Force }
    }
}

$downloadUrl = Resolve-DownloadUrl
if ($downloadUrl) {
    if (Download-And-Extract $downloadUrl) {
        exit 0
    } else {
        Write-Host "[DalamudRestore] Falling back to vendor directory copy..." -ForegroundColor Yellow
    }
} else {
    Write-Host "[DalamudRestore] Falling back to vendor directory copy..." -ForegroundColor Yellow
}

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
        } else {
            Write-Host "[DalamudRestore] Warning: $dll not found in vendor directory." -ForegroundColor Yellow
        }
    }

    $missing = $false
    foreach ($dll in $requiredDlls) {
        if (-not (Test-Path (Join-Path $TargetDir $dll))) {
            $missing = $true
            break
        }
    }

    if (-not $missing) {
        Write-Host "[DalamudRestore] Restore completed via vendor fallback." -ForegroundColor Green
        exit 0
    }
}

Write-Host "[DalamudRestore] Failed to restore Dalamud DLLs." -ForegroundColor Red
Write-Host "[DalamudRestore] Please ensure internet access or place DLLs in vendor/Dalamud." -ForegroundColor Red
exit 1
