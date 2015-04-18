using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models
{
    public partial class JobType
    {
        public JobType() 
        {
            // Start out with empty constructor
        }

        //public JobType(int Id)
        //{
        //    ProjectDbContext context = new ProjectDbContext();
        //    this.Name = context.getJobTypeById(Id).Name;
        //    this.JobCode = context.getJobTypeById(Id).JobCode;
        //}

        public int Id { get; set; }

        public string Name { get; set; }
        public string JobCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
