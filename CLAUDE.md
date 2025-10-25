# AI-DLC and Spec-Driven Development

Kiro-style Spec Driven Development implementation on AI-DLC (AI Development Life Cycle)

## Project Context

### Paths
- Steering: `plans/specs/steering/`
- Specs: `plans/specs/specs/`

### Steering vs Specification

**Steering** (`plans/specs/steering/`) - Guide AI with project-wide rules and context
**Specs** (`plans/specs/specs/`) - Formalize development process for individual features

### Active Specifications
- Check `plans/specs/specs/` for active specifications
- Use `/kiro:spec-status [feature-name]` to check progress

## Development Guidelines
- Think in English, but generate responses in Japanese (思考は英語、回答の生成は日本語で行うように)

## Minimal Workflow
- Phase 0 (optional): `/kiro:steering`, `/kiro:steering-custom`
- Phase 1 (Specification):
  - `/kiro:spec-init "description"`
  - `/kiro:spec-requirements {feature}`
  - `/kiro:validate-gap {feature}` (optional: for existing codebase)
  - `/kiro:spec-design {feature} [-y]`
  - `/kiro:validate-design {feature}` (optional: design review)
  - `/kiro:spec-tasks {feature} [-y]`
- Phase 2 (Implementation): `/kiro:spec-impl {feature} [tasks]`
  - `/kiro:validate-impl {feature}` (optional: after implementation)
- Progress check: `/kiro:spec-status {feature}` (use anytime)

## Development Rules
- 3-phase approval workflow: Requirements → Design → Tasks → Implementation
- Human review required each phase; use `-y` only for intentional fast-track
- Keep steering current and verify alignment with `/kiro:spec-status`

## Steering Configuration
- Load entire `plans/specs/steering/` as project memory
- Default files: `product.md`, `tech.md`, `structure.md`
- Custom files are supported (managed via `/kiro:steering-custom`)

---

## Development History

### Phase 13: Discord Notification Optimization (2025-10-25)

**Specification**: `discord-message-optimization`  
**Status**: ✅ Complete (Phases 1-5 of 6)  
**Duration**: 1 day (implementation + testing + docs)

#### Objective
Simplify Discord notifications for submarine voyages by eliminating redundant "Completed" notifications and optimizing message format for better readability.

#### User Feedback
> "帰還したことは通知する必要はない。出向させたときに、「次に帰還するのはいつか？」が分かれば良い"

This feedback drove the entire Phase 13 design.

#### Key Changes

**1. Completed Notification Removal (Phase 1)**
- Modified `VoyageCompletionProjection.HandleCompletedVoyage()` to skip notification buffering
- Removed "航海完了を通知" checkbox from UI (`NotificationMonitorWindowRenderer.SettingsLayout.cs`)
- Marked `NotificationSettings.NotifyVoyageCompleted` as `[Obsolete]` for backward compatibility
- **Impact**: 50% reduction in notification volume

**2. Notification Format Optimization (Phase 2)**
- **Single Notification** (before: 6 fields → after: 1 field)
  - Title: `{SubmarineLabel} 出航`
  - Description: `{RouteDisplay}`
  - Field: `帰還予定: {ArrivalTime} ({Duration})`
  - Duration format: `12h`, `30m`, `12.5h` (concise)
- **Batch Notification** (before: 12 lines → after: 4 lines)
  - Title: `{CharacterLabel} - {Count}隻出航`
  - Fields: One line per submarine with inline display
  - Format: `{SubmarineLabel}: {ArrivalTime} ({Duration})`
- **Result**: 67% reduction in message length

**3. Discord Reminder Bot Integration (Phase 3)**
- Added `FormatReminderCommand()` to `VoyageNotificationFormatter`
- Command format: `/remind {channel} {M/d HH:mm} {message}`
- Input sanitization:
  - Channel: `#` prefix, alphanumeric only
  - Message: newline removal, 100 char limit
- UI Settings:
  - Checkbox: `EnableReminderCommand` (default: false)
  - Input: `ReminderChannelName` (default: "#submarine")
- Integration: [Reminder Bot](https://reminder-bot.com/)

**4. Test Coverage (Phase 4)**
- Unit tests: 8 cases (`VoyageNotificationFormatterTests.cs`)
- Integration tests: 3 cases (`VoyageCompletionProjectionPhase13Tests.cs`)
- E2E tests: 5 cases (`discord-notification-phase13.spec.ts`)
- **Total**: 16 test cases

**5. Documentation (Phase 5)**
- Updated `README.md` with Reminder Bot usage guide
- Added notification format examples
- Documented deprecation of Completed notifications

#### Design Decisions

**Why remove Completed notifications entirely?**
- User feedback indicated they are unnecessary
- Users only need to know "when will they return?" at departure
- Reduces notification fatigue and Discord channel clutter

**Why use concise time format (12h, 30m)?**
- Standard format (`12:00:00`) is verbose for Discord embeds
- Concise format saves space and improves readability
- Half-hour precision (`12.5h`) for better accuracy

**Why integrate Reminder Bot instead of building custom reminder?**
- Avoids complex scheduling infrastructure
- Leverages existing, well-maintained Discord bot
- Users can customize reminder behavior in Discord

**Why batch notifications remain opt-in?**
- Some users prefer individual notifications for tracking
- 1.5-second window is conservative to avoid false grouping
- Batching primarily benefits multi-submarine deployments

#### Architecture Impact

**Modified Components**:
- `VoyageCompletionProjection`: Notification filtering logic
- `VoyageNotificationFormatter`: Message formatting and reminder command generation
- `NotificationSettings`: Configuration model (added 2 properties)
- `NotificationMonitorWindowRenderer`: UI rendering (3 partial classes)

**No Breaking Changes**:
- `NotifyVoyageCompleted` preserved with `[Obsolete]` attribute
- Existing Webhook URLs and settings remain compatible
- Notification queue behavior unchanged

#### Testing Strategy

**Unit Tests**:
- Formatter output validation (format correctness)
- Reminder command generation (sanitization)
- Exception handling (Completed status rejection)

**Integration Tests**:
- End-to-end notification flow (Underway → Completed → Underway)
- Settings persistence
- Queue behavior

**E2E Tests**:
- UI element visibility (checkbox removal, reminder settings)
- User workflow (enable reminder → input channel → save)

#### Lessons Learned

1. **User feedback is critical**: Initial spec had complex optimization, but user clarified they only need departure notifications
2. **Concise formats matter**: Discord embed real estate is limited; every field counts
3. **External integrations reduce complexity**: Reminder Bot is simpler than custom scheduling
4. **Backward compatibility via deprecation**: `[Obsolete]` allows gradual migration

#### Metrics (Estimated)

- **Message size**: -67% (avg 120 chars → 40 chars)
- **Notification volume**: -50% (Completed notifications removed)
- **User actions**: +0 (Reminder Bot is opt-in, no required changes)
- **Test coverage**: +16 test cases

#### Related Files

**Core Implementation**:
- `src/Application/Notifications/VoyageCompletionProjection.cs`
- `src/Application/Notifications/VoyageNotificationFormatter.cs`
- `src/Infrastructure/Configuration/NotificationSettings.cs`

**UI**:
- `src/Presentation/Rendering/NotificationMonitorWindowRenderer.SettingsLayout.cs`
- `src/Presentation/Rendering/NotificationMonitorWindowRenderer.Queue.cs`

**Tests**:
- `tests/XIVSubmarinesRewrite.Tests/VoyageNotificationFormatterTests.cs`
- `tests/XIVSubmarinesRewrite.Tests/VoyageCompletionProjectionPhase13Tests.cs`
- `tests/Playwright/discord-notification-phase13.spec.ts`

**Specification**:
- `plans/specs/requirements/discord-message-optimization.md`
- `plans/specs/design/discord-message-optimization.md`
- `plans/specs/tasks/discord-message-optimization.md`

#### Next Steps (Phase 6: Release)

- [ ] Update `CHANGELOG.md` with v1.1.5 entry
- [ ] Create `RELEASE_NOTES_v1.1.5.md`
- [ ] Update `manifest.json` version
- [ ] Test in Dalamud environment
- [ ] Merge to `release` branch
- [ ] Create GitHub release

