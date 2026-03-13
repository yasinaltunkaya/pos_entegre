using PosTerminal.Models;

namespace PosTerminal.Services;

/// <summary>
/// Selects the concrete fiscal device service based on config.ini DeviceName.
/// </summary>
public sealed class FiscalDeviceManager
{
    private readonly IFiscalDeviceService _selected;

    public FiscalDeviceManager(AppConfig config, PaygoService paygoService, BekoService bekoService)
    {
        _selected = config.DeviceName.Trim().ToUpperInvariant() switch
        {
            "PAYGO" => paygoService,
            "BEKO" => bekoService,
            _ => throw new InvalidOperationException($"Unsupported DEVICENAME: '{config.DeviceName}'. Supported values: PayGo, Beko")
        };
    }

    public IFiscalDeviceService Current => _selected;
}
