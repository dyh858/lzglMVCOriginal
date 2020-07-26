using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;
using DAL;
using Utils;

namespace DAL
{
    public class EmployeeService
    {
        private const string initSQL = "SELECT empid,name,Sex,birthdate,idcard,mobilephone," +
                "住址,银行账号,invitationcode,postid from employees ";

        /// <summary>
        /// 为雇员分配来自数据库的值
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        private Employee Assign(SqlDataReader objReader)
        {
            Employee vo = new Employee();  
            
            vo.Empid = objReader["empid"].ToString();
            vo.Name = objReader["name"].ToString();
            vo.Birthdate =objReader["birthdate"]!=DBNull.Value ? Convert.ToDateTime(objReader["birthdate"]).ToString("yyyy-MM-dd") : String.Empty.ToString();
            vo.Idcard = objReader["idcard"].ToString();
            vo.MobilePhone = objReader["mobilephone"].ToString();
            vo.Address = objReader["住址"].ToString();
            vo.BankCard = objReader["银行账号"].ToString();
            vo.InvitationCode = objReader["invitationcode"].ToString();
            vo.Position = objReader["postid"] != DBNull.Value? new PostService().FindById(Convert.ToInt32(objReader["postid"])) :null ;
            vo.setSex(Convert.ToInt32(objReader["Sex"]));
            vo.Admin = new SysAdminService().FindByEmpid(objReader["empid"].ToString());
            return vo;
        }        

        /// <summary>
        /// 根据部门名称查询部门人员
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public List<Employee> GetEmpByDept(string deptName)
        {
            string sql = "SELECT 职工号,姓名,性别,出生日期,身份证号码,"+
                "手机长号,住址,银行账号 FROM 员工 WHERE ltrim(rtrim(部门名称))= '{0}' " +
                "OR ltrim(rtrim(班组))= '{0}'";
            
            sql=string.Format(sql,deptName.Trim());
            SqlDataReader objReader=SQLHelper.GetReader(sql);
            List<Employee> list = new List<Employee>();
            while(objReader.Read())
            {
                list.Add(new Employee()
                {
                    Empid=objReader["职工号"].ToString(),
                    Name=objReader["姓名"].ToString(),
                    Birthdate=Convert.ToDateTime(objReader["出生日期"]).ToString("yyyy-MM-dd"),
                    Idcard=objReader["身份证号码"].ToString(),
                    Gender = objReader["性别"].ToString(),
                    MobilePhone=objReader["手机长号"].ToString(),
                    Address = objReader["住址"].ToString(),
                    BankCard = objReader["银行账号"].ToString()
                });
            }
            objReader.Close();
            objReader.Dispose();
            
            return list;
        }
        /// <summary>
        /// 根据职工号查询职工
        /// </summary>
        /// <param name="empid">职工号</param>
        /// <returns></returns>
        public Employee getEmpById(string Empid)
        {
            string sql =string.Format(@""+ initSQL +  "WHERE empid=@empid");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@empid",Empid),
            };
            SqlDataReader objReader = SqlHelper.ExecuteReader(SqlHelper.connString, CommandType.Text, sql, paras);
            Employee vo = null;

            if (objReader.Read())
            {
                vo = Assign(objReader);
            }

            objReader.Close();
            objReader.Dispose();
            return vo;
        }
        /// <summary>
        /// 根据身份证号码查询职工
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public Employee getEmpByIdCard(string IdCard)
        {
            string sql =string.Format(@""+ initSQL + "WHERE idcard=@idcard");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@idcard",IdCard),
            };
            try
            {
                SqlDataReader objReader = SqlHelper.ExecuteReader(SqlHelper.connString, CommandType.Text, sql, paras);
                Employee vo = null;

                if (objReader.Read())
                {
                    vo = Assign(objReader);
                }

                objReader.Close();
                objReader.Dispose();
                return vo;
            }
            catch (Exception ex )
            { 
                throw ex;
            }      
        }
        /// <summary>
        /// 根据电话号码查询职工
        /// </summary>
        /// <param name="Mobilephone"></param>
        /// <returns></returns>
        public Employee getEmpMobilePhone(string Mobilephone)
        {
            string sql = string.Format(@""+ initSQL + "WHERE mobilephone=@phone");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@phone",Mobilephone),
            };
            try
            {
                SqlDataReader objReader = SqlHelper.ExecuteReader(SqlHelper.connString, CommandType.Text, sql, paras);
                Employee vo = null;

                if (objReader.Read())
                {
                    vo = Assign(objReader);
                }

                objReader.Close();
                objReader.Dispose();
                return vo;
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }
        /// <summary>
        /// 修改雇员
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int ModifyEmployee(Employee vo)
        {
            string sql = "update employees set name='{0}',sex={1},birthdate='{2}'," +
                "idcard='{3}',mobilephone={4},住址='{5}' where empid='{6}'";
            sql=string.Format(sql,vo.Name,vo.getSex(),vo.Birthdate,vo.Idcard,
                vo.MobilePhone,vo.Address,vo.Empid);
            try
            {
                return Convert.ToInt32(SQLHelper.Update(sql));
            }
            catch (SqlException ex)
            {

                throw new Exception("数据操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string InvitationCode(string empid) 
        {
            string RandomCode = new RandomStringBuilder().Create(4);

            string sql = "UPDATE employees set invitationcode='{0}' WHERE empid='{1}'";
            sql = string.Format(sql, RandomCode, empid);
            try
            {
                if (SQLHelper.Update(sql) >= 1)
                {
                    return RandomCode;
                }
                else
                {
                    return "";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + ex.Message);
            }
        }
        /// <summary>
        /// 根据姓名查询人员，由于存在重名，因此用list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Employee> getEmpByName(string name)
        {
            string sql = string.Format(@""+ initSQL + "WHERE name=@name");
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@name",name),
            };
            try
            {
                SqlDataReader objReader = SqlHelper.ExecuteReader(SqlHelper.connString, CommandType.Text, sql, paras);
                List<Employee> list = new List<Employee>();

                while (objReader.Read())
                {
                    list.Add(Assign(objReader));
                }

                objReader.Close();
                objReader.Dispose();
                return list;
            }
            catch (Exception ex)
            {              
                throw ex;
            }
        }

    }
}
