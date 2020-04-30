using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.RBAC;
using Models.RBAC;

namespace BLL.RBAC
{
    public class ActionsManager
    {
        public bool add(Actions vo)
        {
            return new ActionService().insert(vo);
        }
        public List<Actions> list(int GroupsId)
        {
            return new ActionService().FindByGroupsId(GroupsId);
        }
        public List<Actions> list()
        {
            return new ActionService().list();
        }
        public Actions Show(int actid)
        {
            return new ActionService().FindById(actid);
        }
        public bool Modify(Actions vo)
        {
            return new ActionService().Modify(vo);
        }
        public bool Delete(int actid)
        {
            return new ActionService().delete(actid);
        }
    }
}
