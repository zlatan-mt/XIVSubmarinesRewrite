<!-- PATH: apps/XIVSubmarinesRewrite/RELEASE_NOTES_v1.2.1.md -->
<!-- DESCRIPTION: Document the v1.2.1 release highlights for the main plugin branch. -->
<!-- REASON: Inform Dalamud users of the rebuild and voyage GUID fix. -->
<!-- RELEVANT FILES: CHANGELOG.md, repo.json, release-package/CHANGELOG.md -->
# XIV Submarines Rewrite v1.2.1 リリースノート

**リリース日**: 2025年11月14日  
**対応API**: Dalamud API Level 13  
**フレームワーク**: .NET 9.0-windows

---

## 🎯 このリリースについて

- 主要なコードは 1.2.0 から変わりませんが、v1.2.1 ではリリースアセットと配布メタデータを再構築しました。
- また、1.2.0 で追加したVoyage GUIDとDiscord集約の修正がリリース ZIP にも反映されるように改めてビルドしています。

## Fixed

- **Voyage GUID の一意性**: キャラクター ID とスロット番号を MD5 ハッシュに含めることで、同一の登録/帰港時刻でも GUID の衝突を防ぎます。
- **Discord 集約の安定化**: slot-aware GUID により、同じキャラクターでも別スロットの通知が混ざる問題を抑制し、通知サイクルの一貫性を高めます。

## Technical Details

- 1.2.1 は 1.2.0 のソースに対する再ビルドであり、最新の `manifest.json`/`repo.json` に 1.2.1 を記録しています。
- `XIVSubmarinesRewrite-v1.2.1.zip` には 1.2.1 の DLL・manifest・icon が含まれており、既存のカスタムリポジトリ URL から取得できます。
