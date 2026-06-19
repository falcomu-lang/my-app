# WinForms Image Viewer

Simple C# WinForms and OpenCvSharp4 image viewer for Visual Studio 2019 and .NET Framework 4.8.

## Features

- Fixed 1280 x 800 interface with an 820 x 600 image preview.
- Standard WinForms Designer controls with fixed locations and sizes.
- Light desktop-tool layout with a top menu, left navigation, and main preview workspace.
- Load BMP, JPG, PNG, and TIFF images through `Cv2.ImRead`.
- Display the selected image with a standard WinForms `Panel` and `PictureBox` so the Visual Studio 2019 Designer can load it.
- Fit the image when loaded, zoom with the mouse wheel, and pan by dragging with the left mouse button.
- Display file name and image dimensions.
- Keep the loaded OpenCV `Mat` ready for future image processing.
- Build as x64 and copy the OpenCV native runtime beside the executable.
- Show four independent binary preprocessing results at the same time.
- Preprocess 1 and 2 detect bright objects with `Gray > Threshold`.
- Preprocess 3 and 4 detect dark objects with `Gray < Threshold`.
- Configure enable, threshold, erode, dilate, open, and close independently for every pipeline.

## Open in Visual Studio

Open `AoiMeasureTool.sln` with Visual Studio 2019. In Solution Explorer, right-click `MainForm.cs` and select `View Designer`.
