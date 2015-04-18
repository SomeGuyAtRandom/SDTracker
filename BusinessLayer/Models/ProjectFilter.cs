using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models
{
    public class ProjectFilter
    {
        public string SearchTerm { get; set; }
        public ICollection<District> Districts { get; set; }


    }
}
