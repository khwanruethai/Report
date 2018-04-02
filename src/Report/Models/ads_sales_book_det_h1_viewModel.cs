using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Report.Models;
using System.Data.SqlClient;

namespace Report.Models
{
    public class ads_sales_book_det_h1_viewModel
    {
        public string ads_date { get; set; }
        public string team_name { get; set; }
        public string h1_name { get; set; }
        public string team_no { get; set; }
        public string emp_id { get; set; }
        public string sale_name { get; set; }
        public string sale_team_id { get; set; }
        public string sale_level_id { get; set; }
        public string group_type_id { get; set; }
        public string group_type_name { get; set; }
        public string sales_amount { get; set; }
        public string dif_amount { get; set; }

        public double ads01 { get; set; }
        public double ads02 { get; set; }
        public double ads03 { get; set; }
        public double ads04 { get; set; }
        public double ads05 { get; set; }
        public double ads06 { get; set; }
        public double ads07 { get; set; }
        public double ads08 { get; set; }
        public double ads09 { get; set; }
        public double ads10 { get; set; }
        public double ads11 { get; set; }
        public double total { get; set; }
        public double target { get; set; }
        public double ads_dif_target { get; set; }
        public double ads_sum_count { get; set; }

        public double amount_sale_old { get; set; }

        //

        public double sum_ads01 { get; set; }
        public double sum_ads02 { get; set; }
        public double sum_ads03 { get; set; }
        public double sum_ads04 { get; set; }
        public double sum_ads05 { get; set; }
        public double sum_ads06 { get; set; }
        public double sum_ads07 { get; set; }
        public double sum_ads08 { get; set; }
        public double sum_ads09 { get; set; }
        public double sum_ads10 { get; set; }
        public double sum_ads11 { get; set; }
        public double sum_total { get; set; }
        public double diff_target { get; set; }
        public double sum_count { get; set; }
        public double sum_amount { get; set; }
        public double sum_dif_amount { get; set; }

        //

           public double total_sum_ads01 { get; set; }
           public double total_sum_ads02 { get; set; }
           public double total_sum_ads03 { get; set; }
           public double total_sum_ads04 { get; set; }
           public double total_sum_ads05 { get; set; }
           public double total_sum_ads06 { get; set; }
           public double total_sum_ads07 { get; set; }
           public double total_sum_ads08 { get; set; }
           public double total_sum_ads09 { get; set; }
           public double total_sum_ads10 { get; set; }
           public double total_sum_ads11 { get; set; }
           public double total_sum_total { get; set; }
           public double total_diff_target { get; set; }
           public double total_sum_count { get; set; }
           public double total_sum_amount { get; set; }
           public double total_sum_dif_amount { get; set; }

        //
        public string head01 { get; set; }
        public string head02 { get; set; }
        public string head03 { get; set; }
        public string head04 { get; set; }
        public string head05 { get; set; }
        public string head06 { get; set; }
        public string head07 { get; set; }
        public string total_head { get; set; }

        public double total_before { get; set; }
        public string result_team { get; set; }
        List<string> emp = new List<string>();

       

        dbFile db = new dbFile();

        public List<ads_sales_book_det_h1_viewModel> select_team() {

            List<ads_sales_book_det_h1_viewModel> obj = new List<ads_sales_book_det_h1_viewModel>();

            List<string> t_name = new List<string>();
            List<string> t_id = new List<string>();
           

            using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT team_name, team_no FROM ads_sales_book_det_h1_view WHERE ads_date = '2018-01-09' GROUP BY team_no, team_name ORDER BY team_no ASC", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {

                    t_name.Add(rdr["team_name"].ToString());
                    t_id.Add(rdr["team_no"].ToString());
                }

                conn.Close();
                
                int count = 0;

                
                foreach (var id in t_id) {

                    ads_sales_book_det_h1_viewModel tm2 = new Models.ads_sales_book_det_h1_viewModel();
                    tm2.result_team = "<td>"+t_name[count]+"</td>"+
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                             "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'></td>";

                    obj.Add(tm2);

                    int i = 0;

                    double before = 0;
                    


                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("EXEC ads_report @param1 = '2018-01-08', @param2 = '" + id + "'", conn);
                    SqlDataReader rdr2 = cmd2.ExecuteReader();
                    while (rdr2.Read())
                    {

                        before = Convert.ToDouble(rdr2["sales_amount"]);
                        total_before += Convert.ToDouble(rdr2["sales_amount"]);
                    }

                    conn.Close();
                    conn.Open();

                    
                    SqlCommand cmd1 = new SqlCommand("EXEC ads_report @param1 = '2018-01-09', @param2 = '"+id+"'", conn);
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                    while (rdr1.Read()) {

                        ads_sales_book_det_h1_viewModel tm = new Models.ads_sales_book_det_h1_viewModel();

                        tm.result_team = "<td> &nbsp;&nbsp;&nbsp;" + rdr1["empName"] + "</td>" +
                           "<td class='text-right'>" + Convert.ToDouble(rdr1["ads01"]).ToString("0,0.00") +"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads02"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads03"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads04"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads05"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads06"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads07"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads08"]).ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>"+Convert.ToDouble(rdr1["ads09"]).ToString("0,0.00") + "</td>"+
                           "<td class='text-right'>"+ Convert.ToDouble(rdr1["ads10"]).ToString("0,0.00") + "</td>"+
                           "<td class='text-right'>" + Convert.ToDouble(rdr1["ads11"]).ToString("0,0.00") + "</td>"+
                           "<td class='text-right'>0.00</td>"+
                           "<td class='text-right'>" + Convert.ToDouble(rdr1["sales_amount"]).ToString("0,0.00") + "</td>"+
                          "<td class='text-right'>" + (Convert.ToDouble(rdr1["sales_amount"]) - Convert.ToDouble("0.00")).ToString("0,0.00") + "</td>"+
                          "<td class='text-right'>" + (Convert.ToDouble(rdr1["sales_amount"]) - before).ToString("0,0.00") + "</td>";

                        sum_ads01 += Convert.ToDouble(rdr1["ads01"]);
                        sum_ads02 += Convert.ToDouble(rdr1["ads02"]);
                        sum_ads03 += Convert.ToDouble(rdr1["ads03"]);
                        sum_ads04 += Convert.ToDouble(rdr1["ads04"]);
                        sum_ads05 += Convert.ToDouble(rdr1["ads05"]);
                        sum_ads06 += Convert.ToDouble(rdr1["ads06"]);
                        sum_ads07 += Convert.ToDouble(rdr1["ads07"]);
                        sum_ads08 += Convert.ToDouble(rdr1["ads08"]);
                        sum_ads09 += Convert.ToDouble(rdr1["ads09"]);
                        sum_ads10 += Convert.ToDouble(rdr1["ads10"]);
                        sum_ads11 += Convert.ToDouble(rdr1["ads11"]);
                        diff_target += (Convert.ToDouble(rdr1["sales_amount"]) - Convert.ToDouble("0.00"));
                        sum_total += Convert.ToDouble(rdr1["sales_amount"]);
                        sum_count += (Convert.ToDouble(rdr1["sales_amount"]) - before);



                        ads01 += Convert.ToDouble(rdr1["ads01"]);
                        ads02 += Convert.ToDouble(rdr1["ads02"]);
                        ads03 += Convert.ToDouble(rdr1["ads03"]);
                        ads04 += Convert.ToDouble(rdr1["ads04"]);
                        ads05 += Convert.ToDouble(rdr1["ads05"]);
                        ads06 += Convert.ToDouble(rdr1["ads06"]);
                        ads07 += Convert.ToDouble(rdr1["ads07"]);
                        ads08 += Convert.ToDouble(rdr1["ads08"]);
                        ads09 += Convert.ToDouble(rdr1["ads09"]);
                        ads10 += Convert.ToDouble(rdr1["ads10"]);
                        ads11 += Convert.ToDouble(rdr1["ads11"]);
                        total += Convert.ToDouble(rdr1["sales_amount"]);
                        ads_dif_target += (Convert.ToDouble(rdr1["sales_amount"]) - Convert.ToDouble("0.00"));
                        ads_sum_count += (Convert.ToDouble(rdr1["sales_amount"]) - before); 
                        obj.Add(tm);
                     
                        i++;

                    }

                    ads_sales_book_det_h1_viewModel tm1 = new Models.ads_sales_book_det_h1_viewModel();

                    tm1.result_team =  "<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;รวม</td>" +
                            "<td class='text-right'>"+sum_ads01.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads02.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads03.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads04.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads05.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads06.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads07.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads08.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads09.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>"+sum_ads10.ToString("0,0.00")+"</td>" +
                            "<td class='text-right'>" + sum_ads11.ToString("0,0.00") + "</td>" +
                            "<td class='text-right'>0.00</td>" +
                            "<td class='text-right'>"+sum_total.ToString("0,0.00")+"</td>"+
                            "<td class='text-right'>"+diff_target.ToString("0,0.00")+"</td>"+
                            "<td class='text-right'>" + sum_count.ToString("0,0.00") + "</td>";


                    sum_ads01  = 0;
                    sum_ads02  = 0;
                    sum_ads03  = 0;
                    sum_ads04  = 0;
                    sum_ads05  = 0;
                    sum_ads06  = 0;
                    sum_ads07  = 0;
                    sum_ads08  = 0;
                    sum_ads09  = 0;
                    sum_ads10  = 0;
                    sum_ads11 = 0;
                    diff_target = 0;
                    sum_total = 0;
                    sum_count = 0;


                    obj.Add(tm1);

                    conn.Close();


                    count++;
                  
                }
            }

            ads_sales_book_det_h1_viewModel tm3 = new Models.ads_sales_book_det_h1_viewModel();
            tm3.result_team = "<td class='text-right'><b>รวมทั้งสิ้น</b></td>" +
                           "<td class='text-right'>" + ads01.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads02.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads03.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads04.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads05.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads06.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads07.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads08.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads09.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads10.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>" + ads11.ToString("0,0.00") + "</td>" +
                           "<td class='text-right'>0.00</td>" +
                           "<td class='text-right'>"+total.ToString("0,0.00")+"</td>"+
                           "<td class='text-right'>" + ads_dif_target.ToString("0,0.00") + "</td>"+
                           "<td class='text-right'>" + ads_sum_count.ToString("0,0.00") + "</td>";
            obj.Add(tm3);

            return obj;
        }
        public void sum_for_head() {

            using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {
                conn.Open();

                SqlCommand cmd = new SqlCommand("EXEC ads_result_head_type_stored @start_date = '2018-01-09', @end_date = '2018-01-09'", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {

                    head01 = Convert.ToDouble(rdr["head01"]).ToString("0,0.00");
                    head02 = Convert.ToDouble(rdr["head02"]).ToString("0,0.00");
                    head03 = Convert.ToDouble(rdr["head03"]).ToString("0,0.00");
                    head04 = Convert.ToDouble(rdr["head04"]).ToString("0,0.00");
                    head05 = Convert.ToDouble(rdr["head05"]).ToString("0,0.00");
                    head06 = Convert.ToDouble(rdr["head06"]).ToString("0,0.00");
                    head07 = Convert.ToDouble(rdr["head07"]).ToString("0,0.00");
                    total_head = Convert.ToDouble(rdr["total"]).ToString("0,0.00");

                }
                
                conn.Close();
            }
            
        }

        public List<ads_sales_book_det_h1_viewModel> select_data(string date_now , string date_old) {

            List<ads_sales_book_det_h1_viewModel> item = new List<ads_sales_book_det_h1_viewModel>();

            databaseClass_new dbl = new databaseClass_new();

           List<string> team = new List<string>();

            dbl.dbConnect();

            dbl.cmd = new SqlCommand("SELECT * FROM ads_team_tab WHERE team_code LIKE 'GP%' AND team_name NOT LIKE '%คุณสุรพล%'", dbl.conn);
            dbl.rdr = dbl.cmd.ExecuteReader();
            while (dbl.rdr.Read()) {

                team.Add(dbl.rdr["team_name"].ToString());

            }
            
            dbl.dbClosed();

            //

            foreach (string name in team) {

                List<string> team_id = new List<string>();
                List<string> h1_name = new List<string>();

                dbl.dbConnect();

                dbl.cmd = new SqlCommand("SELECT * FROM ads_team_tab WHERE team_name = '" + name + "' AND team_code NOT LIKE 'H%' ORDER BY team_code ASC", dbl.conn);
                dbl.rdr = dbl.cmd.ExecuteReader();
                while (dbl.rdr.Read()) {

                    team_id.Add(dbl.rdr["team_id"].ToString());
                    h1_name.Add(dbl.rdr["h1_name"].ToString());

                }

                dbl.dbClosed();


                ///
               int count = team_id.Count();


                for (int i = 0; i < count; i++)
                {
                    /// old

                    dbl.dbConnect();

                    dbl.cmd = new SqlCommand("SELECT empId, empName, team_name, h1_name, MIN(team_no) AS team_no, SUM(sales_amount) AS sales_amount, SUM(ads01) AS ads01, SUM(ads02) AS ads02, " +
                    " SUM(ads03) AS ads03, SUM(ads04) AS ads04, SUM(ads05) AS ads05, SUM(ads06) AS ads06, SUM(ads07) AS ads07, SUM(ads08) AS ads08, SUM(ads09) AS ads09, " +
                    " SUM(ads10) AS ads10, SUM(ads11) AS ads11" +
                    " FROM ( SELECT salesman.emp_id AS empId," +
                    " salesman.emp_name AS empName," +
                    " sales.ads_date," +
                    " team.team_name," +
                    " team.h1_name," +
                    " MIN(team.team_no) AS team_no," +
                    " ads_g.group_type_id," +
                    " ads_g.group_type_name," +
                    " SUM(sales.sales_amount) AS sales_amount," +
                    " CASE WHEN(ads_g.group_type_id = 1) THEN SUM(sales_amount) ELSE 0 END AS ads01, " +
                    " CASE WHEN(ads_g.group_type_id = 2) THEN SUM(sales_amount) ELSE 0 END AS ads02, " +
                    " CASE WHEN(ads_g.group_type_id = 3) THEN SUM(sales_amount) ELSE 0 END AS ads03, " +
                    " CASE WHEN(ads_g.group_type_id = 4) THEN SUM(sales_amount) ELSE 0 END AS ads04, " +
                    " CASE WHEN(ads_g.group_type_id = 5) THEN SUM(sales_amount) ELSE 0 END AS ads05, " +
                    " CASE WHEN(ads_g.group_type_id = 6) THEN SUM(sales_amount) ELSE 0 END AS ads06, " +
                    " CASE WHEN(ads_g.group_type_id = 7) THEN SUM(sales_amount) ELSE 0 END AS ads07, " +
                    " CASE WHEN(ads_g.group_type_id = 8) THEN SUM(sales_amount) ELSE 0 END AS ads08, " +
                    " CASE WHEN(ads_g.group_type_id = 9) THEN SUM(sales_amount) ELSE 0 END AS ads09, " +
                    " CASE WHEN(ads_g.group_type_id = 10) THEN SUM(sales_amount) ELSE 0 END AS ads10," +
                    " CASE WHEN(ads_g.group_type_id = 11) THEN SUM(sales_amount) ELSE 0 END AS ads11 " +
                    " FROM   dbo.ads_sales_det_tab AS sales" +
                    " LEFT JOIN dbo.ads_saleman_tab AS salesman ON sales.emp_id = salesman.emp_id" +
                    " LEFT JOIN dbo.ads_type_det_tab AS ads_t ON sales.book_code = ads_t.book_code AND sales.ads_type_code = ads_t.ads_type_code" +
                    " LEFT JOIN dbo.ads_group_type_tab AS ads_g ON ads_t.group_type_id = ads_g.group_type_id" +
                    " LEFT JOIN dbo.ads_team_tab AS team ON sales.sale_team_id = team.team_id" +
                   " WHERE sales.sales_status = 'Y' AND sales.in_date = '" + date_old + "' AND sales.sale_team_id = '" + team_id[i] + "'" +
                    " GROUP BY  salesman.emp_id, sales.ads_date, team.team_name, team.h1_name, ads_g.group_type_id, ads_g.group_type_name, salesman.emp_name" +
                    " ) AS tmp" +
                   " GROUP BY empId, team_name, h1_name, empName" +
                   " ORDER BY team_no, team_name, h1_name", dbl.conn);
                    
                    dbl.rdr = dbl.cmd.ExecuteReader();
                    if (dbl.rdr.Read() == true)
                    {
                        amount_sale_old = Convert.ToDouble(dbl.rdr["sales_amount"]);

                    }
                    else {

                        amount_sale_old = 0;

                    }


                    dbl.dbClosed();

                    /// now



                    dbl.dbConnect();

                    dbl.cmd = new SqlCommand("SELECT empId, empName, team_name, h1_name, MIN(team_no) AS team_no, SUM(sales_amount) AS sales_amount, SUM(ads01) AS ads01, SUM(ads02) AS ads02, "+
                     " SUM(ads03) AS ads03, SUM(ads04) AS ads04, SUM(ads05) AS ads05, SUM(ads06) AS ads06, SUM(ads07) AS ads07, SUM(ads08) AS ads08, SUM(ads09) AS ads09, " +
                     " SUM(ads10) AS ads10, SUM(ads11) AS ads11" +
                     " FROM ( SELECT salesman.emp_id AS empId," +
                     " salesman.emp_name AS empName," +
                     " sales.ads_date," +
                     " team.team_name," +
                     " team.h1_name," +
                     " MIN(team.team_no) AS team_no," +
                     " ads_g.group_type_id," +
                     " ads_g.group_type_name," +
                     " SUM(sales.sales_amount) AS sales_amount," +
                     " CASE WHEN(ads_g.group_type_id = 1) THEN SUM(sales_amount) ELSE 0 END AS ads01, " +
                     " CASE WHEN(ads_g.group_type_id = 2) THEN SUM(sales_amount) ELSE 0 END AS ads02, " +
                     " CASE WHEN(ads_g.group_type_id = 3) THEN SUM(sales_amount) ELSE 0 END AS ads03, " +
                     " CASE WHEN(ads_g.group_type_id = 4) THEN SUM(sales_amount) ELSE 0 END AS ads04, " +
                     " CASE WHEN(ads_g.group_type_id = 5) THEN SUM(sales_amount) ELSE 0 END AS ads05, " +
                     " CASE WHEN(ads_g.group_type_id = 6) THEN SUM(sales_amount) ELSE 0 END AS ads06, " +
                     " CASE WHEN(ads_g.group_type_id = 7) THEN SUM(sales_amount) ELSE 0 END AS ads07, " +
                     " CASE WHEN(ads_g.group_type_id = 8) THEN SUM(sales_amount) ELSE 0 END AS ads08, " +
                     " CASE WHEN(ads_g.group_type_id = 9) THEN SUM(sales_amount) ELSE 0 END AS ads09, " +
                     " CASE WHEN(ads_g.group_type_id = 10) THEN SUM(sales_amount) ELSE 0 END AS ads10," +
                     " CASE WHEN(ads_g.group_type_id = 11) THEN SUM(sales_amount) ELSE 0 END AS ads11 " +
                     " FROM   dbo.ads_sales_det_tab AS sales" +
                     " LEFT JOIN dbo.ads_saleman_tab AS salesman ON sales.emp_id = salesman.emp_id" +
                     " LEFT JOIN dbo.ads_type_det_tab AS ads_t ON sales.book_code = ads_t.book_code AND sales.ads_type_code = ads_t.ads_type_code" +
                     " LEFT JOIN dbo.ads_group_type_tab AS ads_g ON ads_t.group_type_id = ads_g.group_type_id" +
                     " LEFT JOIN dbo.ads_team_tab AS team ON sales.sale_team_id = team.team_id" +
                    " WHERE sales.sales_status = 'Y' AND sales.in_date = '"+date_now+"' AND sales.sale_team_id = '" + team_id[i]+"'" +
                     " GROUP BY  salesman.emp_id, sales.ads_date, team.team_name, team.h1_name, ads_g.group_type_id, ads_g.group_type_name, salesman.emp_name" +
                     " ) AS tmp" +
                    " GROUP BY empId, team_name, h1_name, empName" +
                    " ORDER BY team_no, team_name, h1_name", dbl.conn);

                    dbl.rdr = dbl.cmd.ExecuteReader();

                    ads_sales_book_det_h1_viewModel det = new ads_sales_book_det_h1_viewModel();

                    if (i == 0)
                    {

                        if (dbl.rdr.Read() == true)
                        {

                            det.team_name = dbl.rdr["team_name"].ToString();
                            det.ads01 = Convert.ToDouble(dbl.rdr["ads01"]);
                            det.ads02 = Convert.ToDouble(dbl.rdr["ads02"]);
                            det.ads03 = Convert.ToDouble(dbl.rdr["ads03"]);
                            det.ads04 = Convert.ToDouble(dbl.rdr["ads04"]);
                            det.ads05 = Convert.ToDouble(dbl.rdr["ads05"]);
                            det.ads06 = Convert.ToDouble(dbl.rdr["ads06"]);
                            det.ads07 = Convert.ToDouble(dbl.rdr["ads07"]);
                            det.ads08 = Convert.ToDouble(dbl.rdr["ads08"]);
                            det.ads09 = Convert.ToDouble(dbl.rdr["ads09"]);
                            det.ads10 = Convert.ToDouble(dbl.rdr["ads10"]);
                            det.ads11 = Convert.ToDouble(dbl.rdr["ads11"]);
                            det.sales_amount = Convert.ToDouble(dbl.rdr["sales_amount"]).ToString();
                            det.dif_amount = (Convert.ToDouble(dbl.rdr["sales_amount"]) - amount_sale_old).ToString();

                            sum_ads01 += Convert.ToDouble(dbl.rdr["ads01"]);
                            sum_ads02 += Convert.ToDouble(dbl.rdr["ads02"]);
                            sum_ads03 += Convert.ToDouble(dbl.rdr["ads03"]);
                            sum_ads04 += Convert.ToDouble(dbl.rdr["ads04"]);
                            sum_ads05 += Convert.ToDouble(dbl.rdr["ads05"]);
                            sum_ads06 += Convert.ToDouble(dbl.rdr["ads06"]);
                            sum_ads07 += Convert.ToDouble(dbl.rdr["ads07"]);
                            sum_ads08 += Convert.ToDouble(dbl.rdr["ads08"]);
                            sum_ads09 += Convert.ToDouble(dbl.rdr["ads09"]);
                            sum_ads10 += Convert.ToDouble(dbl.rdr["ads10"]);
                            sum_ads11 += Convert.ToDouble(dbl.rdr["ads11"]);
                            sum_amount += Convert.ToDouble(dbl.rdr["sales_amount"]);
                            sum_dif_amount += (Convert.ToDouble(dbl.rdr["sales_amount"]) - amount_sale_old);


                            total_sum_ads01 += Convert.ToDouble(dbl.rdr["ads01"]);
                            total_sum_ads02 += Convert.ToDouble(dbl.rdr["ads02"]);
                            total_sum_ads03 += Convert.ToDouble(dbl.rdr["ads03"]);
                            total_sum_ads04 += Convert.ToDouble(dbl.rdr["ads04"]);
                            total_sum_ads05 += Convert.ToDouble(dbl.rdr["ads05"]);
                            total_sum_ads06 += Convert.ToDouble(dbl.rdr["ads06"]);
                            total_sum_ads07 += Convert.ToDouble(dbl.rdr["ads07"]);
                            total_sum_ads08 += Convert.ToDouble(dbl.rdr["ads08"]);
                            total_sum_ads09 += Convert.ToDouble(dbl.rdr["ads09"]);
                            total_sum_ads10 += Convert.ToDouble(dbl.rdr["ads10"]);
                            total_sum_ads11 += Convert.ToDouble(dbl.rdr["ads11"]);
                            total_sum_amount += Convert.ToDouble(dbl.rdr["sales_amount"]);
                            total_sum_dif_amount += (Convert.ToDouble(dbl.rdr["sales_amount"]) - amount_sale_old);

                        }
                        else
                        {

                            det.team_name = name;
                            det.ads01 = 0;
                            det.ads02 = 0;
                            det.ads03 = 0;
                            det.ads04 = 0;
                            det.ads05 = 0;
                            det.ads06 = 0;
                            det.ads07 = 0;
                            det.ads08 = 0;
                            det.ads09 = 0;
                            det.ads10 = 0;
                            det.ads11 = 0;
                            det.sales_amount = "0.00";
                            det.dif_amount = (Convert.ToDouble("0") - amount_sale_old).ToString();

            

                        }

                        item.Add(det);
                    }
                    else
                    {

                        if (dbl.rdr.Read() == true)
                        {

                            det.team_name = "&nbsp;&nbsp;&nbsp;" + dbl.rdr["h1_name"].ToString();
                            det.ads01 = Convert.ToDouble(dbl.rdr["ads01"]);
                            det.ads02 = Convert.ToDouble(dbl.rdr["ads02"]);
                            det.ads03 = Convert.ToDouble(dbl.rdr["ads03"]);
                            det.ads04 = Convert.ToDouble(dbl.rdr["ads04"]);
                            det.ads05 = Convert.ToDouble(dbl.rdr["ads05"]);
                            det.ads06 = Convert.ToDouble(dbl.rdr["ads06"]);
                            det.ads07 = Convert.ToDouble(dbl.rdr["ads07"]);
                            det.ads08 = Convert.ToDouble(dbl.rdr["ads08"]);
                            det.ads09 = Convert.ToDouble(dbl.rdr["ads09"]);
                            det.ads10 = Convert.ToDouble(dbl.rdr["ads10"]);
                            det.ads11 = Convert.ToDouble(dbl.rdr["ads11"]);
                            det.sales_amount = Convert.ToDouble(dbl.rdr["sales_amount"]).ToString();
                            det.dif_amount = (Convert.ToDouble(dbl.rdr["sales_amount"]) - amount_sale_old).ToString();


                            sum_ads01 += Convert.ToDouble(dbl.rdr["ads01"]);
                            sum_ads02 += Convert.ToDouble(dbl.rdr["ads02"]);
                            sum_ads03 += Convert.ToDouble(dbl.rdr["ads03"]);
                            sum_ads04 += Convert.ToDouble(dbl.rdr["ads04"]);
                            sum_ads05 += Convert.ToDouble(dbl.rdr["ads05"]);
                            sum_ads06 += Convert.ToDouble(dbl.rdr["ads06"]);
                            sum_ads07 += Convert.ToDouble(dbl.rdr["ads07"]);
                            sum_ads08 += Convert.ToDouble(dbl.rdr["ads08"]);
                            sum_ads09 += Convert.ToDouble(dbl.rdr["ads09"]);
                            sum_ads10 += Convert.ToDouble(dbl.rdr["ads10"]);
                            sum_ads11 += Convert.ToDouble(dbl.rdr["ads11"]);
                            sum_amount += Convert.ToDouble(dbl.rdr["sales_amount"]);
                            sum_dif_amount += (Convert.ToDouble(dbl.rdr["sales_amount"]) - amount_sale_old);

                            total_sum_ads01 += Convert.ToDouble(dbl.rdr["ads01"]);
                            total_sum_ads02 += Convert.ToDouble(dbl.rdr["ads02"]);
                            total_sum_ads03 += Convert.ToDouble(dbl.rdr["ads03"]);
                            total_sum_ads04 += Convert.ToDouble(dbl.rdr["ads04"]);
                            total_sum_ads05 += Convert.ToDouble(dbl.rdr["ads05"]);
                            total_sum_ads06 += Convert.ToDouble(dbl.rdr["ads06"]);
                            total_sum_ads07 += Convert.ToDouble(dbl.rdr["ads07"]);
                            total_sum_ads08 += Convert.ToDouble(dbl.rdr["ads08"]);
                            total_sum_ads09 += Convert.ToDouble(dbl.rdr["ads09"]);
                            total_sum_ads10 += Convert.ToDouble(dbl.rdr["ads10"]);
                            total_sum_ads11 += Convert.ToDouble(dbl.rdr["ads11"]);
                            total_sum_amount += Convert.ToDouble(dbl.rdr["sales_amount"]);
                            total_sum_dif_amount += (Convert.ToDouble(dbl.rdr["sales_amount"]) - amount_sale_old);

                        }
                        else
                        {
                            det.team_name = "&nbsp;&nbsp;&nbsp;" + h1_name[i];
                            det.ads01 = 0;
                            det.ads02 = 0;
                            det.ads03 = 0;
                            det.ads04 = 0;
                            det.ads05 = 0;
                            det.ads06 = 0;
                            det.ads07 = 0;
                            det.ads08 = 0;
                            det.ads09 = 0;
                            det.ads10 = 0;
                            det.ads11 = 0;
                            det.sales_amount = "0.00";
                            det.dif_amount = ((0) - amount_sale_old).ToString();

                        }

                        item.Add(det);

                    }
                    
                    dbl.dbClosed();

                   

                  


                }
                ads_sales_book_det_h1_viewModel dt = new ads_sales_book_det_h1_viewModel();
                dt.team_name = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; รวม";
                dt.ads01 = sum_ads01;
                dt.ads02 = sum_ads02;
                dt.ads03 = sum_ads03;
                dt.ads04 = sum_ads04;
                dt.ads05 = sum_ads05;
                dt.ads06 = sum_ads06;
                dt.ads07 = sum_ads07;
                dt.ads08 = sum_ads08;
                dt.ads09 = sum_ads09;
                dt.ads10 = sum_ads10;
                dt.ads11 = sum_ads11;
                dt.sales_amount = sum_amount.ToString();
                dt.dif_amount = sum_dif_amount.ToString();

                item.Add(dt);

                sum_ads01 = 0;
                sum_ads02 = 0;
                sum_ads03 = 0;
                sum_ads04 = 0;
                sum_ads05 = 0;
                sum_ads06 = 0;
                sum_ads07 = 0;
                sum_ads08 = 0;
                sum_ads09 = 0;
                sum_ads10 = 0;
                sum_ads11 = 0;
                sum_amount = 0;
                sum_dif_amount = 0;


            }

            ads_sales_book_det_h1_viewModel dt2 = new ads_sales_book_det_h1_viewModel();
            dt2.team_name = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; รวมทั้งสิ้น";
            dt2.ads01 =  total_sum_ads01;
            dt2.ads02 =  total_sum_ads02;
            dt2.ads03 =  total_sum_ads03;
            dt2.ads04 =  total_sum_ads04;
            dt2.ads05 =  total_sum_ads05;
            dt2.ads06 =  total_sum_ads06;
            dt2.ads07 =  total_sum_ads07;
            dt2.ads08 =  total_sum_ads08;
            dt2.ads09 =  total_sum_ads09;
            dt2.ads10 =  total_sum_ads10;
            dt2.ads11 =  total_sum_ads11;
            dt2.sales_amount = total_sum_amount.ToString();
            dt2.dif_amount = total_sum_dif_amount.ToString();

            item.Add(dt2);
            
            return item;
        }
    }
}
