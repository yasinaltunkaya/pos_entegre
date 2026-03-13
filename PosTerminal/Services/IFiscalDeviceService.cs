using System.Threading;
using System.Threading.Tasks;
using PosTerminal.Models;

namespace PosTerminal.Services
{
    public interface IFiscalDeviceService
    {
        string DeviceType { get; }
        Task<object> ConnectAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<object> SaleAsync(SaleRequest request, CancellationToken cancellationToken = default(CancellationToken));
        Task<object> XReportAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<object> ZReportAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<object> CashierLoginAsync(CashierLoginRequest request, CancellationToken cancellationToken = default(CancellationToken));
        Task<object> CashierLogoutAsync(CancellationToken cancellationToken = default(CancellationToken));
        object GetStatus();
    }
}
