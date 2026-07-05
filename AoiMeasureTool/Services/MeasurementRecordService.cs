using System;
using System.Collections.Generic;
using System.Drawing;

namespace AoiMeasureTool
{
    internal static class MeasurementRecordService
    {
        public static MeasureRecord CreateRecord(Point startPoint, Point endPoint, ReferenceCornerCandidate candidate, string sourceName, string directionName)
        {
            var centerX = (startPoint.X + endPoint.X) / 2.0;
            var centerY = (startPoint.Y + endPoint.Y) / 2.0;
            var distance = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            var basis = CreateReferenceBasis(candidate);
            var referenceTopLeft = candidate != null ? candidate.TopLeft : Point.Empty;
            var referenceTopRight = candidate != null ? candidate.TopRight : Point.Empty;

            return new MeasureRecord
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                CenterPoint = new Point((int)Math.Round(centerX), (int)Math.Round(centerY)),
                Distance = distance,
                SourceName = sourceName,
                DirectionName = directionName,
                ReferenceTopLeft = referenceTopLeft,
                ReferenceTopRight = referenceTopRight,
                ReferenceLength = basis.Length,
                LocalStartPoint = ToLocalReferencePoint(startPoint, basis),
                LocalEndPoint = ToLocalReferencePoint(endPoint, basis)
            };
        }

        public static MeasureRecord ReprojectForCurrentReference(MeasureRecord record, ReferenceCornerCandidate candidate)
        {
            var cloned = CloneRecord(record);
            if (candidate == null)
            {
                return cloned;
            }

            var hasRelativeData =
                !(cloned.ReferenceTopLeft == Point.Empty &&
                  cloned.ReferenceTopRight == Point.Empty &&
                  cloned.LocalStartPoint == PointF.Empty &&
                  cloned.LocalEndPoint == PointF.Empty);

            if (!hasRelativeData)
            {
                return cloned;
            }

            var basis = CreateReferenceBasis(candidate);
            cloned.StartPoint = FromNormalizedReferencePoint(cloned.LocalStartPoint, cloned.ReferenceLength, basis);
            cloned.EndPoint = FromNormalizedReferencePoint(cloned.LocalEndPoint, cloned.ReferenceLength, basis);
            cloned.CenterPoint = new Point(
                (int)Math.Round((cloned.StartPoint.X + cloned.EndPoint.X) / 2.0),
                (int)Math.Round((cloned.StartPoint.Y + cloned.EndPoint.Y) / 2.0));
            cloned.Distance = Math.Sqrt(
                Math.Pow(cloned.EndPoint.X - cloned.StartPoint.X, 2) +
                Math.Pow(cloned.EndPoint.Y - cloned.StartPoint.Y, 2));
            return cloned;
        }

        public static double GetDistance(IReadOnlyList<Point> measurePoints)
        {
            if (measurePoints == null || measurePoints.Count < 2)
            {
                return 0d;
            }

            var p1 = measurePoints[0];
            var p2 = measurePoints[1];
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private static MeasureRecord CloneRecord(MeasureRecord record)
        {
            return new MeasureRecord
            {
                StartPoint = record.StartPoint,
                EndPoint = record.EndPoint,
                CenterPoint = record.CenterPoint,
                Distance = record.Distance,
                SourceName = record.SourceName,
                DirectionName = record.DirectionName,
                ReferenceTopLeft = record.ReferenceTopLeft,
                ReferenceTopRight = record.ReferenceTopRight,
                ReferenceLength = record.ReferenceLength,
                LocalStartPoint = record.LocalStartPoint,
                LocalEndPoint = record.LocalEndPoint,
                StatusText = record.StatusText
            };
        }

        private static ReferenceBasis CreateReferenceBasis(ReferenceCornerCandidate candidate)
        {
            if (candidate == null)
            {
                return new ReferenceBasis
                {
                    Anchor = Point.Empty,
                    UnitX = new PointF(1f, 0f),
                    UnitY = new PointF(0f, 1f),
                    Length = 1d
                };
            }

            return CreateReferenceBasis(candidate.TopLeft, candidate.TopRight);
        }

        private static ReferenceBasis CreateReferenceBasis(Point topLeft, Point topRight)
        {
            var dx = topRight.X - topLeft.X;
            var dy = topRight.Y - topLeft.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            if (length < 0.0001)
            {
                return new ReferenceBasis
                {
                    Anchor = topLeft,
                    UnitX = new PointF(1f, 0f),
                    UnitY = new PointF(0f, 1f),
                    Length = 1d
                };
            }

            var ux = (float)(dx / length);
            var uy = (float)(dy / length);
            return new ReferenceBasis
            {
                Anchor = topLeft,
                UnitX = new PointF(ux, uy),
                UnitY = new PointF(-uy, ux),
                Length = length
            };
        }

        private static PointF ToLocalReferencePoint(Point point, ReferenceBasis basis)
        {
            if (basis.Anchor == Point.Empty)
            {
                return new PointF(point.X, point.Y);
            }

            var vx = point.X - basis.Anchor.X;
            var vy = point.Y - basis.Anchor.Y;
            return new PointF(
                (float)((vx * basis.UnitX.X + vy * basis.UnitX.Y) / basis.Length),
                (float)((vx * basis.UnitY.X + vy * basis.UnitY.Y) / basis.Length));
        }

        private static Point FromNormalizedReferencePoint(PointF normalizedPoint, double sourceReferenceLength, ReferenceBasis basis)
        {
            if (basis.Anchor == Point.Empty)
            {
                return new Point((int)Math.Round(normalizedPoint.X), (int)Math.Round(normalizedPoint.Y));
            }

            var sourceLength = sourceReferenceLength > 0 ? sourceReferenceLength : basis.Length;
            var scaledX = normalizedPoint.X * sourceLength;
            var scaledY = normalizedPoint.Y * sourceLength;
            return new Point(
                (int)Math.Round(basis.Anchor.X + scaledX * basis.UnitX.X + scaledY * basis.UnitY.X),
                (int)Math.Round(basis.Anchor.Y + scaledX * basis.UnitX.Y + scaledY * basis.UnitY.Y));
        }
    }
}
