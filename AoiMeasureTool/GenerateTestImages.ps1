Add-Type -AssemblyName System.Drawing

$outDir = Join-Path $PSScriptRoot "TestImages"
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

function New-AoiImage($path, $darkObject) {
    $bmp = New-Object System.Drawing.Bitmap 900, 620
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.Clear([System.Drawing.Color]::FromArgb(95, 105, 115))

    for ($x = 0; $x -lt 900; $x += 30) {
        $pen = New-Object System.Drawing.Pen ([System.Drawing.Color]::FromArgb(105, 115, 125)), 1
        $g.DrawLine($pen, $x, 0, $x, 620)
        $pen.Dispose()
    }

    $objectColor = if ($darkObject) { [System.Drawing.Color]::FromArgb(25, 25, 25) } else { [System.Drawing.Color]::FromArgb(235, 235, 235) }
    $brush = New-Object System.Drawing.SolidBrush $objectColor
    $g.FillRectangle($brush, 210, 155, 430, 260)
    $g.FillEllipse($brush, 575, 125, 115, 90)
    $brush.Dispose()

    $accent = New-Object System.Drawing.Pen ([System.Drawing.Color]::FromArgb(45, 135, 210)), 4
    $g.DrawRectangle($accent, 210, 155, 430, 260)
    $accent.Dispose()

    $font = New-Object System.Drawing.Font "Arial", 20
    $textBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::White)
    $g.DrawString("AOI Test Pattern", $font, $textBrush, 24, 24)
    $textBrush.Dispose()
    $font.Dispose()

    $bmp.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
    $g.Dispose()
    $bmp.Dispose()
}

New-AoiImage (Join-Path $outDir "aoi_bright_object.png") $false
New-AoiImage (Join-Path $outDir "aoi_dark_object.png") $true
