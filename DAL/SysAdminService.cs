using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace DAL
{
    public class SysAdminService
    {
        /// <summary>
        /// 根据账号和密码登录
        /// </summary>
        /// <param name="objAdmin">SysAdmin对象</param>
        /// <returns></returns>
        public SysAdmin AdminLogin(SysAdmin objAdmin)
        {
            Encrypt enc = new Encrypt(objAdmin.LoginPwd);
            string sql = "select AdminName from admins where LoginId={0} and LoginPwd='{1}'";
            sql = string.Format(sql,objAdmin.LoginId,enc.str2);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objAdmin.AdminName = objReader["AdminName"].ToString();
                    objReader.Close();
                }
                else
                {
                    objAdmin = null;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
            return objAdmin;
        }
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="vo">SysAdmin对象</param>
        /// <returns></returns>
        public bool insert(SysAdmin vo)
        {
            Encrypt enc = new Encrypt(vo.LoginPwd);
            string sql = "INSERT INTO admins(LoginId,AdminName,LoginPwd)VALUES('{0}','{1}', '{2}')";
            sql = string.Format(sql,vo.LoginId,vo.AdminName,enc.str2);
            try
            {
                if (SQLHelper.Update(sql) >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
        }
        /// <summary>
        /// 根据账号查找账号和用户名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAdmin findById(string id)
        {
            string sql = "SELECT LoginId,AdminName FROM admins WHERE LoginId='{0}'";
            sql = string.Format(sql, id);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                SysAdmin vo = null;
                if (reader.Read())
                {
                    vo = new SysAdmin()
                    {
                        LoginId= reader["LoginId"].ToString(),
                        AdminName=reader["AdminName"].ToString()
                    };
                }
                return vo;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
