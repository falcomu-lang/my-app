using System;
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
            }

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
            }
        }
    }
}
