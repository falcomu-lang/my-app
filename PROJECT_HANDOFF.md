# PROJECT HANDOFF

## Current Baseline

The multi-image confirm workflow is implemented and stable at the current stage.
The current UI and interaction behavior is the approved handoff baseline.
Do not rework the viewer flow unless a later request explicitly asks for it.

## What Is Working

- The multi-image confirm viewport overlay tracks the displayed image correctly during zoom and pan.
- ROI, baseline, and measurement overlays stay aligned with the image during interaction.
- The multi-image confirm preprocess preview and overlay behavior has been verified by the user as correct.
- The right-side info table in multi-image confirm is part of the approved baseline.
- Multi-image confirm can show:
  - configured source lines
  - detected measured line segments
  - no line overlays
- The detected-line overlay path is cached, so zoom and pan do not re-run line analysis on every paint.
- Switching between preprocess preview and original image preserves the current zoom ratio and viewport position when possible.
- The left sidebar work items control which tabpages are shown.
- The image viewer work item shows only:
  - image viewer
  - binarization
- The reference-corner, measurement-distance, and multi-image-confirm work items each show only their matching workspace tabs.
- The measurement-distance workflow supports editing an existing line segment by reselecting two points after choosing parallel or perpendicular mode.
- Deleting a measurement line segment shows a warning because it affects downstream calculations and saved measurement results.
- The project target framework is .NET Framework 4.7.2.
- The left sidebar also includes:
  - inner settings
  - judgement criteria
- The inner-settings and judgement-criteria tabs are designer-managed tabpages and controls.
- Those tabs are hidden on startup and only appear when their matching sidebar item is selected.
- Inner settings are stored in `innerSetting.ini` beside the executable and do not change when the active product changes.
- Inner settings currently expose CCD X precision and CCD Y precision with a dedicated save button.
- The multi-image confirm info table shows each valid line measurement as millimeters first, with pixel distance in parentheses.

## Multi-Image Confirm Behavior

- A dedicated multi-image confirm tab exists in the main WinForms UI.
- The tab contains:
  - a large image viewport
  - folder loading for confirm-result images
  - previous / next navigation buttons
  - a centered status label area for image index display
  - a preprocess preview source group with:
    - a dropdown for preprocess image 1-4
    - a load-preprocess-image button
    - an original-image button
  - a right-side info table
  - a line-sequence button
  - a line-display-mode selector
- Folder loading reads image files from the selected folder and builds an ordered image list.
- The navigation buttons switch between images in sequence.
- When preprocess preview mode is enabled, previous / next navigation keeps showing preprocess output for each image until original-image mode is selected.
- The preprocess preview dropdown selects which preprocess profile output to display.
- The viewer supports:
  - initial full-image display after loading
  - zoom in / zoom out
  - right-click drag panning after zoom
- Rendering uses a custom paint-based approach to avoid flicker and layout issues.
- Double buffering is enabled for the relevant tab and viewport to reduce redraw flicker.
- Multi-image confirm overlays are mapped against the original source image coordinate size even when the displayed bitmap is a preprocess preview.
- The preprocess preview bitmap is display-only; measurement lines, ROI, and the reference baseline continue to use source-image coordinates.

## Workspace and Sidebar Rules

- The left sidebar work items are ordered as:
  - image viewer
  - reference corner
  - measurement distance
  - multi-image confirm
  - inner settings
  - judgement criteria
- Selecting a work item shows only the corresponding tabpages for that workspace.
- The image viewer workspace shows only the image viewer and binarization tabs.
- The reference-corner workspace shows only the reference-corner tab.
- The measurement-distance workspace shows only the measurement-distance tab.
- The multi-image-confirm workspace shows only the multi-image-confirm tab.
- The inner-settings workspace shows only the inner-settings tab.
- The judgement-criteria workspace shows only the judgement-criteria tab.
- The inner-settings and judgement-criteria tabpages are part of the form designer, but they remain hidden from the startup tab strip until their sidebar item is selected.

## Multi-Image Preview Source Rules

- Multi-image confirm has a preview source group below the folder-load controls.
- The dropdown contains:
  - preprocess image 1
  - preprocess image 2
  - preprocess image 3
  - preprocess image 4
- The load-preprocess-image button displays the selected preprocess output for the currently displayed confirm image.
- The original-image button returns the viewport to the original image.
- The selected preprocess preview mode persists while navigating previous / next images.
- Preprocess preview generation uses the current product's preprocess parameters.
- Multi-image confirm uses the current product context as the product key.
- Confirm-result folder names should not replace the active product profile.
- If current unsaved product parameters exist in the UI, they are used for preview so the result reflects the current working state.
- Switching between original image and preprocess preview preserves the current zoom ratio and current viewport center when possible.
- The image toggle should not force a refit unless there was no valid previous view state.

## Reference Corner and ROI Rules

- The reference corner workflow uses a selected preprocess source as the baseline for detection.
- Multi-image confirm reuses the same preprocess parameters selected for the reference source.
- Multi-image confirm reference detection uses the current product's reference corner profile:
  - enabled state
  - source preprocess index
  - saved ROI
- The current logic does not fall back to Otsu for reference detection in multi-image confirm.
- Multi-image confirm uses the same reference candidate selection rule as reference corner detection:
  - choose a white object fully inside the ROI
  - prefer the largest object by area
- The reference ROI is drawn on the multi-image confirm viewport as a green overlay.
- The detected reference baseline is also drawn on the multi-image confirm viewport.
- The reference baseline is derived from the detected candidate and is displayed together with the ROI so the relative relationship can be inspected visually.
- The top baseline endpoints are derived from the detected rotated rectangle corners to reduce drift caused by contour irregularities.
- Reference detection in multi-image confirm is always computed from the original image file, then the product profile preprocess source is applied for detection.
- Do not run reference detection from the already displayed preprocess preview bitmap, or the reference candidate can drift.

## Measurement Overlay Rules

- Multi-image confirm measurement lines use the current product's measurement records.
- If the current product has live in-memory measurement records, those are preferred so unsaved current UI changes are reflected.
- If live records are not available, the saved product measurement profile is used.
- As a fallback, currently loaded measurement records may be used so existing lines continue to render.
- Measurement line reprojection uses the reference candidate detected from the current confirm image.
- Measurement, ROI, and reference baseline overlays must all use the same source-image coordinate mapping.
- A line-display-mode selector controls whether the viewport shows:
  - the configured source lines
  - the detected measured line segments
  - no line overlays
- Changing the line-display mode should only trigger a redraw.
- It must not reset zoom or pan.
- The line-sequence button temporarily labels line start points with the 1-based line sequence defined in the measurement distance setup for about three seconds.

## Multi-Image Line Measurement Rules

- The right-side info table includes a top-level judgement row and per-line measurement results.
- The `whether it can be judged` row is `can judge` only when:
  - all relevant points are inside the saved ROI
  - a reference corner / baseline candidate was found
- For each line segment, the measurement logic uses the preprocess source that was originally associated with that line segment.
- The implementation currently infers the preprocess source from the saved `SourceName` text such as `preprocess 1` through `preprocess 4`.
- For each line segment:
  - build the corresponding binary preprocess image for the current confirm image
  - sample points along the segment path
  - find white runs along that path
  - keep only the longest continuous white run
  - use the two ends of that longest white run as the detected measured line
  - report the resulting distance in pixels
  - convert the detected segment to millimeters using inner-settings CCD X precision and CCD Y precision
- When the detected-line display mode is active, the viewport draws the detected longest-white-run segment instead of the configured source line.
- The detected-line analysis path is cached per image, line, and mode so pan and zoom do not re-run preprocess analysis during paint.
- The current result table displays each valid line as `{mm} mm ({px} px)`.
- Millimeter conversion uses anisotropic scaling:
  - multiply X delta by CCD X precision
  - multiply Y delta by CCD Y precision
  - calculate Euclidean distance in millimeters
- The line-sequence overlay must follow the configured measurement line order from the distance setup, not a screen-position sort.

## Measurement Editing Rules

- Existing measurement line segments can be edited from the record table.
- Editing requires choosing parallel or perpendicular mode first.
- After choosing the mode, the user reselects two points on the image.
- Confirmation writes the updated line back to the selected record.
- Cancel leaves the original record unchanged.
- Deleting a measurement line segment prompts a warning because it affects later calculations and saved results.

## Inner Settings Rules

- Inner settings are not product-specific.
- The file path is `innerSetting.ini` in the executable directory.
- The current fields are:
  - CCD X precision
  - CCD Y precision
- Inner settings use an explicit save button.
- They are not intended to be treated as product profile state.
- The inner-settings UI is designer-managed so the controls can be repositioned or extended visually in the WinForms designer.

## Judgement Criteria Rules

- A dedicated judgement-criteria work item exists in the left sidebar.
- Selecting it shows a dedicated judgement-criteria tabpage.
- The tabpage is intentionally empty for now and acts as a reserved workspace for future conditions UI.
- The judgement-criteria tabpage is designer-managed so future controls can be added and repositioned visually.

## Files Touched

- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.Measurement.cs`
- `AoiMeasureTool/Forms/MainForm.InnerSettings.cs`
- `AoiMeasureTool/Forms/MainForm.JudgementCriteria.cs`
- `AoiMeasureTool/Forms/MeasureDirectionDialog.cs`
- `AoiMeasureTool/Forms/MainForm.Preprocess.cs`
- `AoiMeasureTool/Forms/MainForm.ReferenceCorner.cs`
- `AoiMeasureTool/Services/ReferenceCornerDetectionService.cs`
- `AoiMeasureTool/Services/MeasurementOverlayService.cs`
- `AoiMeasureTool/Services/MeasurementRecordService.cs`
- `AoiMeasureTool/Services/PreprocessPipelineService.cs`
- `AoiMeasureTool/Utilities/ImageProcessor.cs`
- `AoiMeasureTool/Repositories/InnerSettingsRepository.cs`
- `AoiMeasureTool/Models/ProfileModels.cs`
- `AoiMeasureTool/AoiMeasureTool.csproj`

## Important Notes

- The current state is considered correct by the user unless a later request changes behavior explicitly.
- The image navigation list is sorted with numeric-aware filename ordering.
- The middle status area is used to show the current image index and total count.
- The reference corner and multi-image confirm flows should stay aligned on the same preprocess source.
- The multi-image confirm overlay should continue to use the same image-space to display-space mapping as the image transform itself.
- Do not infer the product profile from the confirm-result folder name unless product selection behavior is explicitly redesigned.
- Keep source-image coordinate mapping separate from the displayed preprocess preview bitmap.
- Keep workspace tab visibility aligned with the left sidebar item that launched the workspace.
- Preserve the reselect-two-points editing flow for measurement records unless the user requests a redesign.
- If future changes affect preprocess preview, verify that measurement lines still fit the displayed image in both original-image and preprocess-preview modes.
- If future changes affect detected-line measurement, preserve the rule that each line uses its own associated preprocess source rather than the currently selected preview source.
- The current association to a preprocess source is text-derived from `SourceName`; if this area is refactored later, prefer storing an explicit preprocess index on the measure record.
- Keep detected-line measurement work out of the paint path.
- Recompute only when the image, source, or relevant measurement inputs change.
- Keep inner settings separate from `setting.ini` product-profile persistence.
- Keep startup workspace behavior such that inner-settings and judgement-criteria tabs are hidden until explicitly opened from the sidebar.
- If future changes extend inner settings or judgement criteria, prefer continuing to manage their controls in the WinForms designer rather than reverting to runtime-generated controls.

## Suggested Next Step

If more work is needed later, verify the existing behavior first, then extend from this baseline rather than replacing the viewer flow.
