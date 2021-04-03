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
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPostList()
        {
            return Json(new JavaScriptSerializer().Serialize(new PostManager().list()),JsonRequestBehavior.AllowGet);
        }
        public String GetPostTable()
        {
            return new PostManager().getTable();
        }

    }
}
