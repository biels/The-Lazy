﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace TheLazyClientMVVM
{
   public class MainClient
    {
       
        public event LogHandler LogOutput;
        public delegate void LogHandler(string m);
        public bool onlineMode = false; 
        public WCFClient wcfClient = new WCFClient();
        public LoginManager loginManager = new LoginManager();

        public List<string> registrats = new List<string>();
        public void init()
        {
           // wcfClient.init();
            DbClient.DbClient.TestConnection();

            registrats = DbClient.DbUserClient.getUserList();
            //TEST
            //Entities.UserEntity u = DbClient.DbUserClient.getUserInfo("biel");

            //connectionParametersRefreshed();
            
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
