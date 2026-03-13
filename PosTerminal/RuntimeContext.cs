using PosTerminal.Models;
using PosTerminal.Services;

namespace PosTerminal
{
    public static class RuntimeContext
    {
        public static FiscalDeviceManager Manager { get; private set; }
        public static LogService LogService { get; private set; }
        public static AppConfig Config { get; private set; }

        public static void Initialize(FiscalDeviceManager manager, LogService logService, AppConfig config)
        {
            Manager = manager;
            LogService = logService;
            Config = config;
        }
    }
}
