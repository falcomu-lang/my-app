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

            if (snapshot.PointMode == ReferenceCornerPointMode.ProtrusionWithoutCompleteShape)
            {
                return FindProtrusionCandidate(binaryMat, roi, snapshot);
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

        private static ReferenceCornerCandidate FindProtrusionCandidate(Mat binaryMat, Rectangle roi, ReferenceCornerSnapshot snapshot)
        {
            if (binaryMat == null || binaryMat.Empty() || roi.Width <= 0 || roi.Height <= 0)
            {
                return null;
            }

            var minWidth = Math.Max(1, snapshot.ProtrusionMinWidth);
            var minHeight = Math.Max(1, snapshot.ProtrusionMinHeight);
            var consecutiveRows = Math.Max(1, snapshot.ProtrusionConsecutiveRows);

            var scanTop = Math.Max(0, roi.Top);
            var scanBottom = Math.Min(binaryMat.Rows, roi.Bottom);
            var scanLeft = Math.Max(0, roi.Left);
            var scanRight = Math.Min(binaryMat.Cols, roi.Right);
            if (scanTop >= scanBottom || scanLeft >= scanRight)
            {
                return null;
            }

            var streak = 0;
            var startRow = -1;
            var endRow = -1;
            var leftEdge = int.MaxValue;
            var rightEdge = int.MinValue;

            for (var y = scanTop; y < scanBottom; y++)
            {
                var rowLeft = int.MaxValue;
                var rowRight = int.MinValue;
                var hasWhite = false;

                for (var x = scanLeft; x < scanRight; x++)
                {
                    var value = binaryMat.At<byte>(y, x);
                    if (value == 0)
                    {
                        continue;
                    }

                    hasWhite = true;
                    if (x < rowLeft)
                    {
                        rowLeft = x;
                    }
                    if (x > rowRight)
                    {
                        rowRight = x;
                    }
                }

                if (!hasWhite)
                {
                    streak = 0;
                    if (startRow >= 0 && endRow < 0)
                    {
                        endRow = y - 1;
                        break;
                    }
                    continue;
                }

                var span = rowRight - rowLeft + 1;
                if (span < minWidth)
                {
                    streak = 0;
                    if (startRow >= 0 && endRow < 0)
                    {
                        endRow = y - 1;
                        break;
                    }
                    continue;
                }

                if (startRow < 0)
                {
                    startRow = y;
                    endRow = -1;
                    streak = 1;
                    leftEdge = rowLeft;
                    rightEdge = rowRight;
                }
                else
                {
                    var widthChange = Math.Abs(span - (rightEdge - leftEdge + 1));
                    if (widthChange > snapshot.ProtrusionWidthIncreaseThreshold)
                    {
                        streak = 1;
                        startRow = y;
                        leftEdge = rowLeft;
                        rightEdge = rowRight;
                    }
                    else
                    {
                        streak++;
                        if (rowLeft < leftEdge)
                        {
                            leftEdge = rowLeft;
                        }
                        if (rowRight > rightEdge)
                        {
                            rightEdge = rowRight;
                        }
                    }

                    if (streak >= consecutiveRows)
                    {
                        endRow = y;
                        break;
                    }
                }
            }

            if (startRow < 0 || endRow < 0 || endRow < startRow)
            {
                if (startRow >= 0 && endRow < 0)
                {
                    endRow = scanBottom - 1;
                }
                else
                {
                    return null;
                }
            }

            if (endRow - startRow + 1 < minHeight)
            {
                return null;
            }

            if (leftEdge == int.MaxValue || rightEdge == int.MinValue)
            {
                return null;
            }

            var topLeft = new System.Drawing.Point(leftEdge, startRow);
            var topRight = new System.Drawing.Point(rightEdge, startRow);
            var boundingRect = Rectangle.FromLTRB(leftEdge, startRow, rightEdge + 1, endRow + 1);
            var centerPoint = new System.Drawing.Point((leftEdge + rightEdge) / 2, (startRow + endRow) / 2);
            var rotatedRect = new RotatedRect(
                new Point2f(centerPoint.X, centerPoint.Y),
                new Size2f(Math.Max(1, boundingRect.Width), Math.Max(1, boundingRect.Height)),
                0);

            return new ReferenceCornerCandidate(rotatedRect, topLeft, topRight, centerPoint, boundingRect);
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
