using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.Common;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(ConfigProject))]
    public partial class Project
    {
    }
    public class ConfigProject
    {
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "District")]
        public District District { get; set; }

        [Display(Name = "Remark")]
        public string CurrRemark { get; set; }

        [Display(Name = "Rush")]
        public bool Rush { get; set; }

        [Display(Name = "Proj No")]
        public string ProjNo { get; set; }

        [Display(Name = "5-digit")]
        public string FiveDigit { get; set; }

        [Display(Name = "Council Dist")]
        public string CDs { get; set; }

        [Display(Name = "Job Type")]
        public JobType JobType { get; set; }

        [Display(Name = "Head Engineer")]
        public AdminUser HeadEngineer { get; set; }

        [Display(Name = "Design Engineer")]
        public AdminUser DesignEngineer { get; set; }

        [Display(Name = "Comment")]
        public string CurrentComment { get; set; }



        [Display(Name = "Date Assigned")]
        [DataType(DataType.Date)]
        [DateRangeAttribute("01/01/2000")]
        public Nullable<DateTime> DateAssigned { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2000", "01/01/2010")]
        public Nullable<DateTime> StartDate { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Updated")]
        public DateTime DateUpdated { get; set; }

    }
    
}
