using System;
using System.Web.Http;
using PosTerminalV3.Api.Models;
using PosTerminalV3.Core.Infrastructure;
using PosTerminalV3.Core.Services;

namespace PosTerminalV3.Api.Controllers
{
    [RoutePrefix("api/pos")]
    public class PosController : ApiController
    {
        private readonly PaygoService _paygoService = new PaygoService();
        private readonly BekoService _bekoService = new BekoService();

        [HttpPost]
        [Route("test")]
        public ApiResponse Test()
        {
            return Execute("Test", service => service.TestConnection());
        }

        [HttpPost]
        [Route("sale")]
        public ApiResponse Sale([FromBody] decimal amount)
        {
            return Execute("Sale", service => service.Sale(amount));
        }

        [HttpPost]
        [Route("x-report")]
        public ApiResponse XReport()
        {
            return Execute("XReport", service => service.XReport());
        }

        [HttpPost]
        [Route("z-report")]
        public ApiResponse ZReport()
        {
            return Execute("ZReport", service => service.ZReport());
        }

        private ApiResponse Execute(string method, Func<IPostDeviceService, string> action)
        {
            try
            {
                var service = ResolveService();
                var message = action(service);
                return new ApiResponse { Success = true, Message = message };
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(PosController), method, ex.Message);
                return new ApiResponse { Success = false, Message = ex.Message };
            }
        }

        private IPostDeviceService ResolveService()
        {
            var activeDevice = ConfigReader.Get("ActiveDevice", "Paygo");

            if (string.Equals(activeDevice, "Paygo", StringComparison.OrdinalIgnoreCase))
            {
                return _paygoService;
            }

            if (string.Equals(activeDevice, "Beko", StringComparison.OrdinalIgnoreCase))
            {
                return _bekoService;
            }

            throw new InvalidOperationException("Unsupported ActiveDevice value.");
        }
    }
}
