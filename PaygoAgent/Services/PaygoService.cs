using PaygoAgent.Models;

namespace PaygoAgent.Services;

/// <summary>
/// Abstraction over PayGo communication channels (USB serial or Ethernet).
/// Methods are placeholders until real SDK/device protocol implementation is added.
/// </summary>
public sealed class PaygoService
{
    private readonly AppConfig _config;
    private readonly LogService _logService;

    private bool _isConnected;
    private string _connectionDescription = "Disconnected";

    public PaygoService(AppConfig config, LogService logService)
    {
        _config = config;
        _logService = logService;
    }

    public async Task<object> ConnectAsync(CancellationToken cancellationToken = default)
    {
        // Placeholder connection logic. Real implementation should open SerialPort or TCP socket.
        _isConnected = true;
        _connectionDescription = _config.CommType.Equals("USB", StringComparison.OrdinalIgnoreCase)
            ? $"USB({_config.SerialPort})"
            : $"ETHERNET({_config.ServerAddr}:{_config.ServerPort})";

        await _logService.InfoAsync($"Connected to PayGo via {_connectionDescription}.", cancellationToken);
        return new { success = true, channel = _connectionDescription };
    }

    public async Task<object> SaleAsync(SaleRequest request, CancellationToken cancellationToken = default)
    {
        EnsureConnected();

        // Placeholder sale operation.
        await _logService.InfoAsync(
            $"Sale requested. Amount={request.Amount}, Currency={request.Currency}, Receipt={request.ReceiptNumber}",
            cancellationToken);

        return new { success = true, message = "Sale placeholder executed.", request.ReceiptNumber };
    }

    public async Task<object> XReportAsync(CancellationToken cancellationToken = default)
    {
        EnsureConnected();

        // Placeholder X report operation.
        await _logService.InfoAsync("X report requested.", cancellationToken);
        return new { success = true, message = "X report placeholder executed." };
    }

    public async Task<object> ZReportAsync(CancellationToken cancellationToken = default)
    {
        EnsureConnected();

        // Placeholder Z report operation.
        await _logService.InfoAsync("Z report requested.", cancellationToken);
        return new { success = true, message = "Z report placeholder executed." };
    }

    public async Task<object> CashierLoginAsync(CashierLoginRequest request, CancellationToken cancellationToken = default)
    {
        EnsureConnected();

        // Placeholder cashier login operation.
        await _logService.InfoAsync($"Cashier login requested for user '{request.Username}'.", cancellationToken);
        return new { success = true, message = "Cashier login placeholder executed." };
    }

    public async Task<object> CashierLogoutAsync(CancellationToken cancellationToken = default)
    {
        EnsureConnected();

        // Placeholder cashier logout operation.
        await _logService.InfoAsync("Cashier logout requested.", cancellationToken);
        return new { success = true, message = "Cashier logout placeholder executed." };
    }

    public object GetStatus() => new
    {
        connected = _isConnected,
        connection = _connectionDescription,
        communicationType = _config.CommType
    };

    private void EnsureConnected()
    {
        if (!_isConnected)
        {
            throw new InvalidOperationException("PayGo device is not connected. Call /api/connect first.");
        }
    }
}
