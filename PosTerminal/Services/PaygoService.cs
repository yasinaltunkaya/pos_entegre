using Microsoft.AspNetCore.Mvc.RazorPages;
using PCPOSOKC;
using PosTerminal.Models;
using static PAYGO_OKC.PAYGO_OKC;

namespace PosTerminal.Services;

/// <summary>
/// Placeholder implementation for PayGo integration.
/// Replace placeholders with real SDK/protocol calls.
/// </summary>
public sealed class PaygoService : IFiscalDeviceService
{
    private readonly AppConfig _config;
    private readonly LogService _logService;
    private bool _isConnected;
    PAYGO_OKC.PAYGO_OKC paygo = new PAYGO_OKC.PAYGO_OKC(false);

    public PaygoService(AppConfig config, LogService logService)
    {
        _config = config;
        _logService = logService;
    }

    public string DeviceType => "PayGo";

    public async Task<object> ConnectAsync(CancellationToken cancellationToken = default)
    {

        PCPOSOKC.DLL.SetCommunicateConfig(COMMTYPE.RS232, _config.SerialPort, "", 444);



        EchoRspParams echoRspPrms = new EchoRspParams();
        paygo.paygo_OpenSerialPortAndGMP3Echo_ToDevice(_config.SerialPort, _config.SerialPort, _config.SerialPort, ref echoRspPrms);
        //DisplayCommandName(rtbDisplay, (Tags)paygo.TranPrms.rspMem.ResponsedTag);
        //Display_GMP3Echo_Members(rtbDisplay, echoRspPrms);
        //btnOpen.Enabled = true;

        string sonuc = paygo.ReturnStatus;
        if (sonuc == "1100")
        {
            //SaveSettingsToIni();
            //cashierlogin();
        }
        if (sonuc == "1101")
        {
           throw new InvalidOperationException("Key Bulunamadý Pair Yapýnýz .");
        }else
        {
            throw new InvalidOperationException($"PayGo connection failed with status: {sonuc}");
        }




            _isConnected = true;
        await _logService.InfoAsync($"[PayGo] Connect placeholder executed via {_config.CommType}.", cancellationToken);
        return new { success = true, device = DeviceType, message = "PayGo connect placeholder executed." };
    }

    public async Task<object> SaleAsync(SaleRequest request, CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        await _logService.InfoAsync($"[PayGo] Sale placeholder. Receipt={request.ReceiptNumber}, Amount={request.Amount}", cancellationToken);
        return new { success = true, device = DeviceType, message = "PayGo sale placeholder executed." };
    }

    public async Task<object> XReportAsync(CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        await _logService.InfoAsync("[PayGo] X report placeholder.", cancellationToken);
        return new { success = true, device = DeviceType, message = "PayGo X report placeholder executed." };
    }

    public async Task<object> ZReportAsync(CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        await _logService.InfoAsync("[PayGo] Z report placeholder.", cancellationToken);
        return new { success = true, device = DeviceType, message = "PayGo Z report placeholder executed." };
    }

    public async Task<object> CashierLoginAsync(CashierLoginRequest request, CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        await _logService.InfoAsync($"[PayGo] Cashier login placeholder for user {request.Username}.", cancellationToken);
        return new { success = true, device = DeviceType, message = "PayGo cashier login placeholder executed." };
    }

    public async Task<object> CashierLogoutAsync(CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        await _logService.InfoAsync("[PayGo] Cashier logout placeholder.", cancellationToken);
        return new { success = true, device = DeviceType, message = "PayGo cashier logout placeholder executed." };
    }

    public object GetStatus() => new { connected = _isConnected, device = DeviceType, communication = _config.CommType };

    private void EnsureConnected()
    {
        if (!_isConnected)
        {
            throw new InvalidOperationException("Selected device is not connected. Call /api/connect first.");
        }
    }
}
