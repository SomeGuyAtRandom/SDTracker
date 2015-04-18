using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(ConfigJobType))]
    public partial class JobType
    {
    }
    public class ConfigJobType
    {
        [Display(Name = "Job Type")]
        public string Name { get; set; }

        [Display(Name = "Code")]
        public string JobCode { get; set; }


    }
}
