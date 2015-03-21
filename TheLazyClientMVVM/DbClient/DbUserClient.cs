using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.DbClient
{
    class DbUserClient
    {
        public static List<string> getUserList()
        {
            if (!DbClient.isOnline()) { return null; }
            List<string> e = new List<string>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT username FROM users");

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                e.Add(rdr.GetString("username"));
            }
            //FILL

            rdr.Close();
            c.Close();
            return e;
        }
        public static UserEntity getUserInfo(string username)
        {
            if (username == null) { return null; }
            if (!DbClient.isOnline()) { return null; }
            UserEntity e = new UserEntity();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM users WHERE username=\"{0}\"", username);

            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                //FILL
                e.id = rdr.GetInt32("user_id");
                e.username = rdr.GetString("username");
                e.real_name = rdr.GetString("real_name");
                e.email = rdr.GetString("email");
                e.status = getUserStatus(e.id); //Cascade status
                e.balance = rdr.GetInt32("balance");
                if (rdr["group_id"] != DBNull.Value)
                {
                    e.group = getGroupInfo(rdr.GetInt32("group_id"));
                }
            }           
            rdr.Close();
            c.Close();
            return e;
        }
        public static GroupEntity getGroupInfo(int id)
        {
            if (id == null) { return null; }
            if (!DbClient.isOnline()) { return null; }
            GroupEntity e = new GroupEntity();

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM groups WHERE group_id={0}", id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            //FILL
            e.id = rdr.GetInt32("group_id");
            e.group_code = rdr.GetString("group_code");
            e.education_center = getEduactionCenterInfo(rdr.GetInt32("education_center_id"));
            e.academic_level = getAcademicLevelInfo(rdr.GetInt32("academic_level_id"));
            rdr.Close();
            c.Close();
            return e;
        }
        public static EducationCenterEntity getEduactionCenterInfo(int id)
        {
            if (!DbClient.isOnline()) { return null; }
            EducationCenterEntity e = new EducationCenterEntity();

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM education_centers WHERE education_center_id={0}", id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            //FILL
            e.id = rdr.GetInt32("education_center_id");
            e.name = rdr.GetString("name");
            e.location = rdr.GetString("location");
            rdr.Close();
            c.Close();
            return e;
        }
        public static AcademicLevelEntity getAcademicLevelInfo(int id)
        {
            if (!DbClient.isOnline()) { return null; }
            AcademicLevelEntity e = new AcademicLevelEntity();

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM academic_levels WHERE academic_level_id={0}", id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            //FILL
            e.id = rdr.GetInt32("academic_level_id");
            e.name = rdr.GetString("name");
            rdr.Close();
            c.Close();
            return e;
        }
        public static string getUserStatus(int userid)
        {
            if (!DbClient.isOnline()) { return null; }
            string e = "";

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT text FROM status WHERE user_id=\"{0}\" ORDER BY create_time DESC LIMIT 1", 
                userid);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                e = rdr.GetString("text");
            }
            //FILL          
            rdr.Close();
            c.Close();
            return e;
        }
        public static bool updateUserStatus(int userid, string status)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "INSERT INTO status VALUES(null, {0}, \"{1}\", NOW())",
                userid, status);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool updateUser(string username, string email, string real_name, int group_id)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "UPDATE users SET email=\"{1}\", real_name=\"{2}\", group_id={3} WHERE username={0}", 
                username, email, real_name, group_id);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
    }

}
