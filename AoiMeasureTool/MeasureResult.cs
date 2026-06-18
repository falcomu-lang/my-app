using OpenCvSharp;

namespace AoiMeasureTool
{
    public sealed class MeasureResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int DistancePx { get; set; }
        public double ScaleUmPerPixel { get; set; }
        public double DistanceUm { get; set; }
        public double DistanceMm { get; set; }
        public Rect Bounds { get; set; }

        public override string ToString()
        {
            if (!Success)
            {
                return Message ?? "Measurement failed.";
            }

            return string.Format(
                "Distance = {0} px\r\nScale = {1:0.####} um/px\r\nResult = {2:0.###} um = {3:0.###} mm\r\nBounds = X:{4}, Y:{5}, W:{6}, H:{7}",
                DistancePx,
                ScaleUmPerPixel,
                DistanceUm,
                DistanceMm,
                Bounds.X,
                Bounds.Y,
                Bounds.Width,
                Bounds.Height);
        }
    }
}
