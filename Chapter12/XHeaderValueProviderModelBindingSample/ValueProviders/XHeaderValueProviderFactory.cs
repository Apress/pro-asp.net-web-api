using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace XHeaderValueProviderModelBindingSample.ValueProviders {
    public class XHeaderValueProviderFactory : ValueProviderFactory {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext) {
            return new XHeaderValueProvider(actionContext);
        }
    }
}