using System.Web.Http;
using System.Web.Http.Description;
using WebApiDocumentation.ApiExplorerExtensions;

namespace WebApiDocumentation.App_Start {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Services.Replace(typeof(IDocumentationProvider), new AttributeDocumentationProvider());
        }
    }
}