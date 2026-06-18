using OpenCvSharp;

namespace AoiMeasureTool
{
    public sealed class RoiInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rect ToRect(Size imageSize)
        {
            var x = Clamp(X, 0, imageSize.Width - 1);
            var y = Clamp(Y, 0, imageSize.Height - 1);
            var maxWidth = imageSize.Width - x;
            var maxHeight = imageSize.Height - y;
            var width = Clamp(Width, 1, maxWidth);
            var height = Clamp(Height, 1, maxHeight);
            return new Rect(x, y, width, height);
        }

        private static int Clamp(int value, int min, int max)
        {
            if (max < min)
            {
                return min;
            }

            if (value < min)
            {
                return min;
            }

            return value > max ? max : value;
        }
    }
}
