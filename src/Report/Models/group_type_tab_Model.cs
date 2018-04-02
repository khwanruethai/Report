using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Models
{
    public class group_type_tab_Model
    {
        public string group_type_id { get; set; }
        public string group_type_name { get; set; }
        public string head_type_id { get; set; }

        dbFile db = new dbFile();

        public List<group_type_tab_Model> select_group_type() {

            List<group_type_tab_Model> obj = new List<group_type_tab_Model>();

            using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ads_group_type_tab WHERE group_type_id IN(1,2,3,4,5,6,7,8,9,10,11)", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {

                    group_type_tab_Model g = new group_type_tab_Model();

                    g.group_type_id = rdr["group_type_id"].ToString();
                    g.group_type_name = rdr["group_type_name"].ToString();
                    g.head_type_id = rdr["head_type_id"].ToString();


                    obj.Add(g);
                }

                conn.Close();
            }

            return obj;

        }



    }
}
