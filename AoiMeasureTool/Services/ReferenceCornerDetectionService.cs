using System;
using System.Drawing;
using OpenCvSharp;

namespace AoiMeasureTool
{
    internal static class ReferenceCornerDetectionService
    {
        public static ReferenceCornerCandidate FindCandidate(Mat binaryMat, Rectangle roi, System.Drawing.Point roiCenter, ReferenceCornerSnapshot snapshot)
        {
            if (binaryMat == null || binaryMat.Empty())
            {
                return null;
            }

            if (snapshot == null)
            {
                snapshot = ProfileDataCloner.CreateDefaultReferenceCornerSnapshot();
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
                var topPoints = GetTopPoints(labels, labelIndex, bestContour, boundingRect, snapshot);
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

        private static Tuple<System.Drawing.Point, System.Drawing.Point> GetTopPoints(
            Mat labels,
            int labelIndex,
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

            if (snapshot.PointMode == ReferenceCornerPointMode.ProtrusionWithoutCompleteShape)
            {
                return GetProtrusionTopPoints(labels, labelIndex, boundingRect, snapshot);
            }

            var rectTopLeft = new System.Drawing.Point(boundingRect.Left, boundingRect.Top);
            var rectTopRight = new System.Drawing.Point(boundingRect.Right, boundingRect.Top);
            return GetNearestContourTopPoints(contour, rectTopLeft, rectTopRight);
        }

        private static Tuple<System.Drawing.Point, System.Drawing.Point> GetProtrusionTopPoints(Mat labels, int labelIndex, Rectangle boundingRect, ReferenceCornerSnapshot snapshot)
        {
            if (labels == null || labels.Empty() || boundingRect.Width <= 0 || boundingRect.Height <= 0)
            {
                return Tuple.Create(System.Drawing.Point.Empty, System.Drawing.Point.Empty);
            }

            var minWidth = Math.Max(1, snapshot.ProtrusionMinWidth);
            var minHeight = Math.Max(1, snapshot.ProtrusionMinHeight);
            var widthIncreaseThreshold = Math.Max(0, snapshot.ProtrusionWidthIncreaseThreshold);
            var consecutiveRows = Math.Max(1, snapshot.ProtrusionConsecutiveRows);
            var topRow = -1;
            var bestSpan = -1;
            var streak = 0;

            for (var y = boundingRect.Top; y < boundingRect.Bottom; y++)
            {
                var rowLeft = int.MaxValue;
                var rowRight = int.MinValue;
                var hasPixel = false;

                for (var x = boundingRect.Left; x < boundingRect.Right; x++)
                {
                    if (labels.At<int>(y, x) != labelIndex)
                    {
                        continue;
                    }

                    hasPixel = true;
                    if (x < rowLeft)
                    {
                        rowLeft = x;
                    }
                    if (x > rowRight)
                    {
                        rowRight = x;
                    }
                }

                if (!hasPixel)
                {
                    streak = 0;
                    continue;
                }

                var span = rowRight - rowLeft + 1;
                if (span >= minWidth && span > bestSpan + widthIncreaseThreshold)
                {
                    streak++;
                    if (streak >= consecutiveRows)
                    {
                        topRow = y - consecutiveRows + 1;
                        bestSpan = span;
                        break;
                    }
                }
                else
                {
                    streak = 0;
                }
            }

            if (topRow < 0)
            {
                var fallbackRow = -1;
                for (var y = boundingRect.Top; y < boundingRect.Bottom; y++)
                {
                    var rowLeft = int.MaxValue;
                    var rowRight = int.MinValue;
                    var hasPixel = false;
                    for (var x = boundingRect.Left; x < boundingRect.Right; x++)
                    {
                        if (labels.At<int>(y, x) != labelIndex)
                        {
                            continue;
                        }
                        hasPixel = true;
                        if (x < rowLeft) rowLeft = x;
                        if (x > rowRight) rowRight = x;
                    }
                    if (!hasPixel)
                    {
                        continue;
                    }
                    var span = rowRight - rowLeft + 1;
                    if (span >= minWidth && (fallbackRow < 0 || span > bestSpan))
                    {
                        fallbackRow = y;
                        bestSpan = span;
                    }
                }

                topRow = fallbackRow;
            }

            if (topRow - boundingRect.Top + 1 < minHeight)
            {
                return Tuple.Create(System.Drawing.Point.Empty, System.Drawing.Point.Empty);
            }

            if (topRow < 0)
            {
                return Tuple.Create(System.Drawing.Point.Empty, System.Drawing.Point.Empty);
            }

            var topLeft = new System.Drawing.Point(int.MaxValue, topRow);
            var topRight = new System.Drawing.Point(int.MinValue, topRow);
            for (var x = boundingRect.Left; x < boundingRect.Right; x++)
            {
                if (labels.At<int>(topRow, x) != labelIndex)
                {
                    continue;
                }
                if (x < topLeft.X)
                {
                    topLeft = new System.Drawing.Point(x, topRow);
                }
                if (x > topRight.X)
                {
                    topRight = new System.Drawing.Point(x, topRow);
                }
            }

            if (topLeft.X == int.MaxValue || topRight.X == int.MinValue)
            {
                return Tuple.Create(System.Drawing.Point.Empty, System.Drawing.Point.Empty);
            }

            return Tuple.Create(topLeft, topRight);
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
