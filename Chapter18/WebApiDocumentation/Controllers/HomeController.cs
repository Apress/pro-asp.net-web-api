using System.Web.Http;
using System.Web.Mvc;
using WebApiDocumentation.Models;

namespace WebApiDocumentation.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            var config = GlobalConfiguration.Configuration;
            var explorer = config.Services.GetApiExplorer();
            return View(new ApiModel(explorer));
        }
    }
}