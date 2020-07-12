using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.PostProp;
namespace Models
{
    public class Post
    {
        public Int32 PostId {get;set;}
        public string PostName {get;set;}
      public Department Team {get;set;}
      public Department Dept {get;set;}
      public PostProperty Property {get;set;}
      public PostSequence Sequence { get; set; }
      public PostGrade Grade { get; set; }

    }
}
