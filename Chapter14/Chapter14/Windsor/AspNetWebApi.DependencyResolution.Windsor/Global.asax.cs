using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;

namespace AspNetWebApi.DependencyResolution.Windsor
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


            var container = new WindsorContainer();
            container.Register(
                Component.For<ITaxCalculator>()
                .ImplementedBy<TaxCalculator>()
                .LifeStyle.Transient,
                Component.For<ValuesController>()
                .ImplementedBy<ValuesController>()
                .LifeStyle.Transient);

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);


            // in order to see performance counters, uncomment lines below
            // var counter = LifecycledComponentsReleasePolicy.GetTrackedComponentsPerformanceCounter(new PerformanceMetricsFactory());
            //var diagnostic = LifecycledComponentsReleasePolicy.GetTrackedComponentsDiagnostic(container.Kernel);
            // container.Kernel.ReleasePolicy = new LifecycledComponentsReleasePolicy(diagnostic, counter);

        }

    }
}