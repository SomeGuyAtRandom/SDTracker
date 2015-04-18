using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.DbImp;

using BusinessLayer.DbInterfaces;
using PagedList.Mvc;
using PagedList;


namespace SDTracker.Controllers
{
    public class EngineerController : Controller
    {
       // private EngineerDbContext db = new EngineerDbContext();
        private IEngineerDb dbRepo;

        public EngineerController(IEngineerDb dbRepo)
        {
            this.dbRepo = dbRepo;
        }

        [HttpGet]
        public ViewResult EditProfile(int Id)
        {
            Engineer engineer = dbRepo.getEngineerUserId(Id);
            return View("EditProfile", engineer);
        }


        [HttpPost]
        public JsonResult SaveField(string objID, string fieldName, string txtValue )
        {
            bool bReturn = false;
            int id = 0;
            try 
            {
                id = Int32.Parse(objID);

                bReturn = dbRepo.SaveField(id, fieldName, txtValue);
            }
            catch (Exception e) 
            {
                string msg = e.ToString();
                //TODO: ADD some logging function
                bReturn = false;
            }
            return Json(bReturn);

        }

        
    }
}
