using System;
using System.IO;

namespace PayGoAgent.Dll
{
    public class PayGoDllWrapper
    {
        private readonly string _dllPath;

        public PayGoDllWrapper(string dllPath)
        {
            _dllPath = dllPath;
        }

        public bool IsDllAvailable()
        {
            return File.Exists(_dllPath);
        }

        public bool Connect()
        {
            // TODO: Replace with real PAYGO_OKC.dll connect function.
            return IsDllAvailable();
        }

        public bool CashierLogin(string username, string password)
        {
            // TODO: Replace with real cashier login method from vendor DLL.
            return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
        }

        public bool CashierLogout()
        {
            // TODO: Replace with real cashier logout method from vendor DLL.
            return true;
        }

        public string Sale(decimal amount)
        {
            // TODO: Replace with real sale function from vendor DLL and return result code.
            return amount > 0 ? "00" : "12";
        }

        public string Refund(decimal amount)
        {
            // TODO: Replace with real refund function from vendor DLL and return result code.
            return amount > 0 ? "00" : "12";
        }

        public string GetXReport()
        {
            // TODO: Replace with real X report function from vendor DLL.
            return "MOCK_X_REPORT_OK";
        }

        public string GetZReport()
        {
            // TODO: Replace with real Z report function from vendor DLL.
            return "MOCK_Z_REPORT_OK";
        }

        public bool IsDeviceReady()
        {
            // TODO: Replace with real health/status check from vendor DLL.
            return IsDllAvailable();
        }
    }
}
