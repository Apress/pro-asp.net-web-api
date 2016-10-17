using System.Web.Http;
using Autofac;
using Newtonsoft.Json.Serialization;
using PizzaApi.Api.Controllers;
using PizzaApi.Domain;
using WebApiContrib.IoC.Autofac;

namespace PizzaApi.Api.App_Start
{

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute(
                name: "OrderItems",
                routeTemplate: "api/Order/{id}/{controller}/{name}",
                defaults: new { name = RouteParameter.Optional }
                );

            var builder = new ContainerBuilder();
            builder.RegisterType<InMemoryOrderService>()
                   .As<IOrderService>()
                   .SingleInstance();
            builder.RegisterType<OrderController>()
                .As<OrderController>();
            builder.RegisterType<OrderItemController>()
             .As<OrderItemController>();
            builder.RegisterType<SimplePricingService>()
                   .As<IPricingService>();

            config.DependencyResolver = new AutofacResolver(builder.Build());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }
    }
}