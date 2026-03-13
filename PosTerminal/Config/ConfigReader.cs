using PosTerminal.Models;

namespace PosTerminal.Config;

/// <summary>
/// Reads simple key=value INI files (no sections) and maps values to AppConfig.
/// </summary>
public static class ConfigReader
{
    public static AppConfig Load(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Configuration file not found: {path}");
        }

        var entries = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in File.ReadLines(path))
        {
            var trimmed = line.Trim();

            if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#') || trimmed.StartsWith(';'))
            {
                continue;
            }

            var separatorIndex = trimmed.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = trimmed[..separatorIndex].Trim();
            var value = trimmed[(separatorIndex + 1)..].Trim();
            entries[key] = value;
        }

        return new AppConfig
        {
            DeviceName = GetOrDefault(entries, "DEVICENAME", "PayGo"),
            CommType = GetOrDefault(entries, "COMMTYPE", "USB"),
            SerialPort = GetOrDefault(entries, "SERIALPORT", "COM1"),
            ServerAddr = GetOrDefault(entries, "SERVERADDR", "127.0.0.1"),
            ServerPort = ParseInt(GetOrDefault(entries, "SERVERPORT", "41200"), 41200),
            CashierUser = GetOrDefault(entries, "CASHIERUSER", "admin"),
            CashierPass = GetOrDefault(entries, "CASHIERPASS", "1234")
        };
    }

    private static string GetOrDefault(IReadOnlyDictionary<string, string> source, string key, string fallback) =>
        source.TryGetValue(key, out var value) ? value : fallback;

    private static int ParseInt(string? value, int fallback) =>
        int.TryParse(value, out var parsed) ? parsed : fallback;
}
