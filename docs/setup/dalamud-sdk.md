# Dalamud SDK 取得手順

## 概要
- Dalamud プラグインをビルドするには、開発用 Hooks (dev) フォルダを参照する必要があります。
- リライト版では SDK をリポジトリに同梱せず、各自で取得・配置する運用とします。

## 手順 (Windows)
1. XIVLauncher をインストールし、Dalamud の開発モードを有効にする。
2. Dalamud Launcher を起動すると `%AppData%\XIVLauncher\addon\Hooks\dev` に DLL が展開されます。
3. `Local.props` を作成し、csproj が参照する `DalamudLibPath` を指定します。
4. `DevPluginsDir` を設定すると、ビルド後に DLL/manifest/icon を自動コピーできます。

## 手順 (WSL/Linux)
1. Windows 側で取得した Hooks フォルダを WSL から参照できるようにマウントする (例: `/mnt/c/Users/<User>/AppData/Roaming/XIVLauncher/addon/Hooks/dev`)。
2. `DALAMUD_LIB_PATH` 環境変数に該当パスを設定、または `Local.props` から直接 UNC 表記で指定。
3. CI で参照する場合は、Secrets ストレージに zip を格納し、展開してパスをセットするスクリプトを用意する。

## 参考情報
- Dalamud API Docs: https://dalamud.dev/api/
- Dalamud Discord: https://discord.gg/xf9wxFJ
- GitHub Actions での例: `scripts/setup-dalamud.ps1` (今後追加予定)

## TODO
- [ ] SDK のバージョン互換性一覧と更新手順を追記。
- [ ] 自動取得スクリプト (PowerShell/Bash) を追加し、Docs から参照。
- [ ] CI 用のダウンロードキャッシュ戦略を検討。
