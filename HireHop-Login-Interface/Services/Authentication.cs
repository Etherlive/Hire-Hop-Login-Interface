using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace HireHop_Login_Interface.Services
{
    public static class Authentication
    {
        public static async Task<ClientConnection> Login(ClientConnection client, string username, string password, string companyId = "ELTH")
        {
            client = await RequestInterface.SendRequest(client, "login.php", contentList: new List<string>()
            {
                "loc=home.php", $"code={companyId}", "rem=1"
            });

            client = await RequestInterface.SendRequest(client, "login_msg.php", contentList: new List<string>()
            {
                "loc=home.php", $"code={companyId}", "rem=1", $"username={username}", $"password={password}"
            });

            var cookie = client.cookies;

            return client;
        }
    }
}
