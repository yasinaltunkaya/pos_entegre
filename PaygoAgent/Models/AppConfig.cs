namespace PaygoAgent.Models;

/// <summary>
/// Strongly typed representation of the agent configuration loaded from config.ini.
/// </summary>
public sealed class AppConfig
{
    public string CommType { get; init; } = "USB";
    public string SerialPort { get; init; } = "COM1";
    public string ServerAddr { get; init; } = "127.0.0.1";
    public int ServerPort { get; init; } = 41200;
    public string CashierUser { get; init; } = "admin";
    public string CashierPass { get; init; } = "1234";
}
