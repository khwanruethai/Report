using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Report.Models;
using System.Globalization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Report.Controllers
{
    public class ReportAdsController : Controller
    {
        // GET: /<controller>/
        CultureInfo th = new CultureInfo("th-TH");

        public IActionResult Index()
        {
            group_type_tab_Model gb = new group_type_tab_Model();
            ads_sales_book_det_h1_viewModel ads_view = new ads_sales_book_det_h1_viewModel();
            ads_sales_type_main_h1_viewModel main = new ads_sales_type_main_h1_viewModel();

            main.select_now_date();
            main.select_old_date();
            ViewData["date_now"] =  main.now_date;
            ViewData["date_old"] = main.old_date;

            ViewData["group_type"] = gb.select_group_type();
            // ViewData["team"] = ads_view.select_team();


            ViewData["det_data"] = ads_view.select_data(main.now_date, main.old_date);

            ads_view.sum_for_head();

            ViewData["head01"] = ads_view.head01;
            ViewData["head02"] = ads_view.head02;
            ViewData["head03"] = ads_view.head03;
            ViewData["head04"] = ads_view.head04;
            ViewData["head05"] = ads_view.head05;
            ViewData["head06"] = ads_view.head06;
            ViewData["head07"] = ads_view.head07;
            ViewData["head_total"] = ads_view.total_head;

            return View();
        }
        public IActionResult ReportAdsMain() {


            ads_type_main_tabModel main = new ads_type_main_tabModel();
            ads_sales_type_main_h1_viewModel type_main = new ads_sales_type_main_h1_viewModel();
            
            ViewData["main"] = main.type_main();
          //  type_main.selectTeam();
            ViewData["main_det"] = type_main.detail2();

            ViewData["date_now"] = type_main.now_date;
            ViewData["date_old"] = type_main.old_date;
            return View();
        }
    }
}
