using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessLayer.Models
{

    [MetadataType(typeof(ConfigDistrict))]
    public partial class District
    {
    }
    public class ConfigDistrict
    {
        [Display(Name = "District")]
        public string Name { get; set; }

        [Display(Name = "Dist")]
        public string Code { get; set; }

    }

}
