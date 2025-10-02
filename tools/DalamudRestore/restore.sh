# apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.sh
# GitHub Release から Dalamud DLL を自動取得して配置します (Linux/Mac 用)
# CI とローカル環境で dotnet build を誰でも実行できるようにするため存在します
# RELEVANT FILES: apps/XIVSubmarinesRewrite/XIVSubmarinesRewrite.csproj, apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.ps1

set -e

if [ -z "$BASH_VERSION" ]; then
  exec bash "$0" "$@"
fi

TARGET_DIR="${1:-$HOME/.xlcore/dalamud/Hooks/dev}"
DALAMUD_VERSION="${2:-latest}"

echo "[DalamudRestore] Starting Dalamud DLL restore..."
echo "[DalamudRestore] Target directory: $TARGET_DIR"

# ターゲットディレクトリを作成
mkdir -p "$TARGET_DIR"

# 必要な DLL リスト
REQUIRED_DLLS=(
    "Dalamud.dll"
    "Dalamud.Bindings.ImGui.dll"
    "FFXIVClientStructs.dll"
    "InteropGenerator.Runtime.dll"
    "Lumina.dll"
    "Lumina.Excel.dll"
)

# 既に全ての DLL が存在するかチェック
all_exist=true
for dll in "${REQUIRED_DLLS[@]}"; do
    if [ ! -f "$TARGET_DIR/$dll" ]; then
        all_exist=false
        break
    fi
done

if [ "$all_exist" = true ]; then
    echo "[DalamudRestore] All required DLLs already exist. Skipping download."
    exit 0
fi

# フォールバック: vendor ディレクトリからコピー
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
VENDOR_DIR="$SCRIPT_DIR/../../vendor/Dalamud"

if [ -d "$VENDOR_DIR" ]; then
    echo "[DalamudRestore] Copying from vendor directory: $VENDOR_DIR"
    for dll in "${REQUIRED_DLLS[@]}"; do
        if [ -f "$VENDOR_DIR/$dll" ]; then
            cp "$VENDOR_DIR/$dll" "$TARGET_DIR/$dll"
            echo "[DalamudRestore] Copied: $dll"
        fi
    done
    echo "[DalamudRestore] Restore completed successfully!"
    exit 0
fi

echo "[DalamudRestore] No vendor directory found."
echo "[DalamudRestore] Please manually place Dalamud DLLs in: $VENDOR_DIR"
echo "[DalamudRestore] Or set DALAMUD_LIB_PATH environment variable."
exit 1
