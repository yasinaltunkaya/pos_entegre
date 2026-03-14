using System;
using System.IO;
using System.Web.Hosting;
using PayGoAgent.Config;
using PayGoAgent.Dll;
using PayGoAgent.Logging;
using PayGoAgent.Models;

namespace PayGoAgent.Services
{
    public class PayGoService : IPayGoService
    {
        private readonly PayGoDllWrapper _dllWrapper;
        public AgentConfig Configuration { get; }
        public FileLogger Logger { get; }

        public PayGoService()
        {
            var basePath = HostingEnvironment.MapPath("~") ?? AppDomain.CurrentDomain.BaseDirectory;
            var iniPath = Path.Combine(basePath, "Config", "agent.ini");
            Configuration = AgentConfig.Load(iniPath);
            Logger = new FileLogger(Configuration.LogPath);

            var dllPath = Path.Combine(basePath, "Dll", "PAYGO_OKC.dll");
            _dllWrapper = new PayGoDllWrapper(dllPath);
        }

        public ApiResponse<DeviceStatusResponse> GetStatus()
        {
            Logger.Info("Status request started.");

            var data = new DeviceStatusResponse
            {
                IsDllAvailable = _dllWrapper.IsDllAvailable(),
                IsDeviceReady = _dllWrapper.IsDeviceReady(),
                CommunicationType = Configuration.CommType,
                SerialPort = Configuration.SerialPort,
                ServerAddress = Configuration.ServerAddress,
                ServerPort = Configuration.ServerPort,
                CashierUserMasked = Configuration.GetMaskedCashierUser(),
                AllowedOrigins = Configuration.AllowedOrigins
            };

            Logger.Info(string.Format("Status request completed. IsDeviceReady: {0}", data.IsDeviceReady));
            return ApiResponse<DeviceStatusResponse>.Ok(data, "POS status retrieved successfully.");
        }

        public ApiResponse<object> Connect()
        {
            Logger.Info("Connection attempt started.");
            var connected = _dllWrapper.Connect();
            Logger.Info(string.Format("Connection attempt completed. Result: {0}", connected));

            return connected
                ? ApiResponse<object>.Ok(null, "POS device connection established.")
                : ApiResponse<object>.Fail("POS device connection failed.", "CONNECT_FAILED");
        }

        public ApiResponse<object> CashierLogin(string username, string password)
        {
            Logger.Info(string.Format("Cashier login started. Username: {0}", username));
            var loginResult = _dllWrapper.CashierLogin(username, password);
            Logger.Info(string.Format("Cashier login completed. Result: {0}", loginResult));

            return loginResult
                ? ApiResponse<object>.Ok(null, "Cashier login successful.")
                : ApiResponse<object>.Fail("Cashier login failed.", "CASHIER_LOGIN_FAILED");
        }

        public ApiResponse<object> CashierLogout()
        {
            Logger.Info("Cashier logout started.");
            var logoutResult = _dllWrapper.CashierLogout();
            Logger.Info(string.Format("Cashier logout completed. Result: {0}", logoutResult));

            return logoutResult
                ? ApiResponse<object>.Ok(null, "Cashier logout successful.")
                : ApiResponse<object>.Fail("Cashier logout failed.", "CASHIER_LOGOUT_FAILED");
        }

        public ApiResponse<object> Sale(decimal amount)
        {
            Logger.Info(string.Format("Sale started. Amount: {0:0.00}", amount));
            var resultCode = _dllWrapper.Sale(amount);
            var isSuccessful = string.Equals(resultCode, "00", StringComparison.OrdinalIgnoreCase);
            Logger.Info(string.Format("Sale completed. ResultCode: {0}", resultCode));

            return isSuccessful
                ? ApiResponse<object>.Ok(new { ResultCode = resultCode }, "Sale successful.")
                : ApiResponse<object>.Fail("Sale failed.", "SALE_FAILED", new { ResultCode = resultCode });
        }

        public ApiResponse<object> Refund(decimal amount)
        {
            Logger.Info(string.Format("Refund started. Amount: {0:0.00}", amount));
            var resultCode = _dllWrapper.Refund(amount);
            var isSuccessful = string.Equals(resultCode, "00", StringComparison.OrdinalIgnoreCase);
            Logger.Info(string.Format("Refund completed. ResultCode: {0}", resultCode));

            return isSuccessful
                ? ApiResponse<object>.Ok(new { ResultCode = resultCode }, "Refund successful.")
                : ApiResponse<object>.Fail("Refund failed.", "REFUND_FAILED", new { ResultCode = resultCode });
        }

        public ApiResponse<ReportResponse> GetXReport()
        {
            Logger.Info("X report request started.");
            var content = _dllWrapper.GetXReport();
            Logger.Info("X report request completed.");

            return ApiResponse<ReportResponse>.Ok(new ReportResponse
            {
                ReportType = "X",
                Content = content
            }, "X report generated successfully.");
        }

        public ApiResponse<ReportResponse> GetZReport()
        {
            Logger.Info("Z report request started.");
            var content = _dllWrapper.GetZReport();
            Logger.Info("Z report request completed.");

            return ApiResponse<ReportResponse>.Ok(new ReportResponse
            {
                ReportType = "Z",
                Content = content
            }, "Z report generated successfully.");
        }
    }
}
