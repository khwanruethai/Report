using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Report.Models;
namespace Report.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            summary_dif_sales_ads_of_day();
            summary_dif_salse_ads_of_month();
          
            return View();
        }
        public void summary_dif_salse_ads_of_month() {

            result_amount_of_day_stroredModel am = new result_amount_of_day_stroredModel();

            am.sum_amount_before("2018-01-01", "2018-01-31");
            am.sum_amount_present("2018-02-01", "2018-02-28");
            am.calculate_Sale_dif_per();


            if (am.sale_dif_percent < 0)
            {
                ViewData["sales_color"] = "text-red";
                ViewData["sales_percen"] = Convert.ToDouble(am.sale_dif_percent).ToString("0.0");
                ViewData["sales_amount"] = Convert.ToDouble(am.result_present).ToString("0,0.00");
                ViewData["sales_caret"] = "fa-caret-down";

            }
            else
            {

                ViewData["sales_color"] = "text-green";
                ViewData["sales_percen"] = Convert.ToDouble(am.sale_dif_percent).ToString("0.0");
                ViewData["sales_amount"] = Convert.ToDouble(am.result_present).ToString("0,0.00");
                ViewData["sales_caret"] = "fa-caret-up";
            }
            
        }
        public void summary_dif_sales_ads_of_day() {

            result_amount_of_day_stroredModel am = new result_amount_of_day_stroredModel();
            
            am.sum_amount_before("2018-02-08", "2018-02-08");
            am.sum_amount_present("2018-02-09", "2018-02-09");
            am.calculate_Sale_dif_per();


            if (am.sale_dif_percent < 0)
            {
                ViewData["ads_color"] = "text-red";
                ViewData["ads_percen"] = Convert.ToDouble(am.sale_dif_percent).ToString("0.0");
                ViewData["ads_amount"] = Convert.ToDouble(am.result_present).ToString("0,0.00");
                ViewData["ads_caret"] = "fa-caret-down";
            }
            else
            {
                ViewData["ads_color"] = "text-green";
                ViewData["ads_percen"] = Convert.ToDouble(am.sale_dif_percent).ToString("0.0");
                ViewData["ads_amount"] = Convert.ToDouble(am.result_present).ToString("0,0.00");
                ViewData["ads_caret"] = "fa-caret-up";
            }

        }

    }
}
