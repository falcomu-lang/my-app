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

## What Is Working

- Image viewer supports load, zoom, and pan.
- Original binarization workflow remains in place.
- Reference corner workflow remains connected to product-specific preprocess settings.
- Measurement-distance workflow supports line creation, edit, delete, and downstream reuse.
- Multi-image confirm supports original-image preview, preprocess preview, navigation, overlays, and right-side info tabs.
- Inner settings and judgement criteria are designer-managed tabs.

## Dual-Threshold Page

- A second binarization tab exists: `二值化處理-2`.
- This page uses dual-threshold preprocessing.
- The original image is shown on the left.
- The dual-threshold binary result is shown in the center.
- The result preview supports:
  - mouse-wheel zoom
  - left-drag pan
  - right-click hold to temporarily show the original image
- The original-image preview on this page also supports zoom and pan.
- The page includes:
  - enable checkbox
  - lower threshold
  - upper threshold
  - erode
  - dilate
  - open
  - close
  - `讀取設定`
  - `儲存目前設定`
- Dual-threshold settings are persisted in `setting.ini`.
- `二值化處理-2` is now a designer-backed tabpage.
- Its child controls are now also designer-managed so future maintenance can reposition controls directly in the WinForms designer.
- Runtime logic now binds to those designer controls instead of relying only on runtime-generated UI.

## Measurement-Distance Rules

- Measurement source dropdown now supports five sources:
  - `前處理 1`
  - `前處理 2`
  - `前處理 3`
  - `前處理 4`
  - `雙門檻`
- When `雙門檻` is selected, the measurement preview uses the dual-threshold binary image.
- Measurement overlays drawn on the dual-threshold source use orange lines and points.
- Measurement records still store the selected source name and are reused by downstream workflows.
- Existing measurement edit flow remains:
  - choose parallel or perpendicular mode
  - reselect two points
  - confirm write-back
- Deleting a measurement line still warns because it affects downstream calculations.

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
- Preview-source dropdown now supports:
  - `前處理影像 1`
  - `前處理影像 2`
  - `前處理影像 3`
  - `前處理影像 4`
  - `雙門檻`
- When preview source is `雙門檻`, multi-image confirm builds preview output from the current dual-threshold settings.
- Switching between original image and preprocess preview preserves zoom and viewport position when possible.
- The selected preprocess preview mode persists while navigating images.
- Rendering uses the existing custom paint-based viewport flow.
- Double buffering remains enabled for the relevant confirm controls.

## Multi-Image Overlay and Analysis Rules

- ROI, baseline, and measurement overlays stay aligned during zoom and pan.
- Overlay mapping uses source-image coordinates, not display-bitmap coordinates.
- Multi-image confirm detected-line analysis remains cached outside the paint path.
- Line-display mode still supports:
  - configured source lines
  - detected measured line segments
  - no line overlays
- Each saved measurement line still uses its own associated source when downstream measurement is recalculated.
- Source association is still inferred from saved `SourceName`.
- That source parsing now also recognizes `雙門檻`.

## Persistence Rules

- Product-related settings remain stored in `setting.ini`.
- Dual-threshold settings are stored in the same app settings payload.
- Inner settings remain stored separately in `innerSetting.ini`.
- Inner settings are not product-specific.

## Designer-Managed Areas

The following UI areas are intended to remain designer-managed for future maintenance:

- inner settings
- judgement criteria
- `二值化處理-2`

Do not move these back to runtime-generated controls unless explicitly requested.

## Files Touched In This Phase

- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.Measurement.cs`
- `AoiMeasureTool/Forms/MainForm.Preprocess.cs`
- `AoiMeasureTool/Models/PreprocessParam.cs`
- `AoiMeasureTool/Models/ProfileModels.cs`
- `AoiMeasureTool/Repositories/IniSettingsRepository.cs`
- `AoiMeasureTool/Services/MeasurementOverlayService.cs`
- `AoiMeasureTool/Services/PreprocessProfileApplier.cs`
- `AoiMeasureTool/Utilities/ImageProcessor.cs`
- `AoiMeasureTool/Utilities/ProfileDataCloner.cs`
- `PROJECT_HANDOFF.md`

## Recent Git History

- `1ffac39` `Move dual-threshold tab controls into designer`
- `de76754` `Add dual-threshold multi-image preview source`
- `652f0f1` `Fix dual-threshold snapshot naming conflict`
- `a1917b4` `Add dual-threshold measurement source overlay`
- `f716ba9` `Make dual-threshold tab designer-visible`

## Important Notes

- There is an existing local MSBuild environment issue on this machine, so full rebuild verification was not reliable during this phase.
- The dual-threshold page now has both functional logic and designer-managed layout support.
- If future work changes source-selection logic, be careful not to break:
  - measurement-distance source mapping
  - multi-image confirm preview-source mapping
  - downstream line analysis source mapping
- If future work changes dual-threshold settings structure, update:
  - UI binding
  - `setting.ini` persistence
  - multi-image confirm preview generation
  - measurement source handling

## Suggested Next Step

Before further feature work, verify these three areas together in the UI:

- `二值化處理-2`
- `框選量測的距離`
- `多影像確認結果`
