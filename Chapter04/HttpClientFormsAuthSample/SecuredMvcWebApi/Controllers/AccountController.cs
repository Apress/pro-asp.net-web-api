using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace SecuredMvcWebApi.Controllers {
    public class AccountController : Controller {
        [HttpPost]
        public ActionResult Login(string username, string password) {
            if (username == password) {
                FormsAuthentication.SetAuthCookie(username, true);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpUnauthorizedResult();
        }
    }
}