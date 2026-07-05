# PROJECT HANDOFF

## Current Baseline

The current WinForms workflow is the approved baseline.
Do not redesign the viewer flow, workspace switching rules, or multi-image confirm interaction unless a later request explicitly asks for it.

The project target framework is `.NET Framework 4.7.2`.

## Workspace Rules

- The left sidebar controls which workspace tabpages are shown.
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
- Inner settings and judgement criteria are designer-managed tabs.
- Detection parameter summary is a designer-managed tab.
- Continuous inspection is now a functional workspace.

## Detection Parameter Summary

A dedicated `瑼Ｘ葫??渡?` workspace now exists.

### Layout

- Left side contains a small `撱箇?銝餃??節 group:
  - main-parameter input textbox
  - `蝣箄?` button
- Middle-left contains the `銝餃??節 listbox.
- Right side top area contains three synchronized sub-parameter list groups:
  - `摮???`
  - `摮???`
  - `摮???`
- Each sub-parameter group now contains:
  - listbox
  - `?
  - `?
  - `靽???`
  - a checkbox near the group title
- A `?脣??` button exists near the lower-right of the sub-parameter area.

### Main Parameter Behavior

- Clicking `蝣箄?` with text in the main-parameter textbox adds a new item to the `銝餃??節 listbox.
- Duplicate main-parameter names are prevented case-insensitively.
- Main-parameter order can be adjusted with `? and `?.
- Clicking `靽???` persists main-parameter order to `parameterReferenceList.ini`.

### Sub-Parameter List Behavior

- `摮???` reads `setting.ini` `[listSort]`.
- If `setting.ini` does not yet contain `[listSort]`, the sub-parameter list falls back to section names from `setting.ini`.
- `摮???` and `摮???` are intentionally synchronized to the exact same item list and order as `摮???`.
- Moving order in any of the three sub-parameter listboxes updates the shared in-memory order and refreshes all three listboxes together.
- Clicking `靽???` in any sub-parameter group writes the shared order back to `setting.ini` `[listSort]`.
- Shared sub-parameter order is reloaded correctly after app startup and after settings restore.
- When saving the current binarization settings under a new parameter name, that name must also be appended to `setting.ini` `[listSort]` if it does not already exist.

### Parameter Reference Behavior

- `parameterReferenceList.ini` stores both:
  - main-parameter display order
  - per-main-parameter sub-parameter associations
- When a main parameter is selected, the UI automatically loads its saved association.
- If `subParameter1`, `subParameter2`, or `subParameter3` contains a value:
  - the corresponding sub-parameter listbox auto-selects that item
  - the corresponding checkbox is checked
- If a saved sub-parameter value is empty:
  - the corresponding listbox selection is cleared
  - the corresponding checkbox is unchecked
- Clicking `?脣??` saves the currently selected main parameter's association.
- For each sub-parameter:
  - if its checkbox is checked, the selected list item is saved
  - if its checkbox is unchecked, an empty value is saved

## Continuous Inspection

A dedicated `???瑼Ｘ葫` workspace now exists.

### Layout

- The page contains:
  - main-parameter dropdown at the top
  - three side-by-side sub-parameter groupboxes
- Each sub-parameter groupbox is designer-managed and contains:
  - sub-parameter name label
  - preview viewport
  - result label
  - `霈??` button
  - `?斗` button
  - yield label
  - `靽???敶勗?` checkbox
- The result / button / yield controls are real designer controls, not runtime-generated controls.
- The groupboxes were extended vertically so the result area no longer overlaps the preview image.

### Main Parameter Behavior

- The main parameter is selected directly on the continuous-inspection page.
- The selected main parameter loads up to three saved sub-parameters from `parameterReferenceList.ini`.
- The last selected main parameter is persisted to `setting.ini` and restored on the next app launch.
- If the app restarts, the continuous-inspection dropdown should not fall back to the first item when a saved selection exists.

### Image Loading Behavior

- Each `霈??` button opens a file picker for that slot.
- Each slot loads one manual image path.
- Loading an image does not trigger judgement.
- If `靽???敶勗?` is checked, the original image is saved immediately when the image is loaded.
- Loading or judging preserves the current zoom scale and viewport position when possible.

### Judgement Behavior

- Each `?斗` button runs only its own slot.
- Judgement uses that slot's sub-parameter name as the product / profile key.
- This means continuous inspection reuses the saved preprocess, reference-corner, measurement, and judgement-criteria data of the selected sub-parameter.
- If no sub-parameter is configured for that slot, the result label shows `?芾身摰?隞跆.
- If no image is loaded, the result label shows `?芾??亙??.
- Result summarization rules are:
  - all A criteria -> `A`
  - any B criterion and no C criterion -> `B`
  - any C criterion -> `NG`
- Result background colors are:
  - `A` -> green
  - `B` -> yellow
  - `NG` -> red
- Yield text is tracked per slot in the format `?舐? x/x(XX%)`.
- Only `A` counts as good.
- `B` and `NG` both count as NG for yield purposes.
- Yield updates only when that slot's `?斗` button is pressed.
- Yield is currently in-memory only and is not persisted across app restart.

### Preview / Overlay Behavior

- Each preview supports:
  - mouse-wheel zoom
  - left-drag pan
- Overlay drawing happens only after pressing `?斗`.
- Continuous inspection reuses the multi-image-confirm style for result overlays.
- The green ROI rectangle is intentionally not drawn in continuous inspection.

## Judgement Criteria

The `?臬??斗璇辣` tabpage contains a syntax help button in the top-right corner.

### Supported Calculation Syntax

- Basic measurement references:
  - `(1)`
  - `(2)`
  - `(3)`
- Direct arithmetic expressions are supported after measurement references are resolved.
- Aggregate functions are supported:
  - `max((1)(2)(3))`
  - `max((1),(2),(3))`
  - `min((1)(2)(3))`
  - `min((1),(2),(3))`
- Nested expressions are supported:
  - `max(((2)-(1))((4)-(3)))`
  - `min(((2)-(1)),((4)-(3)))`

### Missing-Value Behavior

- For `max` / `min`, a missing sub-expression is ignored.
- If one child expression in `max/min` cannot be evaluated, that child is skipped and the remaining children still participate.
- Example: `min((1)-(2),(3))` skips `(1)-(2)` when `(2)` has no value, and the result becomes `(3)` if `(3)` is valid.
- Example: `max(((1)-(2)),((1)-(3)))` returns `0` when both children cannot be evaluated.
- Only when all children are missing does the aggregate result become `0`.
- For non-aggregate arithmetic, if a required value is missing, the result becomes `0`.
- This behavior applies consistently to display, judgement, and multi-image confirm.

### Judgement Rules

- `x<5`
- `x<=5`
- `5<x<10`
- `10>=x>=5`

## Dual-Threshold Page

- A second binarization tab exists and uses dual-threshold preprocessing.
- The original image is shown on the left.
- The dual-threshold binary result is shown in the center.
- Both previews support:
  - mouse-wheel zoom
  - left-drag pan
- The binary-result preview additionally supports:
  - right-click hold to temporarily show the original image
- The page includes:
  - enable checkbox
  - lower threshold
  - upper threshold
  - erode
  - dilate
  - open
  - close
  - original-image load button
  - preview controls
- Dual-threshold settings are persisted in `setting.ini`.
- Dual-threshold settings are product-specific and follow the active recipe / product key.
- This tabpage is designer-backed.

## Reference Corner Rules

- Reference source supports preprocess sources 1 to 4 and dual-threshold.
- Reference point mode supports:
  - contour-nearest corner points
  - ROI top-edge bounding points
- Reference corner settings are product-specific.
- When dual-threshold is used as the reference source, dual-threshold settings must be applied before reference-corner restoration.
- This dependency is already wired into product-state restore order.

## Measurement-Distance Rules

- Measurement source dropdown supports five sources:
  - preprocess 1
  - preprocess 2
  - preprocess 3
  - preprocess 4
  - dual-threshold
- When dual-threshold is selected, the measurement preview uses the dual-threshold binary image.
- Measurement overlays drawn on the dual-threshold source use orange lines and points.
- Measurement records still store the selected source name and are reused by downstream workflows.
- Existing measurement edit flow remains:
  - choose parallel or perpendicular mode
  - reselect two points
  - confirm write-back
- Deleting a measurement line still warns because it affects downstream calculations.
- Reprojected measurement points depend on the restored reference basis.
- The restore-order bug for `dual-threshold -> reference -> measurement` has been fixed.

## Multi-Image Confirm Rules

- A dedicated multi-image confirm tab exists in the main UI.
- The tab contains:
  - a large image viewport
  - folder loading
  - previous / next navigation
  - status label
  - preview source group
  - original-image button
  - preprocess-preview button
  - right-side tabbed info area
  - line-sequence button
  - line-display-mode selector
- Preview-source dropdown supports preprocess 1 to 4 and dual-threshold.
- When preview source is dual-threshold, multi-image confirm builds preview output from the current dual-threshold settings of the active recipe / product.
- Switching between original image and preprocess preview preserves zoom and viewport position when possible.
- The selected preprocess preview mode persists while navigating images.
- Rendering uses the existing custom paint-based viewport flow.
- Double buffering remains enabled for the relevant confirm controls.
- The right-side info table appends a final processing-time row in seconds.
- The multi-image confirm calculation follows the same missing-value rules as judgement criteria.
- Continuous inspection uses the same multi-image judgement core, so it follows the same missing-value rules.
- For `max` / `min`, invalid child expressions are skipped instead of forcing the aggregate to `-1`.
- If every child expression is invalid, the displayed and judged value becomes `0`.

## Persistence Rules

- Product-related settings remain stored in `setting.ini`.
- Product profile persistence includes:
  - preprocess snapshots
  - reference corner settings
  - measurement records
  - judgement criteria
  - dual-threshold settings
- Dual-threshold settings are stored per product section, not as a single global value.
- Shared detection sub-parameter order is stored in `setting.ini` `[listSort]`.
- Continuous inspection remembers the last selected main parameter in `setting.ini` using `ContinuousInspectionMainParameter=...`.
- Detection main-parameter order and per-main-parameter associations are stored in `parameterReferenceList.ini`.
- Inner settings remain stored separately in `innerSetting.ini`.
- Inner settings are not product-specific.
- `innerSetting.ini` now also stores `MeasurementScaleFactor`.

## Inner Settings

The inner-settings tab now contains:

- `CCD X?移摨圳
- `CCD Y?移摨圳
- `閮??澆?`

### Inner Settings Behavior

- `CCD X?移摨圳 and `CCD Y?移摨圳 are used by measurement calculations as before.
- `閮??澆?` defaults to `1.0`.
- The measurement scale factor is applied once to the final calculated measurement value.
- The displayed calculation values and the values used by judgement criteria both use the same scaled value.
- Do not apply the multiplier in multiple places.
- The inner-settings numeric controls now support five decimal places.

## Designer-Managed Areas

The following UI areas are intended to remain designer-managed for future maintenance:

- inner settings
- judgement criteria
- dual-threshold page
- detection parameter summary
- continuous inspection

Do not move these back to runtime-generated controls unless explicitly requested.

## Files Touched In This Phase

- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.DetectionParameterSummary.cs`
- `AoiMeasureTool/Forms/MainForm.InnerSettings.cs`
- `AoiMeasureTool/Forms/MainForm.JudgementCriteria.cs`
- `AoiMeasureTool/Forms/MainForm.Measurement.cs`
- `AoiMeasureTool/Models/ProfileModels.cs`
- `AoiMeasureTool/Repositories/InnerSettingsRepository.cs`
- `AoiMeasureTool/Repositories/ParameterReferenceListRepository.cs`
- `PROJECT_HANDOFF.md`

## Recent Git History

- `2441efe` `Increase inner settings precision to five decimals`
- `fdce569` `Adjust inner settings layout`
- `d1a2a22` `Keep continuous inspection main parameter selection`
- `fb0b308` `Add measurement scale factor to inner settings`
- `2bfd21e` `Update judgement syntax help and aggregate fallback`
- `47f7365` `Add judgement syntax help dialog`
- `2b12037` `Add max min aggregate calculations`
- `fb0b308` `Add measurement scale factor to inner settings`
- `d1a2a22` `Keep continuous inspection main parameter selection`
- `fdce569` `Adjust inner settings layout`
- `2441efe` `Increase inner settings precision to five decimals`

## Important Notes

- `AoiMeasureTool.sln /t:Rebuild /p:Configuration=Release` previously built successfully on this machine.
- The build still emits existing OpenCvSharp analyzer warnings, but current functional changes compile cleanly.
- Judgement syntax help now documents the formal missing-value rules:
  - missing child expressions are skipped inside `max` / `min`
  - `min((1)-(2),(3))` falls back to `(3)` when `(2)` is missing
  - non-aggregate expressions with missing required values resolve to `0`
- The detection parameter summary workflow is functional for:
  - creating main parameters
  - reordering main parameters
  - loading shared sub-parameter items from `setting.ini`
  - reordering and persisting shared sub-parameter sort order
  - saving per-main-parameter sub-parameter associations
  - auto-restoring sub-parameter association UI when a main parameter is selected
- The continuous inspection workflow is functional for:
  - selecting a main parameter
  - auto-loading up to three sub-parameters
  - manually loading three images by file picker
  - saving original images immediately on load when enabled
  - running per-slot judgement against the selected sub-parameter profile
  - showing colored A / B / NG results
  - showing per-slot cumulative yield
  - zooming and panning previews without losing view state on reload / judgement
- The inner-settings page now includes a measurement scale factor; keep it synchronized across save/load and avoid double application.

## Suggested Next Step

Likely next work areas are:

- decide whether continuous-inspection yield should persist across restart
- decide whether `靽???敶勗?` should also persist its checkbox state across restart
- validate continuous-inspection overlay appearance against multi-image-confirm side by side
- add delete / rename behavior for detection main parameters if needed
- decide whether `?脣??` should also auto-save on main-parameter switch


