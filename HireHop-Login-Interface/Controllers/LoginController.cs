using HireHop_Login_Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireHop_Login_Interface.Controllers
{
    [Route("auth")]
    public class LoginController : Controller
    {
        // POST: LoginController/Create
        [HttpPost("login")]
        public async Task<ActionResult> Create([FromHeader]string username, [FromHeader]string password, [FromHeader]string companyId)
        {
            if (username == null || password==null|| username.Length<1 || password.Length < 1)
            {
                return Json(new { status = "error", message = "Invalid Username or Password length" });
            }
            ClientConnection conn;

            if (companyId == null) conn = await Services.Authentication.Login(new Services.ClientConnection(), username, password);
            else conn = await Services.Authentication.Login(new Services.ClientConnection(), username, password, companyId);

            bool successful_login = conn.cookies.Count > 0 && !conn.cookies.Any(x => x.Name == "id" && x.Value == "deleted");

            if (successful_login)
            {
                return Json(new { status = "success", message = "Logged in successfully" });
            }
            else
            {
                return Json(new { status = "error", message = "You provided incorrect details" });
            }
        }
    }
}
