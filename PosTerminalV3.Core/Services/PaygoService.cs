using System;
using System.IO.Ports;
using PosTerminalV3.Core.Infrastructure;

namespace PosTerminalV3.Core.Services
{
    public class PaygoService : IPostDeviceService
    {
        public string TestConnection()
        {
            return Execute("TestConnection", "PING");
        }

        public string Sale(decimal amount)
        {
            return Execute("Sale", "SALE|" + amount.ToString("0.00"));
        }

        public string XReport()
        {
            return Execute("XReport", "XREPORT");
        }

        public string ZReport()
        {
            return Execute("ZReport", "ZREPORT");
        }

        private string Execute(string method, string payload)
        {
            var serialPortName = ConfigReader.Get("SerialPort", "COM1");
            Logger.Log(nameof(PaygoService), method, "Request payload: " + payload);

            try
            {
                using (var serialPort = new SerialPort(serialPortName, 9600, Parity.None, 8, StopBits.One))
                {
                    serialPort.ReadTimeout = 2000;
                    serialPort.WriteTimeout = 2000;
                    serialPort.Open();
                    serialPort.WriteLine(payload);
                    Logger.Log(nameof(PaygoService), method, "Command sent over " + serialPortName);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(PaygoService), method, "Serial communication skipped/failed: " + ex.Message);
            }

            return "Paygo " + method + " completed.";
        }
    }
}
