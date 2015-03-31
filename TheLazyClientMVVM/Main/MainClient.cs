using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using TheLazyClientMVVM.Chat;


namespace TheLazyClientMVVM
{
    public class MainClient
    {

        public event LogHandler LogOutput;
        public delegate void LogHandler(string m);
        public bool onlineMode = false;
        public WCFClient wcfClient = new WCFClient();
        public LoginManager loginManager = new LoginManager();
        public ChatManager chatManager = new ChatManager();
        public Entities.UserEntity localUser;
        public List<string> registrats = new List<string>();
        public ElementFilter filter = new ElementFilter();
        public void init()
        {
            // wcfClient.init();
            DbClient.DbClient.TestConnection();

            filter.init();

            
            //Entities.UserEntity u = DbClient.DbUserClient.getUserInfo("biel");

            //connectionParametersRefreshed();

        }
        public void initChatService()
        {
            chatManager.init();
        }
        public void updateLocalUser()
        {
            localUser = DbClient.DbUserClient.getUserInfo(loginManager.username);
        }
        public void getHeadingInfo()
        {
            registrats = DbClient.DbUserClient.getUserList();
            //TEST
            updateLocalUser();
        }
        public string localStatus
        {
            get
            {
                return DbClient.DbUserClient.getUserStatus(localUser.id);
            }
            set
            {
                DbClient.DbUserClient.updateUserStatus(localUser.id, value);
            }
        }
        public Entities.UserEntity getUserInfo(string name)
        {
            return DbClient.DbUserClient.getUserInfo(name);
        }
        public void connectionParametersRefreshed()
        {
            DbClient.DbClient.TestConnection();
        }
    }
}
