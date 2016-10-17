using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;

namespace AspNetWebApi.DependencyResolution.Unity
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
              );

            var container = new UnityContainer();

            container
                .RegisterType<ITaxCalculator, TaxCalculator>();

            container
                .RegisterType<ValuesController>();

            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }
    }
}