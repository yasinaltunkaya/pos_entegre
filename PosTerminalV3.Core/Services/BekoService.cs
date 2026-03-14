using PosTerminalV3.Core.Infrastructure;

namespace PosTerminalV3.Core.Services
{
    public class BekoService : IPostDeviceService
    {
        public string TestConnection()
        {
            return Execute("TestConnection");
        }

        public string Sale(decimal amount)
        {
            return Execute("Sale " + amount.ToString("0.00"));
        }

        public string XReport()
        {
            return Execute("XReport");
        }

        public string ZReport()
        {
            return Execute("ZReport");
        }

        private string Execute(string operation)
        {
            Logger.Log(nameof(BekoService), operation, "Operation executed.");
            return "Beko " + operation + " completed.";
        }
    }
}
