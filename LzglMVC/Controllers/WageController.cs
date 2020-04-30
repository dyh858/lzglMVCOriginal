using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace LzglMVC.Controllers
{
    public class WageController : Controller
    {
        //
        // GET: /Wage/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetWageById(string empid,string StartDate,string EndDate)
        {

            string JsonString = new WageManager().GetTable(empid,StartDate,EndDate);
            return Json(JsonString,JsonRequestBehavior.AllowGet); 
        }

       

    }

}
