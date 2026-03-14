using System.Web.Http;
using PosterMinalV2.App_Start;

namespace PosterMinalV2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SwaggerConfig.Register();
        }
    }
}
