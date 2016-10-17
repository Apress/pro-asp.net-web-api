using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace AspNetWebApi.DependencyResolution.Unity
{
    public class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver
    {
        public UnityDependencyResolver(IUnityContainer container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new UnityDependencyScope(_container.CreateChildContainer());
        }
    }
}