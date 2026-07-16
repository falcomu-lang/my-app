using System.Collections.Generic;
using System.Drawing;

namespace AoiMeasureTool
{
    internal sealed class ReferenceBasis
    {
        public Point Anchor { get; set; }
        public PointF UnitX { get; set; }
        public PointF UnitY { get; set; }
        public double Length { get; set; }
    }

    internal sealed class MeasureRecord
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Point CenterPoint { get; set; }
        public Point ReferenceTopLeft { get; set; }
        public Point ReferenceTopRight { get; set; }
        public double ReferenceLength { get; set; }
        public PointF LocalStartPoint { get; set; }
        public PointF LocalEndPoint { get; set; }
        public double Distance { get; set; }
        public string SourceName { get; set; }
        public string DirectionName { get; set; }
        public string StatusText { get; set; }
    }

    internal sealed class JudgementCriterionRule
    {
        public string Name { get; set; }
        public string CalculationExpression { get; set; }
        public string SpecExpression { get; set; }
        public string CalculationExpressionB { get; set; }
        public string SpecExpressionB { get; set; }
    }

    internal enum MeasureDirectionMode
    {
        None = 0,
        Parallel = 1,
        Perpendicular = 2
    }

    internal sealed class PreprocessSnapshot
    {
        public bool Enabled { get; set; }
        public int Threshold { get; set; }
        public int UpperThreshold { get; set; }
        public bool UseDualThreshold { get; set; }
        public int ErodeIterations { get; set; }
        public int DilateIterations { get; set; }
        public int OpenIterations { get; set; }
        public int CloseIterations { get; set; }
    }

    internal sealed class ReferenceCornerSnapshot
    {
        public bool Enabled { get; set; }
        public int SourceIndex { get; set; }
        public ReferenceCornerPointMode PointMode { get; set; }
        public int ScanLineThreshold { get; set; }
        public Rectangle Roi { get; set; }
        public bool RoiSaved { get; set; }
        public bool CornerFound { get; set; }
    }

    internal enum ReferenceCornerPointMode
    {
        ContourNearest = 0,
        RoiTopEdge = 1,
        ScanSearch = 2
    }

    internal sealed class ReferenceCornerCandidate
    {
        public ReferenceCornerCandidate(OpenCvSharp.RotatedRect rotatedRect, Point topLeft, Point topRight, Point centerPoint, Rectangle boundingRect)
        {
            RotatedRect = rotatedRect;
            TopLeft = topLeft;
            TopRight = topRight;
            CenterPoint = centerPoint;
            BoundingRect = boundingRect;
        }

        public OpenCvSharp.RotatedRect RotatedRect { get; }
        public Point TopLeft { get; }
        public Point TopRight { get; }
        public Point CenterPoint { get; }
        public Rectangle BoundingRect { get; }
    }

    internal sealed class ReferenceCornerDetectionDebugInfo
    {
        public bool UsedProtrusionMode { get; set; }
        public double BaselineAverage { get; set; }
        public int TriggerRow { get; set; } = -1;
        public int RunStartRow { get; set; } = -1;
        public int RunEndRow { get; set; } = -1;
        public int PeakLeftDelta { get; set; }
        public int PeakWhiteCount { get; set; }
        public string Message { get; set; }
    }

    internal sealed class AppSettingsData
    {
        public AppSettingsData()
        {
            PreprocessProfiles = new Dictionary<string, PreprocessSnapshot[]>(System.StringComparer.OrdinalIgnoreCase);
            ReferenceCornerProfiles = new Dictionary<string, ReferenceCornerSnapshot>(System.StringComparer.OrdinalIgnoreCase);
            MeasureProfiles = new Dictionary<string, List<MeasureRecord>>(System.StringComparer.OrdinalIgnoreCase);
            JudgementCriteriaProfiles = new Dictionary<string, List<JudgementCriterionRule>>(System.StringComparer.OrdinalIgnoreCase);
            DualThresholdProfiles = new Dictionary<string, DualThresholdSnapshot>(System.StringComparer.OrdinalIgnoreCase);
            ListSortItems = new List<string>();
            DualThresholdSettings = new DualThresholdSnapshot();
        }

        public string LastImagePath { get; set; }
        public string ActiveProductKey { get; set; }
        public string ContinuousInspectionMainParameter { get; set; }
        public string UserRole { get; set; }
        public Dictionary<string, PreprocessSnapshot[]> PreprocessProfiles { get; }
        public Dictionary<string, ReferenceCornerSnapshot> ReferenceCornerProfiles { get; }
        public Dictionary<string, List<MeasureRecord>> MeasureProfiles { get; }
        public Dictionary<string, List<JudgementCriterionRule>> JudgementCriteriaProfiles { get; }
        public Dictionary<string, DualThresholdSnapshot> DualThresholdProfiles { get; }
        public List<string> ListSortItems { get; }
        public DualThresholdSnapshot DualThresholdSettings { get; set; }
    }

    internal sealed class DualThresholdSnapshot
    {
        public bool Enabled { get; set; }
        public int LowerThreshold { get; set; }
        public int UpperThreshold { get; set; }
        public int ErodeIterations { get; set; }
        public int DilateIterations { get; set; }
        public int OpenIterations { get; set; }
        public int CloseIterations { get; set; }
    }

    internal sealed class DetectionParameterReference
    {
        public string MainParameterName { get; set; }
        public string SubParameter1 { get; set; }
        public string SubParameter2 { get; set; }
        public string SubParameter3 { get; set; }
        public int InnerSettingsProfileIndex { get; set; }
    }

    internal sealed class SubParameterInnerSettingsBinding
    {
        public string SubParameterName { get; set; }
        public int InnerSettingsProfileIndex { get; set; }
    }

    internal sealed class InnerSettingsData
    {
        public InnerSettingsData()
        {
            CameraProfiles = new List<InnerSettingsCameraProfile>();
        }

        public List<InnerSettingsCameraProfile> CameraProfiles { get; }
        public double CcdXPrecision { get; set; }
        public double CcdYPrecision { get; set; }
        public double MeasurementScaleFactor { get; set; } = 1.0;
    }

    internal sealed class InnerSettingsCameraProfile
    {
        public string CameraName { get; set; }
        public string UsageName { get; set; }
        public double CcdXPrecision { get; set; }
        public double CcdYPrecision { get; set; }
        public double MeasurementScaleFactor { get; set; } = 1.0;
    }

    public sealed class ContinuousInspectionResult
    {
        public ContinuousInspectionResult()
        {
            Rules = new List<ContinuousInspectionRuleResult>();
        }

        public int SlotIndex { get; set; }
        public string Summary { get; set; }
        public string SubParameterName { get; set; }
        public List<ContinuousInspectionRuleResult> Rules { get; }
    }

    public sealed class ContinuousInspectionRuleResult
    {
        public string RuleName { get; set; }
        public string CalculationValue { get; set; }
        public string Judgement { get; set; }
    }
}

