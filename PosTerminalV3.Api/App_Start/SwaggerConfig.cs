using System.Web.Http;
using WebActivatorEx;
using PosTerminalV3.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PosTerminalV3.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "PosTerminalV3 API");
                })
                .EnableSwaggerUi(c =>
                {
                });
        }
    }
}
