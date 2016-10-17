using System.Web.Http;
using System.Web.Routing;
using WebApiTracing.App_Start;

namespace WebApiTracing {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}