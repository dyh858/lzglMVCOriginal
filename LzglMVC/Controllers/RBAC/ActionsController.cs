using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Models.RBAC;
using BLL.RBAC;

namespace LzglMVC.Controllers.RBAC
{
    public class ActionsController : Controller
    {
        //
        // GET: /Actions/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获得权限菜单
        /// </summary>
        /// <param name="GroupsId">权限组ID</param>
        /// <returns>返回该权限组的所有Action子菜单</returns>
        public JsonResult GetActionsList(string GroupsId)
        {
            List<Actions> ActionsList = new ActionsManager().list(Convert.ToInt32(GroupsId));
            return Json(ActionsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(Actions vo)
        {
            if (ModelState.IsValid)
            {
                if (new ActionsManager().add(vo))
                {
                    ViewData["result"] = "增加成功！";
                }
                else
                {
                    ViewData["result"] = "增加失败！";
                }
            }
            return View("Index");
        }

        public ActionResult Browse()
        {
            ViewBag.List = new ActionsManager().list();
            return View("Browse");
        }
        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            //int actid = Convert.ToInt32(Request.Params["actid"]);
            Actions vo = new ActionsManager().Show(id);

            return View("Edit", vo);
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify(Actions vo)
        {
            if (new ActionsManager().Modify(vo))
            {
                ViewData["result"] = "修改人员成功！";
            }
            else
            {
                ViewData["result"] = "修改人员失败！";
            }

            return View("Edit", vo);
        }
        public ActionResult Delete(int id)
        {
            new ActionsManager().Delete(id);
            return RedirectToAction("Browse");
        }
    }
}
