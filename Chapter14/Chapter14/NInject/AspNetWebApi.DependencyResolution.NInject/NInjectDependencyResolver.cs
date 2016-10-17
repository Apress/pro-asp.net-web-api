using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace AspNetWebApi.DependencyResolution.NInject
{
    public class NInjectDependencyResolver : NInjectDependencyScope, IDependencyResolver
    {
        private IKernel _kernel;

        public NInjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NInjectDependencyScope(_kernel.BeginBlock());
        }
    }
}