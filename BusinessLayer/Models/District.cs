using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models
{
    public partial class District
    {
        public District() 
        {
            // Start out with empty constructor
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


    }
}
