# WinForms Image Viewer

Simple C# WinForms and OpenCvSharp4 image viewer for Visual Studio 2019 and .NET Framework 4.8.

## Features

- Fixed 1920 x 1080 interface.
- Standard WinForms Designer controls with fixed locations and sizes.
- Light desktop-tool layout with a top menu, left navigation, and main preview workspace.
- Load BMP, JPG, PNG, and TIFF images through `Cv2.ImRead`.
- Display the selected image with `PictureBoxSizeMode.Zoom`.
- Display file name and image dimensions.
- Keep the loaded OpenCV `Mat` ready for future image processing.

## Open in Visual Studio

Open `AoiMeasureTool.sln` with Visual Studio 2019. In Solution Explorer, right-click `MainForm.cs` and select `View Designer`.
