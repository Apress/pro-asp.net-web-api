using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;

namespace AspNetWebApi.DependencyResolution.AutoFac
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

            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterType<TaxCalculator>()
                .As<ITaxCalculator>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType<ValuesController>()
                .As<ValuesController>();

            GlobalConfiguration.Configuration.DependencyResolver =
                new AutoFacDependencyResolver(containerBuilder.Build());

        }

    }
}