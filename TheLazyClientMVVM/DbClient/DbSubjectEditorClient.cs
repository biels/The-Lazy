using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.DbClient
{
    public class DbSubjectEditorClient
    {
        public static List<SubjectEntity> getSubjectList(int academic_level_id)
        {
            if (!DbClient.isOnline()) { return null; }
            List<SubjectEntity> e = new List<SubjectEntity>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT * FROM subjects WHERE academic_level_id={0}",
                academic_level_id);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                SubjectEntity ent = new SubjectEntity();
                //FILL
                ent.id = rdr.GetInt32("subject_id");
                ent.name = rdr.GetString("name");
                ent.shortcode = rdr.GetString("shortcode");
                if (rdr["description"] != DBNull.Value)
                {
                    ent.description = rdr.GetString("description");
                }
                ent.academic_level = Com.main.cache.academic_level_cache.getAcademicLevel(rdr.GetInt32("academic_level_id"));
                ent.color = rdr.GetInt32("color");
                e.Add(ent);
            }
            rdr.Close();
            c.Close();
            return e;
        }
        public static SubjectEntity getSubjectInfo(int id)
        {
            if (!DbClient.isOnline()) { return null; }
            SubjectEntity e = new SubjectEntity();

            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM subjects WHERE subject_id={0}", id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                //FILL
                e.id = rdr.GetInt32("subject_id");
                e.name = rdr.GetString("name");
                e.shortcode = rdr.GetString("shortcode");
                if (rdr["description"] != DBNull.Value)
                {
                    e.description = rdr.GetString("description");
                }              
                e.academic_level = Com.main.cache.academic_level_cache.getAcademicLevel(rdr.GetInt32("academic_level_id"));
                e.color = rdr.GetInt32("color");
            }
            rdr.Close();
            c.Close();
            return e;
        }
        public static bool insertSubject(string name, string shortcode, int color, int academic_level_id, string description)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "INSERT INTO subjects VALUES(null, \"{0}\", \"{1}\", {2}, {3}, \"{4}\")",
                name, shortcode, color, academic_level_id, description);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool updateSubject(int subject_id, string name, string shortcode, int color, int academic_level_id, string description)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "UPDATE subjects SET name=\"{1}\", shortcode=\"{2}\", color={3}, academic_level_id={4}, description=\"{5}\" WHERE subject_id=\"{0}\" LIMIT 1",
                subject_id, name, shortcode, color, academic_level_id, description);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool deleteSubject(int subject_id)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "DELETE FROM subjects WHERE subject_id={0} LIMIT 1",
                subject_id);
            bool r = false;
            try
            {
                cmd.ExecuteNonQuery();
                r = true;
            }
            catch (Exception)
            {

            }
            c.Close();
            return r;
        }
    }
}
