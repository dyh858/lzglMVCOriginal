using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
namespace LzglMVC.Controllers
{
    [Authorize]
     public class ApplicantController : Controller
    {
         
         public ActionResult Index()
        {
            return View("ApplicantAdd");
        }
         
        /// <summary>
        /// 添加一个新的应聘人员
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(Applicant vo)
        {
            //Applicant vo = new Applicant()
            //{
            //    AName = Request.Params["aname"],
            //    Gender = Convert.ToInt32(Request.Params["gender"]),
            //    Nationality = Request.Params["nationality"],
            //    Birthdate = Convert.ToDateTime(Request.Params["birthdate"]),
            //    IdCard = Request.Params["idcard"],
            //    Address = Request.Params["address"],
            //    Education = Request.Params["education"],
            //    Mobilephone = Request.Params["mobilephone"]
            //};
            if(ModelState.IsValid)
            {
                if (new ApplicantManager().add(vo))
                {
                    ViewData["result"] = "增加人员成功！";
                }
                else
                {
                    ViewData["result"] = "增加人员失败！";
                }
            }
            return View("ApplicantAdd",vo);
        }
        /// <summary>
        /// 显示所有应聘人员
        /// </summary>
        /// <returns></returns>
        [HandleError(ExceptionType=typeof(System.Exception),View="Error")]
        public ActionResult ShowAll()
        {
            List<Applicant> list = new ApplicantManager().list();
            ViewBag.List = list;
            return View("Browse");
        }

        public ActionResult Show()
        {
            int aid = Convert.ToInt32(Request.Params["aid"]);
            Applicant vo = new ApplicantManager().show(aid);
            
            return View("Detail",vo);
        }
        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            int aid = Convert.ToInt32(Request.Params["aid"]);
            Applicant vo = new ApplicantManager().show(aid);

            return View("Edit", vo);
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify(Applicant vo)
        {
            //Applicant vo = new Applicant()
            //{
            //    AId=Convert.ToInt32(Request.Params["aid"]),
            //    AName = Request.Params["aname"],
            //    Gender = Convert.ToInt32(Request.Params["gender"]),
            //    Nationality = Request.Params["nationality"],
            //    Birthdate = Convert.ToDateTime(Request.Params["birthdate"]),
            //    IdCard = Request.Params["idcard"],
            //    Address = Request.Params["address"],
            //    Education = Request.Params["education"],
            //    Mobilephone = Request.Params["mobilephone"]
            //};
            if (new ApplicantManager().Modify(vo))
            {
                ViewData["result"] = "修改人员成功！";
            }
            else
            {
                ViewData["result"] = "修改人员失败！";
            }

            return View("Edit",vo);
        }
        public ActionResult delete(int aid)
        {
            new ApplicantManager().Delete(aid);
            return RedirectToAction("ShowAll");
        }
    }
}

