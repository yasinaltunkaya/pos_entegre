using System;
using System.Collections.Generic;
using System.IO;

namespace PayGoAgent.Config
{
    public class IniFileReader
    {
        private readonly Dictionary<string, string> _values;

        public IniFileReader(string filePath)
        {
            _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("INI configuration file not found.", filePath);
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#") || !line.Contains("="))
                {
                    continue;
                }

                var separatorIndex = line.IndexOf('=');
                var key = line.Substring(0, separatorIndex).Trim();
                var value = line.Substring(separatorIndex + 1).Trim();

                if (!string.IsNullOrWhiteSpace(key))
                {
                    _values[key] = value;
                }
            }
        }

        public string GetValue(string key, string defaultValue = "")
        {
            string value;
            return _values.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
