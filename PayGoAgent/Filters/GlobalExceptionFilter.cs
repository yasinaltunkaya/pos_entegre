using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using PayGoAgent.Logging;
using PayGoAgent.Models;

namespace PayGoAgent.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly FileLogger _logger;

        public GlobalExceptionFilter(FileLogger logger)
        {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.Error("Unhandled exception occurred.", actionExecutedContext.Exception);

            var response = ApiResponse<object>.Fail(
                "An unexpected error occurred.",
                "UNEXPECTED_ERROR");

            actionExecutedContext.Response = actionExecutedContext.Request
                .CreateResponse(HttpStatusCode.InternalServerError, response);
        }
    }
}
