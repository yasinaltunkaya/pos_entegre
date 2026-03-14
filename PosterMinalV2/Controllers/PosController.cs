using System;
using System.Diagnostics;
using System.Web.Http;
using PosterMinalV2.Infrastructure;
using PosterMinalV2.Models;
using PosterMinalV2.Services;

namespace PosterMinalV2.Controllers
{
    [RoutePrefix("api/pos")]
    public class PosController : ApiController
    {
        private readonly PaygoService _paygoService;
        private readonly BekoService _bekoService;

        public PosController()
        {
            _paygoService = new PaygoService();
            _bekoService = new BekoService();
        }

        [HttpPost]
        [Route("test")]
        public ApiResponse Test()
        {
            return Execute("Test", delegate { return RouteByDevice(delegate { return _paygoService.TestConnection(); }, delegate { return _bekoService.TestConnection(); }); });
        }

        [HttpPost]
        [Route("pair")]
        public ApiResponse Pair(PairRequest request)
        {
            return Execute("Pair", delegate { return RouteByDevice(delegate { return _paygoService.Pair(request); }, delegate { return _bekoService.Pair(request); }); });
        }

        [HttpPost]
        [Route("cashier-login")]
        public ApiResponse CashierLogin(CashierLoginRequest request)
        {
            return Execute("CashierLogin", delegate { return RouteByDevice(delegate { return _paygoService.CashierLogin(request); }, delegate { return _bekoService.CashierLogin(request); }); });
        }

        [HttpPost]
        [Route("cashier-logout")]
        public ApiResponse CashierLogout()
        {
            return Execute("CashierLogout", delegate { return RouteByDevice(delegate { return _paygoService.CashierLogout(); }, delegate { return _bekoService.CashierLogout(); }); });
        }

        [HttpPost]
        [Route("receipt")]
        public ApiResponse Receipt(ReceiptRequest request)
        {
            return Execute("Receipt", delegate { return RouteByDevice(delegate { return _paygoService.PrintReceipt(request); }, delegate { return _bekoService.PrintReceipt(request); }); });
        }

        [HttpPost]
        [Route("x-report")]
        public ApiResponse XReport()
        {
            return Execute("XReport", delegate { return RouteByDevice(delegate { return _paygoService.PrintXReport(); }, delegate { return _bekoService.PrintXReport(); }); });
        }

        [HttpPost]
        [Route("z-report")]
        public ApiResponse ZReport()
        {
            return Execute("ZReport", delegate { return RouteByDevice(delegate { return _paygoService.PrintZReport(); }, delegate { return _bekoService.PrintZReport(); }); });
        }

        private ApiResponse Execute(string methodName, Func<ApiResponse> action)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var response = action();
                stopwatch.Stop();
                response.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                Logger.Info("PosController", methodName, response.Message ?? "İşlem başarılı.");
                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.Error("PosController", methodName, "İşlem sırasında hata oluştu.", ex);
                return new ApiResponse
                {
                    Success = false,
                    Message = "İşlem başarısız.",
                    ErrorCode = "UNHANDLED_EXCEPTION",
                    ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
                };
            }
        }

        private ApiResponse RouteByDevice(Func<ApiResponse> paygoAction, Func<ApiResponse> bekoAction)
        {
            var device = ConfigReader.GetActiveDevice();
            if (string.Equals(device, "Paygo", StringComparison.OrdinalIgnoreCase))
            {
                return paygoAction();
            }

            if (string.Equals(device, "Beko", StringComparison.OrdinalIgnoreCase))
            {
                return bekoAction();
            }

            return new ApiResponse
            {
                Success = false,
                Message = "Desteklenmeyen ActiveDevice değeri.",
                ErrorCode = "INVALID_DEVICE"
            };
        }
    }
}
