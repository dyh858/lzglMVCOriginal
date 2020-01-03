using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Script.Serialization;

namespace LzglMVC.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDeptList()
        {
            List<Department> DeptList = new DepartmentManager().list();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string stringList = jss.Serialize(DeptList);
            return Json(stringList, JsonRequestBehavior.AllowGet);
        }
    }
}
