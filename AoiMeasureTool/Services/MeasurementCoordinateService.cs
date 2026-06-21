using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal static class MeasurementCoordinateService
    {
        public static void FitToViewport(PictureBox pictureBox, Control viewport, ref float imageScale, ref float fitScale)
        {
            if (pictureBox.Image == null)
            {
                return;
            }

            var scaleX = viewport.ClientSize.Width / (float)pictureBox.Image.Width;
            var scaleY = viewport.ClientSize.Height / (float)pictureBox.Image.Height;
            fitScale = Math.Min(scaleX, scaleY);
            imageScale = fitScale;
            ApplyTransform(pictureBox, viewport, imageScale, true);
        }

        public static void ApplyTransform(PictureBox pictureBox, Control viewport, float imageScale, bool centerImage)
        {
            if (pictureBox.Image == null)
            {
                return;
            }

            var width = Math.Max(1, (int)Math.Round(pictureBox.Image.Width * imageScale));
            var height = Math.Max(1, (int)Math.Round(pictureBox.Image.Height * imageScale));
            pictureBox.Size = new Size(width, height);

            if (centerImage)
            {
                pictureBox.Left = (viewport.ClientSize.Width - width) / 2;
                pictureBox.Top = (viewport.ClientSize.Height - height) / 2;
            }

            ConstrainPosition(pictureBox, viewport);
        }

        public static void ConstrainPosition(PictureBox pictureBox, Control viewport)
        {
            if (pictureBox.Image == null)
            {
                return;
            }

            pictureBox.Left = pictureBox.Width <= viewport.ClientSize.Width
                ? (viewport.ClientSize.Width - pictureBox.Width) / 2
                : Math.Max(viewport.ClientSize.Width - pictureBox.Width, Math.Min(0, pictureBox.Left));

            pictureBox.Top = pictureBox.Height <= viewport.ClientSize.Height
                ? (viewport.ClientSize.Height - pictureBox.Height) / 2
                : Math.Max(viewport.ClientSize.Height - pictureBox.Height, Math.Min(0, pictureBox.Top));
        }
    }
}
