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
    public class DbElementClient
    {
        public static List<ElementEntity> getFilteredElementList(string where_clause)
        {
            if (!DbClient.isOnline()) { return null; }
            List<ElementEntity> e = new List<ElementEntity>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT * FROM subjects WHERE {0} ORDER BY create_time DESC LIMIT 50",
                where_clause);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ElementEntity ent = new ElementEntity();
                //FILL
                ent.id = rdr.GetInt32("subject_id");
                //ent.user = DbUserClient.getUserInfo
                ent.subject = DbSubjectEditorClient.getSubjectInfo(rdr.GetInt32("subject_id"));
                ent.name = rdr.GetString("name");
                if (rdr["description"] != DBNull.Value)
                {
                    ent.description = rdr.GetString("description");
                }               
                ent.price = rdr.GetInt32("price");
                e.Add(ent);
            }
            rdr.Close();
            c.Close();
            return e;
        }
    }
}
