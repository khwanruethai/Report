using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Report.Models;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Report.Controllers
{
    public class resultAmountDayController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult getAmountofDay()
        {

            result_amount_of_day_stroredModel am = new result_amount_of_day_stroredModel();

            var obj = am.getAmount();


            return Json(obj);
        }
    }
}
