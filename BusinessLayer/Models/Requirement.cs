using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Models
{
    [Table("Requirements")]
    public partial class Requirement
    {
        public Requirement() 
        {
            // Start out with empty constructor
        }

        public int Id { get; set; }
        public int RequirementId { get; set; }
        public int ProjectId { get; set; }
        public string RequirementName { get; set; }
        public Boolean Required { get; set; }

        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> FinishDate { get; set; }
        public string CurrentComment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


    }
}
