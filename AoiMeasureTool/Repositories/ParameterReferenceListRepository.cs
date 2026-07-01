using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoiMeasureTool
{
    internal sealed class ParameterReferenceListRepository
    {
        public Dictionary<string, DetectionParameterReference> LoadReferences(string path)
        {
            var references = new Dictionary<string, DetectionParameterReference>(StringComparer.OrdinalIgnoreCase);
            if (!File.Exists(path))
            {
                return references;
            }

            var sectionValues = LoadSectionValues(path);
            foreach (var pair in sectionValues)
            {
                if (string.Equals(pair.Key, "listSort", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var reference = new DetectionParameterReference
                {
                    MainParameterName = pair.Key,
                    SubParameter1 = GetSectionValue(pair.Value, "subParameter1"),
                    SubParameter2 = GetSectionValue(pair.Value, "subParameter2"),
                    SubParameter3 = GetSectionValue(pair.Value, "subParameter3")
                };
                references[pair.Key] = reference;
            }

            return references;
        }

        public List<string> Load(string path)
        {
            var items = new List<string>();
            if (!File.Exists(path))
            {
                return items;
            }

            var sectionValues = LoadSectionValues(path);

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

        public void Save(string path, IList<string> items, IDictionary<string, DetectionParameterReference> references = null)
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

                    DetectionParameterReference reference = null;
                    if (references != null)
                    {
                        references.TryGetValue(parameterName, out reference);
                    }

                    writer.WriteLine();
                    writer.WriteLine("[" + parameterName + "]");
                    writer.WriteLine("subParameter1=" + (reference?.SubParameter1 ?? string.Empty));
                    writer.WriteLine("subParameter2=" + (reference?.SubParameter2 ?? string.Empty));
                    writer.WriteLine("subParameter3=" + (reference?.SubParameter3 ?? string.Empty));
                }
            }
        }

        private static Dictionary<string, Dictionary<string, string>> LoadSectionValues(string path)
        {
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
                }
            }

            return sectionValues;
        }

        private static string GetSectionValue(Dictionary<string, string> values, string key)
        {
            string value;
            return values != null && values.TryGetValue(key, out value) ? value : string.Empty;
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
