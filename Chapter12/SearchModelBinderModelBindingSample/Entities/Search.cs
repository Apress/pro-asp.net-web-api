using System.Web.Http.ModelBinding;
using SearchModelBinderModelBindingSample.ModelBinders;

namespace SearchModelBinderModelBindingSample.Entities {
    [ModelBinder(typeof (SearchModelBinderProvider))]
    public class Search {
        public string Text { get; set; }
        public int MaxResults { get; set; }
    }
}