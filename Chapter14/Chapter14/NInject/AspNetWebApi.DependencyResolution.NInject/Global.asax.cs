using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Ninject;

namespace AspNetWebApi.DependencyResolution.NInject
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


            var kernel = new StandardKernel();
            kernel.Bind<ITaxCalculator>().To<TaxCalculator>();
            kernel.Bind<ValuesController>().To<ValuesController>();

            GlobalConfiguration.Configuration.DependencyResolver = 
                new NInjectDependencyResolver(kernel);
        }

    }
}