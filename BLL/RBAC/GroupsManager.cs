using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.RBAC;
using DAL.RBAC;
namespace BLL.RBAC
{
    public class GroupsManager
    {
        public bool add(Groups vo)
        {
            return new GroupsService().insert(vo);
        }
        public List<Groups> list(int RoleId)
        {
            return new GroupsService().FindByRoleId(RoleId);
        }
        public List<Groups> list()
        {
            return new GroupsService().list();
        }
        public Groups Show(int gid)
        {
            return new GroupsService().FindById(gid);
        }
        public bool Modify(Groups vo)
        {
            return new GroupsService().Modify(vo);
        }
        public bool Delete(int gid)
        {
            return new GroupsService().delete(gid);
        }
        public bool AllotActions(int gid,int actid) 
        {
            if (new GroupsService().FindActionsByGidActid(gid, actid))
            {
                return false;
            }
            else
            { 
                return new GroupsService().AllotActions(gid, actid);
            }    
        }

        public bool RemoveActions(int gid, int actid)
        {
            return new GroupsService().RemoveActions(gid, actid);
        }
    }
}
