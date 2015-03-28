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
                "SELECT * FROM elements {0} ORDER BY create_time DESC LIMIT 50",
                where_clause);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ElementEntity ent = new ElementEntity();
                //FILL
                ent.id = rdr.GetInt32("element_id");
                //ent.user = DbUserClient.getUserInfo FALTA!!!!
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
        public static ElementEntity getElementInfo(int element_id)
        {
            List<ElementEntity> l = getFilteredElementList(String.Format("WHERE element_id={0}", element_id));
            if (l.Count > 0){
                return l[0];
            }            
            return null;
        }
        public static bool insertElement(int user_id, int subject_id, string name, string description, int price)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "INSERT INTO elements VALUES(null, {0}, {1}, \"{2}\", \"{3}\", {4}, null, null)",
                user_id, subject_id, name, description, price);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool updateElement(int element_id, int user_id, int subject_id, string name, string description, int price)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "UPDATE elements SET user_id={1}, subject_id={2}, name=\"{3}\", description=\"{4}\", price={5} WHERE element_id={0}",
                element_id, user_id, subject_id, name, description, price);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
    }
}
