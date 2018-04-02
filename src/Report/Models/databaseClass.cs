using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using System.Data.SqlClient;
using System.Data;

namespace Report.Models
{
    public class databaseClass
    {
        dbFile db = new dbFile();
        SqlConnection conn;
        SqlCommand cmd;
        public databaseClass()
        {
            dbFile db = new dbFile();
            try
            {
                conn = new SqlConnection(db.sqlConnection);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch
            {
                conn.Close();
            }
        }
        public string  insert(string table, string[] Columns, string [] Value)
        {
            string turnid = "";
           
            string query_set = "";
            string query_get = "";
            int last_arr = Columns.Length - 1;
            int count = 0;
            foreach (string i in Columns)
            {

                if (count == last_arr)
                {
                    query_set = query_set + " " + i + " ";
                    query_get = query_get + " @" + i + " ";
                }
                else
                {
                    query_set = query_set + " " + i + ", ";
                    query_get = query_get + " @" + i + ", ";
                }
                count += 1;
            }

            using (SqlConnection conn = new SqlConnection(db.sqlConnection))
            {
             
                string sql = "INSERT INTO " + table + " (" + query_set + ") VALUES(" + query_get + ") SELECT @@IDENTITY as idemp";

                conn.Open();

                cmd = new SqlCommand(sql, conn);
                int num = 0;
                foreach (string i in Value)
                {

                    cmd.Parameters.AddWithValue("@" + Columns[num], i);
                    num += 1;
                }

                turnid = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
                conn.Close();
               
            }

            return turnid;

        }
        public string insert_db(string table, string[] Columns, string[] Value)
        {
            string turnid = "";

            string query_set = "";
            string query_get = "";
            int last_arr = Columns.Length - 1;
            int count = 0;
            foreach (string i in Columns)
            {

                if (count == last_arr)
                {
                    query_set = query_set + " " + i + " ";
                    query_get = query_get + " @" + i + " ";
                }
                else
                {
                    query_set = query_set + " " + i + ", ";
                    query_get = query_get + " @" + i + ", ";
                }
                count += 1;
            }

            using (SqlConnection conn = new SqlConnection(db.sqlConnection))
            {
                conn.Open();
                string sql = "INSERT INTO " + table + " (" + query_set + ") VALUES(" + query_get + ")";

                cmd = new SqlCommand(sql, conn);
                cmd_AddWithValue(Columns, Value);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                conn.Close();


            }

            return turnid;

        }
        public string insert_returnId(string table, string[] Columns, string[] Value)
        {

            string query_set = "";
            string query_get = "";
            int last_arr = Columns.Length - 1;
            int count = 0;
            foreach (string i in Columns)
            {

                if (count == last_arr)
                {
                    query_set = query_set + " " + i + " ";
                    query_get = query_get + " @" + i + " ";
                }
                else
                {
                    query_set = query_set + " " + i + ", ";
                    query_get = query_get + " @" + i + ", ";
                }
                count += 1;
            }


            string sql = "INSERT INTO " + table + " (" + query_set + ") VALUES(" + query_get + ") SELECT SCOPE_IDENTITY() AS returnid";

            cmd_sql(sql);
            cmd_AddWithValue(Columns, Value);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            string id = "";
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                id = rdr["returnid"].ToString();
            }
            
            conn.Close();


            return id;
        }

        public SqlDataReader getdb(SqlDataReader rdr, string table, string where = null, string field = "*")
        {
          
            //var count = 1;
            if (where != null)
            {
                where = "WHERE " + where;
            }
            else
            {
                where = "";
            }
            cmd = new SqlCommand("SELECT " + field + " FROM " + table + " " + where + "", conn);

            return rdr;

        }
        public void delete(string table, string where)
        {

            cmd = new SqlCommand("DELETE " + table + " WHERE " + where + "", conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            conn.Close();

        }
        public void update(string table, string[] Columns, string[] Value, string where = null)
        {
            

            string query_update = "";
            int last_arr = Columns.Length - 1;
            int count = 0;
            foreach (string i in Columns)
            {

                if (count == last_arr)
                {

                    query_update = query_update + " " + Columns[count] + " =@" + Columns[count] + "";
                }
                else
                {

                    query_update = query_update + " " + Columns[count] + " =@" + Columns[count] + ",";

                }
                count += 1;
            }
            if (where == null)
            {
                where = "";
            }
            else
            {
                where = "WHERE " + where;
            }

            string sql = "UPDATE " + table + " SET " + query_update + " " + where;


            //using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

            //    conn.Open();


            //    SqlCommand cmd = new SqlCommand(sql, conn);
            //    cmd.ExecuteNonQuery();
            //    cmd.Dispose();

            //    conn.Close();
            //}


            cmd_sql(sql);
            cmd_AddWithValue(Columns, Value);
            cmd_query();

        }
        public void cmd_sql(string sql)
        {
            cmd = new SqlCommand(sql, conn);
          
        }
        public void cmd_query()
        {
            // conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        public void cmd_AddWithValue(string[] Columns, string[] Value)
        {
            int count = 0;
            foreach (string i in Value)
            {

                cmd.Parameters.AddWithValue("@" + Columns[count], i);
                count += 1;
            }
        }

      
    }
}
