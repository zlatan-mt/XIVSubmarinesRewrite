<!-- PATH: apps/XIVSubmarinesRewrite/RELEASE_NOTES_v1.2.3.md -->
<!-- DESCRIPTION: Document the v1.2.3 patch release contents and rationale. -->
<!-- REASON: Provide Dalamud users with the logging-focused hotfix summary. -->
<!-- RELEVANT FILES: CHANGELOG.md, repo.json, manifest.json -->
# XIV Submarines Rewrite v1.2.3 リリースノート

**リリース日**: 2025年11月16日  
**対応API**: Dalamud API Level 13  
**フレームワーク**: .NET 9.0-windows

---

## 🎯 このリリースについて

- 1.2.2 で実装したゴーストスロット除去をそのままに、プロダクション環境でのログノイズを削減しました。
- Dalamud manifest / plugin / repo メタデータを 1.2.3 に揃え、配布 ZIP を再作成する準備を整えています。

## Changed

- **ゴースト除去ログのINFO化**: 起動時の slot>3 データクレンジングを WARNING から INFORMATION へ引き下げました。
- **UI 名前抽出デバッグを削除**: `ExtractName` 系の DEBUG 出力を除去し、UI操作でのログスパムを排除しました。
- **完了航海デバッグを削除**: 完了通知を無効化している旨の DEBUG ログを取り除き、Discord 通知経路を静穏化しました。

## Packaging Notes

- `XIVSubmarinesRewrite.csproj`, `manifest.json`, `plugin.json`, `repo.json`、および `release-package/*` を 1.2.3 に更新しました。
- GitHub Releases `v1.2.3` 用の ZIP (`XIVSubmarinesRewrite-v1.2.3.zip`) をアップロードすれば、そのまま配布可能です。
