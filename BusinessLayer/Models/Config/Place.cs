using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(ConfigPlace))]
    public partial class Place
    {
       
    }

    public class ConfigPlace
    {
        [Display(Name = "5-Digit")]
        public string FiveDigit { get; set; }

        [Display(Name = "District")]
        public int DistrictId { get; set; }


        [Display(Name = "Location")]
        public string PlaceLocation { get; set; }

    }
}
