using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(ProjectConfig))]
    public class Project
    {
        public Project() 
        {
            // Start out with empty constructor
        }

        public string[] AllCDs
        {
            get
            {
                string s = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16";
                return s.Split(',');

            }
            set { }

        }
        
        private string[] selectedCDs;

        public string[] SelectedCDs { 
            get
            {
                if (String.IsNullOrEmpty(CDs))
                {
                    return null;
                }
                else
                {
                    return CDs.Split(',');
                }

            }
            set 
            {
                selectedCDs = value;
                string temp = "";
                if (value != null)
                { 
                    foreach (string s in selectedCDs)
                    {
                        temp += s + ",";
                    }
                    if (temp.Length > 1) 
                    {
                        temp = temp.Substring(0, temp.Length - 2);
                    }
                }
                this.CDs = temp;
            } 
        }

        public IEnumerable<SelectListItem> CouncilDistricts { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public int Id { get; set; }

        public string Location { get; set; }

        public District District { get; set; }
        public int DistrictId { get; set; }

        public string CurrRemark { get; set; }
        public bool Rush { get; set; }
        public string ProjNo { get; set; }
        public string FiveDigit { get; set; }
        public string CDs { get; set; }

        public JobType JobType { get; set; }

        public int JobTypeId { get; set; }

        public AdminUser HeadEngineer { get; set; }
        public int HeadEngineerId { get; set; }

        public AdminUser DesignEngineer { get; set; }
        public int DesignEngineerId { get; set; }

        public string CurrentComment { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        
        private District setGetDistrict(SqlDataReader rdr)
        {
            District district = new District();
            district.Id = Convert.ToInt32(rdr["Id"]);

            if (!(rdr["Code"] is DBNull))
            {
                district.Code = rdr["Code"].ToString();
            }
            else { district.Code = ""; }

            if (!(rdr["Name"] is DBNull))
            {
                district.Name = rdr["Name"].ToString();
            }
            else { district.Name = ""; }


            return district;

        }

    }

    public class ProjectConfig
    {
        [Display(Name = "Design Engineer")]
        public int DesignEngineerId { get; set; }

        [Display(Name = "Head Engineer")]
        public int HeadEngineerId { get; set; }


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
        [Range(typeof(DateTime), "01/01/2000", "01/01/2020")]
        public Nullable<DateTime> DateAssigned { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2000", "01/01/2020")]
        public Nullable<DateTime> StartDate { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Updated")]
        [DataType(DataType.Date)]
        public DateTime DateUpdated { get; set; }

    }
 
}
