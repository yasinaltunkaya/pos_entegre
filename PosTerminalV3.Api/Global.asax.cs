using System.Web.Http;

namespace PosTerminalV3.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(App_Start.WebApiConfig.Register);
            App_Start.SwaggerConfig.Register();
        }
    }
}
