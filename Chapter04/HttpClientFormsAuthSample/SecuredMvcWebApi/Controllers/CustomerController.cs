using System.Web.Mvc;

namespace SecuredMvcWebApi.Controllers {
    public class CustomerController : Controller {
        [Authorize]
        public ActionResult Get(int id) {
            return Json(new {Id = 1, Name = "Microsoft"}, JsonRequestBehavior.AllowGet);
        }
    }
}