# PROJECT HANDOFF

## Current Baseline

This is a WinForms `.NET Framework 4.7.2` application.
Keep the existing workspace flow, tab switching rules, and multi-image confirm interaction unless a later request explicitly changes them.

## Workspace Rules

- Left sidebar controls which workspace tabpages are shown.
- Startup image-viewer workspace shows only:
  - image viewer
  - binarization
  - binarization-2
- Reference-corner workspace shows only the reference-corner tab.
- Measurement-distance workspace shows only the measurement-distance tab.
- Multi-image-confirm workspace shows only the multi-image-confirm tab.
- Inner-settings workspace shows only the inner-settings tab.
- Judgement-criteria workspace shows only the judgement-criteria tab.
- Detection-parameter-summary workspace shows only the detection-parameter-summary tab.
- Continuous-inspection workspace shows only the continuous-inspection tab.

## What Is Working

- Image viewer supports load, zoom, and pan.
- Original binarization workflow remains in place.
- Dual-threshold workflow remains in place.
- Reference corner workflow remains connected to product-specific preprocess settings.
- Measurement-distance workflow supports line creation, edit, delete, and downstream reuse.
- Multi-image confirm supports original-image preview, preprocess preview, navigation, overlays, and right-side info tabs.
- Inner settings and judgement criteria remain designer-managed tabs.
- Detection parameter summary remains a designer-managed tab.
- Continuous inspection is functional.
- Role-based sidebar and workspace switching is functional.

## Role Modes

Three role buttons exist at the lower-left of the sidebar:

- `操作員`
- `工程師`
- `管理者`

### Role Persistence

- The selected role is stored in `setting.ini` as `UserRole=...`.
- App restart restores the last selected role.

### Operator Mode

- Sidebar keeps only `連續檢測`.
- The visible workspace is only `連續檢測`.

### Engineer Mode

- Sidebar keeps:
  - `圖片檢視`
  - `良品判斷條件`
  - `多影像確認結果`
  - `框選量測的距離`
  - `連續檢測`
- Switching into engineer mode initially shows only `連續檢測`.
- In engineer mode, each sidebar button opens only its own single tabpage.
- In engineer mode, `圖片檢視` is allowed.
- In engineer mode, pressing `圖片檢視` shows only the image-viewer tabpage.
- In engineer mode, `二值化處理` and `二值化處理-2` must not be shown or reachable.

### Manager Mode

- All sidebar work items are visible.
- Pressing manager mode returns to `圖片檢視`.
- Manager image-viewer workspace still shows:
  - image viewer
  - binarization
  - binarization-2

## Detection Parameter Summary

This workspace manages main parameters, sub-parameters, and sub-parameter to inner-settings binding.

### Main Parameter Behavior

- Main parameters can be created from the textbox and confirm button.
- Duplicate main-parameter names are blocked case-insensitively.
- Main-parameter order can be moved up/down and saved.
- Main-parameter order and the saved references are written to `parameterReferenceList.ini`.

### Sub-Parameter List Behavior

- The shared sub-parameter list is loaded from `setting.ini` `[listSort]`.
- If `[listSort]` is missing, the list falls back to section names from `setting.ini`.
- All three sub-parameter listboxes stay synchronized to the same item list and order.
- Reordering any sub-parameter list updates the shared in-memory order and refreshes all three listboxes.
- Saving the order writes back to `setting.ini` `[listSort]`.

### Parameter Reference Behavior

- `parameterReferenceList.ini` now stores:
  - main-parameter display order and saved sub-parameter associations
  - `[subParameterInnerSettings]` mapping for sub-parameter to inner-settings profile index
- Main-parameter selection still restores the saved association UI.
- For each sub-parameter:
  - if its checkbox is checked, the selected list item is saved
  - if its checkbox is unchecked, an empty value is saved

## Continuous Inspection

This workspace uses the selected sub-parameter as the runtime binding key.

### Binding Model

- The inner-settings dropdown in image viewer is bound to the current sub-parameter, not the main parameter.
- The selected sub-parameter maps to an inner-settings profile index via `parameterReferenceList.ini`.
- When the app switches to a different sub-parameter, the dropdown refreshes to that sub-parameter's saved inner-settings profile.

### Judgement Behavior

- Each slot judges only its own image.
- Judgement uses that slot's sub-parameter name as the product / profile key.
- Continuous inspection therefore reuses the saved preprocess, reference-corner, measurement, and judgement-criteria data of the selected sub-parameter.
- If no sub-parameter is configured, the result label shows "未設定條件".
- If no image is loaded, the result label shows "未載入圖片".
- Result summarization rules remain:
  - all A criteria -> `A`
  - any B criterion and no C criterion -> `B`
  - any C criterion -> `NG`
- Yield is tracked per slot in memory only.

### Preview / Overlay Behavior

- Each preview supports mouse-wheel zoom and left-drag pan.
- Overlay drawing happens only after pressing the judge button.
- Continuous inspection reuses the multi-image-confirm style for result overlays.
- The green ROI rectangle is intentionally not drawn in continuous inspection.

## Persistence Rules

- Product-related settings remain stored in `setting.ini`.
- Product profile persistence includes:
  - preprocess snapshots
  - reference corner settings
  - measurement records
  - judgement criteria
  - dual-threshold settings
- Dual-threshold settings are stored per product section.
- Shared detection sub-parameter order is stored in `setting.ini` `[listSort]`.
- Continuous inspection remembers the last selected main parameter in `setting.ini` using `ContinuousInspectionMainParameter=...`.
- Detection main-parameter order and per-main-parameter associations are stored in `parameterReferenceList.ini`.
- Sub-parameter to inner-settings bindings are also stored in `parameterReferenceList.ini` under `[subParameterInnerSettings]`.
- Inner settings remain stored separately in `innerSetting.ini`.
- `innerSetting.ini` stores the three camera profiles plus the legacy top-level CCD / scale defaults.

## Inner Settings

The inner-settings tab contains three camera profile blocks.

### Inner Settings Behavior

- Each camera profile can define:
  - camera name
  - usage name
  - CCD X precision
  - CCD Y precision
  - measurement scale factor
- The saved inner-settings file is `innerSetting.ini`.
- The selected inner-settings profile is applied when a sub-parameter is selected.
- The measurement scale factor is applied once to the final calculated measurement value.
- The displayed calculation values and judgement values use the same scaled value.

## Files Touched In This Phase

- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.DetectionParameterSummary.cs`
- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Models/ProfileModels.cs`
- `AoiMeasureTool/Repositories/IniSettingsRepository.cs`
- `AoiMeasureTool/Repositories/ParameterReferenceListRepository.cs`
- `PROJECT_HANDOFF.md`

## Recent Git History

- `0547d0e` `Enable image viewer in engineer mode`
- `9ebadd1` `Restrict engineer mode to single workspace tabs`
- `8f66f1e` `Fix engineer workspace tab reference`
- `fa7104a` `Remove unused inner settings fields`
- `cc4ae62` `Persist user role across restarts`
- `d283342` `Add role-based sidebar switching`
- `5b22761` `Bind inner settings to sub parameters`
- `918f84b` `Persist and apply selected inner settings`
- `f493821` `Persist detection parameter camera bindings`
- `138b942` `Restore image viewer inner profile from saved main parameter`

## Important Notes

- I could not run a full build in this environment because `dotnet` / `MSBuild.exe` were not available.
- The current binding model is sub-parameter based.
- If a later change touches continuous inspection image loading or judge flows, verify that the selected sub-parameter still maps to the expected inner-settings profile before running measurement logic.
- A previous `CS0103` issue from an incorrect engineer-workspace image-viewer tab reference has already been fixed.
- A previous `CS0169` issue from unused duplicated inner-settings fields in `MainForm.cs` has already been fixed.
- `NU1100` for `OpenCvSharp4.runtime.win (>= 4.13.0.20260602)` has not been resolved in this environment. The csproj currently references:
  - `OpenCvSharp4 4.13.0.20260602`
  - `OpenCvSharp4.Extensions 4.13.0.20260602`
  - `OpenCvSharp4.runtime.win 4.13.0.20260602`
- `project.assets.json` in `AoiMeasureTool/obj` showed these packages had been restored at least once on one machine, so the remaining failure is likely a restore-source or package-availability issue rather than a C# source issue.

## Suggested Next Step

Likely next work areas are:

- verify the sub-parameter to inner-settings mapping on real A / B image cases
- resolve the `NU1100` OpenCvSharp restore issue on the target build machine
- decide whether the selected sub-parameter binding should also be surfaced more explicitly in the UI
- decide whether yield / slot state should persist across restart
