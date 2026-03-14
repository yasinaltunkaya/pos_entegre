using PayGoAgent.Models;

namespace PayGoAgent.Services
{
    public interface IPayGoService
    {
        ApiResponse<DeviceStatusResponse> GetStatus();
        ApiResponse<object> Connect();
        ApiResponse<object> CashierLogin(string username, string password);
        ApiResponse<object> CashierLogout();
        ApiResponse<object> Sale(decimal amount);
        ApiResponse<object> Refund(decimal amount);
        ApiResponse<ReportResponse> GetXReport();
        ApiResponse<ReportResponse> GetZReport();
    }
}
