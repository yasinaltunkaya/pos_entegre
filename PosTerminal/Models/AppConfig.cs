namespace PosTerminal.Models
{
    /// <summary>
    /// Runtime configuration loaded from config.ini.
    /// DeviceName determines which fiscal integration service is used by the manager.
    /// </summary>
    public sealed class AppConfig
    {
        public string DeviceName { get; set; } = "PayGo";
        public string CommType { get; set; } = "USB";
        public string SerialPort { get; set; } = "COM1";
        public string ServerAddr { get; set; } = "127.0.0.1";
        public int ServerPort { get; set; } = 41200;
        public string CashierUser { get; set; } = "admin";
        public string CashierPass { get; set; } = "1234";
    }
}
