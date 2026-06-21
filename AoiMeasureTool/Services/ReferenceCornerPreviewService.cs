using System;
using System.Drawing;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal static class ReferenceCornerPreviewService
    {
        public static Point ToDisplayPoint(Point imagePoint, float imageScale)
        {
            return imageScale <= 0
                ? imagePoint
                : new Point((int)Math.Round(imagePoint.X * imageScale), (int)Math.Round(imagePoint.Y * imageScale));
        }

        public static Point ToImagePoint(Point displayPoint, float imageScale)
        {
            return imageScale <= 0
                ? displayPoint
                : new Point((int)Math.Round(displayPoint.X / imageScale), (int)Math.Round(displayPoint.Y / imageScale));
        }

        public static Rectangle ToDisplayRectangle(Rectangle imageRect, float imageScale)
        {
            if (imageScale <= 0)
            {
                return imageRect;
            }

            return new Rectangle(
                (int)Math.Round(imageRect.X * imageScale),
                (int)Math.Round(imageRect.Y * imageScale),
                Math.Max(1, (int)Math.Round(imageRect.Width * imageScale)),
                Math.Max(1, (int)Math.Round(imageRect.Height * imageScale)));
        }

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
            pictureBox.Size = new Size(
                Math.Max(1, (int)Math.Round(pictureBox.Image.Width * imageScale)),
                Math.Max(1, (int)Math.Round(pictureBox.Image.Height * imageScale)));
            pictureBox.Left = (viewport.ClientSize.Width - pictureBox.Width) / 2;
            pictureBox.Top = (viewport.ClientSize.Height - pictureBox.Height) / 2;
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
    }
}
