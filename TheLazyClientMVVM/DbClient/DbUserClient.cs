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
            if (!DbClient.isOnline()) { return null; }
            UserEntity e = new UserEntity();
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM users WHERE username=\"{0}\"", username);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            //FILL
            e.id = rdr.GetInt32("user_id");
            e.username = rdr.GetString("username");
            e.real_name = rdr.GetString("real_name");
            e.email = rdr.GetString("email");
            e.group = getGroupInfo(rdr.GetInt32("group_id"));
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
    }

}
