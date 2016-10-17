using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace SelfHostingSamples
{
    public class NtlmHttpSelfHostConfiguration : HttpSelfHostConfiguration
    {
        public NtlmHttpSelfHostConfiguration(string baseAddress)
            : base(baseAddress)
        {
            this.ClientCredentialType = HttpClientCredentialType.Ntlm;
        }

        public NtlmHttpSelfHostConfiguration(Uri baseAddress)
            : base(baseAddress)
        {
            this.ClientCredentialType = HttpClientCredentialType.Ntlm;
        }

        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.TransportCredentialOnly;
            return base.OnConfigureBinding(httpBinding);
        }
    }

}
