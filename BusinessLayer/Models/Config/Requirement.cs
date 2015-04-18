using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessLayer.Models.Config
{
    [MetadataType(typeof(ConfigRequirement))]
    public partial class Requirement
    {
    }
    public class ConfigRequirement
    {
        [Display(Name = "Requirement Type")]
        public string RequirementType { get; set; }

    }
}
