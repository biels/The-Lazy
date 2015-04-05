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
        //BASAT EN IDs
        public static List<int> getFilteredElementIDList(string where_clause, string order_by_clause = "ORDER BY create_time DESC", int limit = 50)
        {
            if (!DbClient.isOnline()) { return null; }
            List<int> e = new List<int>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT element_id FROM elements {0} {1} LIMIT {2}",
                where_clause, order_by_clause, limit);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                e.Add(rdr.GetInt32("element_id"));
            }
            rdr.Close();
            c.Close();
            return e;
        }
        //TRADICIONAL
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
                ent.user = Com.main.cache.user_cache.getUser(rdr.GetInt32("user_id"));
                ent.subject = Com.main.cache.subject_cache.getSubject(rdr.GetInt32("subject_id"));
                ent.name = rdr.GetString("name");
                if (rdr["description"] != DBNull.Value)
                {
                    ent.description = rdr.GetString("description");
                }               
                ent.price = rdr.GetInt32("price");
                ent.create_time = DateTime.Parse(rdr.GetString("create_time"));
                ent.update_time = DateTime.Parse(rdr.GetString("update_time")); 

                ent.favourite_amount = getFavouriteCount(ent.id);
                ent.purchase_count = getElementPurchaseCount(ent.id);

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
            System.Threading.Thread.Sleep(500);
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
            e.purchase = getElementPurchaseInfoForUser(user_id, element_id);
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
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT COUNT(*) FROM element_ratings WHERE element_id={0}",
                element_id);
            int s = -1;
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
        //Comments
        public static List<ElementCommentEntity> getFilteredCommentList(string where_clause)
        {
            if (!DbClient.isOnline()) { return null; }
            List<ElementCommentEntity> e = new List<ElementCommentEntity>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT * FROM element_comments {0} ORDER BY create_time DESC LIMIT 50",
                where_clause);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ElementCommentEntity ent = new ElementCommentEntity();
                //FILL
                ent.id = rdr.GetInt32("comment_id");
                ent.user = Com.main.cache.user_cache.getUser(rdr.GetInt32("user_id"));
                if (rdr["text"] != DBNull.Value)
                {
                    ent.text = rdr.GetString("text");
                }
                ent.create_time = DateTime.Parse(rdr.GetString("create_time"));
                ent.update_time = DateTime.Parse(rdr.GetString("update_time")); 


                e.Add(ent);
            }
            rdr.Close();
            c.Close();
            return e;
        }
        public static List<ElementCommentEntity> getElementCommentList(int element_id)
        {
            return getFilteredCommentList(String.Format("WHERE element_id={0}", element_id));
        }
        //--INSERT, UPDATE & DELETE COMMENTS
        public static bool insertElementComment(int user_id, int element_id, string text)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "INSERT INTO element_comments VALUES(null, {0}, {1}, \"{2}\", NOW(), null)",
                user_id, element_id, text);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool updateElementComment(int comment_id, string text)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "UPDATE element_comments SET text=\"{1}\", update_time=NOW() WHERE comment_id={0}",
                comment_id, text);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        public static bool deleteElementComment(int comment_id)
        {
            if (!DbClient.isOnline()) { return false; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "DELETE FROM element_comments WHERE comment_id={0} LIMIT 1",
                comment_id);
            cmd.ExecuteNonQuery();
            c.Close();
            return true;
        }
        //Purchase info & operations
        public static int getFilteredElementPurchaseCount(string where_clause)
        {
            if (!DbClient.isOnline()) { return -1; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT COUNT(*) FROM element_purchases {0}",
                where_clause);
            int s = -1;
            try
            {
                s = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            catch { }
            c.Close();
            return s;
        }
        public static int getElementPurchaseCount(int element_id)
        {
            return getFilteredElementPurchaseCount(String.Format("WHERE element_id={0}", element_id));
        }
        public static List<ElementPurchaseEntity> getFilteredElementPurchaseList(string where_clause)
        {
            if (!DbClient.isOnline()) { return null; }
            List<ElementPurchaseEntity> e = new List<ElementPurchaseEntity>();
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(
                "SELECT * FROM element_purchases {0} ORDER BY create_time DESC",
                where_clause);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ElementPurchaseEntity ent = new ElementPurchaseEntity();
                ent.id = rdr.GetInt32("purchase_id");
                ent.price = rdr.GetInt32("price");
                ent.create_time = DateTime.Parse(rdr.GetString("create_time"));
                e.Add(ent);
            }
            rdr.Close();
            c.Close();
            return e;
        }
        public static List<ElementPurchaseEntity> getUserElementPurchaseList(int user_id)
        {
            List<ElementPurchaseEntity> l = getFilteredElementPurchaseList(String.Format("WHERE user_id={0}", user_id));
            return l;
        }
        public static ElementPurchaseEntity getElementPurchaseInfoForUser(int user_id, int element_id)
        {
            List<ElementPurchaseEntity> l = getFilteredElementPurchaseList(String.Format("WHERE user_id={0} AND element_id={1}", user_id, element_id));
            if (l.Count > 0)
            {
                return l[0];
            }
            return null;
        }        
        public static int buyElement(string user_id, string element_id)
        {
            if (!Com.main.onlineMode) { return -1; }
            MySqlConnection c = DbClient.getConnection();
            c.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = c;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BuyElement";

            cmd.Parameters.AddWithValue("_user_id", user_id);
            cmd.Parameters["_user_id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("_element_id", element_id);
            cmd.Parameters["_element_id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("_purchase_id", MySqlDbType.String);
            cmd.Parameters["_purchase_id"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            c.Close();
            int result = (int)cmd.Parameters["_purchase_id"].Value;
            return result;
        }

    }
}
