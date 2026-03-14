using System;
using System.IO;
using System.Web.Hosting;

namespace PayGoAgent.Config
{
    public class AgentConfig
    {
        public string CommType { get; set; }
        public string SerialPort { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string CashierUser { get; set; }
        public string CashierPass { get; set; }
        public string LogPath { get; set; }
        public string AllowedOrigins { get; set; }

        public static AgentConfig Load(string iniPhysicalPath)
        {
            var iniReader = new IniFileReader(iniPhysicalPath);

            return new AgentConfig
            {
                CommType = iniReader.GetValue("COMMTYPE", "USB"),
                SerialPort = iniReader.GetValue("SERIALPORT", "COM1"),
                ServerAddress = iniReader.GetValue("SERVERADDR", "127.0.0.1"),
                ServerPort = ParsePort(iniReader.GetValue("SERVERPORT", "41200")),
                CashierUser = iniReader.GetValue("CASHIERUSER", "1"),
                CashierPass = iniReader.GetValue("CASHIERPASS", "1234"),
                LogPath = ResolveLogPath(iniReader.GetValue("LOGPATH", "Logs")),
                AllowedOrigins = iniReader.GetValue("ALLOWEDORIGINS", "*")
            };
        }

        private static int ParsePort(string portText)
        {
            int parsedPort;
            return int.TryParse(portText, out parsedPort) ? parsedPort : 41200;
        }

        private static string ResolveLogPath(string configuredPath)
        {
            if (Path.IsPathRooted(configuredPath))
            {
                return configuredPath;
            }

            var basePath = HostingEnvironment.MapPath("~") ?? AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, configuredPath);
        }

        public string GetMaskedCashierUser()
        {
            if (string.IsNullOrWhiteSpace(CashierUser))
            {
                return "***";
            }

            return CashierUser.Length <= 1 ? "*" : string.Format("{0}***", CashierUser.Substring(0, 1));
        }
    }
}
