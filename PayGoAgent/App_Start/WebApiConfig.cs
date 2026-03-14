using System.Web.Http;
using System.Web.Http.Cors;
using PayGoAgent.Filters;
using PayGoAgent.Services;

namespace PayGoAgent.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var payGoService = new PayGoService();
            var corsOrigins = payGoService.Configuration.AllowedOrigins;

            config.EnableCors(new EnableCorsAttribute(corsOrigins, "*", "*"));
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new GlobalExceptionFilter(payGoService.Logger));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
