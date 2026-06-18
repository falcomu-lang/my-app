# AOI Image Size Measurement Tool

C# WinForms AOI image size measurement tool for Visual Studio 2019, .NET Framework 4.8, and OpenCvSharp4.

## Features

- Load BMP, JPG, PNG, and TIFF images.
- Display original image and four independent preprocessing result images.
- Convert all processing input to grayscale even when the source image is color.
- Four threshold pipelines:
  - Preprocess 1: Gray > Threshold
  - Preprocess 2: Gray > Threshold
  - Preprocess 3: Gray < Threshold
  - Preprocess 4: Gray < Threshold
- Per-pipeline enable, threshold, erode, dilate, open, and close settings.
- ROI setting by NumericUpDown controls.
- ROI overlay on the main image.
- Upper-edge helper that finds the leftmost and rightmost valid upper-edge points in ROI.
- Top-bottom and left-right distance measurement.
- Pixel, um, and mm result display.

## Requirements

- Visual Studio 2019
- .NET Framework 4.8 targeting pack
- NuGet restore enabled
- Visual C++ Redistributable for native OpenCvSharp runtime

## Build

Open `AoiMeasureTool.sln` in Visual Studio 2019, restore NuGet packages, and build.

Command-line build:

```powershell
MSBuild.exe AoiMeasureTool.sln /restore /p:Configuration=Release
```

## Test Images

Sample images are included in `AoiMeasureTool\TestImages` after generation. They contain bright and dark objects for threshold and ROI tests.
