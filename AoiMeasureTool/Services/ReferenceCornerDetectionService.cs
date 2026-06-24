using System;
using System.Drawing;
using OpenCvSharp;

namespace AoiMeasureTool
{
    internal static class ReferenceCornerDetectionService
    {
        public static ReferenceCornerCandidate FindCandidate(Mat binaryMat, Rectangle roi, System.Drawing.Point roiCenter)
        {
            if (binaryMat == null || binaryMat.Empty())
            {
                return null;
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

                    var candidate = CreateCandidate(labels, i, rect);
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

        private static ReferenceCornerCandidate CreateCandidate(Mat labels, int labelIndex, Rectangle boundingRect)
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
                var vertices = rotatedRect.Points();
                var topPoints = GetTopEdgePoints(vertices);
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

        private static Tuple<System.Drawing.Point, System.Drawing.Point> GetTopEdgePoints(Point2f[] vertices)
        {
            if (vertices == null || vertices.Length == 0)
            {
                return Tuple.Create(System.Drawing.Point.Empty, System.Drawing.Point.Empty);
            }

            var top1 = vertices[0];
            var top2 = vertices.Length > 1 ? vertices[1] : vertices[0];

            for (var i = 1; i < vertices.Length; i++)
            {
                var candidate = vertices[i];
                if (candidate.Y < top1.Y || (Math.Abs(candidate.Y - top1.Y) < 0.5f && candidate.X < top1.X))
                {
                    top2 = top1;
                    top1 = candidate;
                    continue;
                }

                if (candidate.Y < top2.Y || (Math.Abs(candidate.Y - top2.Y) < 0.5f && candidate.X < top2.X))
                {
                    top2 = candidate;
                }
            }

            if (top1.X <= top2.X)
            {
                return Tuple.Create(
                    new System.Drawing.Point((int)Math.Round(top1.X), (int)Math.Round(top1.Y)),
                    new System.Drawing.Point((int)Math.Round(top2.X), (int)Math.Round(top2.Y)));
            }

            return Tuple.Create(
                new System.Drawing.Point((int)Math.Round(top2.X), (int)Math.Round(top2.Y)),
                new System.Drawing.Point((int)Math.Round(top1.X), (int)Math.Round(top1.Y)));
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
