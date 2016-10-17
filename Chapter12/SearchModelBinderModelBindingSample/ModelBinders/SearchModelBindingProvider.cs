using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace SearchModelBinderModelBindingSample.ModelBinders {
    public class SearchModelBinderProvider : ModelBinderProvider {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType) {
            return new SearchModelBinder();
        }
    }
}