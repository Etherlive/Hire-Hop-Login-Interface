using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireHop_Login_Interface.Services
{
    public class TrackedIdentity
    {
        public ClientConnection clientConnection = new ClientConnection();
        public User user;

        private string identity_str;

        public TrackedIdentity(string identity)
        {
            identity_str = identity;
        }

        public void Update()
        {
        }
    }

    public class User
    {
        public string username { get; set; }
    }
}
