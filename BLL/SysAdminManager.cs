﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Web;

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
                HttpContext.Current.Session["CurrentAdmin"] = objAdmin;
                
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
    }
}
