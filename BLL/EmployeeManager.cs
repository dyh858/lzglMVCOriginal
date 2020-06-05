using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL
{
    public class EmployeeManager 
    {
        public List<Employee> GetEmployeeByDept(String deptName)
        {
            return new EmployeeService().GetEmpByDept(deptName);
        }
        public Employee show(string empid)
        { 
            return new EmployeeService().getEmpById(empid); 
        }
        public int ModifyEmployee(Employee vo)
        {
            return new EmployeeService().ModifyEmployee(vo);
        }
        public Employee ShowByIdCard(string IdCard)
        {
            return new EmployeeService().getEmpByIdCard(IdCard);
        }
        public string InvitationCode(string empid)
        {
            return new EmployeeService().InvitationCode(empid);
        }
        public Employee ShowByMobilephone(string Mobliephone)
        {
            return new EmployeeService().getEmpMobilePhone(Mobliephone);
        }
        public List<Employee> ShowByName(string name)
        {
            return new EmployeeService().getEmpByName(name);
        }
    }
}
