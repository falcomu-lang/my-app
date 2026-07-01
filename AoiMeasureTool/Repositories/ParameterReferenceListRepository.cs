using System;
using System.Collections.Generic;
using System.IO;

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

            foreach (var rawLine in File.ReadAllLines(path))
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
                {
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    continue;
                }

                var value = line;
                var equalsIndex = line.IndexOf('=');
                if (equalsIndex >= 0)
                {
                    value = line.Substring(equalsIndex + 1).Trim();
                }

                if (!string.IsNullOrWhiteSpace(value))
                {
                    items.Add(value);
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
                writer.WriteLine("[MainParameters]");
                for (var i = 0; i < items.Count; i++)
                {
                    writer.WriteLine("Item" + (i + 1) + "=" + (items[i] ?? string.Empty));
                }
            }
        }
    }
}
