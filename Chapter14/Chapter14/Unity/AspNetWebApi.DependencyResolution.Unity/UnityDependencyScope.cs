using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;

namespace AspNetWebApi.DependencyResolution.Unity
{
    public class UnityDependencyScope : IDependencyScope
    {
        protected readonly IUnityContainer _container;

        public UnityDependencyScope(IUnityContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ?
                _container.Resolve(serviceType) :
                null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ?
                _container.ResolveAll(serviceType) :
                new object[0];
        }
    }
}