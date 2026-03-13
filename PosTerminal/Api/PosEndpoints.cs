using System;
using System.Threading.Tasks;
using System.Web.Http;
using PosTerminal.Models;

namespace PosTerminal.Api
{
    /// <summary>
    /// Web API controller exposing ERP-facing endpoints.
    /// Requests are delegated to the selected device via FiscalDeviceManager.
    /// </summary>
    [RoutePrefix("api")]
    public class PosController : ApiController
    {
        [HttpPost]
        [Route("connect")]
        public async Task<IHttpActionResult> Connect()
        {
            return Ok(await RuntimeContext.Manager.Current.ConnectAsync());
        }

        [HttpPost]
        [Route("sale")]
        public async Task<IHttpActionResult> Sale([FromBody] SaleRequest request)
        {
            return Ok(await RuntimeContext.Manager.Current.SaleAsync(request));
        }

        [HttpPost]
        [Route("xreport")]
        public async Task<IHttpActionResult> XReport()
        {
            return Ok(await RuntimeContext.Manager.Current.XReportAsync());
        }

        [HttpPost]
        [Route("zreport")]
        public async Task<IHttpActionResult> ZReport()
        {
            return Ok(await RuntimeContext.Manager.Current.ZReportAsync());
        }

        [HttpPost]
        [Route("cashier/login")]
        public async Task<IHttpActionResult> CashierLogin([FromBody] CashierLoginRequest request)
        {
            return Ok(await RuntimeContext.Manager.Current.CashierLoginAsync(request));
        }

        [HttpPost]
        [Route("cashier/logout")]
        public async Task<IHttpActionResult> CashierLogout()
        {
            return Ok(await RuntimeContext.Manager.Current.CashierLogoutAsync());
        }

        [HttpGet]
        [Route("status")]
        public IHttpActionResult Status()
        {
            return Ok(RuntimeContext.Manager.Current.GetStatus());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
