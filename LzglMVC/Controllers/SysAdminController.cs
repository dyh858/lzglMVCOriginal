using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.RBAC;
using BLL;
using BLL.RBAC;
using System.Web.Security;
using Newtonsoft.Json;

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
                    return RedirectToAction("HRManage",objAdmin);
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
            if (vo.AdminName == null) { 
                vo.AdminName = User.Identity.Name;
                vo = new SysAdminManager().ShowByName(vo.AdminName);
            }
            List<Groups> GroupsList = new GroupsManager().list(vo.rid);
            ViewBag.Groups = GroupsList;
            return View(vo);
        }
        //退出登录
        public ActionResult Destroy()
        {
            FormsAuthentication.SignOut();//注销Cookie
            if ((SysAdmin)Session["CurrentAdmin"] != null)
            { 
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
        /// <summary>
        /// 注册结果
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ActionResult RegisterResult(SysAdmin vo)
        {
            if (ModelState.IsValid)
            {
                if (new SysAdminManager().add(vo))
                {
                    ViewData["result"]= "注册成功！请登录:";
                    return View("AdminLogin");
                }
                else
                {
                    ViewData["result"]= "注册失败！";
                }
            }
            return View("Signup");
        }
        /// <summary>
        /// 根据类容进行搜索，没找到返回“查询失败！”，
        /// 在雇员中找到，用empid在用户中查找，如果找到，返回“您已注册！”
        /// 如未找到，返回雇员类的json数据
        /// </summary>
        /// <param name="TxtSearch"></param>
        /// <returns></returns>
        public ActionResult Search(string TxtSearch) 
        {
            Employee emp = null;
            if (TxtSearch.Length == 18)
            {
                emp = new EmployeeManager().ShowByIdCard(TxtSearch);
            }
            else if(TxtSearch.Length==11)
            {
                emp = new EmployeeManager().ShowByMobilephone(TxtSearch);
            }
            else
            {
                emp = new EmployeeManager().show(TxtSearch);
            }
                        
            if(emp != null){
                ViewBag.emp = emp;
                SysAdmin admin = new SysAdminManager().ShowByEmpid(emp.Empid);
                if (admin == null)
                {
                    String JsonEmp = JsonConvert.SerializeObject(emp);
                    return this.Content(JsonEmp);
                }
                else
                {
                    return  this.Content("您已注册！");
                }
                
            }
            return this.Content("查询失败！");
        }
        public ActionResult SearchName(string TxtSearch)
        {
            List<Employee> list = new EmployeeManager().ShowByName(TxtSearch);
            if(list != null)
            {
                String JsonList = JsonConvert.SerializeObject(list);
                return this.Content(JsonList);
            }

            return this.Content("查询失败！");
        }
        /// <summary>
        /// 验证用户ID是否已经存在
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public ActionResult IsLoginId(string LoginId) {
            SysAdmin admin = new SysAdminManager().ShowByLoginId(LoginId);
            if (admin != null)
            {
                return this.Content("false");
            }
            else
            {
                return this.Content("true");
            }
        }

        public ActionResult ShowInvitationCode(string Empid)
        {
            string inviteCode = new EmployeeManager().InvitationCode(Empid);
            return this.Content(inviteCode);
        }

        public ActionResult InvitationCodeView()
        {
            return View();
        }

        public ActionResult RetrievePassword()
        {
            return View();
        }
    }
}
