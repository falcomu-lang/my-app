using System;
using System.Drawing;

namespace AoiMeasureTool
{
    internal static class MeasurementOverlayService
    {
        public static Color GetSourceColor(string sourceName)
        {
            if (string.IsNullOrWhiteSpace(sourceName))
            {
                return Color.Red;
            }

            if (sourceName.IndexOf("1", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Color.Red;
            }

            if (sourceName.IndexOf("2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Color.FromArgb(255, 105, 180);
            }

            if (sourceName.IndexOf("3", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Color.Blue;
            }

            if (sourceName.IndexOf("4", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Color.LightSkyBlue;
            }

            return Color.Red;
        }

        public static Point ConstrainPoint(
            Point startPoint,
            Point rawPoint,
            ReferenceCornerCandidate referenceCornerCandidate,
            MeasureDirectionMode directionMode)
        {
            if (referenceCornerCandidate == null || directionMode == MeasureDirectionMode.None)
            {
                return rawPoint;
            }

            var axis = new Point(
                referenceCornerCandidate.TopRight.X - referenceCornerCandidate.TopLeft.X,
                referenceCornerCandidate.TopRight.Y - referenceCornerCandidate.TopLeft.Y);

            var length = Math.Sqrt(axis.X * axis.X + axis.Y * axis.Y);
            if (length <= 0.001)
            {
                return rawPoint;
            }

            var unitX = axis.X / length;
            var unitY = axis.Y / length;
            if (directionMode == MeasureDirectionMode.Perpendicular)
            {
                var temp = unitX;
                unitX = -unitY;
                unitY = temp;
            }

            var deltaX = rawPoint.X - startPoint.X;
            var deltaY = rawPoint.Y - startPoint.Y;
            var projected = deltaX * unitX + deltaY * unitY;
            return new Point(
                (int)Math.Round(startPoint.X + projected * unitX),
                (int)Math.Round(startPoint.Y + projected * unitY));
        }

        public static Rectangle GetImageDisplayRect(Size clientSize, Size imageSize)
        {
            if (clientSize.Width <= 0 || clientSize.Height <= 0 || imageSize.Width <= 0 || imageSize.Height <= 0)
            {
                return Rectangle.Empty;
            }

            var scale = Math.Min(clientSize.Width / (float)imageSize.Width, clientSize.Height / (float)imageSize.Height);
            var drawWidth = Math.Max(1, (int)Math.Round(imageSize.Width * scale));
            var drawHeight = Math.Max(1, (int)Math.Round(imageSize.Height * scale));
            var offsetX = (clientSize.Width - drawWidth) / 2;
            var offsetY = (clientSize.Height - drawHeight) / 2;
            return new Rectangle(offsetX, offsetY, drawWidth, drawHeight);
        }

        public static Point ToImagePoint(Point displayPoint, Rectangle imageRect, Size imageSize)
        {
            if (imageRect.Width <= 0 || imageRect.Height <= 0 || imageSize.Width <= 0 || imageSize.Height <= 0)
            {
                return Point.Empty;
            }

            var x = (displayPoint.X - imageRect.Left) * imageSize.Width / (float)imageRect.Width;
            var y = (displayPoint.Y - imageRect.Top) * imageSize.Height / (float)imageRect.Height;
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public static Point ToDisplayPoint(Point imagePoint, Rectangle imageRect, Size imageSize)
        {
            if (imageRect.Width <= 0 || imageRect.Height <= 0 || imageSize.Width <= 0 || imageSize.Height <= 0)
            {
                return imagePoint;
            }

            return new Point(
                (int)Math.Round(imageRect.Left + imagePoint.X * imageRect.Width / (float)imageSize.Width),
                (int)Math.Round(imageRect.Top + imagePoint.Y * imageRect.Height / (float)imageSize.Height));
        }

        public static void DrawMeasureRecord(Graphics graphics, Pen pen, Brush brush, Point startPoint, Point endPoint)
        {
            graphics.DrawLine(pen, startPoint, endPoint);
            graphics.FillEllipse(brush, startPoint.X - 4, startPoint.Y - 4, 8, 8);
            graphics.FillEllipse(brush, endPoint.X - 4, endPoint.Y - 4, 8, 8);
        }
    }
}
