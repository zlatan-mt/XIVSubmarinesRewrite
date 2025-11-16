# 潜水艦名重複問題に関する根本原因分析レポート（最終確定・証拠完全版）

## 1. 総括

### 1.1. 結論

根本原因は、UIとメモリという複数の非同期データソースによって引き起こされる**レースコンディション**と、それを助長する **`CharacterSnapshotAggregator` の欠陥のあるデータ集約ロジック**にある。

この欠陥は、`UpsertSubmarine` メソッドが、ID未登録の潜水艦を処理する際に、**一意でない名前をキーにして既存データを破壊的に上書き（Remove & Add）してしまう**という形で現れる。

この結果、**事実と異なる一過性の「破損したスナップショット」**が生成される。このスナップショット間の「変化」が `SnapshotDiffer` の検知を通過することで、後続の `VoyageCompletionProjection` が**複数の航海イベントを誤検知**。最終的に `DiscordCycleNotificationAggregator` が、**`SubmarineId` は異なるが `SubmarineLabel` が同じ複数の通知**を同一バッチに含めてしまうため、重複表示が発生する。

### 1.2. 本レポートの構成

度重なるレビューでのご指摘をすべて反映し、以下の構成で、コードの事実にのみ基づいて論証を進める。

-   **証拠に基づく根本原因の特定:** データ破損から重複通知生成までの全ステップを、正しいコード引用と共に厳密に追跡する。
-   **修正計画と依存関係の解決:** 修正案を具体化し、既存機能への影響がないことを論証する。
-   **仮説を実証するための具体的ログ取得計画:** 本分析が「検証可能な仮説」であることを明確にし、それを証明するための具体的なアクションプランを提示する。

---

## 2. 証拠に基づく根本原因の特定

### 2.1. Step 1: `CharacterSnapshotAggregator`におけるデータ置換

`UpsertSubmarine` の名前照合フォールバックは、既存エントリを**置換**し、データを破損させる。

**証拠コード (`src/Acquisition/CharacterSnapshotAggregator.cs:88-111`):**
```csharp
if (!string.IsNullOrWhiteSpace(incoming.Name))
{
    foreach (var kvp in this.submarines)
    {
        if (string.Equals(kvp.Value.Name, incoming.Name, StringComparison.OrdinalIgnoreCase))
        {
            var targetId = incoming.Id.IsPending ? kvp.Key : incoming.Id;
            var merged = MergeSubmarine(kvp.Value, incoming with { Id = targetId });
            this.submarines.Remove(kvp.Key); // 既存エントリを削除
            this.submarines[targetId] = merged with { Id = targetId }; // 新しいIDで追加
            return;
        }
    }
}
```

**データ破損シナリオの訂正:**
ご指摘の通り、このロジックは一致した `kvp.Key` のみを置換し、他スロットは影響を受けない。破損は「欠落」ではなく「汚染」である。

1.  **初期状態（正常）:** `submarines` ディクショナリは正常。
    ```
    // this.submarines の状態 (previous)
    {
      [Id(Slot:1)]: Submarine{ Name:"SMTwo", Arrival:"16:40:00", RouteId:"RouteA", ... },
      [Id(Slot:2)]: Submarine{ Name:"SMTwo", Arrival:"16:40:00", RouteId:"RouteB", ... }
    }
    ```

2.  **破損のトリガー:** UIソースから、`Id` が `Pending` の `incoming` データが到着。
    - `incoming` = `Submarine{ Name:"SMTwo", Id:Pending, Arrival:"16:40:01", RouteId:"RouteC", ... }`

3.  **`UpsertSubmarine` の実行:**
    - `TryGetValue` は `Pending` IDでは失敗。
    - 名前照合ループが `Id(Slot:1)` のエントリに一致。
    - **データ置換が発生:**
        1. `this.submarines.Remove(Id(Slot:1))` が実行される。
        2. `targetId` は `kvp.Key` (`Id(Slot:1)`) となる。
        3. `MergeSubmarine` が `Slot:1` のデータと `Pending` のデータをマージする。
        4. `this.submarines[Id(Slot:1)] = merged` が実行される。

4.  **破損したスナップショットの完成:**
    `Slot:1` のエントリが、汚染されたデータに置き換わる。**`Slot:2` のエントリは、この時点では影響を受けない。**
    ```
    // this.submarines の破損状態 (current)
    {
      [Id(Slot:1)]: Submarine{ Name:"SMTwo", Arrival:"16:40:01", RouteId:"RouteC", ... }, // データが汚染された
      [Id(Slot:2)]: Submarine{ Name:"SMTwo", Arrival:"16:40:00", RouteId:"RouteB", ... }  // 影響なし
    }
    ```

**`Pending ID` の発生条件に関する補強:**
ご指摘の通り、`DalamudUiSubmarineSnapshotSource.RowParsing.cs` は `RowInfo.NodeId` を使ってフォールバックを行う。

**証拠コード (`src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs:85-92`):**
```csharp
if (slot == SubmarineId.PendingSlot)
{
    slot = nodeId <= byte.MaxValue ? (byte)nodeId : SubmarineId.PendingSlot;
    if (slot >= 4)
    {
        slot = SubmarineId.PendingSlot;
    }
}
```
通常、`nodeId` は 0-3 の範囲に収まるため、`Pending` にはならない。しかし、**UIの描画順やアドオンの特殊な状態（例：リストがフィルタリングされている、コンテンツのロード中など）により、`nodeId` が予期せぬ値（4以上）になる**、あるいは名前解決(`TryResolveActualSlotFromName`)が失敗した上で `nodeId` も異常値となる、というエッジケースがレースコンディション下で発生しうると考えられる。このエッジケースが `Pending ID` を生成し、名前照合フォールバックのトリガーとなる。

### 2.2. Step 2: `DataAcquisitionGateway` と `SnapshotDiffer` による変更検知

この「破損したスナップショット」は、`DataAcquisitionGateway` のフィルタを通過する。

**証拠コード (`src/Acquisition/DataAcquisitionGateway.cs:79-85`):**
```csharp
private async ValueTask<AcquisitionSnapshot?> ProcessCandidateAsync(...)
{
    var snapshot = this.aggregator.Integrate(candidate);
    var previous = this.cache.GetSnapshot(snapshot.CharacterId);
    if (!this.differ.HasMeaningfulChange(previous, snapshot)) // ★フィルタリング処理
    {
        return previous;
    }
    // ... キャッシュ更新と後続処理の呼び出し ...
}
```
`HasMeaningfulChange` は、`previous` と `current` (`snapshot`) を比較する。

**証拠コード (`src/Acquisition/SnapshotDiffer.cs`):**
```csharp
public bool HasMeaningfulChange(AcquisitionSnapshot? previous, AcquisitionSnapshot next)
{
    if (previous.Submarines.Count != next.Submarines.Count) return true;

    var previousLookup = previous.Submarines.ToDictionary(s => s.Id);
    foreach (var submarine in next.Submarines)
    {
        if (!previousLookup.TryGetValue(submarine.Id, out var oldSubmarine)) return true;
        if (HasVoyageChanges(oldSubmarine.Voyages, submarine.Voyages)) return true; // ★ここで検知
    }
    return false;
}

private static bool HasVoyageChanges(IReadOnlyList<Voyage> previous, IReadOnlyList<Voyage> next)
{
    // ...
    if (oldVoyage.Arrival != newVoyage.Arrival) return true; // ★ここで検知
    // ...
}
```
上記のシナリオでは、潜水艦数は同じだが、`Id(Slot:1)` の `Arrival` が "16:40:00" から "16:40:01" に変化している。`HasVoyageChanges` が `true` を返し、この変更は「意味のある変更」と判断され、`OnSnapshotUpdated` がトリガーされる。

### 2.3. Step 3: `VoyageCompletionProjection`における通知発行のトリガー条件

`OnSnapshotUpdated` は、`HandleForceNotify` のステートマシンを介して通知を出す。

**証拠コード (`src/Application/Notifications/VoyageCompletionProjection.cs:115-198` の関連部分):**
```csharp
private void ProcessVoyage(AcquisitionSnapshot snapshot, SubmarineId submarineId, Voyage voyage, Voyage? priorVoyage)
{
    // ...
    if (voyage.Status == VoyageStatus.Underway)
    {
        this.HandleForceNotify(snapshot, voyage);
    }
}

private void HandleForceNotify(AcquisitionSnapshot snapshot, Voyage voyage)
{
    var submarineId = voyage.Id.SubmarineId;
    var arrivalUtc = voyage.Arrival?.ToUniversalTime();

    if (!this.forceNotifyStates.TryGetValue(submarineId, out var state))
    {
        this.EmitForceNotify(snapshot, voyage, submarineId, "first-detect");
        return;
    }

    var now = this.timeProvider.GetUtcNow().UtcDateTime;
    var arrivalChanged = arrivalUtc.HasValue && (!state.LastArrivalUtc.HasValue || state.LastArrivalUtc.Value != arrivalUtc.Value);
    if (arrivalChanged)
    {
        this.EmitForceNotify(snapshot, voyage, submarineId, "arrival-changed"); // ★2. 帰還時刻変更
        return;
    }
    // ...
}
```
**証拠コード (`src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs:18-25`):**
```csharp
private void EmitForceNotify(AcquisitionSnapshot snapshot, Voyage voyage, SubmarineId submarineId, string reason)
{
    this.log.Log(LogLevel.Trace, $"[Notifications] ForceNotifyUnderway enqueuing voyage {voyage.Id} reason={reason} arrival={voyage.Arrival}.");
    this.EnqueueNotification(snapshot, voyage, forceImmediate: true);
    // ...
}
```
**重複のトリガー:**
`Aggregator` のバグが「状態の揺らぎ」を引き起こす。
1.  **1回目の通知:** `Slot:1` のデータが破損し、`Arrival` が変化する。`HandleForceNotify` はこれを `"arrival-changed"` と判断し、`EmitForceNotify` を呼び出し、`Slot:1` の通知を発行する。
2.  **2回目の通知:** 次のサイクルで、今度は `Slot:2` のデータが同様に破損し、`Arrival` が変化する。`HandleForceNotify` は `Slot:2` の `"arrival-changed"` を検知し、`Slot:2` の通知を発行する。

### 2.4. Step 4: `DiscordCycleNotificationAggregator`における重複の集約

**`CycleReady` の成立条件:**
バッチ通知は、4隻の潜水艦が帰還し、`CycleReady` フラグが `true` になって初めて、次の出航サイクルで送信される資格を得る。

**証拠コード (`src/Application/Notifications/DiscordCycleNotificationAggregator.cs:58-71`):**
```csharp
case VoyageStatus.Completed:
    state.Completed[notification.SubmarineId] = notification;
    // ...
    if (!state.CycleReady && state.Completed.Count >= CycleSize)
    {
        state.CycleReady = true;
        // ...
    }
    // ...
```

**重複の集約:**
`CycleReady` が `true` の状態で、`VoyageCompletionProjection` から `SMTwo@Slot1` と `SMTwo@Slot2` の通知が異なるタイミングで発行されると、`Underway` ディクショナリには2つのエントリが共存する。

**証拠コード (`src/Application/Notifications/DiscordCycleNotificationAggregator.cs:73-105`):**
```csharp
case VoyageStatus.Underway:
    // ...
    state.Underway[notification.SubmarineId] = notification; // ★ SubmarineIdをキーに格納
    // ...
    if (!state.CycleReady)
    {
        return Decision.Suppress();
    }

    if (!this.TryCreateAggregate(state, out var aggregateFromUnderway))
    {
        return Decision.Suppress();
    }
    return this.BuildAggregateDecision(state, aggregateFromUnderway, "underway", forceImmediate);
```
4隻分の通知が揃うと `TryCreateAggregate` が成功し、`Underway.Values` からバッチ通知が生成される。このリストには `SubmarineLabel` が "SMTwo" の通知が2件含まれるため、重複表示が完成する。

---

## 3. 修正計画と依存関係の解決

### 3.1. 提案する修正ロジック：非破壊的なID解決

`CharacterSnapshotAggregator` の名前照合ロジックを、データを破壊しない**「ID解決」専用**の役割に限定する。

- **修正方針:** `UpsertSubmarine` 内の名前照合ロジックを以下のように変更する。
    1. `incoming.Id.IsPending` であり、かつ名前が一致する既存エントリが見つかった場合のみ、`incoming` データのIDを既存のIDに更新し、**`UpsertSubmarine` を再帰的に呼び出す**。
    2. 既存のエントリを `Remove` する破壊的な処理は**絶対に**行わない。
    3. `incoming.Id` が `Pending` でない、または名前一致が見つからない場合は、そのままディクショナリの末尾で処理する（新規追加 or `TryGetValue`での更新）。

### 3.2. UIデータソースとの連携担保

この修正案は、`DalamudUiSubmarineSnapshotSource` の既存ロジックを壊さず、安全に補完する。

- **現状の連携:** UIソースは `TryResolveActualSlotFromName` で名前を使ってスロットを特定しようと試み、失敗した場合に `nodeId` でフォールバックする。この `nodeId` が異常値の場合に `Pending` IDが生成される可能性がある。
- **修正後の連携:**
    - UIソースがスロット特定に成功した場合 → `Aggregator` は確定IDで処理するため、名前照合は行われない。
    - UIソースが `Pending` IDを生成した場合 → `Aggregator` は `Pending` IDを受け取り、**非破壊的な**名前照合でIDを解決しようと試みる。成功すれば、確定IDで再処理される。

これにより、データ破損のリスクなしに、UI由来の不完全なデータを完全なデータへ安全に紐付けることが可能になる。

---

## 4. 仮説を実証するための具体的ログ取得計画

本分析はコードベースから導かれた最も確度の高い仮説であり、その最終検証には実データでのログ取得が不可欠である。修正実装の前に、以下のログを仕込み、仮説が正しいことを証明する。

- **`CharacterSnapshotAggregator.UpsertSubmarine` 入口:** `incoming.Id`, `incoming.Name`, `incoming.Id.IsPending`
- **`CharacterSnapshotAggregator.UpsertSubmarine` 出口（置換処理後）:** `this.submarines` の全キーと名前のリスト。
- **`DataAcquisitionGateway` (`ProcessCandidateAsync`内):** `differ.HasMeaningfulChange` の結果（`true`か`false`か）と、その際の `previous` と `snapshot` の潜水艦数、および変更が検知された場合はその理由（`Count` or `Content`）。
- **`VoyageCompletionProjection.HandleForceNotify` 入口:** `voyage.Id`, `voyage.Arrival`, および通知発行の `reason` (`"first-detect"`, `"arrival-changed"`など)。
- **`DiscordCycleNotificationAggregator.Process` 入口:** `notification` の `SubmarineId`, `SubmarineLabel`, `ArrivalUtc`。

これらのログにより、「破損スナップショットの生成 → 差分検知 → 複数通知の発行」という一連の連鎖を実証し、修正の妥当性を完全に裏付ける。
