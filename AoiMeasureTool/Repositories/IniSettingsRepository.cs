using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AoiMeasureTool
{
    internal sealed class IniSettingsRepository
    {
        public AppSettingsData Load(string settingsPath)
        {
            var data = new AppSettingsData();
            if (!File.Exists(settingsPath))
            {
                return data;
            }

            var lines = File.ReadAllLines(settingsPath);
            string currentSection = null;

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
                {
                    continue;
                }

                if (line.StartsWith("ImagePath=", StringComparison.OrdinalIgnoreCase))
                {
                    data.LastImagePath = line.Substring("ImagePath=".Length).Trim();
                    continue;
                }

                if (line.StartsWith("ActiveProductKey=", StringComparison.OrdinalIgnoreCase))
                {
                    data.ActiveProductKey = line.Substring("ActiveProductKey=".Length).Trim();
                    continue;
                }

                if (line.StartsWith("ContinuousInspectionMainParameter=", StringComparison.OrdinalIgnoreCase))
                {
                    data.ContinuousInspectionMainParameter = line.Substring("ContinuousInspectionMainParameter=".Length).Trim();
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Substring(1, line.Length - 2).Trim();
                    continue;
                }

                if (string.IsNullOrWhiteSpace(currentSection))
                {
                    continue;
                }

                var equalsIndex = line.IndexOf('=');
                if (equalsIndex <= 0)
                {
                    continue;
                }

                var name = line.Substring(0, equalsIndex).Trim();
                var value = line.Substring(equalsIndex + 1).Trim();

                if (string.Equals(currentSection, "listSort", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        data.ListSortItems.Add(value);
                    }

                    continue;
                }

                if (name.StartsWith("Preprocess", StringComparison.OrdinalIgnoreCase))
                {
                    ApplyPreprocessSetting(data, currentSection, name, value);
                    continue;
                }

                if (name.StartsWith("Reference", StringComparison.OrdinalIgnoreCase))
                {
                    ApplyReferenceSetting(data, currentSection, name, value);
                    continue;
                }

                if (name.StartsWith("Measure", StringComparison.OrdinalIgnoreCase))
                {
                    ApplyMeasureSetting(data, currentSection, name, value);
                    continue;
                }

                if (name.StartsWith("JudgementCriterion", StringComparison.OrdinalIgnoreCase))
                {
                    ApplyJudgementCriterionSetting(data, currentSection, name, value);
                    continue;
                }

                if (name.StartsWith("DualThreshold", StringComparison.OrdinalIgnoreCase))
                {
                    ApplyDualThresholdSetting(data, currentSection, name, value);
                }
            }

            return data;
        }

        public void Save(string settingsPath, AppSettingsData data, string fallbackProductKey)
        {
            var directory = Path.GetDirectoryName(settingsPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var writer = new StreamWriter(settingsPath, false))
            {
                if (!string.IsNullOrWhiteSpace(data.LastImagePath))
                {
                    writer.WriteLine("ImagePath=" + data.LastImagePath);
                }

                if (!string.IsNullOrWhiteSpace(data.ActiveProductKey))
                {
                    writer.WriteLine("ActiveProductKey=" + data.ActiveProductKey);
                }

                if (!string.IsNullOrWhiteSpace(data.ContinuousInspectionMainParameter))
                {
                    writer.WriteLine("ContinuousInspectionMainParameter=" + data.ContinuousInspectionMainParameter);
                }

                if (data.ListSortItems.Count > 0)
                {
                    writer.WriteLine("[listSort]");
                    for (var i = 0; i < data.ListSortItems.Count; i++)
                    {
                        writer.WriteLine("Item" + (i + 1) + "=" + (data.ListSortItems[i] ?? string.Empty));
                    }

                    writer.WriteLine(string.Empty);
                }

                var sectionKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var key in data.PreprocessProfiles.Keys)
                {
                    sectionKeys.Add(key);
                }
                foreach (var key in data.ReferenceCornerProfiles.Keys)
                {
                    sectionKeys.Add(key);
                }
                foreach (var key in data.MeasureProfiles.Keys)
                {
                    sectionKeys.Add(key);
                }
                foreach (var key in data.DualThresholdProfiles.Keys)
                {
                    sectionKeys.Add(key);
                }

                if (sectionKeys.Count == 0 && !string.IsNullOrWhiteSpace(fallbackProductKey))
                {
                    sectionKeys.Add(fallbackProductKey);
                }

                foreach (var sectionKey in sectionKeys)
                {
                    writer.WriteLine("[" + sectionKey + "]");

                    PreprocessSnapshot[] preprocessSnapshots;
                    if (!data.PreprocessProfiles.TryGetValue(sectionKey, out preprocessSnapshots))
                    {
                        preprocessSnapshots = ProfileDataCloner.CreateDefaultPreprocessSnapshots();
                    }

                    for (var i = 0; i < 4; i++)
                    {
                        var snapshot = preprocessSnapshots[i];
                        if (snapshot == null)
                        {
                            continue;
                        }

                        writer.WriteLine("Preprocess" + (i + 1) + "Enabled=" + snapshot.Enabled);
                        writer.WriteLine("Preprocess" + (i + 1) + "Threshold=" + snapshot.Threshold);
                        writer.WriteLine("Preprocess" + (i + 1) + "Erode=" + snapshot.ErodeIterations);
                        writer.WriteLine("Preprocess" + (i + 1) + "Dilate=" + snapshot.DilateIterations);
                        writer.WriteLine("Preprocess" + (i + 1) + "Open=" + snapshot.OpenIterations);
                        writer.WriteLine("Preprocess" + (i + 1) + "Close=" + snapshot.CloseIterations);
                    }

                    ReferenceCornerSnapshot referenceSnapshot;
                    if (!data.ReferenceCornerProfiles.TryGetValue(sectionKey, out referenceSnapshot))
                    {
                        referenceSnapshot = ProfileDataCloner.CreateDefaultReferenceCornerSnapshot();
                    }

                    writer.WriteLine("ReferenceCornerEnabled=" + referenceSnapshot.Enabled);
                    writer.WriteLine("ReferenceSourceIndex=" + referenceSnapshot.SourceIndex);
                    writer.WriteLine("ReferencePointMode=" + (int)referenceSnapshot.PointMode);
                    writer.WriteLine("ReferenceRoiX=" + referenceSnapshot.Roi.X);
                    writer.WriteLine("ReferenceRoiY=" + referenceSnapshot.Roi.Y);
                    writer.WriteLine("ReferenceRoiWidth=" + referenceSnapshot.Roi.Width);
                    writer.WriteLine("ReferenceRoiHeight=" + referenceSnapshot.Roi.Height);
                    writer.WriteLine("ReferenceRoiSaved=" + referenceSnapshot.RoiSaved);
                    writer.WriteLine("ReferenceProtrusionMinWidth=" + referenceSnapshot.ProtrusionMinWidth);
                    writer.WriteLine("ReferenceProtrusionMinHeight=" + referenceSnapshot.ProtrusionMinHeight);
                    writer.WriteLine("ReferenceProtrusionWidthIncreaseThreshold=" + referenceSnapshot.ProtrusionWidthIncreaseThreshold);
                    writer.WriteLine("ReferenceProtrusionConsecutiveRows=" + referenceSnapshot.ProtrusionConsecutiveRows);

                    List<MeasureRecord> measureRecords;
                    if (!data.MeasureProfiles.TryGetValue(sectionKey, out measureRecords))
                    {
                        measureRecords = new List<MeasureRecord>();
                    }

                    for (var i = 0; i < measureRecords.Count; i++)
                    {
                        var record = measureRecords[i];
                        writer.WriteLine("Measure" + (i + 1) + "X1=" + record.StartPoint.X);
                        writer.WriteLine("Measure" + (i + 1) + "Y1=" + record.StartPoint.Y);
                        writer.WriteLine("Measure" + (i + 1) + "X2=" + record.EndPoint.X);
                        writer.WriteLine("Measure" + (i + 1) + "Y2=" + record.EndPoint.Y);
                        writer.WriteLine("Measure" + (i + 1) + "LtX=" + record.ReferenceTopLeft.X);
                        writer.WriteLine("Measure" + (i + 1) + "LtY=" + record.ReferenceTopLeft.Y);
                        writer.WriteLine("Measure" + (i + 1) + "RtX=" + record.ReferenceTopRight.X);
                        writer.WriteLine("Measure" + (i + 1) + "RtY=" + record.ReferenceTopRight.Y);
                        writer.WriteLine("Measure" + (i + 1) + "RefLen=" + record.ReferenceLength.ToString("0.###", CultureInfo.InvariantCulture));
                        writer.WriteLine("Measure" + (i + 1) + "CenterX=" + record.CenterPoint.X);
                        writer.WriteLine("Measure" + (i + 1) + "CenterY=" + record.CenterPoint.Y);
                        writer.WriteLine("Measure" + (i + 1) + "LocalX1=" + record.LocalStartPoint.X.ToString("0.###", CultureInfo.InvariantCulture));
                        writer.WriteLine("Measure" + (i + 1) + "LocalY1=" + record.LocalStartPoint.Y.ToString("0.###", CultureInfo.InvariantCulture));
                        writer.WriteLine("Measure" + (i + 1) + "LocalX2=" + record.LocalEndPoint.X.ToString("0.###", CultureInfo.InvariantCulture));
                        writer.WriteLine("Measure" + (i + 1) + "LocalY2=" + record.LocalEndPoint.Y.ToString("0.###", CultureInfo.InvariantCulture));
                        writer.WriteLine("Measure" + (i + 1) + "Distance=" + record.Distance.ToString(CultureInfo.InvariantCulture));
                        writer.WriteLine("Measure" + (i + 1) + "Source=" + (record.SourceName ?? string.Empty));
                        writer.WriteLine("Measure" + (i + 1) + "Direction=" + (record.DirectionName ?? string.Empty));
                        writer.WriteLine("Measure" + (i + 1) + "Status=" + (record.StatusText ?? string.Empty));
                    }

                    List<JudgementCriterionRule> judgementCriteriaRules;
                    if (!data.JudgementCriteriaProfiles.TryGetValue(sectionKey, out judgementCriteriaRules))
                    {
                        judgementCriteriaRules = new List<JudgementCriterionRule>();
                    }

                    for (var i = 0; i < judgementCriteriaRules.Count; i++)
                    {
                        var rule = judgementCriteriaRules[i];
                        writer.WriteLine("JudgementCriterion" + (i + 1) + "Name=" + (rule?.Name ?? string.Empty));
                        writer.WriteLine("JudgementCriterion" + (i + 1) + "Calc=" + (rule?.CalculationExpression ?? string.Empty));
                        writer.WriteLine("JudgementCriterion" + (i + 1) + "Spec=" + (rule?.SpecExpression ?? string.Empty));
                    writer.WriteLine("JudgementCriterion" + (i + 1) + "CalcB=" + (rule?.CalculationExpressionB ?? string.Empty));
                    writer.WriteLine("JudgementCriterion" + (i + 1) + "SpecB=" + (rule?.SpecExpressionB ?? string.Empty));
                }

                    DualThresholdSnapshot dual;
                    if (!data.DualThresholdProfiles.TryGetValue(sectionKey, out dual))
                    {
                        dual = data.DualThresholdSettings ?? ProfileDataCloner.CreateDefaultDualThresholdSnapshot();
                    }

                    writer.WriteLine("DualThresholdEnabled=" + dual.Enabled);
                    writer.WriteLine("DualThresholdLower=" + dual.LowerThreshold);
                    writer.WriteLine("DualThresholdUpper=" + dual.UpperThreshold);
                    writer.WriteLine("DualThresholdErode=" + dual.ErodeIterations);
                    writer.WriteLine("DualThresholdDilate=" + dual.DilateIterations);
                    writer.WriteLine("DualThresholdOpen=" + dual.OpenIterations);
                    writer.WriteLine("DualThresholdClose=" + dual.CloseIterations);

                    writer.WriteLine(string.Empty);
                }
            }
        }

        private static void ApplyPreprocessSetting(AppSettingsData data, string section, string name, string value)
        {
            var suffix = name.Substring("Preprocess".Length);
            var digitEnd = 0;
            while (digitEnd < suffix.Length && char.IsDigit(suffix[digitEnd]))
            {
                digitEnd++;
            }

            var numberText = digitEnd > 0 ? suffix.Substring(0, digitEnd) : null;
            var propertyName = digitEnd < suffix.Length ? suffix.Substring(digitEnd) : string.Empty;
            int preprocessIndex;
            if (!int.TryParse(numberText, out preprocessIndex))
            {
                return;
            }

            preprocessIndex -= 1;
            if (preprocessIndex < 0 || preprocessIndex >= 4)
            {
                return;
            }

            PreprocessSnapshot[] productSnapshots;
            if (!data.PreprocessProfiles.TryGetValue(section, out productSnapshots))
            {
                productSnapshots = new PreprocessSnapshot[4];
                data.PreprocessProfiles[section] = productSnapshots;
            }

            if (productSnapshots[preprocessIndex] == null)
            {
                productSnapshots[preprocessIndex] = new PreprocessSnapshot();
            }

            var snapshot = productSnapshots[preprocessIndex];
            if (propertyName.Equals("Enabled", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Enabled = bool.Parse(value);
            }
            else if (propertyName.Equals("Threshold", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Threshold = int.Parse(value);
            }
            else if (propertyName.Equals("Erode", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.ErodeIterations = int.Parse(value);
            }
            else if (propertyName.Equals("Dilate", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.DilateIterations = int.Parse(value);
            }
            else if (propertyName.Equals("Open", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.OpenIterations = int.Parse(value);
            }
            else if (propertyName.Equals("Close", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.CloseIterations = int.Parse(value);
            }
        }

        private static void ApplyReferenceSetting(AppSettingsData data, string section, string name, string value)
        {
            ReferenceCornerSnapshot snapshot;
            if (!data.ReferenceCornerProfiles.TryGetValue(section, out snapshot))
            {
                snapshot = ProfileDataCloner.CreateDefaultReferenceCornerSnapshot();
                data.ReferenceCornerProfiles[section] = snapshot;
            }

            if (name.Equals("ReferenceCornerEnabled", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Enabled = bool.Parse(value);
            }
            else if (name.Equals("ReferenceSourceIndex", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.SourceIndex = int.Parse(value);
            }
            else if (name.Equals("ReferencePointMode", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.PointMode = (ReferenceCornerPointMode)int.Parse(value);
            }
            else if (name.Equals("ReferenceRoiX", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Roi = new Rectangle(int.Parse(value), snapshot.Roi.Y, snapshot.Roi.Width, snapshot.Roi.Height);
            }
            else if (name.Equals("ReferenceRoiY", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Roi = new Rectangle(snapshot.Roi.X, int.Parse(value), snapshot.Roi.Width, snapshot.Roi.Height);
            }
            else if (name.Equals("ReferenceRoiWidth", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Roi = new Rectangle(snapshot.Roi.X, snapshot.Roi.Y, int.Parse(value), snapshot.Roi.Height);
            }
            else if (name.Equals("ReferenceRoiHeight", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Roi = new Rectangle(snapshot.Roi.X, snapshot.Roi.Y, snapshot.Roi.Width, int.Parse(value));
            }
            else if (name.Equals("ReferenceRoiSaved", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.RoiSaved = bool.Parse(value);
            }
            else if (name.Equals("ReferenceProtrusionMinWidth", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.ProtrusionMinWidth = int.Parse(value);
            }
            else if (name.Equals("ReferenceProtrusionMinHeight", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.ProtrusionMinHeight = int.Parse(value);
            }
            else if (name.Equals("ReferenceProtrusionWidthIncreaseThreshold", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.ProtrusionWidthIncreaseThreshold = int.Parse(value);
            }
            else if (name.Equals("ReferenceProtrusionConsecutiveRows", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.ProtrusionConsecutiveRows = int.Parse(value);
            }
        }

        private static void ApplyMeasureSetting(AppSettingsData data, string section, string name, string value)
        {
            List<MeasureRecord> measureRecords;
            if (!data.MeasureProfiles.TryGetValue(section, out measureRecords))
            {
                measureRecords = new List<MeasureRecord>();
                data.MeasureProfiles[section] = measureRecords;
            }

            var suffix = name.Substring("Measure".Length);
            var digitEnd = 0;
            while (digitEnd < suffix.Length && char.IsDigit(suffix[digitEnd]))
            {
                digitEnd++;
            }

            var numberText = digitEnd > 0 ? suffix.Substring(0, digitEnd) : null;
            var propertyName = digitEnd < suffix.Length ? suffix.Substring(digitEnd) : string.Empty;
            int measureIndex;
            if (!int.TryParse(numberText, out measureIndex))
            {
                return;
            }

            measureIndex -= 1;
            while (measureRecords.Count <= measureIndex)
            {
                measureRecords.Add(new MeasureRecord());
            }

            var record = measureRecords[measureIndex];
            if (propertyName.Equals("X1", StringComparison.OrdinalIgnoreCase))
            {
                record.StartPoint = new Point(int.Parse(value), record.StartPoint.Y);
            }
            else if (propertyName.Equals("Y1", StringComparison.OrdinalIgnoreCase))
            {
                record.StartPoint = new Point(record.StartPoint.X, int.Parse(value));
            }
            else if (propertyName.Equals("X2", StringComparison.OrdinalIgnoreCase))
            {
                record.EndPoint = new Point(int.Parse(value), record.EndPoint.Y);
            }
            else if (propertyName.Equals("Y2", StringComparison.OrdinalIgnoreCase))
            {
                record.EndPoint = new Point(record.EndPoint.X, int.Parse(value));
            }
            else if (propertyName.Equals("CenterX", StringComparison.OrdinalIgnoreCase))
            {
                record.CenterPoint = new Point(int.Parse(value), record.CenterPoint.Y);
            }
            else if (propertyName.Equals("CenterY", StringComparison.OrdinalIgnoreCase))
            {
                record.CenterPoint = new Point(record.CenterPoint.X, int.Parse(value));
            }
            else if (propertyName.Equals("Distance", StringComparison.OrdinalIgnoreCase))
            {
                record.Distance = double.Parse(value, CultureInfo.InvariantCulture);
            }
            else if (propertyName.Equals("Source", StringComparison.OrdinalIgnoreCase))
            {
                record.SourceName = value;
            }
            else if (propertyName.Equals("Direction", StringComparison.OrdinalIgnoreCase))
            {
                record.DirectionName = value;
            }
            else if (propertyName.Equals("Status", StringComparison.OrdinalIgnoreCase))
            {
                record.StatusText = value;
            }
            else if (propertyName.Equals("LtX", StringComparison.OrdinalIgnoreCase))
            {
                record.ReferenceTopLeft = new Point(int.Parse(value), record.ReferenceTopLeft.Y);
            }
            else if (propertyName.Equals("LtY", StringComparison.OrdinalIgnoreCase))
            {
                record.ReferenceTopLeft = new Point(record.ReferenceTopLeft.X, int.Parse(value));
            }
            else if (propertyName.Equals("RtX", StringComparison.OrdinalIgnoreCase))
            {
                record.ReferenceTopRight = new Point(int.Parse(value), record.ReferenceTopRight.Y);
            }
            else if (propertyName.Equals("RtY", StringComparison.OrdinalIgnoreCase))
            {
                record.ReferenceTopRight = new Point(record.ReferenceTopRight.X, int.Parse(value));
            }
            else if (propertyName.Equals("RefLen", StringComparison.OrdinalIgnoreCase))
            {
                record.ReferenceLength = double.Parse(value, CultureInfo.InvariantCulture);
            }
            else if (propertyName.Equals("LocalX1", StringComparison.OrdinalIgnoreCase))
            {
                record.LocalStartPoint = new PointF(float.Parse(value, CultureInfo.InvariantCulture), record.LocalStartPoint.Y);
            }
            else if (propertyName.Equals("LocalY1", StringComparison.OrdinalIgnoreCase))
            {
                record.LocalStartPoint = new PointF(record.LocalStartPoint.X, float.Parse(value, CultureInfo.InvariantCulture));
            }
            else if (propertyName.Equals("LocalX2", StringComparison.OrdinalIgnoreCase))
            {
                record.LocalEndPoint = new PointF(float.Parse(value, CultureInfo.InvariantCulture), record.LocalEndPoint.Y);
            }
            else if (propertyName.Equals("LocalY2", StringComparison.OrdinalIgnoreCase))
            {
                record.LocalEndPoint = new PointF(record.LocalEndPoint.X, float.Parse(value, CultureInfo.InvariantCulture));
            }
        }

        private static void ApplyJudgementCriterionSetting(AppSettingsData data, string section, string name, string value)
        {
            List<JudgementCriterionRule> rules;
            if (!data.JudgementCriteriaProfiles.TryGetValue(section, out rules))
            {
                rules = new List<JudgementCriterionRule>();
                data.JudgementCriteriaProfiles[section] = rules;
            }

            var suffix = name.Substring("JudgementCriterion".Length);
            var digitEnd = 0;
            while (digitEnd < suffix.Length && char.IsDigit(suffix[digitEnd]))
            {
                digitEnd++;
            }

            var numberText = digitEnd > 0 ? suffix.Substring(0, digitEnd) : null;
            var propertyName = digitEnd < suffix.Length ? suffix.Substring(digitEnd) : string.Empty;
            int ruleIndex;
            if (!int.TryParse(numberText, out ruleIndex))
            {
                return;
            }

            ruleIndex -= 1;
            while (rules.Count <= ruleIndex)
            {
                rules.Add(new JudgementCriterionRule());
            }

            var rule = rules[ruleIndex];
            if (rule == null)
            {
                rule = new JudgementCriterionRule();
                rules[ruleIndex] = rule;
            }

            if (propertyName.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                rule.Name = value;
            }
            else if (propertyName.Equals("Calc", StringComparison.OrdinalIgnoreCase))
            {
                rule.CalculationExpression = value;
            }
            else if (propertyName.Equals("Spec", StringComparison.OrdinalIgnoreCase))
            {
                rule.SpecExpression = value;
            }
            else if (propertyName.Equals("CalcB", StringComparison.OrdinalIgnoreCase))
            {
                rule.CalculationExpressionB = value;
            }
            else if (propertyName.Equals("SpecB", StringComparison.OrdinalIgnoreCase))
            {
                rule.SpecExpressionB = value;
            }
        }

        private static void ApplyDualThresholdSetting(AppSettingsData data, string section, string name, string value)
        {
            section = string.IsNullOrWhiteSpace(section) ? "DEFAULT" : section;

            DualThresholdSnapshot snapshot;
            if (!data.DualThresholdProfiles.TryGetValue(section, out snapshot))
            {
                snapshot = ProfileDataCloner.CreateDefaultDualThresholdSnapshot();
                data.DualThresholdProfiles[section] = snapshot;
            }

            if (name.Equals("DualThresholdEnabled", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.Enabled = bool.Parse(value);
            }
            else if (name.Equals("DualThresholdLower", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.LowerThreshold = int.Parse(value);
            }
            else if (name.Equals("DualThresholdUpper", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.UpperThreshold = int.Parse(value);
            }
            else if (name.Equals("DualThresholdErode", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.ErodeIterations = int.Parse(value);
            }
            else if (name.Equals("DualThresholdDilate", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.DilateIterations = int.Parse(value);
            }
            else if (name.Equals("DualThresholdOpen", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.OpenIterations = int.Parse(value);
            }
            else if (name.Equals("DualThresholdClose", StringComparison.OrdinalIgnoreCase))
            {
                snapshot.CloseIterations = int.Parse(value);
            }

            data.DualThresholdSettings = ProfileDataCloner.CloneDualThresholdSnapshot(snapshot);
        }
    }
}
