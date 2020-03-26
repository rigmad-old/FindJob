using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindJob.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }

    }
}