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
    public class DbAcademicLevelClient
    {
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
       
    }
}
