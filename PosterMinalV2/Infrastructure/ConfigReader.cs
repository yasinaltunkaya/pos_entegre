using System;
using System.Configuration;

namespace PosterMinalV2.Infrastructure
{
    public static class ConfigReader
    {
        public static string GetActiveDevice()
        {
            return ConfigurationManager.AppSettings["ActiveDevice"] ?? string.Empty;
        }

        public static string Get(string key, string defaultValue = "")
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        public static int GetInt(string key, int defaultValue)
        {
            var raw = ConfigurationManager.AppSettings[key];
            int parsed;
            return int.TryParse(raw, out parsed) ? parsed : defaultValue;
        }
    }
}
