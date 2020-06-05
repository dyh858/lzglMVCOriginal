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
                "住址,银行账号,invitationcode from employees ";
        /// <summary>
        /// 为雇员分配来自数据库的值
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        private Employee Assign(SqlDataReader objReader)
        {
            Employee vo = new Employee()  //类的实例初始化器
            {
                Empid = objReader["empid"].ToString(),
                Name = objReader["name"].ToString(),
                Birthdate = Convert.ToDateTime(objReader["birthdate"]).ToString("yyyy-MM-dd"),
                Idcard = objReader["idcard"].ToString(),
                MobilePhone = objReader["mobilephone"].ToString(),
                Address = objReader["住址"].ToString(),
                BankCard = objReader["银行账号"].ToString(),
                InvitationCode = objReader["invitationcode"].ToString(),
            };
            vo.setSex(Convert.ToInt32(objReader["Sex"]));
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
            return list;
        }
        /// <summary>
        /// 根据职工号查询职工
        /// </summary>
        /// <param name="empid">职工号</param>
        /// <returns></returns>
        public Employee getEmpById(string Empid)
        {
            string sql = initSQL +  "WHERE empid='{0}'";
            sql = string.Format(sql, Empid);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Employee vo = null;

            if (objReader.Read())
            {
                vo = Assign(objReader);
            }

            objReader.Close();
            return vo;
        }
        /// <summary>
        /// 根据身份证号码查询职工
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public Employee getEmpByIdCard(string IdCard)
        {
            string sql = initSQL + "WHERE idcard='{0}'";
            sql = string.Format(sql, IdCard);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Employee vo = null;

            if (objReader.Read())
            {
                vo = Assign(objReader);
            }

            objReader.Close();
            return vo;
        }
        /// <summary>
        /// 根据电话号码查询职工
        /// </summary>
        /// <param name="Mobilephone"></param>
        /// <returns></returns>
        public Employee getEmpMobilePhone(string Mobilephone)
        {
            string sql = initSQL + "WHERE mobilephone='{0}'";
            sql = string.Format(sql, Mobilephone);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Employee vo = null;

            if (objReader.Read())
            {
                vo = Assign(objReader);
            }

            objReader.Close();
            return vo;
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
            string sql = initSQL + "WHERE name='{0}'";
            sql = string.Format(sql, name);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Employee> list = new List<Employee>();

            while (objReader.Read())
            {
                list.Add(Assign(objReader));
            }

            objReader.Close();
            return list;
        }

    }
}
