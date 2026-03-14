namespace PayGoAgent.Models
{
    public class DeviceStatusResponse
    {
        public bool IsDllAvailable { get; set; }
        public bool IsDeviceReady { get; set; }
        public string CommunicationType { get; set; }
        public string SerialPort { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string CashierUserMasked { get; set; }
        public string AllowedOrigins { get; set; }
    }
}
