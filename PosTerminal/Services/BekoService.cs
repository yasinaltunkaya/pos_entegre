using System;
using System.Threading;
using System.Threading.Tasks;
using PosTerminal.Models;

namespace PosTerminal.Services
{
    /// <summary>
    /// Placeholder implementation for Beko integration.
    /// Replace placeholders with real SDK/protocol calls.
    /// </summary>
    public sealed class BekoService : IFiscalDeviceService
    {
        private readonly AppConfig _config;
        private readonly LogService _logService;
        private bool _isConnected;

        public BekoService(AppConfig config, LogService logService)
        {
            _config = config;
            _logService = logService;
        }

        public string DeviceType { get { return "Beko"; } }

        public async Task<object> ConnectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            _isConnected = true;
            await _logService.InfoAsync("[Beko] Connect placeholder executed via " + _config.CommType + ".", cancellationToken);
            return new { success = true, device = DeviceType, message = "Beko connect placeholder executed." };
        }

        public async Task<object> SaleAsync(SaleRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureConnected();
            await _logService.InfoAsync("[Beko] Sale placeholder. Receipt=" + request.ReceiptNumber + ", Amount=" + request.Amount, cancellationToken);
            return new { success = true, device = DeviceType, message = "Beko sale placeholder executed." };
        }

        public async Task<object> XReportAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureConnected();
            await _logService.InfoAsync("[Beko] X report placeholder.", cancellationToken);
            return new { success = true, device = DeviceType, message = "Beko X report placeholder executed." };
        }

        public async Task<object> ZReportAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureConnected();
            await _logService.InfoAsync("[Beko] Z report placeholder.", cancellationToken);
            return new { success = true, device = DeviceType, message = "Beko Z report placeholder executed." };
        }

        public async Task<object> CashierLoginAsync(CashierLoginRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureConnected();
            await _logService.InfoAsync("[Beko] Cashier login placeholder for user " + request.Username + ".", cancellationToken);
            return new { success = true, device = DeviceType, message = "Beko cashier login placeholder executed." };
        }

        public async Task<object> CashierLogoutAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureConnected();
            await _logService.InfoAsync("[Beko] Cashier logout placeholder.", cancellationToken);
            return new { success = true, device = DeviceType, message = "Beko cashier logout placeholder executed." };
        }

        public object GetStatus()
        {
            return new { connected = _isConnected, device = DeviceType, communication = _config.CommType };
        }

        private void EnsureConnected()
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("Selected device is not connected. Call /api/connect first.");
            }
        }
    }
}
