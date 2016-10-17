using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace SelfHostingSamples
{
    public class BasicAuthenticationSelfHostConfiguration : HttpSelfHostConfiguration
    {
        private readonly FunctionalUserNamePasswordValidator _functionalUserNamePasswordValidator;

        private class FunctionalUserNamePasswordValidator : UserNamePasswordValidator
        {
            private readonly Func<string, string, bool> _userNamePasswordValidator;

            public FunctionalUserNamePasswordValidator(Func<string, string, bool> userNamePasswordValidator)
            {
                if (userNamePasswordValidator == null)
                    throw new ArgumentNullException("userNamePasswordValidator");

                _userNamePasswordValidator = userNamePasswordValidator;
            }

            public override void Validate(string userName, string password)
            {
                if (!_userNamePasswordValidator(userName, password))
                    throw new SecurityException("Invalid username/password pair.");
            }
        }


        public BasicAuthenticationSelfHostConfiguration(string baseAddress, Func<string, string, bool> userNamePasswordValidator)
            : base(baseAddress)
        {
            _functionalUserNamePasswordValidator = new FunctionalUserNamePasswordValidator(userNamePasswordValidator);
        }

        public BasicAuthenticationSelfHostConfiguration(Uri baseAddress, Func<string, string, bool> userNamePasswordValidator)
            : base(baseAddress)
        {
            _functionalUserNamePasswordValidator = new FunctionalUserNamePasswordValidator(userNamePasswordValidator);
        }



        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.TransportCredentialOnly;
            httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            this.UserNamePasswordValidator = _functionalUserNamePasswordValidator;
            return base.OnConfigureBinding(httpBinding);
        }
    }

}
