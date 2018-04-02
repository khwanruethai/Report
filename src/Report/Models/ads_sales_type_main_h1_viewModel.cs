using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Models
{
    public class ads_sales_type_main_h1_viewModel
    {
        public string empId { get; set; }
        public string empName { get; set; }
        public string team_name { get; set; }
        public string h1_name { get; set; }
        public string team_no { get; set; }
        public string sales_amount { get; set; }
        public string ads01 { get; set; }
        public string ads02 { get; set; }
        public string ads03 { get; set; }
        public string ads04 { get; set; }
        public string ads05 { get; set; }
        public string dif { get; set; }


        public double sum_ads01 { get; set; }
        public double sum_ads02 { get; set; }
        public double sum_ads03 { get; set; }
        public double sum_ads04 { get; set; }
        public double sum_ads05 { get; set; }
        public double sum_amount { get; set; }
        public double amount_old { get; set; }
        public double sum_amount_old { get; set; }
        public double sum_dif { get; set; }

        public double total_sum_ads01 { get; set; }
        public double total_sum_ads02 { get; set; }
        public double total_sum_ads03 { get; set; }
        public double total_sum_ads04 { get; set; }
        public double total_sum_ads05 { get; set; }
        public double total_sum_amount { get; set; }
        public double total_amount_old { get; set; }
        public double total_sum_amount_old { get; set; }
        public double total_sum_dif { get; set; }


        public List<string> team_no_list { get; set; }
        public List<string> team_name_list { get; set; }


        public string old_date { get; set; }
        public string now_date { get; set; }
        public string select_date { get; set; }

        dbFile db = new dbFile();

        CultureInfo en = new CultureInfo("EN");

    
        public void select_now_date()
        {

            databaseClass_new dbl = new databaseClass_new();

            dbl.dbConnect();

            dbl.cmd = new SqlCommand("SELECT TOP 1 in_date AS today FROM ads_sales_book_det_h1_view  GROUP BY in_date order by in_date DESC", dbl.conn);
            dbl.rdr = dbl.cmd.ExecuteReader();
            while (dbl.rdr.Read())
            {

                now_date = Convert.ToDateTime(dbl.rdr["today"]).ToString("yyyy-MM-dd", en);

            }

            dbl.dbClosed();

        }
        public void select_old_date() {

            databaseClass_new dbl = new databaseClass_new();

            dbl.dbConnect();

            dbl.cmd = new SqlCommand("SELECT TOP 1 in_date AS old FROM ads_sales_book_det_h1_view WHERE in_date < '"+now_date+"' GROUP BY in_date order by in_date DESC", dbl.conn);
            dbl.rdr = dbl.cmd.ExecuteReader();
            while (dbl.rdr.Read()) {

                old_date = Convert.ToDateTime(dbl.rdr["old"]).ToString("yyyy-MM-dd", en);

            }

            dbl.dbClosed();

        }
        
        public List<ads_sales_type_main_h1_viewModel> detail2()
        {

            select_now_date();
            select_old_date();


            List<ads_sales_type_main_h1_viewModel> item = new List<ads_sales_type_main_h1_viewModel>();
            databaseClass_new dbl = new databaseClass_new();

            List<string> team_name = new List<string>();

            dbl.dbConnect();

            dbl.cmd = new SqlCommand("SELECT * FROM ads_team_tab WHERE team_code LIKE 'GP%' AND team_name NOT LIKE '%คุณสุรพล%'", dbl.conn);
            dbl.rdr = dbl.cmd.ExecuteReader();
            while (dbl.rdr.Read())
            {
                
                team_name.Add(dbl.rdr["team_name"].ToString());

            }

            dbl.dbClosed();

            ///

           
            foreach (string txt in team_name)
            {
              

                List<string> team_id = new List<string>();
                List<string> h1_name = new List<string>();


                dbl.dbConnect();
                dbl.cmd = new SqlCommand("SELECT * FROM ads_team_tab WHERE team_name = '"+txt+ "' AND team_code NOT LIKE 'H%' ORDER BY team_code ASC", dbl.conn);
                dbl.rdr = dbl.cmd.ExecuteReader();
                while (dbl.rdr.Read())
                {
                    
                    team_id.Add(dbl.rdr["team_id"].ToString());
                    h1_name.Add(dbl.rdr["h1_name"].ToString());

                }

                dbl.dbClosed();


                ///
                int count = team_id.Count();


                for (int i = 0; i < count; i++)
                {


                    using (SqlConnection conn = new SqlConnection(db.sqlConnection)) {

                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT  team_name, h1_name, MIN(team_no) AS team_no, SUM(sales_amount) AS sales_amount, SUM(ads01) AS ads01, SUM(ads02) AS ads02, " +
                         " SUM(ads03) AS ads03, SUM(ads04) AS ads04, SUM(ads05) AS ads05" +
                         " FROM(SELECT sales.ads_date," +
                         " team.team_name," +
                         " team.h1_name," +
                         " MIN(team.team_no) AS team_no," +
                         " ads_g.group_type_id," +
                         " ads_g.group_type_name," +
                         " SUM(sales.sales_amount) AS sales_amount," +
                         " CASE WHEN(sales.type_ads = 1) THEN SUM(sales_amount) ELSE 0 END AS ads01," +
                         " CASE WHEN(sales.type_ads = 2) THEN SUM(sales_amount) ELSE 0 END AS ads02," +
                         " CASE WHEN(sales.type_ads = 3) THEN SUM(sales_amount) ELSE 0 END AS ads03," +
                         " CASE WHEN(sales.type_ads = 4) THEN SUM(sales_amount) ELSE 0 END AS ads04," +
                         " CASE WHEN(sales.type_ads = 5) THEN SUM(sales_amount) ELSE 0 END AS ads05 " +
                         " FROM   dbo.ads_sales_det_tab AS sales" +
                         " INNER JOIN dbo.ads_saleman_tab AS salesman ON sales.emp_id = salesman.emp_id" +
                         " INNER JOIN dbo.ads_type_det_tab AS ads_t ON sales.book_code = ads_t.book_code AND sales.ads_type_code = ads_t.ads_type_code" +
                         " INNER JOIN dbo.ads_group_type_tab AS ads_g ON ads_t.group_type_id = ads_g.group_type_id" +
                         " INNER JOIN dbo.ads_team_tab AS team ON sales.sale_team_id = team.team_id" +
                         " WHERE  sales.sales_status = 'Y' AND  sales.sale_team_id = '" + team_id[i] + "' AND sales.in_date = '" + old_date + "'" +
                         " GROUP BY  sales.ads_date, team.team_name, team.h1_name, ads_g.group_type_id, ads_g.group_type_name, sales.type_ads" +
                         " ) AS tmp" +
                         " GROUP BY  team_name, h1_name" +
                         " ORDER BY team_no, team_name, h1_name", conn);

                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read() == true)
                        {

                            amount_old = Convert.ToDouble(rdr["sales_amount"]);

                        }
                        else {

                            amount_old = 0;


                        }


                    }

                    using (SqlConnection conn = new SqlConnection(db.sqlConnection))
                    {

                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT  team_name, h1_name, MIN(team_no) AS team_no, SUM(sales_amount) AS sales_amount, SUM(ads01) AS ads01, SUM(ads02) AS ads02, " +
                         " SUM(ads03) AS ads03, SUM(ads04) AS ads04, SUM(ads05) AS ads05" +
                         " FROM(SELECT sales.ads_date," +
                         " team.team_name," +
                         " team.h1_name," +
                         " MIN(team.team_no) AS team_no," +
                         " ads_g.group_type_id," +
                         " ads_g.group_type_name," +
                         " SUM(sales.sales_amount) AS sales_amount," +
                         " CASE WHEN(sales.type_ads = 1) THEN SUM(sales_amount) ELSE 0 END AS ads01," +
                         " CASE WHEN(sales.type_ads = 2) THEN SUM(sales_amount) ELSE 0 END AS ads02," +
                         " CASE WHEN(sales.type_ads = 3) THEN SUM(sales_amount) ELSE 0 END AS ads03," +
                         " CASE WHEN(sales.type_ads = 4) THEN SUM(sales_amount) ELSE 0 END AS ads04," +
                         " CASE WHEN(sales.type_ads = 5) THEN SUM(sales_amount) ELSE 0 END AS ads05 " +
                         " FROM   dbo.ads_sales_det_tab AS sales" +
                         " INNER JOIN dbo.ads_saleman_tab AS salesman ON sales.emp_id = salesman.emp_id" +
                         " INNER JOIN dbo.ads_type_det_tab AS ads_t ON sales.book_code = ads_t.book_code AND sales.ads_type_code = ads_t.ads_type_code" +
                         " INNER JOIN dbo.ads_group_type_tab AS ads_g ON ads_t.group_type_id = ads_g.group_type_id" +
                         " INNER JOIN dbo.ads_team_tab AS team ON sales.sale_team_id = team.team_id" +
                         " WHERE  sales.sales_status = 'Y' AND  sales.sale_team_id = '" + team_id[i] + "' AND sales.in_date = '" + now_date + "'" +
                         " GROUP BY  sales.ads_date, team.team_name, team.h1_name, ads_g.group_type_id, ads_g.group_type_name, sales.type_ads" +
                         " ) AS tmp" +
                         " GROUP BY  team_name, h1_name" +
                         " ORDER BY team_no, team_name, h1_name", conn);

                        SqlDataReader rdr = cmd.ExecuteReader();


                        if (i == 0)
                        {
                            ads_sales_type_main_h1_viewModel main = new ads_sales_type_main_h1_viewModel();

                            if (rdr.Read() == true)
                            {

                                main.empName = rdr["h1_name"].ToString();
                                main.team_name = rdr["team_name"].ToString();
                                main.h1_name = rdr["h1_name"].ToString();
                                main.team_no = rdr["team_no"].ToString();
                                if (rdr["sales_amount"].ToString() == "0")
                                {
                                    main.sales_amount = Convert.ToDouble(rdr["sales_amount"]).ToString("0.00");
                                }
                                else
                                {
                                    main.sales_amount = Convert.ToDouble(rdr["sales_amount"]).ToString("0,0.00");
                                }

                                if (rdr["ads01"].ToString() == "0")
                                {
                                    main.ads01 = Convert.ToDouble(rdr["ads01"]).ToString("0.00");
                                }
                                else {
                                    main.ads01 = Convert.ToDouble(rdr["ads01"]).ToString("0,0.00");

                                }

                                if (rdr["ads02"].ToString() == "0")
                                {
                                    main.ads02 = Convert.ToDouble(rdr["ads02"]).ToString("0.00");
                                }
                                else {

                                    main.ads02 = Convert.ToDouble(rdr["ads02"]).ToString("0,0.00");
                                }

                                if (rdr["ads03"].ToString() == "0")
                                {
                                    main.ads03 = Convert.ToDouble(rdr["ads03"]).ToString("0.00");
                                }
                                else {
                                    main.ads03 = Convert.ToDouble(rdr["ads03"]).ToString("0,0.00");
                                }

                                if (rdr["ads04"].ToString() == "0")
                                {
                                    main.ads04 = Convert.ToDouble(rdr["ads04"]).ToString("0.00");
                                }
                                else {

                                    main.ads04 = Convert.ToDouble(rdr["ads04"]).ToString("0,0.00");
                                }

                                if (rdr["ads05"].ToString() == "0")
                                {
                                    main.ads05 = Convert.ToDouble(rdr["ads05"]).ToString("0.00");
                                }
                                else {

                                    main.ads05 = Convert.ToDouble(rdr["ads05"]).ToString("0,0.00");
                                }

                                if (((Convert.ToDouble(main.sales_amount)) - (amount_old)) == 0)
                                {
                                    main.dif = ((Convert.ToDouble(main.sales_amount)) - (amount_old)).ToString("0.00");

                                }
                                else
                                {

                                    main.dif = ((Convert.ToDouble(main.sales_amount)) - (amount_old)).ToString("0,0.00");

                                }

                                sum_ads01 += Convert.ToDouble(rdr["ads01"]);
                                sum_ads02 += Convert.ToDouble(rdr["ads02"]);
                                sum_ads03 += Convert.ToDouble(rdr["ads03"]);
                                sum_ads04 += Convert.ToDouble(rdr["ads04"]);
                                sum_ads05 += Convert.ToDouble(rdr["ads05"]);
                                sum_amount += Convert.ToDouble(rdr["sales_amount"]);
                                sum_dif += (Convert.ToDouble(rdr["sales_amount"]) - amount_old);

                               total_sum_ads01 += Convert.ToDouble(rdr["ads01"]);
                               total_sum_ads02 += Convert.ToDouble(rdr["ads02"]);
                               total_sum_ads03 += Convert.ToDouble(rdr["ads03"]);
                               total_sum_ads04 += Convert.ToDouble(rdr["ads04"]);
                               total_sum_ads05 += Convert.ToDouble(rdr["ads05"]);
                               total_sum_amount += Convert.ToDouble(rdr["sales_amount"]);
                               total_sum_dif += (Convert.ToDouble(rdr["sales_amount"]) - amount_old);



                            }
                            else
                            {

                                main.team_name = txt;
                                main.sales_amount = "0.00";
                                main.ads01 = "0.00";
                                main.ads02 = "0.00";
                                main.ads03 = "0.00";
                                main.ads04 = "0.00";
                                main.ads05 = "0.00";
                                main.dif = (0 - amount_old).ToString("0.00");


                            }


                            item.Add(main);
                        }
                        else {

                            ads_sales_type_main_h1_viewModel main = new ads_sales_type_main_h1_viewModel();

                            if (rdr.Read() == true)
                            {

                                main.empName = rdr["h1_name"].ToString();
                                main.team_name = "&nbsp;&nbsp;&nbsp;" + rdr["h1_name"].ToString();
                                main.h1_name = rdr["h1_name"].ToString();
                                main.team_no = rdr["team_no"].ToString();
                                if (rdr["sales_amount"].ToString() == "0")
                                {
                                    main.sales_amount = Convert.ToDouble(rdr["sales_amount"]).ToString("0.00");
                                }
                                else
                                {
                                    main.sales_amount = Convert.ToDouble(rdr["sales_amount"]).ToString("0,0.00");
                                }

                                if (rdr["ads01"].ToString() == "0")
                                {
                                    main.ads01 = Convert.ToDouble(rdr["ads01"]).ToString("0.00");
                                }
                                else
                                {
                                    main.ads01 = Convert.ToDouble(rdr["ads01"]).ToString("0,0.00");

                                }

                                if (rdr["ads02"].ToString() == "0")
                                {
                                    main.ads02 = Convert.ToDouble(rdr["ads02"]).ToString("0.00");
                                }
                                else
                                {

                                    main.ads02 = Convert.ToDouble(rdr["ads02"]).ToString("0,0.00");
                                }

                                if (rdr["ads03"].ToString() == "0")
                                {
                                    main.ads03 = Convert.ToDouble(rdr["ads03"]).ToString("0.00");
                                }
                                else
                                {
                                    main.ads03 = Convert.ToDouble(rdr["ads03"]).ToString("0,0.00");
                                }

                                if (rdr["ads04"].ToString() == "0")
                                {
                                    main.ads04 = Convert.ToDouble(rdr["ads04"]).ToString("0.00");
                                }
                                else
                                {

                                    main.ads04 = Convert.ToDouble(rdr["ads04"]).ToString("0,0.00");
                                }

                                if (rdr["ads05"].ToString() == "0")
                                {
                                    main.ads05 = Convert.ToDouble(rdr["ads05"]).ToString("0.00");
                                }
                                else
                                {

                                    main.ads05 = Convert.ToDouble(rdr["ads05"]).ToString("0,0.00");
                                }

                                if (((Convert.ToDouble(main.sales_amount)) - (amount_old)) == 0)
                                {
                                    main.dif = ((Convert.ToDouble(main.sales_amount)) - (amount_old)).ToString("0.00");

                                }
                                else
                                {

                                    main.dif = ((Convert.ToDouble(main.sales_amount)) - (amount_old)).ToString("0,0.00");

                                }
                                if ((Convert.ToDouble(rdr["sales_amount"]) - amount_old) == 0)
                                {
                                    main.dif = (Convert.ToDouble(rdr["sales_amount"]) - amount_old).ToString("0.00");

                                }
                                else
                                {

                                    main.dif = (Convert.ToDouble(rdr["sales_amount"]) - amount_old).ToString("0,0.00");
                                }

                                sum_ads01 += Convert.ToDouble(rdr["ads01"]);
                                sum_ads02 += Convert.ToDouble(rdr["ads02"]);
                                sum_ads03 += Convert.ToDouble(rdr["ads03"]);
                                sum_ads04 += Convert.ToDouble(rdr["ads04"]);
                                sum_ads05 += Convert.ToDouble(rdr["ads05"]);
                                sum_amount += Convert.ToDouble(rdr["sales_amount"]);
                                sum_dif += (sum_amount - amount_old);

                                total_sum_ads01 += Convert.ToDouble(rdr["ads01"]);
                                total_sum_ads02 += Convert.ToDouble(rdr["ads02"]);
                                total_sum_ads03 += Convert.ToDouble(rdr["ads03"]);
                                total_sum_ads04 += Convert.ToDouble(rdr["ads04"]);
                                total_sum_ads05 += Convert.ToDouble(rdr["ads05"]);
                                total_sum_amount += Convert.ToDouble(rdr["sales_amount"]);
                                total_sum_dif += (Convert.ToDouble(rdr["sales_amount"]) - amount_old);

                            }
                            else
                            {

                                main.team_name = "&nbsp;&nbsp;&nbsp;" + h1_name[i];
                                main.sales_amount = "0.00";
                                main.ads01 = "0.00";
                                main.ads02 = "0.00";
                                main.ads03 = "0.00";
                                main.ads04 = "0.00";
                                main.ads05 = "0.00";

                                if ((0 - amount_old) == 0)
                                {
                                    main.dif = Convert.ToDouble(0 - amount_old).ToString("0.00");

                                }
                                else {
                                    main.dif = Convert.ToDouble(0 - amount_old).ToString("0,0.00");
                                }

                             
                                
                            }

                            item.Add(main);
                        }

                        conn.Close();

                    }
                        
             }


                ads_sales_type_main_h1_viewModel result = new ads_sales_type_main_h1_viewModel();
                result.team_name = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;รวม";
                if (sum_amount == 0)
                {
                    result.sales_amount = sum_amount.ToString("0.00");
                }
                else {

                    result.sales_amount = sum_amount.ToString("0,0.00");
                }

                if (sum_ads01 == 0)
                {
                    result.ads01 = sum_ads01.ToString("0.00");
                }
                else {
                    result.ads01 = sum_ads01.ToString("0,0.00");

                }

                if (sum_ads02 == 0)
                {
                    result.ads02 = sum_ads02.ToString("0.00");
                }
                else {
                    result.ads02 = sum_ads02.ToString("0,0.00");
                }

                if (sum_ads03 == 0)
                {
                    result.ads03 = sum_ads03.ToString("0.00");
                }
                else {

                    result.ads03 = sum_ads03.ToString("0,0.00");
                }

                if (sum_ads04 == 0)
                {
                    result.ads04 = sum_ads04.ToString("0.00");
                }
                else {
                    result.ads04 = sum_ads04.ToString("0,0.00");

                }

                if (sum_ads05 == 0)
                {
                    result.ads05 = sum_ads05.ToString("0.00");
                }
                else {
                    result.ads05 = sum_ads05.ToString("0,0.00");

                }

                if (sum_dif == 0)
                {

                    result.dif = sum_dif.ToString("0.00");
                }
                else
                {

                    result.dif = sum_dif.ToString("0,0.00");
                }

                item.Add(result);

                sum_amount = 0;
                sum_ads01 = 0;
                sum_ads02 = 0;
                sum_ads03 = 0;
                sum_ads04 = 0;
                sum_ads05 = 0;
                sum_dif = 0;
            }

            ads_sales_type_main_h1_viewModel total = new ads_sales_type_main_h1_viewModel();
            total.team_name = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;รวมทั้งสิ้น";
            if (total_sum_amount == 0)
            {
                total.sales_amount = total_sum_amount.ToString("0.00");
            }
            else
            {

                total.sales_amount = total_sum_amount.ToString("0,0.00");
            }

            if (total_sum_ads01 == 0)
            {
                total.ads01 = total_sum_ads01.ToString("0.00");
            }
            else
            {
                total.ads01 = total_sum_ads01.ToString("0,0.00");

            }

            if (total_sum_ads02 == 0)
            {
                total.ads02 = total_sum_ads02.ToString("0.00");
            }
            else
            {
                total.ads02 = total_sum_ads02.ToString("0,0.00");
            }

            if (total_sum_ads03 == 0)
            {
                total.ads03 = total_sum_ads03.ToString("0.00");
            }
            else
            {

                total.ads03 = total_sum_ads03.ToString("0,0.00");
            }

            if (total_sum_ads04 == 0)
            {
                total.ads04 = total_sum_ads04.ToString("0.00");
            }
            else
            {
                total.ads04 = total_sum_ads04.ToString("0,0.00");

            }

            if (total_sum_ads05 == 0)
            {
                total.ads05 = total_sum_ads05.ToString("0.00");
            }
            else
            {
                total.ads05 = total_sum_ads05.ToString("0,0.00");

            }

            if (total_sum_dif == 0)
            {

                total.dif = total_sum_dif.ToString("0.00");
            }
            else
            {

                total.dif = total_sum_dif.ToString("0,0.00");
            }

            item.Add(total);


            return item;


        }
           
    }
}
