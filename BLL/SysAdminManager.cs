using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Web;
using System.Data;
using Utils;
 
namespace BLL
{
    public class SysAdminManager
    {
        /// <summary>
        /// 根据帐号和密码登录
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public SysAdmin AdminLogin(SysAdmin objAdmin)
        {
            objAdmin = new SysAdminService().AdminLogin(objAdmin);
            if(objAdmin!=null)
            {
                //将登录对象保存Session
                //HttpContext.Current.Session["CurrentAdmin"] = objAdmin;
                
            }
            return objAdmin;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="vo">SysAdmin对象</param>
        /// <returns></returns>
        public bool add(SysAdmin vo)
        {
            return new SysAdminService().insert(vo);
        }
        public SysAdmin show(string id)
        {
            return new SysAdminService().findById(id);
        }
        public SysAdmin ShowByName(string name)
        {
            return new SysAdminService().FindByName(name);
        }
        public SysAdmin ShowByEmpid(string Empid)
        {
            return new SysAdminService().FindByEmpid(Empid);
        }
        public SysAdmin ShowByLoginId(string loginid)
        { 
            return new SysAdminService().FindByLoginId(loginid);
        }

        public bool Update(SysAdmin vo)
        {
            return new SysAdminService().Update(vo);
        }
        /// <summary>
        /// 实现系统登录（编写登录方法）
        /// </summary>
        /// <param name="strLoginName">登录名</param>
        /// <param name="strLoginPwd">密码</param>
        /// <returns></returns>
        public bool Login(string strLoginName, string strLoginPwd)
        {
            try
            {
                //1.调用数据访问层：根据用户名得到用户信息
                DataTable dtUser= SysAdminService.GetUserInfoByUserName(strLoginName);
                if(dtUser.Rows.Count>0)
                {
                    //2.把用户信息中的密码与表示层的密码进行对比
                    string pwd= new Encrypt(strLoginPwd).str2;
                    if (dtUser.Rows[0]["LoginPwd"].Equals(pwd))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
