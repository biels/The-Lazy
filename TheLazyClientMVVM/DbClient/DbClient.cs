using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TheLazyClientMVVM.DbClient
{
    class DbClient
    {
        public static MySqlConnection getConnection()
        {
            SelectorConnexions.ConnectionInfo cinfo = new SelectorConnexions.ConnectionInfo();
            string address = cinfo.Address;
            string port = cinfo.Port.ToString();
           
            MySqlConnection c = new MySqlConnection("server=" + address + ";user=biel;database=the_lazy;port=" + port + ";password=1234;");
            return c;
        }
        public static bool TestConnection()
        {
            try
            {
                MySqlConnection c = getConnection();
                c.Open();
                c.Close();
                //Log.I("Connexió realitzada correctament!");
                Com.main.onlineMode = true;
            }
            catch (Exception ex)
            {
                Com.main.onlineMode = false;
            }
            return Com.main.onlineMode;
        }
        public static bool isOnline()
        {
            Com.main.sql_query_count++; //Sempre es crida abans de les consultes
            return Com.main.onlineMode;
        }
        public static string formatDateTime(DateTime dateValue)
        {
            return dateValue.ToString("yyyy-MM-dd HH:mm");
        }
        
    }
}
