using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Models
{
    public class result_amount_of_day_stroredModel
    {
        public string ads_date { get; set; }
        public string amount { get; set;}
        public string date_start { get; set; }
        public string date_end { get; set; }

        public string result_before { get; set; }
        public string result_present { get; set; }

        public double sale_dif_percent { get; set; }

        dbFile db = new dbFile();

        public List<object> getAmount() {

            List<object> obj = new List<object>();

            using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

                conn.Open();

                SqlCommand cmd = new SqlCommand("EXEC result_amount_of_day_stored @date_start = '2018-01-01', @date_end = '2018-02-28'", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {

                    obj.Add(new { label = Convert.ToDateTime(rdr["ads_date"]).ToString("dd MMM"), y = rdr["amount"]});
                    
                }
                
                conn.Close();
            }
            return obj; 
        }
        public void sum_amount_before(string start, string end) {


            using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT SUM(sales_amount) AS amount FROM ads_sales_det_tab WHERE ads_date BETWEEN '"+start+"' AND '"+end+"'", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {

                    result_before = rdr["amount"].ToString();

                }
                
                conn.Close();
            }
            
        }
        public void sum_amount_present(string start, string end)
        {
            
            using (SqlConnection conn = new SqlConnection(db.sqlConnection))
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT SUM(sales_amount) AS amount FROM ads_sales_det_tab WHERE ads_date BETWEEN '"+start+"' AND '"+end+"'", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    result_present = rdr["amount"].ToString();

                }

                conn.Close();
            }

        }
        public void calculate_Sale_dif_per() {


            double before = Convert.ToDouble(result_before);
            double present = Convert.ToDouble(result_present);

            sale_dif_percent = ((present - before) / before) * 100;
            
        }

    }
}
