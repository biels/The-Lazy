using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM
{
    public class LoginManager
    {
        public string username;
        public int sessionId = -1;

        public bool login(string username, string password)
        {
            this.username = username;
            sessionId = DbClient.DbLoginClient.loginUser(username, password);
            return loggedIn();
        }
        public bool register(string username, string password, string realName, string email)
        {
            return DbClient.DbLoginClient.register(username, password, realName, email);
        }
        public void logout()
        {
            //Send close session
            sessionId = -1;
        }
        public bool loggedIn()
        {
            return (sessionId >= 0);
        }
    }
}
