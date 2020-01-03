using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
namespace BLL
{
    public class DepartmentManager
    {
        public List<Department> list()
        {
            return new DepartmentService().list();
        }
    }
}
