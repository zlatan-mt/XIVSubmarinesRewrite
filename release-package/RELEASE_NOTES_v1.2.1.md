<!-- PATH: apps/XIVSubmarinesRewrite/release-package/RELEASE_NOTES_v1.2.1.md -->
<!-- DESCRIPTION: Describe the packaged v1.2.1 release for Dalamud distribution. -->
<!-- REASON: Let release-package consumers know what changed in this rebuild. -->
<!-- RELEVANT FILES: release-package/manifest.json, release-package/plugin.json, release-package/CHANGELOG.md -->
# XIV Submarines Rewrite v1.2.1 リリースノート

**リリース日**: 2025年11月14日  
**対応API**: Dalamud API Level 13  
**フレームワーク**: .NET 9.0-windows

---

## 🎯 このリリースについて

- 配布アセットを改めてビルドし直し、v1.2.1 としてパッケージしました。
- `manifest.json` と `plugin.json` の `AssemblyVersion` も 1.2.1 に更新し、リリース配信と通知文字列が一致するようにしています。

## Fixed

- **Voyage GUID の一意化**: キャラクター ID 及びスロットを含めたハッシュで同時登録時の衝突を防止します。
- **Discord 通知の安定性改善**: slot-aware GUID により、ForceImmediate モードや集約サイクルの状態が誤った通知を生成しにくくなりました。

## Technical Details

- `XIVSubmarinesRewrite-v1.2.1.zip` は 1.2.1 の DLL・manifest・icon を含み、release-package/manifest.json・plugin.json と整合しています。
- `repo.json` にも 1.2.1 のダウンロード URL を登録済みです。
