# PROJECT HANDOFF

## Current State

The multi-image confirm workflow is implemented and currently stable at the current stage.
The current UI / interaction behavior is the approved baseline for handoff.
The multi-image confirm viewport overlay now tracks the displayed image correctly during zoom and pan, so ROI / baseline / measurement overlays stay aligned with the image.

## Implemented Behavior

- A dedicated multi-image confirm tab exists in the main WinForms UI.
- The tab contains:
  - a large image viewport
  - folder loading for confirm-result images
  - previous / next navigation buttons
  - a centered status label area for image index display
- Folder loading reads image files from the selected folder and builds an ordered image list.
- The navigation buttons switch between images in sequence.
- The viewer supports:
  - initial full-image display after loading
  - zoom in / zoom out
  - right-click drag panning after zoom
- Rendering uses a custom paint-based approach to avoid flicker and layout issues.
- Double buffering is enabled for the relevant tab / viewport to reduce redraw flicker.
- The multi-image confirm overlay is drawn using the current image scale and offset, so the boxes and baseline follow the image during zooming and panning.

## Reference Corner / ROI Behavior

- The reference corner workflow uses a selected preprocess source as the baseline for detection.
- Multi-image confirm reuses the same preprocess parameters selected for the reference source.
- The current logic does not fall back to Otsu for reference detection in multi-image confirm.
- The reference ROI is drawn on the multi-image confirm viewport as a green overlay.
- The detected reference baseline is also drawn on the multi-image confirm viewport.
- The reference baseline is derived from the detected candidate and is displayed together with the ROI so the relative relationship can be inspected visually.
- The current candidate selection rule is:
  - choose a white object fully inside the ROI
  - prefer the object whose top edge is highest
  - if needed, prefer the larger area among objects with the same top position
- The top baseline endpoints are derived from the detected rotated rectangle corners to reduce drift caused by contour irregularities.

## Files Touched

- `AoiMeasureTool/Forms/MainForm.Designer.cs`
- `AoiMeasureTool/Forms/MainForm.cs`
- `AoiMeasureTool/Forms/MainForm.Measurement.cs`
- `AoiMeasureTool/Forms/MainForm.Preprocess.cs`
- `AoiMeasureTool/Forms/MainForm.ReferenceCorner.cs`
- `AoiMeasureTool/Services/ReferenceCornerDetectionService.cs`

## Important Notes

- The current state is considered correct by the user.
- Do not rework the viewer interaction unless the user requests it.
- The image navigation list is sorted with numeric-aware filename ordering.
- The middle status area is used to show the current image index / total count.
- The reference corner and multi-image confirm flows should stay aligned on the same preprocess source.
- The multi-image confirm overlay should continue to use the same image-space to display-space mapping as the image transform itself.

## Suggested Next Step

If more work is needed later, verify the existing behavior first, then extend from this baseline rather than replacing the viewer flow.
