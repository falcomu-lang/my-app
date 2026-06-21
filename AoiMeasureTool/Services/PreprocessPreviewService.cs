using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal static class PreprocessPreviewService
    {
        public static void FitToViewport(PictureBox pictureBox, Control viewport, ref float imageScale, ref float fitScale)
        {
            if (pictureBox.Image == null)
            {
                pictureBox.Location = Point.Empty;
                pictureBox.Size = viewport.ClientSize;
                imageScale = 1f;
                fitScale = 1f;
                return;
            }

            var scaleX = viewport.ClientSize.Width / (float)pictureBox.Image.Width;
            var scaleY = viewport.ClientSize.Height / (float)pictureBox.Image.Height;
            fitScale = Math.Min(scaleX, scaleY);
            imageScale = fitScale;
            var width = Math.Max(1, (int)Math.Round(pictureBox.Image.Width * imageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBox.Image.Height * imageScale));
            pictureBox.Size = new Size(width, height);
            pictureBox.Left = (viewport.ClientSize.Width - width) / 2;
            pictureBox.Top = (viewport.ClientSize.Height - height) / 2;
        }

        public static void ConstrainPosition(PictureBox pictureBox, Control viewport)
        {
            pictureBox.Left = pictureBox.Width <= viewport.ClientSize.Width
                ? (viewport.ClientSize.Width - pictureBox.Width) / 2
                : Math.Max(viewport.ClientSize.Width - pictureBox.Width, Math.Min(0, pictureBox.Left));

            pictureBox.Top = pictureBox.Height <= viewport.ClientSize.Height
                ? (viewport.ClientSize.Height - pictureBox.Height) / 2
                : Math.Max(viewport.ClientSize.Height - pictureBox.Height, Math.Min(0, pictureBox.Top));
        }

        public static void ZoomAt(
            PictureBox pictureBox,
            Control viewport,
            Point mousePosition,
            int delta,
            ref float imageScale,
            float fitScale)
        {
            var imageX = (mousePosition.X - pictureBox.Left) / imageScale;
            var imageY = (mousePosition.Y - pictureBox.Top) / imageScale;
            var zoomFactor = delta > 0 ? 1.15f : 1f / 1.15f;
            var minimumScale = fitScale * 0.25f;
            var maximumScale = fitScale * 20f;
            imageScale = Math.Max(minimumScale, Math.Min(maximumScale, imageScale * zoomFactor));

            var width = Math.Max(1, (int)Math.Round(pictureBox.Image.Width * imageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBox.Image.Height * imageScale));
            pictureBox.Size = new Size(width, height);
            pictureBox.Left = (int)Math.Round(mousePosition.X - imageX * imageScale);
            pictureBox.Top = (int)Math.Round(mousePosition.Y - imageY * imageScale);
            ConstrainPosition(pictureBox, viewport);
        }
    }
}
