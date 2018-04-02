using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Models
{
    public class ads_type_main_tabModel
    {
        public string type_ads { get; set; }
        public string type_ads_name { get; set; }
        public List<string> main_name { get; set; }
        public List<string> main_id { get; set; }

        dbFile db = new dbFile();


        public List<ads_type_main_tabModel> type_main() {


       List<ads_type_main_tabModel> item = new List<ads_type_main_tabModel>();

          main_name = new List<string>();
          main_id = new List<string>();

            using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ads_type_main_tab WHERE type_ads > 0", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {

                    ads_type_main_tabModel ads = new ads_type_main_tabModel();

                    ads.type_ads = rdr["type_ads"].ToString();
                    ads.type_ads_name = rdr["type_ads_name"].ToString();

                    main_id.Add(rdr["type_ads"].ToString());
                    main_name.Add(rdr["type_ads_name"].ToString());

                    item.Add(ads);
                }

                conn.Close();
            }


            return item;
        }

    }
}
