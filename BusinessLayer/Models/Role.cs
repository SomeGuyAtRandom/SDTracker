using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models
{
    public partial class Role
    {
        public Role() 
        {
            // Start out with empty constructor
        }

        public int Id { get; set; }
        public string RoleType { get; set; }

        public bool IsMember { get; set; }
    }
}
