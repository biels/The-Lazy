using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TheLazyServer.DbClients
{
    class DbLogin
    {
        public static int loginUser(string username, string password)
        {
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            // Dim sql As String = "call LoginUser('" & username & "', '" & password & "', '" & ip.ToString & "', '" & result_session_id & "');"
            //"SELECT * FROM user"
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LoginUser";

            cmd.Parameters.AddWithValue("_username", username);
            cmd.Parameters["_username"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("_password", password);
            cmd.Parameters["_password"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("_ipendpoint", "[undefined]");
            cmd.Parameters["_ipendpoint"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("_session_id", MySqlDbType.String);
            cmd.Parameters["_session_id"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            c.Close();
            int result = (int) cmd.Parameters["_session_id"].Value;
            return result;
        }
        static public bool register(string username, string password, string email)
        {

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RegisterUser";

            cmd.Parameters.AddWithValue("d_username", username);
            cmd.Parameters["d_username"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("pass", password);
            cmd.Parameters["pass"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters["email"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("result", MySqlDbType.Int32);
            cmd.Parameters["result"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            c.Close();
            int result = (int)cmd.Parameters["result"].Value;
            return (result == 1);
        }
        static public void keepAlive(int session_id)
        {

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "KeepSessionAlive";

            cmd.Parameters.AddWithValue("_session_id", session_id);
            cmd.Parameters["_session_id"].Direction = ParameterDirection.Input;         

            cmd.ExecuteNonQuery();
            c.Close();
            return;
        }

    }
}
