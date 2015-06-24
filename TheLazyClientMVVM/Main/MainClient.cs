using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using TheLazyClientMVVM.Chat;
using TheLazyClientMVVM.Filter;
using TheLazyClientMVVM.FileExplorer;
using TheLazyClientMVVM.Achievements;


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
        public FileExploreHandlerManager fileExploreHandlerManager = new FileExploreHandlerManager();
        public Entities.UserEntity localUser;
        public Cache.LocalCache cache = new Cache.LocalCache();
        public List<string> registrats = new List<string>();
        public ElementFilter filter = new ElementFilter();
        public AchievementManager achievementManager = new AchievementManager();
        public int sql_query_count = 0;
        public int cached_query_count = 0;
        public LocalConfig config = new LocalConfig();
        public void init()
        {
            // wcfClient.init();
            DbClient.DbClient.TestConnection();

            filter.init();
            achievementManager.init();
           
            //Entities.UserEntity u = DbClient.DbUserClient.getUserInfo("biel");

            //connectionParametersRefreshed();

        }
        public void initChatService()
        {
            chatManager.init();
        }
        public void updateLocalUser()
        {
            localUser = cache.user_cache.getUser(loginManager.username, true); // DIRECT, Sempre independent de la memòria cau
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
            return Com.main.cache.user_cache.getUser(name);
        }
        public void connectionParametersRefreshed()
        {
            DbClient.DbClient.TestConnection();
        }
    }
}
