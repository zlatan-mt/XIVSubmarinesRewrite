<!-- PATH: apps/XIVSubmarinesRewrite/release-package/RELEASE_NOTES_v1.2.3.md -->
<!-- DESCRIPTION: Shipping copy of the v1.2.3 release announcement bundled with the ZIP. -->
<!-- REASON: Ensure the distributable documents the logging-focused hotfix. -->
<!-- RELEVANT FILES: release-package/CHANGELOG.md, release-package/manifest.json, release-package/plugin.json -->
# XIV Submarines Rewrite v1.2.3 リリースノート

**リリース日**: 2025年11月16日  
**対応API**: Dalamud API Level 13  
**フレームワーク**: .NET 9.0-windows

---

## 変更点

- ゴーストスロット除去の WARN を INFO へ引き下げ、起動ログのノイズを減らしました。
- `ExtractName` 系と航海完了関連の DEBUG ログを削除し、UI 操作や ForceNotify 以外でログが溢れないようにしました。
- リリースメタデータと ZIP 名称を 1.2.3 へ更新し、配布準備を完了しました。

## 配布メモ

- ZIP 内の manifest/plugin と `repo.json` エントリは v1.2.3 を指しています。
- ゴーストデータ修正は v1.2.2 と同じコードパスで、今回の hotfix で初めて配布 DLL と一致します。
