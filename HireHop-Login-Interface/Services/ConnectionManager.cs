using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireHop_Login_Interface.Services
{
    public static class ConnectionManager
    {
        private static Dictionary<string, ClientConnection> tracked_connections = new Dictionary<string, ClientConnection>();


        private static Random rnd = new Random();
        private static string rndString(uint length = 32)
        {
            string s = "";
            for (uint i = 0; i < length; i++) s += (char)rnd.Next(65,122);
            return s.Replace("\\", "/");
        }

        public static string CreateClient()
        {
            string identifier = rndString();
            tracked_connections.Add(identifier, new ClientConnection());
            return identifier;
        }

        public static bool IsIdentity(string identity, out ClientConnection client)
        {
            if (tracked_connections.ContainsKey(identity))
            {
                client = tracked_connections[identity];
                return true;
            }
            else
            {
                client = null;
                return false;
            }
        }
    }
}
