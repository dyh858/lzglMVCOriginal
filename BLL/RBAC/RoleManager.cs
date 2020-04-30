using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.RBAC;
using DAL;
using DAL.RBAC;
namespace BLL.RBAC
{
    public class RoleManager
    {
        public bool add(Role vo)
        {
            return new RoleService().insert(vo);
        }
        public Role show(int id)
        {
            return new RoleService().FindById(id);
        }
        public List<Role> list()
        {
            return new RoleService().list();
        }
        public bool Modify(Role vo)
        {
            return new RoleService().Modify(vo);
        }
        public bool Delete(int id)
        {
            return new RoleService().delete(id);
        }
        public bool AllotGroups(int rid, int gid) 
        {
            if (new RoleService().FindGroupsByRidGid(rid, gid))
            {
                return false;
            }
            else {
                return new RoleService().AllotGroups(rid, gid);
            }
            
        }
        public bool RemoveGroups(int rid, int gid)
        {
            return new RoleService().RemoveGroups(rid, gid);
        }
    }
}
