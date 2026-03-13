using System;
using PosTerminal.Models;

namespace PosTerminal.Services
{
    /// <summary>
    /// Selects the concrete fiscal device service based on config.ini DeviceName.
    /// </summary>
    public sealed class FiscalDeviceManager
    {
        private readonly IFiscalDeviceService _selected;

        public FiscalDeviceManager(AppConfig config, PaygoService paygoService, BekoService bekoService)
        {
            var device = (config.DeviceName ?? string.Empty).Trim().ToUpperInvariant();
            switch (device)
            {
                case "PAYGO":
                    _selected = paygoService;
                    break;
                case "BEKO":
                    _selected = bekoService;
                    break;
                default:
                    throw new InvalidOperationException("Unsupported DEVICENAME: '" + config.DeviceName + "'. Supported values: PayGo, Beko");
            }
        }

        public IFiscalDeviceService Current { get { return _selected; } }
    }

}
