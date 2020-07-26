using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Script.Serialization;

namespace StudentManagerMVC.Controllers
{
    
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        [Authorize]
        public ActionResult Index()
        {
            return View("DeptEmp");
        }
        /// <summary>
        /// 根据部门查询人员
        /// </summary>
        /// <returns></returns>
        //public ActionResult GetEmpList()
        //{
        //    string deptName = Request.Params["deptName"];
        //    List<Employee> empList = new EmployeeManager().GetEmployeeByDept(deptName);

        //    ViewBag.deptName = deptName;
        //    ViewBag.empList = empList;

        //    return View("EmployeeManage");
        //}
        /// <summary>
        /// 根据部门获取雇员对象
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult GetEmpList(string dept)
        {
            List<Employee> empList = new EmployeeManager().GetEmployeeByDept(dept);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string stringList = jss.Serialize(empList);
            return Json(stringList, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 根据部门获取雇员对象，分页
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">页码</param>
        /// <param name="dept">部门名称</param>
        /// <returns></returns>
        [Authorize]
        public JsonResult GetEmpListJson(int limit, int offset, string dept)
        {
            List<Employee> empList = new EmployeeManager().GetEmployeeByDept(dept);
            var total = empList.Count;
            var rows = empList.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据职工号查询职工
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult getEmpDetail()
        {
            string empid = Request.QueryString["empId"];
            Employee emp = new EmployeeManager().show(empid);
            //保存数据
            //ViewData["emp"] = vo;
            //调用带Model的重载的视图方法
            return View("EmpDetail", emp);
        }
        [Authorize]
        public ActionResult EmployeeEdit()
        {
            string empid = Request.QueryString["empId"];
            Employee emp = new EmployeeManager().show(empid);
            //保存数据
            //ViewData["emp"] = vo;
            //调用带Model的重载的视图方法
            return View("EmployeeEdit", emp);
        }
        [Authorize]
        public ActionResult EmployeeAdminLogin(SysAdmin objAdmin)
        {
            if (ModelState.IsValid)
            {
                TempData["objAdmin"] = objAdmin;
                return RedirectToAction("AdminLogin", "SysAdmin");
            }
            else
            {
                return RedirectToAction("Index", "SysAdmin");
            }

        }
        [Authorize]
        public ActionResult Edit()
        {
            Employee vo = new Employee()
            {
                Empid = Request.Params["empid"],
                Name = Request.Params["name"],
                Gender = Request.Params["gender"],
                Birthdate = Convert.ToDateTime(Request.Params["birthdate"]).ToString("yyyy-MM-dd"),
                Idcard = Request.Params["idcard"],
                MobilePhone = Request.Params["mobilephone"],
                Address = Request.Params["address"],
            };
            int result = new EmployeeManager().ModifyEmployee(vo);

            return View("EmployeeManage");
        }
        [Authorize]
        public ActionResult DeptEmp()
        {
            return View();
        }

        public ActionResult PartialSearch()
        {
            return PartialView();
        }
    }
}

