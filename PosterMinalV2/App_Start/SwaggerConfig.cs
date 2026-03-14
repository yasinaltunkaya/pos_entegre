using System.Web.Http;
using Swashbuckle.Application;

namespace PosterMinalV2.App_Start
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "PosterMinalV2 API");
                })
                .EnableSwaggerUi(c =>
                {
                });
        }
    }
}
