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
    }
}
