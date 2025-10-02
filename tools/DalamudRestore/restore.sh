# apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.sh
# GitHub Release から Dalamud DLL を自動取得して配置します (Linux/Mac 用)
# CI とローカル環境で dotnet build を誰でも実行できるようにするため存在します
# RELEVANT FILES: apps/XIVSubmarinesRewrite/XIVSubmarinesRewrite.csproj, apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.ps1

set -euo pipefail

if [ -z "${BASH_VERSION:-}" ]; then
  exec bash "$0" "$@"
fi

TARGET_DIR="${1:-$HOME/.xlcore/dalamud/Hooks/dev}"

echo "[DalamudRestore] Starting Dalamud DLL restore..."
echo "[DalamudRestore] Target directory: $TARGET_DIR"

mkdir -p "$TARGET_DIR"

REQUIRED_DLLS=(
  "Dalamud.dll"
  "Dalamud.Bindings.ImGui.dll"
  "FFXIVClientStructs.dll"
  "InteropGenerator.Runtime.dll"
  "Lumina.dll"
  "Lumina.Excel.dll"
)

have_all_dlls=true
for dll in "${REQUIRED_DLLS[@]}"; do
  if [ ! -f "$TARGET_DIR/$dll" ]; then
    have_all_dlls=false
    break
  fi
done

if [ "$have_all_dlls" = true ]; then
  echo "[DalamudRestore] All required DLLs already exist. Skipping download."
  exit 0
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
VENDOR_DIR="$SCRIPT_DIR/../../vendor/Dalamud"
VERSION_INFO_URL="https://kamori.goats.dev/Dalamud/Release/VersionInfo"

TEMP_DIR=""
cleanup() {
  if [ -n "$TEMP_DIR" ] && [ -d "$TEMP_DIR" ]; then
    rm -rf "$TEMP_DIR"
  fi
}
trap cleanup EXIT

download_and_extract() {
  TEMP_DIR="$(mktemp -d)"
  local zip_path="$TEMP_DIR/dalamud.zip"
  local payload_dir="$TEMP_DIR/payload"

  echo "[DalamudRestore] Downloading Dalamud package..."
  curl -fsSL "$1" -o "$zip_path"

  echo "[DalamudRestore] Extracting DLLs..."
  unzip -qo "$zip_path" -d "$payload_dir"

  for dll in "${REQUIRED_DLLS[@]}"; do
    local source_path="$payload_dir/$dll"
    if [ ! -f "$source_path" ]; then
      echo "[DalamudRestore] Missing ${dll} in downloaded package." >&2
      return 1
    fi
    cp "$source_path" "$TARGET_DIR/$dll"
    echo "[DalamudRestore] Installed: $dll"
  done

  echo "[DalamudRestore] Restore completed successfully!"
  TEMP_DIR=""
}

resolve_download_url() {
  local version_json
  if ! version_json=$(curl -fsSL "$VERSION_INFO_URL"); then
    return 1
  fi

  if command -v node >/dev/null 2>&1; then
    printf '%s' "$version_json" | node -e 'let data="";process.stdin.on("data",c=>data+=c);process.stdin.on("end",()=>{try{const json=JSON.parse(data);if(json.downloadUrl){console.log(json.downloadUrl);}else{process.exit(1);}}catch(e){process.exit(1);}});'
  elif command -v python3 >/dev/null 2>&1; then
    printf '%s' "$version_json" | python3 -c 'import json,sys; data=json.load(sys.stdin); print(data.get("downloadUrl",""))'
  else
    return 1
  fi
}

DOWNLOAD_URL=""
if DOWNLOAD_URL=$(resolve_download_url); then
  if [ -n "$DOWNLOAD_URL" ]; then
    if download_and_extract "$DOWNLOAD_URL"; then
      exit 0
    else
      echo "[DalamudRestore] Download failed, attempting vendor fallback..."
    fi
  fi
else
  echo "[DalamudRestore] Unable to resolve download URL, attempting vendor fallback..."
fi

if [ -d "$VENDOR_DIR" ]; then
  echo "[DalamudRestore] Copying from vendor directory: $VENDOR_DIR"
  for dll in "${REQUIRED_DLLS[@]}"; do
    if [ -f "$VENDOR_DIR/$dll" ]; then
      cp "$VENDOR_DIR/$dll" "$TARGET_DIR/$dll"
      echo "[DalamudRestore] Copied: $dll"
    else
      echo "[DalamudRestore] Warning: $dll not found in vendor directory." >&2
    fi
  done

  missing=false
  for dll in "${REQUIRED_DLLS[@]}"; do
    if [ ! -f "$TARGET_DIR/$dll" ]; then
      missing=true
      break
    fi
  done

  if [ "$missing" = false ]; then
    echo "[DalamudRestore] Restore completed via vendor fallback."
    exit 0
  fi
fi

echo "[DalamudRestore] Failed to restore Dalamud DLLs."
echo "[DalamudRestore] Ensure unzip and curl are available, or place DLLs in $VENDOR_DIR."
exit 1
