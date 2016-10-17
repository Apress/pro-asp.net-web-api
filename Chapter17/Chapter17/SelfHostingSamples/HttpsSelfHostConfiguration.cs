using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace SelfHostingSamples
{
    public class HttpsSelfHostConfiguration : HttpSelfHostConfiguration
    {
        public HttpsSelfHostConfiguration(string baseAddress)
            : base(baseAddress)
        {
        }

        public HttpsSelfHostConfiguration(Uri baseAddress)
            : base(baseAddress)
        {
        }

        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;
            return base.OnConfigureBinding(httpBinding);
        }
    }

}
