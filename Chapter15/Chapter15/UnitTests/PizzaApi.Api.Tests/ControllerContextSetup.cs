using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace PizzaApi.Api.Tests
{
    public class ControllerContextSetup
    {

        private const string DefaultApiName = "DefaultApi";
        private readonly ApiController _controller;
        private HttpRouteData _httpRouteData;

        private ControllerContextSetup(ApiController controller)
        {
            _controller = controller;
            _controller.Request = new HttpRequestMessage();
        }

        public static ControllerContextSetup Of<T>(Func<T> factory)
            where T : ApiController
        {
            return new ControllerContextSetup(factory());
        }

        public static ControllerContextSetup Of<T>()
            where T : ApiController, new()
        {
            return new ControllerContextSetup(new T());
        }

        public ControllerContextSetup WithDefaultConfig()
        {
            _controller.Configuration = new HttpConfiguration();
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = _controller.Configuration;
            return this;
        }

        public ControllerContextSetup WithConfig(HttpConfiguration configuration)
        {
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
            _controller.Configuration = configuration;
            return this;
        }

        public ControllerContextSetup Requesting(string uriString)
        {
            Uri uri = null;
            bool success = Uri.TryCreate(uriString, UriKind.Relative, out uri);
            if (success)
                return Requesting(uri);

            success = Uri.TryCreate(uriString, UriKind.Absolute, out uri);
            if(success)
                return Requesting(uri);

            return Requesting(new Uri(uriString));

        }

        public ControllerContextSetup Requesting(Uri uri)
        {
            _controller.Request.RequestUri = uri;
            return this;
        }

        public ControllerContextSetup WithDefaultRoute()
        {
            _controller.Configuration.Routes.MapHttpRoute(
                name: DefaultApiName,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            return this;
        } 

        public ControllerContextSetup WithRoute(string name, string routeTemplate)
        {
            _controller.Configuration.Routes.MapHttpRoute(name, routeTemplate);
            return this;
        }

        public ControllerContextSetup WithRoute(string name, string routeTemplate, object defaults)
        {
            _controller.Configuration.Routes.MapHttpRoute(name, routeTemplate, defaults);
            return this;
        }

        public ControllerContextSetup WithRoute(string name, string routeTemplate, object defaults, object constraints)
        {
            _controller.Configuration.Routes.MapHttpRoute(name, routeTemplate, defaults, constraints);
            return this;
        }

        public ControllerContextSetup WithRoute(string name, IHttpRoute route)
        {
            _controller.Configuration.Routes.Add(name, route);
            return this;
        }

        /// <summary>
        /// Uses default route
        /// </summary>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public ControllerContextSetup WithRouteData(object routeValues)
        {
            return WithRouteData(new HttpRouteValueDictionary(routeValues));
        }

        /// <summary>
        /// Uses default route
        /// </summary>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public ControllerContextSetup WithRouteData(HttpRouteValueDictionary routeValues)
        {
            var route = _controller.Configuration.Routes[DefaultApiName];
            _httpRouteData = new HttpRouteData(route, routeValues); 
            _controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = _httpRouteData;
            return this;
        }

        public ControllerContextSetup WithMethod(HttpMethod method)
        {
            _controller.Request.Method = method;
            return this;
        } 

        public ApiController Build()
        {
            _controller.ControllerContext =
                new HttpControllerContext(_controller.Configuration, 
                    _httpRouteData ?? new HttpRouteData(_controller.Configuration.Routes.FirstOrDefault()) , 
                    _controller.Request);
            return _controller;
        }


        public T Build<T>()
            where T : ApiController
        {
            return (T) Build();
        }
    }
}
