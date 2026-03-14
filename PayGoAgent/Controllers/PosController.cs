using System.Web.Http;
using PayGoAgent.Models;
using PayGoAgent.Services;

namespace PayGoAgent.Controllers
{
    [RoutePrefix("api/pos")]
    public class PosController : ApiController
    {
        private readonly IPayGoService _payGoService;

        public PosController()
            : this(new PayGoService())
        {
        }

        public PosController(IPayGoService payGoService)
        {
            _payGoService = payGoService;
        }

        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetStatus()
        {
            return Ok(_payGoService.GetStatus());
        }

        [HttpPost]
        [Route("connect")]
        public IHttpActionResult Connect()
        {
            var response = _payGoService.Connect();
            return response.Success ? (IHttpActionResult)Ok(response) : Content(System.Net.HttpStatusCode.BadRequest, response);
        }

        [HttpPost]
        [Route("cashier-login")]
        public IHttpActionResult CashierLogin([FromBody] CashierLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Content(System.Net.HttpStatusCode.BadRequest,
                    ApiResponse<object>.Fail("Invalid cashier login request.", "VALIDATION_ERROR", ModelState));
            }

            var response = _payGoService.CashierLogin(request.Username, request.Password);
            return response.Success ? (IHttpActionResult)Ok(response) : Content(System.Net.HttpStatusCode.BadRequest, response);
        }

        [HttpPost]
        [Route("cashier-logout")]
        public IHttpActionResult CashierLogout()
        {
            var response = _payGoService.CashierLogout();
            return response.Success ? (IHttpActionResult)Ok(response) : Content(System.Net.HttpStatusCode.BadRequest, response);
        }

        [HttpPost]
        [Route("sale")]
        public IHttpActionResult Sale([FromBody] SaleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Content(System.Net.HttpStatusCode.BadRequest,
                    ApiResponse<object>.Fail("Invalid sale request.", "VALIDATION_ERROR", ModelState));
            }

            var response = _payGoService.Sale(request.Amount);
            return response.Success ? (IHttpActionResult)Ok(response) : Content(System.Net.HttpStatusCode.BadRequest, response);
        }

        [HttpPost]
        [Route("refund")]
        public IHttpActionResult Refund([FromBody] SaleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Content(System.Net.HttpStatusCode.BadRequest,
                    ApiResponse<object>.Fail("Invalid refund request.", "VALIDATION_ERROR", ModelState));
            }

            var response = _payGoService.Refund(request.Amount);
            return response.Success ? (IHttpActionResult)Ok(response) : Content(System.Net.HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("report/x")]
        public IHttpActionResult GetXReport()
        {
            return Ok(_payGoService.GetXReport());
        }

        [HttpGet]
        [Route("report/z")]
        public IHttpActionResult GetZReport()
        {
            return Ok(_payGoService.GetZReport());
        }
    }
}
