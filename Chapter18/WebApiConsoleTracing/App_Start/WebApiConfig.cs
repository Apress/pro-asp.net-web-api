using System.Web.Http;

namespace WebApiConsoleTracing.App_Start {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            RouteConfig.Register(config);
            TraceConfig.Register(config);
        }
    }
}