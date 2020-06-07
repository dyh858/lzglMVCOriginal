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
            string sql = "select AdminName,empid,rid from admins where LoginId='{0}' and LoginPwd='{1}'";
            sql = string.Format(sql,objAdmin.LoginId,enc.str2);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objAdmin.AdminName = objReader["AdminName"].ToString();
                    objAdmin.empid = objReader["empid"].ToString();
                    objAdmin.rid = Convert.ToInt32(objReader["rid"]);
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
            string sql = "INSERT INTO admins(LoginId,AdminName,LoginPwd,empid,rid)VALUES('{0}','{1}', '{2}','{3}',{4})";
            sql = string.Format(sql,vo.LoginId,vo.AdminName,enc.str2,vo.empid,vo.rid);
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
            string sql = "SELECT LoginId,AdminName,empid,rid FROM admins WHERE LoginId='{0}'";
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
                        AdminName=reader["AdminName"].ToString(),
                        empid = reader["empid"].ToString(),
                        rid = Convert.ToInt32(reader["rid"])
                    };
                }
                return vo;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// 根据用户名获得对象，主要从Cookie获得用户名
        /// </summary>
        /// <param name="AdminName">用户名</param>
        /// <returns>返回一个登录对象，包涵角色id</returns>
        public SysAdmin FindByName(string AdminName)
        {
            string sql = "SELECT LoginId,AdminName,rid,empid FROM admins WHERE adminName='{0}'";
            sql = string.Format(sql, AdminName);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                SysAdmin vo = null;
                if (reader.Read())
                {
                    vo = new SysAdmin()
                    {
                        LoginId = reader["LoginId"].ToString(),
                        AdminName = reader["AdminName"].ToString(),
                        empid = reader["empid"].ToString(),
                        rid=Convert.ToInt32(reader["rid"])
                    };
                }
                return vo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据职工号查询
        /// </summary>
        /// <param name="Empid"></param>
        /// <returns></returns>
        public SysAdmin FindByEmpid(string Empid)
        {
            string sql = "SELECT LoginId,AdminName,rid,empid FROM admins WHERE empid='{0}'";
            sql = string.Format(sql, Empid);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                SysAdmin vo = null;
                if (reader.Read())
                {
                    vo = new SysAdmin()
                    {
                        LoginId = reader["LoginId"].ToString(),
                        AdminName = reader["AdminName"].ToString(),
                        empid = reader["empid"].ToString(),
                        rid = Convert.ToInt32(reader["rid"])
                    };
                }
                return vo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SysAdmin FindByLoginId(string LoginId)
        {
            string sql = "SELECT LoginId,AdminName,rid,empid FROM admins WHERE loginid='{0}'";
            sql = string.Format(sql, LoginId);
            try
            {
                SqlDataReader reader = SQLHelper.GetReader(sql);
                SysAdmin vo = null;
                if (reader.Read())
                {
                    vo = new SysAdmin()
                    {
                        LoginId = reader["LoginId"].ToString(),
                        AdminName = reader["AdminName"].ToString(),
                        empid = reader["empid"].ToString(),
                        rid = Convert.ToInt32(reader["rid"])
                    };
                }
                return vo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(SysAdmin vo)
        {
            Encrypt enc = new Encrypt(vo.LoginPwd);
            
            string sql = "Update admins set AdminName='{0}', LoginPwd='{1}',empid='{2}', rid={3} WHERE loginid='{4}'";
            sql = string.Format(sql, vo.AdminName,enc.str2,vo.empid,vo.rid,vo.LoginId);
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
    }
}
