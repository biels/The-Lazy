using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.DbClient
{
    public class DbGroupEditor
    {
        public static List<EducationCenterEntity> getEducationCenterList()
        {
            if (!DbClient.isOnline()) { return null; }
            List<EducationCenterEntity> e = new List<EducationCenterEntity>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM education_centers");

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                EducationCenterEntity ent = new EducationCenterEntity();
                ent.id = rdr.GetInt32("education_center_id");
                ent.name = rdr.GetString("name");
                ent.location = rdr.GetString("location");
                e.Add(ent);
            }
            //FILL

            rdr.Close();
            c.Close();
            return e;
        }
        public static List<AcademicLevelEntity> getAcademicLevelList()
        {
            if (!DbClient.isOnline()) { return null; }
            List<AcademicLevelEntity> e = new List<AcademicLevelEntity>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("SELECT * FROM academic_levels");

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                AcademicLevelEntity ent = new AcademicLevelEntity();
                ent.id = rdr.GetInt32("academic_level_id");
                ent.name = rdr.GetString("name");
                e.Add(ent);
            }
            //FILL

            rdr.Close();
            c.Close();
            return e;
        }
        //public static List<GroupEntity> getGroupList(EducationCenterEntity education_center, AcademicLevelEntity academic_level)
        //{
        //    if (education_center == null || academic_level == null) { return null; }
        //    if (!DbClient.isOnline()) { return null; }
        //    List<GroupEntity> e = new List<GroupEntity>();
        //    MySqlConnection c = DbClient.getConnection();
        //    c.Open();
        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.Connection = c;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = String.Format(
        //        "SELECT * FROM groups WHERE education_center_id={0} && academic_level_id={1}",
        //        education_center.id, academic_level.id);
        //    MySqlDataReader rdr = cmd.ExecuteReader();
        //    while (rdr.Read())
        //    {
        //        GroupEntity ent = new GroupEntity();
        //        ent.id = rdr.GetInt32("group_id");
        //        ent.group_code = rdr.GetString("group_code");
        //        ent.education_center = education_center;
        //        ent.academic_level = academic_level;
        //        e.Add(ent);
        //    }
        //    //FILL

        //    rdr.Close();
        //    c.Close();
        //    return e;
        //}
        public static bool insertEducationCenter(string name, string location)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "INSERT INTO education_centers VALUES(null, \"{0}\", \"{1}\")",
                name, location);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool insertAcademicLevel(string name)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "INSERT INTO academic_levels VALUES(null, \"{0}\")",
                name);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        //public static bool insertGroup(string group_code, AcademicLevelEntity academic_level, EducationCenterEntity eduaction_center)
        //{
        //    try
        //    {
        //        if (!DbClient.isOnline()) { return false; }
        //        MySqlConnection c = DbClient.getConnection();
        //        c.Open();
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = c;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = String.Format(
        //            "INSERT INTO groups VALUES(null, \"{0}\", {1}, {2})",
        //            group_code, academic_level.id, eduaction_center.id);
        //        cmd.ExecuteNonQuery();
        //        c.Close();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}
