using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 把角色、权限组、权限整理成上下树形关系
    /// </summary>
    public class Right
    {
        public int RightId { get; set; }
        public string RightName{get;set;}
        public int UpperId{get;set;}
        public string url { get; set; }
        public string Type { get; set; } //那种类型，角色、权限组、权限
        public int ID { get; set; }  //所属类型的ID
    }

}
