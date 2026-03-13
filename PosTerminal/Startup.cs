using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using Swashbuckle.Application;

namespace PosTerminal
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            var httpConfig = new HttpConfiguration();
            httpConfig.MapHttpAttributeRoutes();
            httpConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            httpConfig
                .EnableSwagger(c => c.SingleApiVersion("v1", "PosTerminal API"))
                .EnableSwaggerUi();

            app.UseWebApi(httpConfig);
        }
    }
}
