using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BusinessLayer.DbImp;
using BusinessLayer.Models;

using BusinessLayer.DbInterfaces;

namespace SDTracker.Controllers
{
    public partial class ProjectController : Controller
    {
        private IProjectDb dbRepo;

        public ProjectController(IProjectDb dbRepo)
        {
            this.dbRepo = dbRepo;
        }

        private int numRows = 10;

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int Id)
        {
            dbRepo.DeleteProject(Id);
            return RedirectToAction("Table", "Project");
        }


        [HttpGet]
        [ActionName("Table")]
        public ViewResult Table(int? page)
        {
            
            int showPage = page ?? 1;
            IPagedList<Project> projects
                = dbRepo.Projects().ToList().ToPagedList<Project>(showPage, numRows);

            ViewData["Districts"] = GetDistrictCbo(0);
            ViewData["JobTypes"] = GetJobTypeCbo(0);
            ViewData["Field"] = GetFieldsCbo("");


            return View("Table",projects);
        }


        [HttpPost]
        [ActionName("Table")]
        public ActionResult Table(string searchTerm, string Districts, string JobTypes, string Field, string searchDate, int? page)
        {
            
            DateTime d = DateTime.Now.AddYears(-25);
            int distId = 0;
            int jobTypeId = 0;
            try
            {
                d = DateTime.Parse(searchDate);
            }
            catch { }

            try
            {
                distId = Int32.Parse(Districts);
            }
            catch {  }

            try
            {
                jobTypeId = Int32.Parse(JobTypes);
            }
            catch { }

            string fieldSelected = Field;



            List<Project> projects = dbRepo.GetProjectsWithSearch(searchTerm, distId, jobTypeId, fieldSelected, d).ToList();


            ViewData["JobTypes"] = GetJobTypeCbo(jobTypeId);
            ViewData["Districts"] = GetDistrictCbo(distId);
            ViewData["Field"] = GetFieldsCbo(fieldSelected);
            ViewData["searchDate"] = d;


            return View("Table", projects.ToPagedList(page ?? 1, numRows));
        }


        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(string searchTerm, string Districts, int? page)
        {
            
            int distId = 0;

            try
            {
                distId = Int32.Parse(Districts);
            }
            catch { }
            List<Place> places = dbRepo.GetPlacesWithSearch(searchTerm, distId).ToList();

            ViewData["Districts"] = GetDistrictCbo(distId); 


            return View("Create", places.ToPagedList(page ?? 1, numRows));

        }


        [HttpGet]
        public ActionResult Create(int? page)
        {
            
            int showPage = page ?? 1;
            IPagedList<Place> places
                = dbRepo.Places().ToList().ToPagedList<Place>(showPage, numRows);

            ViewData["Districts"] = GetDistrictCbo(0); 

            return View("Create", places);
        }


        [HttpPost]
        [ActionName("CreateItem")]
        public ActionResult CreateItem(int Id, int? page)
        {
            int iProjectId = dbRepo.CreateProject(Id);
            return RedirectToAction("Edit", new { Id = iProjectId });
        }

        
        [HttpGet]
        public ActionResult Edit(int Id)
        {

            Project project = dbRepo.GetProject(Id);

            ViewData["Districts"] = GetDistrictCbo(project.DistrictId); ;
            ViewData["HeadEngineers"] = GetHeadEngineerCbo(project.HeadEngineerId);
            ViewData["DesignEngineers"] = GetDesignEngineerCbo(project.DesignEngineerId);
            ViewData["JobTypes"] = GetJobTypeCbo(project.JobTypeId);

            
            List<SelectListItem> allCDsMultiSelect = new List<SelectListItem>();

            for (int i = 1; i < 20; i++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = "" + i,
                    Value = "" + i,
                    Selected = (true)
                };
            }

            ViewData["AllCDs"] = allCDsMultiSelect;

            return View("Edit", project);
        }


        [HttpPost]
        public JsonResult SaveField(string objID, string fieldName, string txtValue)
        {
            bool bReturn = false;
            int id = 0;
            string sMsg = "";
            try
            {
                id = Int32.Parse(objID);

                bReturn = dbRepo.SaveField(id, fieldName, txtValue);
                if(bReturn){sMsg = "Good"; }
            }
            catch (Exception e)
            {
                string msg = e.ToString();
                sMsg = msg;
                //TODO: ADD some logging function
                bReturn = false;
            }
            return Json(new { IsUpdated = bReturn, Msg = sMsg });
        }


        [HttpGet]
        public JsonResult GetProjectsSearch(string term, string Districts)
        {
            
            List<Project> projects;
            projects = dbRepo.GetProjectsWithSearch(term, 0, 0, "noFieldSelected", DateTime.Now.AddYears(-25)).ToList();
            List<string> returnVals = new List<string>();
            foreach (Project p in projects)
            {
                returnVals.Add(p.Location);
            }

            return Json(returnVals, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetPlacesSearch(string term)
        {
            
            List<Place> places;
            places = dbRepo.GetPlacesWithSearch(term, 0).ToList();
            List<string> returnVals = new List<string>();
            foreach (Place p in places)
            {
                returnVals.Add(p.PlaceLocation);
            }

            return Json(returnVals, JsonRequestBehavior.AllowGet);
 
        }
       
    }
}
