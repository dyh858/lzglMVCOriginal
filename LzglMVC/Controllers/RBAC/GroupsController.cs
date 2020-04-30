using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL.RBAC;
using Models.RBAC;

namespace LzglMVC.Controllers.RBAC
{
    public class GroupsController : Controller
    {
        //
        // GET: /Groups/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Groups vo)
        {
            if (ModelState.IsValid)
            {
                if (new GroupsManager().add(vo))
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
        /// <summary>
        /// 浏览
        /// </summary>
        /// <returns></returns>
        public ActionResult Browse()
        {
            List<Groups> list = new GroupsManager().list();
            ViewBag.List = list;
            return View("Browse");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            new GroupsManager().Delete(id);
            return RedirectToAction("Browse");
        }
        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Groups vo = new GroupsManager().Show(id);

            return View("Edit", vo);
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify(Groups vo)
        {
            if (new GroupsManager().Modify(vo))
            {
                ViewData["result"] = "修改成功！";
            }
            else
            {
                ViewData["result"] = "修改失败！";
            }

            return View("Edit", vo);
        }

        /// <summary>
        /// 权限组菜单
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns>返回角色的所有权限组菜单</returns>
        public JsonResult GetGroupsList(string RoleId)
        {
            List<Groups> ActionsList = new GroupsManager().list(Convert.ToInt32(RoleId));
            return Json(ActionsList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 分配权限，返回该权限组的所有权限
        /// </summary>
        /// <param name="gid">权限组ID</param>
        /// <param name="actid">权限ID</param>
        /// <returns>权限组的所有权限</returns>
        public JsonResult AllotActions(int gid, int actid)
        {
            new GroupsManager().AllotActions(gid, actid);
            return Json(new ActionsManager().list(gid), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 移去权限组的权限
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="actid"></param>
        /// <returns></returns>
        public JsonResult RemoveActions(int gid, int actid)
        {
            new GroupsManager().RemoveActions(gid, actid);
            return Json(new ActionsManager().list(gid),JsonRequestBehavior.AllowGet);
        }
    }
}
