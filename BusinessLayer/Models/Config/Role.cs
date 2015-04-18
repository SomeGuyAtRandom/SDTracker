using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessLayer.Models
{
    [MetadataType(typeof(ConfigRole))]
    public partial class Role
    {
    }
    public class ConfigRole
    {
        [Display(Name = "Permission")]
        public string Role { get; set; }

    }
}
