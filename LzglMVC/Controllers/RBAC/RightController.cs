using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.RBAC;
using Models;
using BLL.RBAC;
using BLL;
using System.Web.Script.Serialization;

namespace LzglMVC.Controllers.RBAC
{
    public class RightController : Controller
    {
        //
        // GET: /Right/

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult GetRightListById(int rid)
        //{
        //    List<Right> RightList = new RightManager().GetRightList(rid);
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    string stringList = jss.Serialize(RightList);
        //    return Json(stringList, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetRightList()
        {
            List<Right> RightList = new RightManager().GetRightList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string stringList = jss.Serialize(RightList);
            return Json(stringList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialRight()
        {
            return PartialView();
        }
    }
}
