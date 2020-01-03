using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Security;

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
        [HttpPost]
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
                //用户登录是否成功
                if (objAdmin != null)
                {
                    //为当前用户提供一个身份验证票证，并将该票证添加到Cookie
                    FormsAuthentication.SetAuthCookie(objAdmin.AdminName, false);
                    ViewBag.info = "欢迎您：" + objAdmin.AdminName;
                    ViewData["info"] = "欢迎您：" + objAdmin.AdminName;
                    return View("HRManage", objAdmin);
                }
                else
                {
                    ViewBag.info = "用户名或密码错误！";
                    ViewData["info"] = "用户名或密码错误！";
                }
            }
            return View();
        }
        //直接到HRManage页面
        [Authorize]
        public ActionResult HRManage(SysAdmin vo)
        {
            return View(vo);
        }
        //退出登录
        public ActionResult Destroy()
        {
            if ((SysAdmin)Session["CurrentAdmin"] != null)
            {
                FormsAuthentication.SignOut();//注销Cookie
                Session.Remove("CurrentAdmin");//注销Session
            }
            return RedirectToAction("Index", "Home");
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
