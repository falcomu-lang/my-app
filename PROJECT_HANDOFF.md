# PROJECT HANDOFF

## Current State

The multi-image confirm workflow is implemented and currently stable at the current stage.
The current UI / interaction behavior is the approved baseline for handoff.
The multi-image confirm viewport overlay now tracks the displayed image correctly during zoom and pan, so ROI / baseline / measurement overlays stay aligned with the image.
The multi-image confirm preprocess preview and overlay behavior has been verified by the user as correct.
The multi-image confirm right-side info table is now part of the approved baseline.
The multi-image confirm supports showing the configured source lines, the detected measured line segments, or hiding line overlays entirely.
The detected-line overlay path is now cached so zooming and panning do not re-run line analysis on every paint.
Switching between preprocess preview and original image now preserves the current zoom ratio and viewport position instead of resetting to fit.
The left sidebar work items now control which tabpages are shown.
The image viewer work item only shows the image viewer and binarization tabpages.
The reference-corner, measurement-distance, and multi-image-confirm work items each show their matching workspace tabs when selected.
The measurement-distance workflow now supports editing an existing line segment by reselecting two points after choosing parallel or perpendicular mode.
When deleting a measurement line segment, the UI shows a warning that the deletion will affect downstream calculations and saved measurement results.
The project target framework is now .NET Framework 4.7.2.

## Implemented Behavior

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
- Double buffering is enabled for the relevant tab / viewport to reduce redraw flicker.
- The multi-image confirm overlay is drawn using the current image scale and offset, so the boxes and baseline follow the image during zooming and panning.
- Multi-image confirm overlays are mapped against the original source image coordinate size even when the displayed bitmap is a preprocess preview.
- The preprocess preview bitmap is display-only; measurement lines, ROI, and reference baseline continue to use source-image coordinates.
- The main workspace only shows the image viewer and binarization tabpages when entering the image viewer work item.

## Sidebar Workspace Behavior

- The left sidebar work items are ordered as:
  - image viewer
  - reference corner
  - measurement distance
  - multi-image confirm
- Selecting a work item shows only the corresponding tabpages for that workspace.
- The image viewer workspace shows only:
  - image viewer
  - binarization
- The reference-corner workspace shows only the reference-corner tab.
- The measurement-distance workspace shows only the measurement-distance tab.
- The multi-image-confirm workspace shows only the multi-image-confirm tab.

## Multi-Image Preprocess Preview Behavior

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
- Multi-image confirm uses the current product context as the product key. Confirm-result folder names should not replace the active product profile.
- If current unsaved product parameters exist in the UI, they are used for preview so the result reflects the current working state.
- Switching between original image and preprocess preview preserves the current zoom ratio and current viewport center when possible.
- The image toggle should not force a refit unless there was no valid previous view state.

## Reference Corner / ROI Behavior

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
- The current candidate selection rule is:
  - choose a white object fully inside the ROI
  - prefer the largest object by area
- The top baseline endpoints are derived from the detected rotated rectangle corners to reduce drift caused by contour irregularities.
- Reference detection in multi-image confirm is always computed from the original image file, then the product profile preprocess source is applied for detection.
- Do not run reference detection from the already displayed preprocess preview bitmap, or the reference candidate can drift.

## Measurement Overlay Behavior

- Multi-image confirm measurement lines use the current product's measurement records.
- If the current product has live in-memory measurement records, those are preferred so unsaved current UI changes are reflected.
- If live records are not available, the saved product measurement profile is used.
- As a fallback, currently loaded measurement records may be used so existing lines continue to render.
- Measurement line reprojection uses the reference candidate detected from the current confirm image.
- Measurement, ROI, and reference baseline overlays must all use the same source-image coordinate mapping.
- A line-display-mode selector now controls whether the viewport shows:
  - the configured source lines
  - the detected measured line segments
  - no line overlays
- Changing the line-display mode should only trigger a redraw. It must not reset zoom or pan.
- The line-sequence button temporarily labels line start points with 1-based indices for about three seconds.

## Multi-Image Line Measurement Behavior

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
- When the detected-line display mode is active, the viewport draws the detected longest-white-run segment instead of the configured source line.
- The detected-line analysis path is cached per image / line / mode so pan and zoom do not re-run preprocess analysis during paint.
- The current result table displays pixel distances only. No physical-unit conversion is implemented yet.

## Measurement Editing Behavior

- Existing measurement line segments can be edited from the record table.
- Editing requires choosing parallel or perpendicular mode first.
- After choosing the mode, the user reselects two points on the image.
- Confirmation writes the updated line back to the selected record.
- Cancel leaves the original record unchanged.
- Deleting a measurement line segment prompts a warning because it affects later calculations and saved results.

## Files Touched

- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.Measurement.cs`
- `AoiMeasureTool/Forms/MeasureDirectionDialog.cs`
- `AoiMeasureTool/Forms/MainForm.Preprocess.cs`
- `AoiMeasureTool/Forms/MainForm.ReferenceCorner.cs`
- `AoiMeasureTool/Services/ReferenceCornerDetectionService.cs`
- `AoiMeasureTool/Services/MeasurementOverlayService.cs`
- `AoiMeasureTool/Services/MeasurementRecordService.cs`
- `AoiMeasureTool/Services/PreprocessPipelineService.cs`
- `AoiMeasureTool/Utilities/ImageProcessor.cs`

## Important Notes

- The current state is considered correct by the user unless a later request changes behavior explicitly.
- Do not rework the viewer interaction unless the user requests it.
- The image navigation list is sorted with numeric-aware filename ordering.
- The middle status area is used to show the current image index / total count.
- The reference corner and multi-image confirm flows should stay aligned on the same preprocess source.
- The multi-image confirm overlay should continue to use the same image-space to display-space mapping as the image transform itself.
- Do not infer the product profile from the confirm-result folder name unless product selection behavior is explicitly redesigned.
- Keep source-image coordinate mapping separate from the displayed preprocess preview bitmap.
- Keep workspace tab visibility aligned with the left sidebar item that launched the workspace.
- Preserve the reselect-two-points editing flow for measurement records unless the user requests a redesign.
- If future changes affect preprocess preview, verify that measurement lines still fit the displayed image in both original-image and preprocess-preview modes.
- If future changes affect detected-line measurement, preserve the rule that each line uses its own associated preprocess source rather than the currently selected preview source.
- The current association to a preprocess source is text-derived from `SourceName`; if this area is refactored later, prefer storing an explicit preprocess index on the measure record.
- Keep detected-line measurement work out of the paint path. Recompute only when the image, source, or relevant measurement inputs change.

## Suggested Next Step

If more work is needed later, verify the existing behavior first, then extend from this baseline rather than replacing the viewer flow.
