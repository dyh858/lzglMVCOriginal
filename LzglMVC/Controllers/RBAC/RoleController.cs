using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.RBAC;
using BLL.RBAC;
using BLL;
using System.Web.Script.Serialization;

namespace LzglMVC.Controllers.RBAC
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Role vo)
        {
            if (ModelState.IsValid)
            {
                if (new RoleManager().add(vo))
                {
                    ViewData["result"] = "增加人员成功！";
                }
                else
                {
                    ViewData["result"] = "增加人员失败！";
                }
            }
            return View("Index");
        }

        public ActionResult Browse()
        {
            List<Role> list = new RoleManager().list();
            ViewBag.List = list;
            return View("Browse");
        }

        public JsonResult GetRoleList()
        {
            List<Role> list = new RoleManager().list();         
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Show()
        {
            int rid = Convert.ToInt32(Request.Params["rid"]);
            Role vo = new RoleManager().show(rid);

            return View("Detail", vo);
        }

        public JsonResult getRole()
        {
            int rid = new SysAdminManager().ShowByName(User.Identity.Name).rid;
            Role vo = new RoleManager().show(rid);
            return Json(vo,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            int aid = Convert.ToInt32(Request.Params["aid"]);
            Role vo = new RoleManager().show(aid);

            return View("Edit", vo);
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify(Role vo)
        {
            if (new RoleManager().Modify(vo))
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
            new RoleManager().Delete(id);
            return RedirectToAction("Browse");
        }
        /// <summary>
        /// 分配权限页面，同时向页面传角色、权限组、权限集合
        /// </summary>
        /// <returns></returns>
        public ActionResult AllotGroupsIndex()
        {
            ViewBag.ListGroups = new GroupsManager().list();
            ViewBag.ListRole = new RoleManager().list();
            ViewBag.ListActions = new ActionsManager().list();
            return View();
        }
        /// <summary>
        /// 分配权限组
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public JsonResult AllotGroups(int rid, int gid)
        {
            List<Groups> ListGroups = new List<Groups>();
            if (new RoleManager().AllotGroups(rid, gid))
            {
                ViewData["result"] = "权限分配成功！";
            }
            else
            {
                ViewData["result"] = "权限分配失败！";
            }
            ListGroups = new GroupsManager().list(rid);
            return Json(ListGroups,JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveGroups(int rid, int gid)
        {
            List<Groups> ListGroups = new List<Groups>();
            if (new RoleManager().RemoveGroups(rid, gid))
            {
                ViewData["result"] = "权限移去成功！";
            }
            else
            {
                ViewData["result"] = "权限移去失败！";
            }
            ListGroups = new GroupsManager().list(rid);
            return Json(ListGroups, JsonRequestBehavior.AllowGet);
        } 
    }
}
