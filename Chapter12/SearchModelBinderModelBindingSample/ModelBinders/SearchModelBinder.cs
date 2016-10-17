using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using SearchModelBinderModelBindingSample.Entities;

namespace SearchModelBinderModelBindingSample.ModelBinders {
    public class SearchModelBinder : IModelBinder {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext) {
            var model = (Search) bindingContext.Model ?? new Search();

            model.Text =
                bindingContext.ValueProvider.GetValue("Text").AttemptedValue;

            var maxResults = 0;
            if (int.TryParse(
                bindingContext
                    .ValueProvider
                    .GetValue("MaxResults").AttemptedValue, out maxResults)) {
                model.MaxResults = maxResults;
            }

            bindingContext.Model = model;

            return true;
        }
    }
}