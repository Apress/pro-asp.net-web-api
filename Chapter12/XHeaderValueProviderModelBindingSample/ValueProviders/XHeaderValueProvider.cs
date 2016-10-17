using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace XHeaderValueProviderModelBindingSample.ValueProviders {
    public class XHeaderValueProvider : IValueProvider {
        private readonly HttpRequestHeaders _headers;
        private const string XHeaderPrefix = "X-";

        public XHeaderValueProvider(HttpActionContext actionContext) {
            _headers = actionContext.ControllerContext.Request.Headers;
        }

        public bool ContainsPrefix(string prefix) {
            return _headers.Any(header => header.Key.Contains(XHeaderPrefix + prefix));
        }

        public ValueProviderResult GetValue(string key) {
            IEnumerable<string> values;

            return _headers.TryGetValues(XHeaderPrefix + key, out values)
                       ? new ValueProviderResult(
                             values.First(),
                             values.First(),
                             CultureInfo.CurrentCulture)
                       : null;
        }
    }
}