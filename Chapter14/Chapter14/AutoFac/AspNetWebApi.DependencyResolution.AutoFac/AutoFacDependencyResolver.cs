using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Autofac;

namespace AspNetWebApi.DependencyResolution.AutoFac
{
    public class AutoFacDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public AutoFacDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            object instance = null;
            _container.TryResolve(serviceType, out instance);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            object instance = null;
            var ienumerableType = typeof (IEnumerable<>).MakeGenericType(serviceType);
            _container.TryResolve(ienumerableType, out instance);
            return (IEnumerable<object>) instance;
        }

        public IDependencyScope BeginScope()
        {
            return new AutoFacDependencyScope(_container.BeginLifetimeScope());
        }
    }
}