using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AoiMeasureTool
{
    internal sealed class InnerSettingsRepository
    {
        public InnerSettingsData Load(string settingsPath)
        {
            var data = new InnerSettingsData();
            if (!File.Exists(settingsPath))
            {
                return data;
            }

            var lines = File.ReadAllLines(settingsPath);
            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#") || line.StartsWith("["))
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

                if (name.Equals("CCDXPrecision", StringComparison.OrdinalIgnoreCase))
                {
                    double parsed;
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
                    {
                        data.CcdXPrecision = parsed;
                    }
                }
                else if (name.Equals("CCDYPrecision", StringComparison.OrdinalIgnoreCase))
                {
                    double parsed;
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
                    {
                        data.CcdYPrecision = parsed;
                    }
                }
                else if (name.Equals("MeasurementScaleFactor", StringComparison.OrdinalIgnoreCase))
                {
                    double parsed;
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed) && parsed > 0)
                    {
                        data.MeasurementScaleFactor = parsed;
                    }
                }
                else if (name.StartsWith("Camera", StringComparison.OrdinalIgnoreCase))
                {
                    LoadCameraProfileValue(data, name, value);
                }
            }

            EnsureDefaultCameraProfiles(data);
            return data;
        }

        public void Save(string settingsPath, InnerSettingsData data)
        {
            var directory = Path.GetDirectoryName(settingsPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var writer = new StreamWriter(settingsPath, false))
            {
                writer.WriteLine("[InnerSettings]");
                writer.WriteLine("CCDXPrecision=" + data.CcdXPrecision.ToString(CultureInfo.InvariantCulture));
                writer.WriteLine("CCDYPrecision=" + data.CcdYPrecision.ToString(CultureInfo.InvariantCulture));
                writer.WriteLine("MeasurementScaleFactor=" + data.MeasurementScaleFactor.ToString(CultureInfo.InvariantCulture));
                for (var i = 0; i < data.CameraProfiles.Count; i++)
                {
                    var profile = data.CameraProfiles[i];
                    var prefix = "Camera" + (i + 1).ToString(CultureInfo.InvariantCulture);
                    writer.WriteLine(prefix + "Name=" + (profile.CameraName ?? string.Empty));
                    writer.WriteLine(prefix + "Usage=" + (profile.UsageName ?? string.Empty));
                    writer.WriteLine(prefix + "CCDXPrecision=" + profile.CcdXPrecision.ToString(CultureInfo.InvariantCulture));
                    writer.WriteLine(prefix + "CCDYPrecision=" + profile.CcdYPrecision.ToString(CultureInfo.InvariantCulture));
                    writer.WriteLine(prefix + "MeasurementScaleFactor=" + profile.MeasurementScaleFactor.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private static void EnsureDefaultCameraProfiles(InnerSettingsData data)
        {
            while (data.CameraProfiles.Count < 3)
            {
                data.CameraProfiles.Add(new InnerSettingsCameraProfile
                {
                    CameraName = "Camera " + (data.CameraProfiles.Count + 1).ToString(CultureInfo.InvariantCulture),
                    UsageName = string.Empty,
                    CcdXPrecision = data.CcdXPrecision,
                    CcdYPrecision = data.CcdYPrecision,
                    MeasurementScaleFactor = data.MeasurementScaleFactor
                });
            }
        }

        private static void LoadCameraProfileValue(InnerSettingsData data, string name, string value)
        {
            var match = System.Text.RegularExpressions.Regex.Match(name, @"^Camera(\d+)(Name|Usage|CCDXPrecision|CCDYPrecision|MeasurementScaleFactor)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return;
            }

            var index = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture) - 1;
            if (index < 0)
            {
                return;
            }

            while (data.CameraProfiles.Count <= index)
            {
                data.CameraProfiles.Add(new InnerSettingsCameraProfile());
            }

            var profile = data.CameraProfiles[index];
            var suffix = match.Groups[2].Value;
            if (suffix.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                profile.CameraName = value;
            }
            else if (suffix.Equals("Usage", StringComparison.OrdinalIgnoreCase))
            {
                profile.UsageName = value;
            }
            else if (suffix.Equals("CCDXPrecision", StringComparison.OrdinalIgnoreCase))
            {
                double parsed;
                if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
                {
                    profile.CcdXPrecision = parsed;
                }
            }
            else if (suffix.Equals("CCDYPrecision", StringComparison.OrdinalIgnoreCase))
            {
                double parsed;
                if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed))
                {
                    profile.CcdYPrecision = parsed;
                }
            }
            else if (suffix.Equals("MeasurementScaleFactor", StringComparison.OrdinalIgnoreCase))
            {
                double parsed;
                if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed) && parsed > 0)
                {
                    profile.MeasurementScaleFactor = parsed;
                }
            }
        }
    }
}
