using System.Collections.Generic;

namespace AoiMeasureTool
{
    internal static class ProfileDataCloner
    {
        public static PreprocessSnapshot[] CreateDefaultPreprocessSnapshots()
        {
            var snapshots = new PreprocessSnapshot[4];
            for (var i = 0; i < 4; i++)
            {
                snapshots[i] = new PreprocessSnapshot
                {
                    Enabled = false,
                    Threshold = 128,
                    ErodeIterations = 0,
                    DilateIterations = 0,
                    OpenIterations = 0,
                    CloseIterations = 0
                };
            }

            return snapshots;
        }

        public static ReferenceCornerSnapshot CreateDefaultReferenceCornerSnapshot()
        {
            return new ReferenceCornerSnapshot
            {
                Enabled = false,
                SourceIndex = 0,
                Roi = System.Drawing.Rectangle.Empty,
                RoiSaved = false,
                CornerFound = false
            };
        }

        public static PreprocessSnapshot[] CloneSnapshots(PreprocessSnapshot[] snapshots)
        {
            if (snapshots == null)
            {
                return null;
            }

            var cloned = new PreprocessSnapshot[snapshots.Length];
            for (var i = 0; i < snapshots.Length; i++)
            {
                if (snapshots[i] == null)
                {
                    continue;
                }

                cloned[i] = new PreprocessSnapshot
                {
                    Enabled = snapshots[i].Enabled,
                    Threshold = snapshots[i].Threshold,
                    ErodeIterations = snapshots[i].ErodeIterations,
                    DilateIterations = snapshots[i].DilateIterations,
                    OpenIterations = snapshots[i].OpenIterations,
                    CloseIterations = snapshots[i].CloseIterations
                };
            }

            return cloned;
        }

        public static ReferenceCornerSnapshot CloneReferenceCornerSnapshot(ReferenceCornerSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return null;
            }

            return new ReferenceCornerSnapshot
            {
                Enabled = snapshot.Enabled,
                SourceIndex = snapshot.SourceIndex,
                Roi = snapshot.Roi,
                RoiSaved = snapshot.RoiSaved,
                CornerFound = snapshot.CornerFound
            };
        }

        public static List<MeasureRecord> CloneMeasureRecords(List<MeasureRecord> records)
        {
            var cloned = new List<MeasureRecord>();
            if (records == null)
            {
                return cloned;
            }

            foreach (var record in records)
            {
                if (record == null)
                {
                    continue;
                }

                cloned.Add(new MeasureRecord
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
                    LocalEndPoint = record.LocalEndPoint
                });
            }

            return cloned;
        }

        public static List<JudgementCriterionRule> CloneJudgementCriteria(List<JudgementCriterionRule> rules)
        {
            var cloned = new List<JudgementCriterionRule>();
            if (rules == null)
            {
                return cloned;
            }

            foreach (var rule in rules)
            {
                if (rule == null)
                {
                    continue;
                }

                cloned.Add(new JudgementCriterionRule
                {
                    Name = rule.Name,
                    CalculationExpression = rule.CalculationExpression,
                    SpecExpression = rule.SpecExpression,
                    CalculationExpressionB = rule.CalculationExpressionB,
                    SpecExpressionB = rule.SpecExpressionB
                });
            }

            return cloned;
        }
    }
}
