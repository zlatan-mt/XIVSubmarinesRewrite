# GitHub Release作成手順

## 完了した作業

✅ **リモートプッシュ**: `develop`ブランチをリモートにプッシュ済み
✅ **タグ作成**: `v1.1.6`タグを作成してリモートにプッシュ済み
✅ **パッケージ準備**: `XIVSubmarinesRewrite-v1.1.6.zip`を作成済み

## 次のステップ: GitHub Releaseの作成

### 方法1: GitHub Web UIを使用（推奨）

1. **GitHubリポジトリにアクセス**
   - https://github.com/zlatan-mt/XIVSubmarinesRewrite

2. **Releasesページに移動**
   - リポジトリの右側メニューから「Releases」をクリック
   - または直接: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases

3. **新しいリリースを作成**
   - 「Create a new release」または「Draft a new release」をクリック

4. **リリース情報を入力**
   - **Tag**: `v1.1.6` を選択（既にプッシュ済み）
   - **Title**: `v1.1.6 - Discord notification layout improvements`
   - **Description**: `RELEASE_NOTES_v1.1.6.md`の内容をコピー＆ペースト

5. **パッケージをアップロード**
   - 「Attach binaries by dropping them here or selecting them」セクションに
   - `XIVSubmarinesRewrite-v1.1.6.zip`をドラッグ＆ドロップ

6. **リリースを公開**
   - 「Publish release」をクリック

### 方法2: GitHub CLIを使用

```bash
# GitHub CLIがインストールされている場合
gh release create v1.1.6 \
  --title "v1.1.6 - Discord notification layout improvements" \
  --notes-file RELEASE_NOTES_v1.1.6.md \
  XIVSubmarinesRewrite-v1.1.6.zip
```

### 方法3: GitHub APIを使用（curl）

```bash
# 認証トークンが必要（GITHUB_TOKEN環境変数に設定）
curl -X POST \
  -H "Authorization: token $GITHUB_TOKEN" \
  -H "Accept: application/vnd.github.v3+json" \
  https://api.github.com/repos/zlatan-mt/XIVSubmarinesRewrite/releases \
  -d @- << EOF
{
  "tag_name": "v1.1.6",
  "name": "v1.1.6 - Discord notification layout improvements",
  "body": "$(cat RELEASE_NOTES_v1.1.6.md | sed 's/"/\\"/g' | sed ':a;N;$!ba;s/\n/\\n/g')",
  "draft": false,
  "prerelease": false
}
EOF

# アセットをアップロード
RELEASE_ID=$(curl -s -H "Authorization: token $GITHUB_TOKEN" \
  https://api.github.com/repos/zlatan-mt/XIVSubmarinesRewrite/releases/tags/v1.1.6 \
  | jq -r '.id')

curl -X POST \
  -H "Authorization: token $GITHUB_TOKEN" \
  -H "Content-Type: application/zip" \
  --data-binary @XIVSubmarinesRewrite-v1.1.6.zip \
  "https://uploads.github.com/repos/zlatan-mt/XIVSubmarinesRewrite/releases/$RELEASE_ID/assets?name=XIVSubmarinesRewrite.zip"
```

## リリース情報

- **タグ**: v1.1.6
- **タイトル**: v1.1.6 - Discord notification layout improvements
- **説明**: RELEASE_NOTES_v1.1.6.mdの内容
- **パッケージ**: XIVSubmarinesRewrite-v1.1.6.zip

## 確認事項

- [x] developブランチをリモートにプッシュ済み
- [x] v1.1.6タグを作成してリモートにプッシュ済み
- [x] リリースパッケージ（ZIP）を作成済み
- [ ] GitHub Releaseを作成（Web UI推奨）
- [ ] パッケージをアップロード

---

**注意**: GitHub Releaseの作成とパッケージのアップロードは、GitHubのWeb UIから行うのが最も簡単です。

