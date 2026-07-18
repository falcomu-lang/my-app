# my-app

WinForms AOI measurement application built with `.NET Framework 4.7.2`.

## Overview

This project is a desktop inspection tool for image viewing, preprocessing, measurement, judgement criteria, and continuous inspection workflows.

## Start Here

Read [PROJECT_HANDOFF.md](./PROJECT_HANDOFF.md) first for the current baseline, rules, and next steps.

## Current Highlights

- WinForms UI with the left sidebar as the main workspace navigator.
- Role-based access for `操作員`, `工程師`, and `管理者`.
- Continuous inspection APIs for both manual and camera/`Mat` input.
- Multi-image confirm and preprocessing workspaces are already in place.
- The top `MenuStrip` is removed from both Designer and runtime UI.

## Build

Open `AoiMeasureTool.sln` with Visual Studio 2019 and build with the configured .NET Framework target.

Known verified build command:

```powershell
& 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe' 'C:\Users\falcomu\Documents\Codex\程式撰寫 專案資料夾\量測程式\my-app\AoiMeasureTool.sln' /t:Rebuild /p:Configuration=Debug
```

## Repository Files

- [`PROJECT_HANDOFF.md`](./PROJECT_HANDOFF.md): detailed handoff notes
- [`README.md`](./README.md): short project overview
