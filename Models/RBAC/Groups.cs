using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.RBAC
{
    [Serializable]
    public class Groups
    {
        public int Gid { get; set; }
        public string Title { get; set; }
        public int Upperid { get; set; }
        public string Note { get; set; }
        public List<Actions> AllActions { get; set; }
    }
}
