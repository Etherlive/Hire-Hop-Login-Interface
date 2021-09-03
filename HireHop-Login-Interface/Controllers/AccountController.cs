using HireHop_Login_Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace HireHop_Login_Interface.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        #region Methods

        [HttpGet("me")]
        public ActionResult GetMe()
        {
            if (Request.Cookies.TryGetValue("identity", out string identity) &&
                  Services.ConnectionManager.IsIdentity(identity, out TrackedIdentity identity_obj))
            {
                return Json(new { status = "success", messaged = "Fetched your data", you = identity_obj.user });
            }
            return Json(new { status = "error", message = "Your identity is invalid" });
        }

        #endregion Methods
    }
}