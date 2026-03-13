using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Hosting;
using PosTerminal.Config;
using PosTerminal.Services;

namespace PosTerminal
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Architecture (NET Framework 4.7.2):
            // - Console host starts OWIN pipeline on http://0.0.0.0:5050.
            // - Runtime config is loaded from config.ini.
            // - FiscalDeviceManager selects PayGo/Beko integration via DEVICENAME.
            // - Web API controller layer exposes REST + Swagger UI.

            var configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
            var appConfig = ConfigReader.Load(configPath);

            var services = new ServiceCollection();
            services.AddSingleton(appConfig);
            services.AddSingleton(new LogService(AppDomain.CurrentDomain.BaseDirectory));
            services.AddSingleton<PaygoService>();
            services.AddSingleton<BekoService>();
            services.AddSingleton<FiscalDeviceManager>();

            var provider = services.BuildServiceProvider();
            RuntimeContext.Initialize(provider.GetRequiredService<FiscalDeviceManager>(), provider.GetRequiredService<LogService>(), appConfig);

            const string baseAddress = "http://0.0.0.0:5050";
            using (WebApp.Start<Startup>(baseAddress))
            {
                RuntimeContext.LogService.InfoAsync("PosTerminal (.NET Framework 4.7.2) started.").GetAwaiter().GetResult();
                Console.WriteLine("PosTerminal running at " + baseAddress);
                Console.WriteLine("Swagger UI: " + baseAddress + "/swagger");
                Console.WriteLine("Press ENTER to stop...");
                Console.ReadLine();
            }
        }
    }
}

