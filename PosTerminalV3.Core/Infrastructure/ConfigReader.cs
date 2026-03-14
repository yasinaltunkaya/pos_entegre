using System.Configuration;

namespace PosTerminalV3.Core.Infrastructure
{
    public static class ConfigReader
    {
        public static string Get(string key, string defaultValue = "")
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}
