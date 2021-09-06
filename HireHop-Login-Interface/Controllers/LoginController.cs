using Hire_Hop_Interface.Management;
using Hire_Hop_Interface.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HireHop_Login_Interface.Controllers
{
    [Route("auth")]
    public class LoginController : Controller
    {
        #region Methods

        // POST: auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Create([FromHeader] string username, [FromHeader] string password, [FromHeader] string companyId)
        {
            if (Request.Cookies.TryGetValue("identity", out string identity) &&
                ConnectionManager.IsIdentity(identity, out TrackedIdentity identity_obj))
            {
                ClientConnection client = identity_obj.clientConnection;

                if (username == null || password == null || username.Length < 1 || password.Length < 1)
                {
                    return Json(new { status = "error", message = "Invalid Username or Password length" });
                }
                ClientConnection conn;

                if (companyId == null) conn = await Authentication.Login(client, username, password);
                else conn = await Authentication.Login(client, username, password, companyId);

                bool successful_login = conn.cookies.Count > 0 && !conn.cookies.Any(x => x.Name == "id" && x.Value == "deleted");

                if (successful_login)
                {
                    identity_obj.user = new User() { username = username };
                    //identity_obj.Update();
                    return Json(new { status = "success", message = "Logged in successfully" });
                }
                else
                {
                    return Json(new { status = "error", message = "You provided incorrect details" });
                }
            }
            return Json(new { status = "error", message = "Your identity is invalid" });
        }

        [HttpGet("trackme")]
        public ActionResult TrackMe()
        {
            string identifier = ConnectionManager.CreateClient();
            Response.Cookies.Append("identity", identifier);
            return Json(new { status = "success", messaged = "Identity now being tracked" });
        }

        #endregion Methods
    }
}