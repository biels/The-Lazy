using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TheLazyServer
{
    class DbClient
    {
        public static MySqlConnection getConnection()
        {
            string address = "10.0.0.100";
            string port = "3306";
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
               Log.I("Connexió realitzada correctament!");
               return true;
            }
            catch (Exception ex)
            {
               // gest.OnlineMode = false;
            }
           
            return false;
        }
       
    }
}
