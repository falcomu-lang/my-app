# PROJECT HANDOFF

## Current Baseline

This is a WinForms `.NET Framework 4.7.2` AOI measurement application.
The main form is `AoiMeasureTool/Forms/MainForm.cs`, with WinForms layout in `AoiMeasureTool/Forms/MainForm.Designer.cs`.

Current important baseline:

- The top `MenuStrip` has been removed from the Designer and runtime UI.
- The left sidebar is the main workspace navigator.
- Text-bearing C# files that include Chinese UI strings should be kept as UTF-8 with BOM.
- Build verification has been done with VS2019 MSBuild:
  `C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe`
- Current build status: `Rebuild` succeeds with `0 errors`.
- Existing warnings are mostly OpenCvSharp analyzer load warnings and unused private field warnings.

## Workspace Rules

Left sidebar buttons control which workspace tab pages are shown.
The active workspace button should be disabled, and all other role-visible workspace buttons should remain clickable.

Workspace behavior:

- Image viewer workspace:
  - Manager: shows image viewer, binarization, and binarization-2 tabs.
  - Engineer: shows only image viewer tab.
- Reference corner workspace shows only the reference-corner tab.
- Measurement distance workspace shows only the measurement-distance tab.
- Multi-image confirm workspace shows only the multi-image-confirm tab.
- Inner settings workspace shows only the inner-settings tab.
- Judgement criteria workspace shows only the judgement-criteria tab.
- Detection parameter summary workspace shows only the detection-parameter-summary tab.
- Continuous inspection workspace shows only the continuous-inspection tab.

## Role Modes

There are three role buttons at the lower-left of the sidebar:

- `操作員`
- `工程師`
- `管理者`

### Role Passwords

Engineer and manager modes require password input.
Operator mode does not require a password.

Passwords are loaded from `setting.ini`:

```ini
[password]
engineer=0000
admin=0000
```

If the password section or value is missing, the default password is `0000`.
The password file is plain text by design.

### Role Persistence

- The selected role is stored in `setting.ini` as `UserRole=...`.
- App restart restores the last selected role.
- Initial role restore is applied in `OnShown(...)`, not from the constructor.
- This avoids `BeginInvoke(new Action(ApplyInitialUserRole))` timing problems before the form handle is ready.
- `_initialUserRoleApplied` prevents the initial role restore from running multiple times.

### Operator Mode

- Only continuous inspection is visible in the sidebar.
- Operator mode opens the continuous inspection workspace.
- In continuous inspection, operator cannot press load-image or judge buttons.

### Engineer Mode

Engineer mode can access:

- Image viewer
- Measurement distance
- Multi-image confirm
- Judgement criteria
- Continuous inspection

Engineer mode cannot access:

- Reference corner
- Inner settings
- Detection parameter summary
- Binarization tabs in image viewer

Engineer can use continuous inspection load-image and judge buttons.

### Manager Mode

Manager mode can access all sidebar work items.
Manager image viewer still shows:

- Image viewer
- Binarization
- Binarization-2

Manager can use continuous inspection load-image and judge buttons.

## Detection Parameter Summary

This workspace manages main parameters, sub-parameters, and sub-parameter to inner-settings binding.

### Main Parameter Behavior

- Main parameters can be created from the textbox and confirm button.
- Duplicate main-parameter names are blocked case-insensitively.
- Main-parameter order can be moved up/down and saved.
- Main-parameter order and saved references are written to `parameterReferenceList.ini`.

### Sub-Parameter List Behavior

- Shared sub-parameter list is loaded from `setting.ini` `[listSort]`.
- If `[listSort]` is missing, the list falls back to section names from `setting.ini`.
- All three sub-parameter listboxes stay synchronized to the same item list and order.
- Reordering any sub-parameter list updates the shared in-memory order and refreshes all three listboxes.
- Saving the order writes back to `setting.ini` `[listSort]`.

### Parameter Reference Behavior

`parameterReferenceList.ini` stores:

- main-parameter display order
- saved sub-parameter associations
- `[subParameterInnerSettings]` mapping for sub-parameter to inner-settings profile index

For each sub-parameter:

- if its checkbox is checked, the selected list item is saved
- if its checkbox is unchecked, an empty value is saved

## Continuous Inspection

Continuous inspection uses the selected sub-parameter as the runtime binding key.

### Binding Model

- The image viewer inner-settings dropdown is bound to the current sub-parameter, not the main parameter.
- Selected sub-parameter maps to an inner-settings profile index via `parameterReferenceList.ini`.
- When switching sub-parameters, the dropdown refreshes to that sub-parameter's saved inner-settings profile.

### Judgement Behavior

- Each slot judges only its own image.
- Judgement uses that slot's sub-parameter name as the product/profile key.
- Continuous inspection reuses saved preprocess, reference-corner, measurement, and judgement-criteria data of the selected sub-parameter.
- If no sub-parameter is configured, the result label shows `未設定條件`.
- If no image is loaded, the result label shows `未載入圖片`.
- Result summary rules:
  - all A criteria -> `A`
  - any B criterion and no C criterion -> `B`
  - any C criterion -> `NG`
- Yield is tracked per slot in memory only.

### Preview / Overlay Behavior

- Each preview supports mouse-wheel zoom and left-drag pan.
- Overlay drawing happens only after judging.
- Continuous inspection reuses multi-image-confirm overlay style.
- The green ROI rectangle is intentionally not drawn in continuous inspection.

### Save Original Image Behavior

Each slot has a `保存原始影像` checkbox.

If checked, both manual loading and Mat API loading save the source image.
The save occurs when an image is loaded into the slot, before/independent of judgement result.

Current save path pattern:

```text
{AppDomain.CurrentDomain.BaseDirectory}\1\{SubParameterName}_{MM_dd_HH_mm_ss}.png
{AppDomain.CurrentDomain.BaseDirectory}\2\{SubParameterName}_{MM_dd_HH_mm_ss}.png
{AppDomain.CurrentDomain.BaseDirectory}\3\{SubParameterName}_{MM_dd_HH_mm_ss}.png
```

Slot index mapping:

- slot 0 -> folder `1`
- slot 1 -> folder `2`
- slot 2 -> folder `3`

## External Continuous Inspection APIs

Only expose the combined load-and-judge APIs externally.
The earlier lower-level APIs should be treated as internal implementation details.

### `LoadAndJudgeContinuousInspectionMat(...)`

Signature:

```csharp
public string LoadAndJudgeContinuousInspectionMat(int slotIndex, CvMat imageMat, string sourceName = null)
```

Behavior:

- Converts the provided OpenCvSharp `Mat` into a slot image.
- Updates the corresponding continuous-inspection preview slot.
- Saves the source image if that slot's `保存原始影像` checkbox is checked.
- Runs judgement for that slot.
- Updates result label, yield count, and overlay.
- Returns only the summary string, for example `A`, `B`, `NG`, `未設定條件`, or `不可判斷`.

Usage example:

```csharp
var summary = mainForm.LoadAndJudgeContinuousInspectionMat(0, cameraMat, "Camera1");
```

### `LoadAndJudgeContinuousInspectionMatWithDetails(...)`

Signature:

```csharp
public ContinuousInspectionResult LoadAndJudgeContinuousInspectionMatWithDetails(int slotIndex, CvMat imageMat, string sourceName = null)
```

Behavior:

- Same load, preview update, optional save, judgement, result-label update, yield update, and overlay update as `LoadAndJudgeContinuousInspectionMat(...)`.
- Returns a structured result object.

Returned data currently includes:

- `SlotIndex`
- `SubParameterName`
- `Summary`
- detailed judgement/rule result rows through the result detail collection

Recommended use:

- Use `LoadAndJudgeContinuousInspectionMat(...)` if the caller only needs final judgement.
- Use `LoadAndJudgeContinuousInspectionMatWithDetails(...)` if the caller needs rule names, calculated values, judgement text, or structured result data.

## Manual vs Camera/Mat Flow

Manual image flow and Mat camera flow are intentionally aligned.

Manual flow:

1. User selects slot image.
2. Slot preview updates.
3. Optional original image save happens if checkbox is checked.
4. User presses judge.
5. Result, yield, and overlay update.

Mat flow:

1. External caller passes `Mat` to `LoadAndJudgeContinuousInspectionMat...`.
2. Slot preview updates.
3. Optional original image save happens if checkbox is checked.
4. Judgement runs immediately.
5. Result, yield, and overlay update.

## Persistence Rules

Product-related settings remain stored in `setting.ini`.
Product profile persistence includes:

- preprocess snapshots
- reference corner settings
- measurement records
- judgement criteria
- dual-threshold settings

Other persistence files:

- Shared detection sub-parameter order: `setting.ini` `[listSort]`
- Last selected continuous inspection main parameter: `setting.ini` `ContinuousInspectionMainParameter=...`
- Role: `setting.ini` `UserRole=...`
- Engineer/admin passwords: `setting.ini` `[password]`
- Detection main-parameter order and associations: `parameterReferenceList.ini`
- Sub-parameter to inner-settings bindings: `parameterReferenceList.ini` `[subParameterInnerSettings]`
- Inner settings: `innerSetting.ini`

## Inner Settings

The inner-settings tab contains three camera profile blocks.

Each camera profile can define:

- camera name
- usage name
- CCD X precision
- CCD Y precision
- measurement scale factor

Behavior:

- The saved inner-settings file is `innerSetting.ini`.
- The selected inner-settings profile is applied when a sub-parameter is selected.
- The measurement scale factor is applied once to final calculated measurement value.
- Displayed calculation values and judgement values use the same scaled value.

## UI / Designer Notes

- `MenuStrip` has been removed from `MainForm.Designer.cs`.
- Designer and runtime should both show the UI without the top `檔案 / 編輯 / 檢視 / 說明` menu row.
- `panelSidebar` starts at `Y=0` and height `800`.
- `panelMain` starts at `Y=0` and height `800`.
- Do not reintroduce `MainMenuStrip` unless the UI requirement changes.

## Build Notes

Known build command:

```powershell
& 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe' `
  'C:\Users\falcomu\Documents\Codex\程式撰寫 專案資料夾\量測程式\my-app\AoiMeasureTool.sln' `
  /t:Rebuild `
  /p:Configuration=Debug
```

Last verified status:

- Build succeeds.
- `0 errors`.
- Existing warnings include OpenCvSharp analyzer load warnings and unused private field warnings.

## Files Commonly Touched

- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Forms/MainForm.DetectionParameterSummary.cs`
- `AoiMeasureTool/Forms/MainForm.Measurement.cs`
- `AoiMeasureTool/Models/ProfileModels.cs`
- `AoiMeasureTool/Repositories/IniSettingsRepository.cs`
- `AoiMeasureTool/Repositories/ParameterReferenceListRepository.cs`
- `AoiMeasureTool/Services/ProductProfileService.cs`
- `PROJECT_HANDOFF.md`

## Recent Git History

Most relevant recent commits:

- `7300703` Add continuous inspection detail API
- `385d208` Fix continuous inspection overlay and role buttons
- `529b242` Disable active workspace buttons
- `3b29537` Fix workspace button mode enum
- `08ccce7` Add role password protection
- `faf1ab0` Restrict continuous inspection actions by role
- `7be3c1a` Remove top menu strip
- `a18f511` Fix UI text encoding and hide legacy menu
- `0fb65d6` Remove designer menu strip and fix startup role state
- `edd1d4e` Defer startup role restore until form is ready
- `b9bc227` Apply startup role after form shown

## Important Cautions

- Avoid editing Chinese UI string files without preserving UTF-8 BOM.
- If Chinese text appears garbled after editing, check file BOM first.
- Avoid calling `BeginInvoke(new Action(ApplyInitialUserRole))` from the constructor; use `OnShown(...)` based startup role application.
- If role or workspace buttons look disabled after restart, inspect:
  - `_currentUserRole`
  - `_currentWorkspaceButton`
  - `ApplyInitialUserRole()`
  - `ApplyUserRole(...)`
  - `UpdateSidebarVisibilityForRole(...)`
  - `UpdateWorkspaceButtonStates()`
- If continuous inspection judgement changes, verify that Mat API and manual UI flow still produce the same overlay and result behavior.
- If save-original behavior changes, verify both manual load and Mat load paths.

## Suggested Next Steps

- Verify saved engineer/manager restart behavior on the actual production machine.
- Verify `LoadAndJudgeContinuousInspectionMatWithDetails(...)` with real camera `Mat` inputs for all three slots.
- Confirm whether save-original should happen on load, on judgement, or both; current behavior is on load.
- Decide whether yield/slot state should persist across restart.
- Consider adding a small wrapper/service layer if external camera integration will grow beyond direct `MainForm` method calls.