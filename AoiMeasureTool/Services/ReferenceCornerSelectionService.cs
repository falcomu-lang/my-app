using System.Drawing;

namespace AoiMeasureTool
{
    internal static class ReferenceCornerSelectionService
    {
        public static ReferenceCornerSnapshot CaptureSnapshot(bool enabled, int sourceIndex, ReferenceCornerPointMode pointMode, Rectangle roi, bool roiSaved, bool cornerFound)
        {
            return new ReferenceCornerSnapshot
            {
                Enabled = enabled,
                SourceIndex = sourceIndex,
                PointMode = pointMode,
                Roi = roi,
                RoiSaved = roiSaved,
                CornerFound = cornerFound
            };
        }

        public static Rectangle NormalizeRectangle(Rectangle rect)
        {
            var x = rect.Width < 0 ? rect.Right : rect.Left;
            var y = rect.Height < 0 ? rect.Bottom : rect.Top;
            return new Rectangle(x, y, System.Math.Abs(rect.Width), System.Math.Abs(rect.Height));
        }
    }
}
