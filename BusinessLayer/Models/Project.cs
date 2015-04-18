using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace BusinessLayer.Models
{
    public partial class Project
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
}
