using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.MainClient
{
    class LoginManager
    {
        string username;
        bool loggedIn = false;
        void login(string username, string password)
        {
            this.username = username;

        }
        void logout()
        {
            loggedIn = false;
        }
    }
}
