using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.RBAC;
using Models;
namespace BLL.RBAC
{
    public class RightManager
    {
        public List<Right> GetRightList()
        {
            return new RightService().GetRightList();
        }
        public List<Right> GetRightList(int rid)
        {
            return new RightService().GetRightList(rid);
        }
    }
}
