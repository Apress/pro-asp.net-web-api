using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SearchQueryStringModelBindingSample.Entities;

namespace SearchQueryStringModelBindingSample.Controllers {
    public class SearchController : ApiController
    {
        private readonly string[] _persons =
            new[] { "Bill", "Steve", "Scott", "Glenn", "Daniel" };

        public IEnumerable<string> Get([FromUri] Search search)
        {
            return _persons
            .Where(w => w.Contains(search.Text))
            .Take(search.MaxResults);
        }
    }

}