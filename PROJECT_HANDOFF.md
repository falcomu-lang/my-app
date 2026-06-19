# AOI WinForms Project Handoff

## Project Summary

This project is a WinForms AOI image tool built with:

- Visual Studio Community 2019
- C# 7.3
- .NET Framework 4.8
- WinForms
- x64 target
- `Prefer32Bit = false`
- OpenCvSharp4 `4.13.0.20260602`
- OpenCvSharp4.Extensions `4.13.0.20260602`
- OpenCvSharp4.runtime.win `4.13.0.20260602`

Repository:

- Local path: `C:\Users\falcomu\Documents\Codex\2026-06-16\github\my-app`
- Solution: `AoiMeasureTool.sln`
- GitHub: `https://github.com/falcomu-lang/my-app.git`
- Branch: `main`

## Current Status

- `setting.ini` is active and uses `[ProductKey]` sections.
- `ActiveProductKey` is the loaded image file name without extension.
- Each product section stores:
  - 4 preprocess parameter sets
  - reference-corner enabled state
  - reference-corner source index
  - reference-corner ROI rectangle
  - reference-corner ROI saved flag
  - reference-corner found flag
- New products start from explicit default values instead of inheriting from another product.
- The last successfully loaded image path is stored and auto-loaded on startup if it still exists.
- The left sidebar contains:
  - image viewer
  - reference corner
- The right side switches between the normal workspace tabs and the dedicated reference-corner page.
- The current Release build succeeds.
- The build still shows existing OpenCvSharp analyzer warnings about missing `Microsoft.CodeAnalysis 4.8.0.0`, but the EXE is produced correctly.

## `setting.ini` Format

`setting.ini` is plain text and lives beside the executable.

Example:

```ini
ImagePath=C:\images\ABC_001.bmp
ActiveProductKey=ABC_001

[ABC_001]
Preprocess1Enabled=True
Preprocess1Threshold=128
Preprocess1Erode=0
Preprocess1Dilate=0
Preprocess1Open=0
Preprocess1Close=0
Preprocess2Enabled=True
Preprocess2Threshold=128
Preprocess2Erode=0
Preprocess2Dilate=0
Preprocess2Open=0
Preprocess2Close=0
Preprocess3Enabled=False
Preprocess3Threshold=128
Preprocess3Erode=0
Preprocess3Dilate=0
Preprocess3Open=0
Preprocess3Close=0
Preprocess4Enabled=True
Preprocess4Threshold=140
Preprocess4Erode=0
Preprocess4Dilate=1
Preprocess4Open=0
Preprocess4Close=0
ReferenceCornerEnabled=True
ReferenceSourceIndex=0
ReferenceRoiX=689
ReferenceRoiY=739
ReferenceRoiWidth=329
ReferenceRoiHeight=82
ReferenceRoiSaved=True
ReferenceCornerFound=True
```

Field meanings:

- `Preprocess1` to `Preprocess4`: the four preprocess slots
- `Enabled`: whether the slot is active
- `Threshold`: binary threshold value
- `Erode`: erosion iterations
- `Dilate`: dilation iterations
- `Open`: open-morphology iterations
- `Close`: close-morphology iterations
- `ReferenceCornerEnabled`: whether reference-corner search is enabled
- `ReferenceSourceIndex`: which preprocess preview is used as the base
- `ReferenceRoiX/Y/Width/Height`: ROI rectangle in image coordinates
- `ReferenceRoiSaved`: whether the ROI is currently treated as saved
- `ReferenceCornerFound`: whether a valid target object was found for the saved ROI/source combination

## Main UI

- Window size: `1280 x 800`
- Left sidebar contains the main work items.
- The main workspace on the right is driven by tab pages.
- The reference-corner workspace is shown as a dedicated page when selected from the sidebar.

### Image Viewer

- Opens standard image files such as BMP, JPG, JPEG, PNG, TIF, TIFF.
- Loads images with `Cv2.ImRead(..., ImreadModes.Color)`.
- Supports fit-to-viewport and zoom/pan.
- Keeps the last loaded image path in settings.

### Binarization

- Contains 4 preprocess slots.
- Each slot has:
  - Enable checkbox
  - Threshold value
  - Erode iterations
  - Dilate iterations
  - Open iterations
  - Close iterations
- Threshold is synchronized between trackbar and numeric input.
- Preprocess images are generated from the grayscale source image.
- Right-click on the active preprocess preview temporarily shows the original image and restores the processed view on release without losing zoom/pan state.

### Reference Corner

- This is a separate workspace for reference-corner search.
- The page includes:
  - enable checkbox
  - preprocess image source dropdown
  - large preview area
  - direct ROI drag on the preview
  - `?? ROI ??` / `???? ROI ??` button
- The user flow is:
  1. enable reference-corner search
  2. choose the preprocess source
  3. drag an ROI directly on the preview
  4. click save
  5. see the selected candidate overlay appear

## Reference Corner Behavior

The reference-corner logic is no longer a placeholder. It is already implemented in `MainForm.cs`.

### Detection Rules

- The search source is the selected preprocess image after thresholding.
- Candidate objects are extracted from the binarized image using `Cv2.ConnectedComponentsWithStats(...)`.
- Only connected components whose bounding rectangle is fully inside the ROI are eligible.
- "Nearest" means the connected component whose centroid is closest to the ROI center.
- If no component is fully inside the ROI, the result is treated as not found.

### Overlay Rules

- When a candidate is found, the preview draws:
  - the candidate bounding box
  - a circular point at the top-left corner
  - a circular point at the top-right corner
  - a circular point at the object center
- The ROI itself is drawn as a green translucent rectangle.

### Save / Refresh Rules

- ROI drag immediately clears the previously found result.
- During ROI re-selection, the old candidate overlay should not remain visible.
- The candidate overlay appears again only after the user clicks save.
- If the user re-enters the reference-corner page and a saved ROI already exists, the overlay is recalculated and shown again.
- Switching to another preprocess source forces the state to behave as "no ROI selected / no valid result":
  - ROI rectangle is cleared
  - ROI saved flag becomes `false`
  - found flag becomes `false`
  - the new state is saved immediately

### Immediate Persistence Rules

The following actions immediately persist the reference-corner state:

- checking the enable checkbox
- unchecking the enable checkbox
- changing the preprocess source dropdown
- starting a new ROI drag
- moving the ROI while dragging
- clicking save ROI
- any transition of `_referenceCornerFound` from `true` to `false`
- any transition of `_referenceCornerFound` from `false` to `true`

This persistence is handled by:

- `PersistReferenceCornerState()`
- `UpdateReferenceCornerFoundState(bool found, bool previousFound)`

## Important Code Locations

- `AoiMeasureTool\MainForm.cs`
  - main UI logic
  - image loading
  - preprocess generation
  - workspace switching
  - settings load/save
  - reference-corner ROI and candidate logic
- `AoiMeasureTool\MainForm.Designer.cs`
  - WinForms layout
  - reference-corner page controls
- `AoiMeasureTool\ImageProcessor.cs`
  - OpenCV preprocessing pipeline
- `AoiMeasureTool\PreprocessParam.cs`
  - preprocess parameter model
- `AoiMeasureTool\Program.cs`
  - WinForms startup
  - native DLL path setup

## Important Methods In `MainForm.cs`

- `CaptureCurrentReferenceCornerSnapshot()`
  - packages enabled/source/ROI/ROI saved/found state
- `ApplyReferenceCornerSnapshot(...)`
  - restores saved reference-corner state
- `PersistReferenceCornerState()`
  - writes the active product reference-corner snapshot into memory and `setting.ini`
- `ReferenceCornerEnabled_CheckedChanged(...)`
  - enable checkbox now saves immediately on both check and uncheck
- `ReferenceSource_SelectedIndexChanged(...)`
  - clears ROI/result and saves immediately
- `RefreshReferenceCornerCandidate()`
  - recalculates the candidate from the selected preprocess image
- `FindReferenceCornerCandidate(...)`
  - connected-component selection using ROI-center distance
- `ReferencePreview_MouseDown(...)`
  - starts ROI drag and clears previous found result
- `ReferencePreview_MouseMove(...)`
  - updates ROI while dragging and persists current state
- `ReferencePreview_Paint(...)`
  - draws ROI and candidate overlay
- `SaveReferenceRoi_Click(...)`
  - finalizes ROI and re-runs candidate search

## Known UX Notes

- `labelReferenceCornerStatus` is intentionally hidden during normal reference-corner interaction. The debug-style status text is not meant to stay visible anymore.
- Some user-facing strings were recently corrected after temporary encoding damage in `MainForm.cs`.
- If more garbled text appears later, first search `MainForm.cs` for hard-coded UI strings before editing `Designer.cs`.

## OpenCV Native Runtime Notes

If `OpenCvSharp.Internal.NativeMethods` fails to load, check:

- the build target is x64
- `Prefer32Bit` stays disabled
- native DLLs exist under the expected runtime folder
- `Program.cs` adds the `dll\x64` path before OpenCV is used

## Suggested Next Work

Most of the requested reference-corner workflow is already in place. The next useful work items are likely:

1. Add explicit serialization helpers for `ReferenceCornerFound` in every `setting.ini` read/write branch if future refactors touch settings code again.
2. Add a small regression checklist for ROI/source switching because this flow is stateful and easy to break.
3. Consider extracting reference-corner logic from `MainForm.cs` into a smaller helper class if the feature keeps growing.
4. If multilingual UI matters, centralize user-facing strings instead of leaving them inline in `MainForm.cs`.

## Handoff Checklist

When continuing work, verify:

- `git status`
- Visual Studio 2019 build
- .NET Framework 4.8 compatibility
- OpenCV native runtime loading
- last image auto-load still works
- product-specific preprocess settings still restore correctly
- saved reference-corner state restores correctly per product
- switching preprocess source clears ROI/result immediately
- ROI drag hides the old overlay until save is clicked
- saved ROI reappears correctly when returning to the reference-corner page
- checkbox enable/disable saves immediately
