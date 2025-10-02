<!-- apps/XIVSubmarinesRewrite/vendor/Dalamud/README.md -->
<!-- Dalamud 参照 DLL の配置先を説明するドキュメントです -->
<!-- CI とローカルの依存復元手順を共有するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.sh, apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.ps1 -->

# Dalamud DLL Vendor Directory

このディレクトリは Dalamud 参照 DLL のフォールバック配置先です。


## 概要

`XIVSubmarinesRewrite` プロジェクトのビルドには以下の Dalamud DLL が必要です：

- `Dalamud.dll`
- `Dalamud.Bindings.ImGui.dll`
- `FFXIVClientStructs.dll`
- `InteropGenerator.Runtime.dll`
- `Lumina.dll`
- `Lumina.Excel.dll`


## 自動セットアップ

通常は `tools/DalamudRestore/restore.ps1` または `restore.sh` を実行することで、
インストール済みの XIVLauncher から自動的に DLL をコピーします。


### Windows の場合

```powershell
.\tools\DalamudRestore\restore.ps1
```

デフォルトでは `%APPDATA%\XIVLauncher\addon\Hooks\dev` から DLL を取得します。


### Linux/Mac の場合

```bash
./tools/DalamudRestore/restore.sh
```


## 手動配置

XIVLauncher がインストールされていない環境や、カスタムビルドを使う場合は、
このディレクトリに上記の DLL を手動で配置してください。

DalamudRestore スクリプトは以下の優先順位で DLL を検索します：

1. インストール済み XIVLauncher (`%APPDATA%\XIVLauncher\addon\Hooks\dev`)
2. この vendor ディレクトリ (`vendor/Dalamud/`)
3. 環境変数 `DALAMUD_LIB_PATH`


## CI 環境

GitHub Actions では `DalamudRestore/restore.sh` が自動実行され、
vendor ディレクトリから DLL をコピーします。

CI で使用する DLL をコミットする場合は、このディレクトリに配置してください。
（ただし、ライセンスと容量に注意）


## 関連ファイル

- `tools/DalamudRestore/restore.ps1` - Windows 用 DLL 復元スクリプト
- `tools/DalamudRestore/restore.sh` - Linux/Mac 用 DLL 復元スクリプト
- `Local.props.example` - ローカル開発環境の設定例
- `XIVSubmarinesRewrite.csproj` - DLL 参照設定

