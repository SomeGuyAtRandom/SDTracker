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
        public JsonResult SaveField(string objID, string fieldName, string txtValue)
        {
            bool bReturn = false;
            int id = 0;
            string sMsg = "";
            try
            {
                id = Int32.Parse(objID);

                bReturn = dbRepo.SaveField(id, fieldName, txtValue);
                if (bReturn) { sMsg = "Good"; }
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



        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int Id)
        {
            dbRepo.DeleteProject(Id);
            return RedirectToAction("Table", "Project");
        }


        //[HttpGet]
        //public ViewResult Table(int? page)
        //{
           
        //    IPagedList<Project> projects
        //        = dbRepo.Projects().ToList().ToPagedList<Project>(1, numRows);

        //    ViewData["Districts"] = GetDistrictCbo(0);
        //    ViewData["JobTypes"] = GetJobTypeCbo(0);
        //    ViewData["Field"] = GetFieldsCbo("");

        //    return View("Table",projects);
        //}

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Table(string searchTerm, string Districts, string JobTypes, string Fields, string searchDateObj, int? page)
        {
            
            int iYear = DateTime.Now.Year - 2;
            DateTime searchDate = DateTime.Parse("1/1/" + iYear);
            int distId = 0;
            int jobTypeId = 0;

            if (searchTerm == null) { searchTerm = ""; }
            try { distId = Int32.Parse(Districts); }
            catch {  }
            try { jobTypeId = Int32.Parse(JobTypes); }
            catch { }
            if (Fields == null) { Fields = ""; }
            try { searchDate = DateTime.Parse(searchDateObj); }
            catch { }
            

            //searchTerm
            // Combo Boxes
            // Note tags are plural
            ViewData["Districts"] = GetDistrictCbo(distId);
            ViewData["JobTypes"] = GetJobTypeCbo(jobTypeId);
            ViewData["Fields"] = GetFieldsCbo(Fields);
            ViewData["searchDateObj"] = searchDate;


            // Note tags are singular
            ViewBag.searchTerm = searchTerm;
            ViewBag.District = distId;
            ViewBag.JobType = jobTypeId;
            ViewBag.Field = Fields;
            ViewBag.searchDateString = searchDateObj;

            List<Project> projects = dbRepo.GetProjectsWithSearch(searchTerm, distId, jobTypeId, Fields, searchDate).ToList();

            int iPage = page ?? 1;
            return View("Table", projects.ToPagedList(iPage, numRows));
        }

        // Used in Project/Table
        // to return a list of Places as auto complete
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

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Create(string searchTerm, string Districts, int? page)
        {

            if (searchTerm == null) { searchTerm = ""; }
            int distId = 0;
            try { distId = Int32.Parse(Districts); }
            catch { }


            List<Place> places = dbRepo.GetPlacesWithSearch(searchTerm, distId).ToList();
            ViewData["Districts"] = GetDistrictCbo(distId);
            
            ViewBag.searchTerm = searchTerm;
            ViewBag.District = distId;

            
            int iPage = page ?? 1;
            return View("Create", places.ToPagedList(iPage, numRows));

        }

        [HttpPost]
        [ActionName("CreateItem")]
        public ActionResult CreateItem(string FiveDigit, int? page)
        {
            int iProjectId = dbRepo.CreateProject(FiveDigit);
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
