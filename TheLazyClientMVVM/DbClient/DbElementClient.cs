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
                ent.user = DbUserClient.getUserInfo(rdr.GetInt32("user_id"));
                ent.subject = DbSubjectEditorClient.getSubjectInfo(rdr.GetInt32("subject_id"));
                ent.name = rdr.GetString("name");
                if (rdr["description"] != DBNull.Value)
                {
                    ent.description = rdr.GetString("description");
                }               
                ent.price = rdr.GetInt32("price");

                ent.favourite_amount = getFavouriteCount(ent.id);

                ent.local_data = getLocalElementData(Com.main.localUser.id, ent.id);

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
        //LOCAL DATA
        public static LocalElementDataEntity getLocalElementData(int user_id, int element_id)
        {
            if (!DbClient.isOnline()) { return null; }
            LocalElementDataEntity e = new LocalElementDataEntity();
            e.favourite = getFavourite(user_id, element_id);
            e.rating = getElementRating(user_id, element_id);
            return e;
        }
        public static int getFavouriteCount(int element_id)
        {
            if (!DbClient.isOnline()) { return -1; }
            bool e = false;
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT COUNT(*) FROM favourites WHERE element_id={0}",
                element_id);
            int s = 200;
            try
            {
                s = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            } catch { }
            c.Close();
            return s;
        }
        public static bool getFavourite(int user_id, int element_id)
        {
            if (!DbClient.isOnline()) { return false; }
            bool e = false;
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT favourite_id FROM favourites WHERE user_id={0} AND element_id={1} LIMIT 1",
                user_id, element_id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            e = rdr.Read();
            rdr.Close();
            c.Close();
            return e;
        }
        public static bool setFavourite(int user_id, int element_id, bool value)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            if (value)
            {
                cmd.CommandText = String.Format(
               "INSERT IGNORE INTO favourites VALUES(null, {0}, {1}, null)",
               user_id, element_id);
            }
            else
            {
                cmd.CommandText = String.Format(
              "DELETE FROM favourites WHERE user_id={0} AND element_id={1} LIMIT 1",
              user_id, element_id);
            }         
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static int getElementRatingCount(int element_id)
        {
            if (!DbClient.isOnline()) { return -1; }
            bool e = false;
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT COUNT(*) FROM element_ratings WHERE element_id={0}",
                element_id);
            int s = 200;
            try
            {
                s = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            } catch { }
            c.Close();
            return s;
        }
        public static int getElementRating(int user_id, int element_id) //Pot retornar -1
        {
            if (!DbClient.isOnline()) { return -1; }
            int r = -1;
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT value FROM element_ratings WHERE user_id={0} AND element_id={1} LIMIT 1",
                user_id, element_id);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                r = rdr.GetInt32("value");
            }
            rdr.Close();
            c.Close();
            return r;
        }
        public static bool setElementRating(int user_id, int element_id, int value)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            if (value != -1)
            {
                cmd.CommandText = String.Format(
               "INSERT INTO element_ratings VALUES(null, {0}, {1}, {2}, null) ON DUPLICATE KEY UPDATE value={2}",
               user_id, element_id, value);
            }
            else
            {
                cmd.CommandText = String.Format(
              "DELETE FROM element_ratings WHERE user_id={0} AND element_id={1} LIMIT 1",
              user_id, element_id);
            }
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
    }
}
