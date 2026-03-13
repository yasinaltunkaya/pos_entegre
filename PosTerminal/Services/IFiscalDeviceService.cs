using PosTerminal.Models;

namespace PosTerminal.Services;

public interface IFiscalDeviceService
{
    string DeviceType { get; }
    Task<object> ConnectAsync(CancellationToken cancellationToken = default);
    Task<object> SaleAsync(SaleRequest request, CancellationToken cancellationToken = default);
    Task<object> XReportAsync(CancellationToken cancellationToken = default);
    Task<object> ZReportAsync(CancellationToken cancellationToken = default);
    Task<object> CashierLoginAsync(CashierLoginRequest request, CancellationToken cancellationToken = default);
    Task<object> CashierLogoutAsync(CancellationToken cancellationToken = default);
    object GetStatus();
}
