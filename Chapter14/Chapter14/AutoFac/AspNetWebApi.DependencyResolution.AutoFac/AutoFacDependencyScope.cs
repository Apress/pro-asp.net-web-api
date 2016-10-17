using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Autofac;

namespace AspNetWebApi.DependencyResolution.AutoFac
{
    public class AutoFacDependencyScope : IDependencyScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutoFacDependencyScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            object instance = null;
            _lifetimeScope.TryResolve(serviceType, out instance);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            object instance = null;
            var ienumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            _lifetimeScope.TryResolve(ienumerableType, out instance);
            return (IEnumerable<object>)instance;
        }
    }
}