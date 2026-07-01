using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoiMeasureTool
{
    internal sealed class ParameterReferenceListRepository
    {
        public List<string> Load(string path)
        {
            var items = new List<string>();
            if (!File.Exists(path))
            {
                return items;
            }

            var sectionValues = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            string currentSection = null;

            foreach (var rawLine in File.ReadAllLines(path))
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
                {
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Substring(1, line.Length - 2).Trim();
                    if (!sectionValues.ContainsKey(currentSection))
                    {
                        sectionValues[currentSection] =
                            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    }

                    continue;
                }

                var equalsIndex = line.IndexOf('=');
                if (equalsIndex > 0 && !string.IsNullOrWhiteSpace(currentSection))
                {
                    var key = line.Substring(0, equalsIndex).Trim();
                    var value = line.Substring(equalsIndex + 1).Trim();
                    sectionValues[currentSection][key] = value;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(currentSection))
                {
                    var fallbackValue = line.Trim();
                    if (!string.IsNullOrWhiteSpace(fallbackValue))
                    {
                        items.Add(fallbackValue);
                    }
                }
            }

            Dictionary<string, string> sortSection;
            if (sectionValues.TryGetValue("listSort", out sortSection))
            {
                items.Clear();
                foreach (var pair in sortSection.OrderBy(p => ExtractTrailingNumber(p.Key)))
                {
                    if (!string.IsNullOrWhiteSpace(pair.Value))
                    {
                        items.Add(pair.Value.Trim());
                    }
                }
            }

            return items;
        }

        public void Save(string path, IList<string> items)
        {
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var writer = new StreamWriter(path, false))
            {
                writer.WriteLine("[listSort]");
                for (var i = 0; i < items.Count; i++)
                {
                    var parameterName = (items[i] ?? string.Empty).Trim();
                    writer.WriteLine("Item" + (i + 1) + "=" + parameterName);
                }

                foreach (var item in items)
                {
                    var parameterName = (item ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(parameterName))
                    {
                        continue;
                    }

                    writer.WriteLine();
                    writer.WriteLine("[" + parameterName + "]");
                    writer.WriteLine("subParameter1=");
                    writer.WriteLine("subParameter2=");
                    writer.WriteLine("subParameter3=");
                }
            }
        }

        private static int ExtractTrailingNumber(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return int.MaxValue;
            }

            var digitStart = key.Length;
            while (digitStart > 0 && char.IsDigit(key[digitStart - 1]))
            {
                digitStart--;
            }

            int number;
            return int.TryParse(key.Substring(digitStart), out number) ? number : int.MaxValue;
        }
    }
}
