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
            //和模型交换，获取所有的新闻分类
            List<string> yearmonth = new WageManager().GetYearMonthList();
            SelectList list = new SelectList(yearmonth);
            //返回视图（如果需要传递数据，则首先保存数据）
            
            ViewBag.StartMonthDefault =yearmonth[yearmonth.Count - 4];
            ViewBag.EndMonthDefault = yearmonth[yearmonth.Count - 1];
            ViewBag.YearMonthList = list;
            return View();
        }

        public JsonResult GetWageById(string empid,string StartDate,string EndDate)
        {

            string JsonString = new WageManager().GetTable(empid,StartDate,EndDate);
            return Json(JsonString,JsonRequestBehavior.AllowGet); 
        }

       

    }

}
