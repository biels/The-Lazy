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
    public class DbUserClient
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
        public static UserEntity getUserInfo(int id)
        {
            return getUserSQLInfo(String.Format("WHERE user_id=\"{0}\"", id));
        }
        public static UserEntity getUserInfo(string username)
        {
            return getUserSQLInfo(String.Format("WHERE username=\"{0}\"", username));
        }
        static UserEntity getUserSQLInfo(string where_clause)
        {
            if (where_clause == null) { return null; }
            if (!DbClient.isOnline()) { return null; }
            UserEntity e = new UserEntity();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM users {0} LIMIT 1", where_clause);

            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                //FILL
                e.id = rdr.GetInt32("user_id");
                e.username = rdr.GetString("username");
                e.real_name = rdr.GetString("real_name");
                e.email = rdr.GetString("email");
                e.status = getUserStatus(e.id); //Estat recursiu
                e.balance = rdr.GetInt32("balance");
                e.permission_level = rdr.GetInt32("permission_level");
                if (rdr["gender"] != DBNull.Value)
                {
                    e.gender = (UserEntity.GenderEnum) Enum.Parse(typeof(UserEntity.GenderEnum), rdr.GetString("gender"), true);
                }
                if (rdr["group_code"] != DBNull.Value)
                {
                    e.group_code = rdr.GetString("group_code");
                }
                if (rdr["academic_level_id"] != DBNull.Value)
                {
                    e.academic_level = getAcademicLevelInfo(rdr.GetInt32("academic_level_id"));
                }
                if (rdr["education_center_id"] != DBNull.Value)
                {
                    e.education_center = getEducationCenterInfo(rdr.GetInt32("education_center_id"));
                }
            }           
            rdr.Close();
            c.Close();
            return e;
        }
        public static EducationCenterEntity getEducationCenterInfo(int id)
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
            if (rdr.Read())
            {
                //FILL
                e.id = rdr.GetInt32("education_center_id");
                e.name = rdr.GetString("name");
                e.location = rdr.GetString("location");
            }
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
            if (rdr.Read())
            {
                //FILL
                e.id = rdr.GetInt32("academic_level_id");
                e.name = rdr.GetString("name");
            }           
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
        public static bool updateUser(string username, string email, string real_name, string gender, string group_code, int academic_level_id, int education_center_id)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "UPDATE users SET email=\"{1}\", real_name=\"{2}\", gender=\"{3}\", group_code=\"{4}\", academic_level_id={5}, education_center_id={6} WHERE username=\"{0}\"", 
                username, email, real_name, gender, group_code, academic_level_id, education_center_id);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
    }

}
