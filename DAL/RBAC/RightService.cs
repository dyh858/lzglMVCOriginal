using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.RBAC;
using Models;

namespace DAL.RBAC
{
    public class RightService
    {
        /// <summary>
        /// 把一个角色的权限组、权限整理成上下树形关系
        /// </summary>
        /// <param name="rid">角色ID</param>
        /// <returns></returns>
        public List<Right> GetRightList(int rid)
        {
            List<Right> list = new List<Right>();
            Role role = new RoleService().FindById(rid);
            
            list.Add(new Right()
            {
                RightId = 1,
                RightName = role.Title,
                UpperId = 0,
                url = ""
            });
            List<Groups> gps = new GroupsService().FindByRoleId(role.Rid);
            int i = 1;
            foreach (Groups gr in gps) {
                i+=1;
                int gid = i;
                list.Add(new Right() { 
                    RightId=gid,
                    RightName=gr.Title,
                    UpperId=1,
                    url=""
                });
                List<Actions> acts = new ActionService().FindByGroupsId(gr.Gid);
                i += 1;
                foreach (Actions act in acts) {
                    list.Add(new Right() { 
                        RightId=i,
                        RightName=act.Title,
                        UpperId=gid,
                        url=""
                    });
                }
            }
            
            return list;
        }
        /// <summary>
        /// 获得所有角色的所有权限列表
        /// </summary>
        /// <returns></returns>
        public List<Right> GetRightList()
        {
            List<Role> RoleList = new RoleService().list();

            List<Right> list = new List<Right>();
            int i = 0;
            foreach (Role role in RoleList)
            {
                i += 1;
                list.Add(new Right()
                {
                    RightId = i,
                    RightName = role.Title,
                    UpperId = 0,
                    url = "",
                    Type="Role",
                    ID=role.Rid
                });
                List<Groups> gps = new GroupsService().FindByRoleId(role.Rid);
                
                foreach (Groups gr in gps)
                {
                    i += 1;
                    int gid = i;
                    list.Add(new Right()
                    {
                        RightId = gid,
                        RightName = gr.Title,
                        UpperId = 1,
                        url = "",
                        Type="Groups",
                        ID=gr.Gid
                    });
                    List<Actions> acts = new ActionService().FindByGroupsId(gr.Gid);
                    i += 1;
                    foreach (Actions act in acts)
                    {
                        list.Add(new Right()
                        {
                            RightId = i,
                            RightName = act.Title,
                            UpperId = gid,
                            url = act.Url,
                            Type="Actions",
                            ID=act.Actid
                        });
                    }
                }
            
            }

            return list;
        }
    }
}
