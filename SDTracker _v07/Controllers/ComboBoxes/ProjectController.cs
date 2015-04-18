using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BusinessLayer.DbImp;
using BusinessLayer.Models;
using System.Globalization;

using BusinessLayer.DbInterfaces;


namespace SDTracker.Controllers
{
    public partial class ProjectController : Controller
    {
        private List<SelectListItem> GetCbo(int selectedId, IEnumerable<IProjectDb> objs)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Object obj in objs)
            {
                string sText = "";
                string sValue = "";
                SelectListItem item = new SelectListItem
                {
                    Text = sText,
                    Value = sValue
                    //Selected = (selectedId == item.Id)
                };
            }
            return items;

        }

        private List<SelectListItem> GetHeadEngineerCbo(int selectedId)
        {
            List<SelectListItem> headEngineerCbo = new List<SelectListItem>();
            foreach (AdminUser headEng in dbRepo.HeadEngineers())
            {
                SelectListItem item = new SelectListItem
                {
                    Text = headEng.FirstName + " " + headEng.LastName,
                    Value = "" + headEng.Id,
                    Selected = (selectedId == headEng.Id)
                };
                headEngineerCbo.Add(item);

            }
            return headEngineerCbo;

        }

        private List<SelectListItem> GetDesignEngineerCbo(int selectedId)
        {
            List<SelectListItem> designEngineerCbo = new List<SelectListItem>();
            foreach (AdminUser designEng in dbRepo.HeadEngineers())
            {
                SelectListItem item = new SelectListItem
                {
                    Text = designEng.FirstName + " " + designEng.LastName,
                    Value = "" + designEng.Id,
                    Selected = (selectedId == designEng.Id)
                };
                designEngineerCbo.Add(item);

            }
            return designEngineerCbo;

        }

        private List<SelectListItem> GetJobTypeCbo(int selectedId)
        {
            List<SelectListItem> jobTypeCbo = new List<SelectListItem>();
            foreach (JobType jobType in dbRepo.JobTypes())
            {
                SelectListItem item = new SelectListItem
                {
                    Text = jobType.Name,
                    Value = "" + jobType.Id,
                    Selected = (selectedId == jobType.Id)
                };
                jobTypeCbo.Add(item);

            }
            return jobTypeCbo;

        }

        private List<SelectListItem> GetDistrictCbo(int selectedId)
        {
            List<SelectListItem> districtCbo = new List<SelectListItem>();
            foreach (District district in dbRepo.Districts())
            {
                SelectListItem item;
                item = new SelectListItem
                {
                    Text = district.Name,
                    Value = "" + district.Id,
                    Selected = (selectedId == district.Id)
                };
                districtCbo.Add(item);

            }
            return districtCbo;

        }

        private List<SelectListItem> GetFieldsCbo(string selectedField)
        {
            List<SelectListItem> dateFieldsCbo = new List<SelectListItem>();

            SelectListItem itemStartDate = new SelectListItem
            {
                Text = "Start Date >=",
                Value = "StartDate",
                Selected = (selectedField == "StartDate")
                
            };
            dateFieldsCbo.Add(itemStartDate);

            SelectListItem itemDateCreated = new SelectListItem
            {
                Text = "Date Created >=",
                Value = "DateCreated",
                Selected = (selectedField == "DateCreated")

            };
            dateFieldsCbo.Add(itemDateCreated);

            SelectListItem itemDateUpdated = new SelectListItem
            {
                Text = "Last Update >=",
                Value = "DateUpdated",
                Selected = (selectedField == "DateUpdated")

            };
            dateFieldsCbo.Add(itemDateUpdated);


            return dateFieldsCbo;
        }
    }
}
