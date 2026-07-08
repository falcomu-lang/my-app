using System;
using System.Drawing;
using OpenCvSharp;

namespace AoiMeasureTool
{
    internal static class ReferenceCornerDetectionService
    {
        public static ReferenceCornerDetectionDebugInfo LastDebugInfo { get; private set; }

        public static ReferenceCornerCandidate FindCandidate(Mat binaryMat, Rectangle roi, System.Drawing.Point roiCenter, ReferenceCornerSnapshot snapshot)
        {
            LastDebugInfo = new ReferenceCornerDetectionDebugInfo();

            if (binaryMat == null || binaryMat.Empty())
            {
                LastDebugInfo.Message = "binary empty";
                return null;
            }

            if (snapshot == null)
            {
                snapshot = ProfileDataCloner.CreateDefaultReferenceCornerSnapshot();
            }

            if (snapshot.PointMode == ReferenceCornerPointMode.ScanSearch)
            {
                return FindScanSearchCandidate(binaryMat, roi, snapshot.ScanLineThreshold);
            }

            using (var labels = new Mat())
            using (var stats = new Mat())
            using (var centroids = new Mat())
            {
                var componentCount = Cv2.ConnectedComponentsWithStats(binaryMat, labels, stats, centroids);
                ReferenceCornerCandidate bestCandidate = null;
                var bestArea = -1;

                for (var i = 1; i < componentCount; i++)
                {
                    var left = stats.At<int>(i, 0);
                    var top = stats.At<int>(i, 1);
                    var width = stats.At<int>(i, 2);
                    var height = stats.At<int>(i, 3);
                    var area = stats.At<int>(i, 4);
                    var rect = new Rectangle(left, top, width, height);

                    if (!IsTargetFullyInsideRoi(rect, roi))
                    {
                        continue;
                    }

                    if (area <= bestArea)
                    {
                        continue;
                    }

                    var candidate = CreateCandidate(labels, i, rect, snapshot);
                    if (candidate == null)
                    {
                        continue;
                    }

                    bestCandidate = candidate;
                    bestArea = area;
                }

                return bestCandidate;
            }
        }

        private static ReferenceCornerCandidate CreateCandidate(Mat labels, int labelIndex, Rectangle boundingRect, ReferenceCornerSnapshot snapshot)
        {
            if (labels == null || labels.Empty() || boundingRect.Width <= 0 || boundingRect.Height <= 0)
            {
                return null;
            }

            using (var labelMask = new Mat())
            {
                Cv2.InRange(labels, labelIndex, labelIndex, labelMask);

                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(labelMask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                if (contours == null || contours.Length == 0)
                {
                    return null;
                }

                var bestContour = contours[0];
                var bestArea = Math.Abs(Cv2.ContourArea(bestContour));
                for (var i = 1; i < contours.Length; i++)
                {
                    var area = Math.Abs(Cv2.ContourArea(contours[i]));
                    if (area > bestArea)
                    {
                        bestArea = area;
                        bestContour = contours[i];
                    }
                }

                var rotatedRect = Cv2.MinAreaRect(bestContour);
                var topPoints = GetTopPoints(bestContour, boundingRect, snapshot);
                var topLeft = topPoints.Item1;
                var topRight = topPoints.Item2;
                return new ReferenceCornerCandidate(
                    rotatedRect,
                    topLeft,
                    topRight,
                    new System.Drawing.Point((int)Math.Round(rotatedRect.Center.X), (int)Math.Round(rotatedRect.Center.Y)),
                    boundingRect);
            }
        }

        private static ReferenceCornerCandidate FindScanSearchCandidate(Mat binaryMat, Rectangle roi, int scanLineThreshold)
        {
            var left = Math.Max(0, roi.Left);
            var top = Math.Max(0, roi.Top);
            var right = Math.Min(binaryMat.Width, roi.Right);
            var bottom = Math.Min(binaryMat.Height, roi.Bottom);

            if (right <= left || bottom <= top)
            {
                return null;
            }

            var threshold = Math.Max(1, scanLineThreshold);
            for (var y = top; y < bottom; y++)
            {
                var x = left;
                while (x < right)
                {
                    while (x < right && binaryMat.At<byte>(y, x) == 0)
                    {
                        x++;
                    }

                    if (x >= right)
                    {
                        break;
                    }

                    var segmentStart = x;
                    while (x < right && binaryMat.At<byte>(y, x) != 0)
                    {
                        x++;
                    }

                    var segmentEnd = x - 1;
                    var segmentLength = segmentEnd - segmentStart + 1;
                    if (segmentLength < threshold)
                    {
                        continue;
                    }

                    var boundingRect = new Rectangle(segmentStart, y, segmentLength, 1);
                    var topLeft = new System.Drawing.Point(segmentStart, y);
                    var topRight = new System.Drawing.Point(segmentEnd, y);
                    var centerX = segmentStart + segmentLength / 2;
                    return new ReferenceCornerCandidate(
                        new RotatedRect(
                            new Point2f(centerX, y),
                            new Size2f(segmentLength, 1),
                            0),
                        topLeft,
                        topRight,
                        new System.Drawing.Point(centerX, y),
                        boundingRect);
                }
            }

            return null;
        }

        private static Tuple<System.Drawing.Point, System.Drawing.Point> GetTopPoints(
            OpenCvSharp.Point[] contour,
            Rectangle boundingRect,
            ReferenceCornerSnapshot snapshot)
        {
            if (snapshot.PointMode == ReferenceCornerPointMode.RoiTopEdge)
            {
                return Tuple.Create(
                    new System.Drawing.Point(boundingRect.Left, boundingRect.Top),
                    new System.Drawing.Point(boundingRect.Right, boundingRect.Top));
            }

            var rectTopLeft = new System.Drawing.Point(boundingRect.Left, boundingRect.Top);
            var rectTopRight = new System.Drawing.Point(boundingRect.Right, boundingRect.Top);
            return GetNearestContourTopPoints(contour, rectTopLeft, rectTopRight);
        }

        private static Tuple<System.Drawing.Point, System.Drawing.Point> GetNearestContourTopPoints(
            OpenCvSharp.Point[] contour,
            System.Drawing.Point idealTopLeft,
            System.Drawing.Point idealTopRight)
        {
            if (contour == null || contour.Length == 0)
            {
                return Tuple.Create(System.Drawing.Point.Empty, System.Drawing.Point.Empty);
            }

            var topLeft = FindNearestContourPoint(contour, idealTopLeft, -1);
            var topRight = FindNearestContourPoint(contour, idealTopRight, topLeft.Item2);

            return Tuple.Create(
                new System.Drawing.Point(topLeft.Item1.X, topLeft.Item1.Y),
                new System.Drawing.Point(topRight.Item1.X, topRight.Item1.Y));
        }

        private static Tuple<OpenCvSharp.Point, int> FindNearestContourPoint(
            OpenCvSharp.Point[] contour,
            System.Drawing.Point target,
            int excludedIndex)
        {
            var bestIndex = -1;
            var bestDistance = double.MaxValue;
            var bestPoint = contour[0];

            for (var i = 0; i < contour.Length; i++)
            {
                if (i == excludedIndex)
                {
                    continue;
                }

                var candidate = contour[i];
                var dx = candidate.X - target.X;
                var dy = candidate.Y - target.Y;
                var distance = dx * dx + dy * dy;

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestIndex = i;
                    bestPoint = candidate;
                    continue;
                }

                if (Math.Abs(distance - bestDistance) < 0.0001)
                {
                    if (candidate.Y < bestPoint.Y || (candidate.Y == bestPoint.Y && candidate.X < bestPoint.X))
                    {
                        bestIndex = i;
                        bestPoint = candidate;
                    }
                }
            }

            if (bestIndex < 0)
            {
                bestIndex = 0;
                bestPoint = contour[0];
            }

            return Tuple.Create(bestPoint, bestIndex);
        }

        private static bool IsTargetFullyInsideRoi(Rectangle rect, Rectangle roi)
        {
            return rect.Left >= roi.Left
                && rect.Top >= roi.Top
                && rect.Right <= roi.Right
                && rect.Bottom <= roi.Bottom;
        }
    }
}
