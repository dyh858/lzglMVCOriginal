using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace StudentManagerMVC.Controllers
{
    public class SysAdminController : Controller
    {
        //进入登录页面
        public ActionResult Index()
        {
            return View("AdminLogin");
        }
        //登录
        public ActionResult AdminLogin(SysAdmin vo)
        {
            if(ModelState.IsValid)
            {
                SysAdmin objAdmin = null;
                if ((SysAdmin)Session["CurrentAdmin"] != null)
                {
                    objAdmin = (SysAdmin)Session["CurrentAdmin"];
                    return View("HRManage", objAdmin);
                }
                objAdmin = vo;
                //调用业务逻辑
                objAdmin = new SysAdminManager().AdminLogin(objAdmin);
                if (objAdmin != null)
                {
                    ViewData["info"] = "欢迎您：" + objAdmin.AdminName;
                    return View("HRManage", objAdmin);
                }
                else
                {
                    ViewData["info"] = "用户名或密码错误！";
                    return View("");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Destroy()
        {
            if ((SysAdmin)Session["CurrentAdmin"] != null)
            {
                Session.Remove("CurrentAdmin");
            }
            return View("Index");
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult RegisterResult(SysAdmin vo)
        {
            if (ModelState.IsValid)
            {
                if (new SysAdminManager().add(vo))
                {
                    ViewData["result"] = "注册成功！请在登录页面登录。";
                }
                else
                {
                    ViewData["result"] = "注册失败！";
                }
            }
            return View("Register", vo);
        }
    }
}
