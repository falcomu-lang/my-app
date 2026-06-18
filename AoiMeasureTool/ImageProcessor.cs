using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace AoiMeasureTool
{
    public sealed class EdgePointResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Point LeftUpperEdgePoint { get; set; }
        public Point RightUpperEdgePoint { get; set; }
        public Mat BinaryInRoi { get; set; }
    }

    public static class ImageProcessor
    {
        public static Mat LoadColorImage(string path)
        {
            var image = Cv2.ImRead(path, ImreadModes.Color);
            if (image.Empty())
            {
                throw new InvalidOperationException("Image could not be loaded.");
            }

            return image;
        }

        public static Mat ToGray(Mat source)
        {
            if (source == null || source.Empty())
            {
                throw new ArgumentException("Source image is empty.");
            }

            if (source.Channels() == 1)
            {
                return source.Clone();
            }

            var gray = new Mat();
            Cv2.CvtColor(source, gray, ColorConversionCodes.BGR2GRAY);
            return gray;
        }

        public static Mat Preprocess(Mat gray, PreprocessParam param)
        {
            if (gray == null || gray.Empty())
            {
                throw new ArgumentException("Gray image is empty.");
            }

            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            var binary = new Mat();
            var thresholdType = param.WhiteWhenGreaterThanThreshold
                ? ThresholdTypes.Binary
                : ThresholdTypes.BinaryInv;
            Cv2.Threshold(gray, binary, param.Threshold, 255, thresholdType);

            var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));

            if (param.ErodeIterations > 0)
            {
                Cv2.Erode(binary, binary, kernel, iterations: param.ErodeIterations);
            }

            if (param.DilateIterations > 0)
            {
                Cv2.Dilate(binary, binary, kernel, iterations: param.DilateIterations);
            }

            if (param.OpenIterations > 0)
            {
                Cv2.MorphologyEx(binary, binary, MorphTypes.Open, kernel, iterations: param.OpenIterations);
            }

            if (param.CloseIterations > 0)
            {
                Cv2.MorphologyEx(binary, binary, MorphTypes.Close, kernel, iterations: param.CloseIterations);
            }

            return binary;
        }

        public static MeasureResult MeasureDistance(Mat binary, RoiInfo roi, bool verticalDistance, double scaleUmPerPixel)
        {
            if (binary == null || binary.Empty())
            {
                return new MeasureResult { Success = false, Message = "No preprocess image selected." };
            }

            var rect = roi.ToRect(binary.Size());
            using (var roiMat = new Mat(binary, rect))
            {
                using (var points = new Mat())
                {
                    Cv2.FindNonZero(roiMat, points);
                    if (points.Empty())
                    {
                        return new MeasureResult { Success = false, Message = "No valid object pixels found in ROI." };
                    }

                    var bounds = Cv2.BoundingRect(points);
                    var distance = verticalDistance ? bounds.Height : bounds.Width;
                    var globalBounds = new Rect(rect.X + bounds.X, rect.Y + bounds.Y, bounds.Width, bounds.Height);
                    var distanceUm = distance * scaleUmPerPixel;

                    return new MeasureResult
                    {
                        Success = true,
                        DistancePx = distance,
                        ScaleUmPerPixel = scaleUmPerPixel,
                        DistanceUm = distanceUm,
                        DistanceMm = distanceUm / 1000.0,
                        Bounds = globalBounds
                    };
                }
            }
        }

        public static EdgePointResult FindUpperEdgePoints(
            Mat gray,
            RoiInfo roi,
            int threshold,
            bool whiteObject,
            bool invertBinary)
        {
            if (gray == null || gray.Empty())
            {
                return new EdgePointResult { Success = false, Message = "No gray image loaded." };
            }

            var thresholdType = whiteObject ? ThresholdTypes.Binary : ThresholdTypes.BinaryInv;
            using (var binary = new Mat())
            {
                Cv2.Threshold(gray, binary, threshold, 255, thresholdType);
                if (invertBinary)
                {
                    Cv2.BitwiseNot(binary, binary);
                }

                using (var largest = KeepLargestObject(binary))
                {
                    var rect = roi.ToRect(gray.Size());
                    using (var roiMat = new Mat(largest, rect))
                    {
                        var localPoints = FindUpperEdgePointsInBinaryRoi(roiMat);
                        if (localPoints.Count == 0)
                        {
                            return new EdgePointResult { Success = false, Message = "No upper edge pixels found in ROI." };
                        }

                        var left = localPoints.OrderBy(p => p.X).ThenBy(p => p.Y).First();
                        var right = localPoints.OrderByDescending(p => p.X).ThenBy(p => p.Y).First();

                        return new EdgePointResult
                        {
                            Success = true,
                            LeftUpperEdgePoint = new Point(rect.X + left.X, rect.Y + left.Y),
                            RightUpperEdgePoint = new Point(rect.X + right.X, rect.Y + right.Y),
                            BinaryInRoi = roiMat.Clone()
                        };
                    }
                }
            }
        }

        public static Mat DrawOverlay(
            Mat colorSource,
            RoiInfo roi,
            MeasureResult measureResult,
            EdgePointResult edgePointResult)
        {
            var display = colorSource.Clone();
            var roiRect = roi.ToRect(display.Size());
            Cv2.Rectangle(display, roiRect, new Scalar(0, 255, 0), 2);

            if (measureResult != null && measureResult.Success)
            {
                Cv2.Rectangle(display, measureResult.Bounds, new Scalar(235, 206, 135), 2);
            }

            if (edgePointResult != null && edgePointResult.Success)
            {
                DrawEdgePoint(display, edgePointResult.LeftUpperEdgePoint, new Scalar(0, 0, 255));
                DrawEdgePoint(display, edgePointResult.RightUpperEdgePoint, new Scalar(0, 255, 255));
                Cv2.Line(display, edgePointResult.LeftUpperEdgePoint, edgePointResult.RightUpperEdgePoint, new Scalar(0, 165, 255), 1);
            }

            return display;
        }

        private static void DrawEdgePoint(Mat image, Point point, Scalar color)
        {
            Cv2.Circle(image, point, 6, color, 2);
            Cv2.Line(image, new Point(point.X - 12, point.Y), new Point(point.X + 12, point.Y), color, 1);
            Cv2.Line(image, new Point(point.X, point.Y - 12), new Point(point.X, point.Y + 12), color, 1);
        }

        private static Mat KeepLargestObject(Mat binary)
        {
            var labels = new Mat();
            var stats = new Mat();
            var centroids = new Mat();
            var count = Cv2.ConnectedComponentsWithStats(binary, labels, stats, centroids);
            var result = Mat.Zeros(binary.Size(), MatType.CV_8UC1).ToMat();

            if (count <= 1)
            {
                return result;
            }

            var largestLabel = 1;
            var largestArea = 0;
            for (var label = 1; label < count; label++)
            {
                var area = stats.At<int>(label, (int)ConnectedComponentsTypes.Area);
                if (area > largestArea)
                {
                    largestArea = area;
                    largestLabel = label;
                }
            }

            Cv2.Compare(labels, largestLabel, result, CmpTypes.EQ);
            labels.Dispose();
            stats.Dispose();
            centroids.Dispose();
            return result;
        }

        private static List<Point> FindUpperEdgePointsInBinaryRoi(Mat roiBinary)
        {
            var points = new List<Point>();
            for (var x = 0; x < roiBinary.Width; x++)
            {
                for (var y = 0; y < roiBinary.Height; y++)
                {
                    if (roiBinary.At<byte>(y, x) > 0)
                    {
                        points.Add(new Point(x, y));
                        break;
                    }
                }
            }

            return points;
        }
    }
}
