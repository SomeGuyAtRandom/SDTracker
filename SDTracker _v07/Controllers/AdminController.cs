using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SDTracker.Common;
using PagedList;

using BusinessLayer.Models;
using BusinessLayer.DbInterfaces;



namespace SDTracker.Controllers
{
    [AuthorizerAttribute(Roles = ("Admin"))]
    public class AdminController : Controller
    {
        private IAdminDb dbRepo;

        public ActionResult Create()
        {
            return RedirectToAction("Register", "Home");
        }


        public AdminController(IAdminDb dbRepo)
        {
            this.dbRepo = dbRepo;
        }

        public ViewResult UserList(int? page)
        {
            IEnumerable<AdminUser> users = dbRepo.GetUserWithSearch("").ToList();
            return View("UserList", users.ToPagedList(page ?? 1, 5));
        }

        [HttpPost]
        public ViewResult UserList(string searchTerm, int? page)
        {
            List<AdminUser> users = dbRepo.GetUserWithSearch(searchTerm).ToList();
            return View("UserList", users.ToPagedList(page ?? 1, 5));
        }

        public ViewResult EditProfile(int Id)
        {
            AdminUser user = dbRepo.getAdminUserById(Id);
            return View("EditProfile", user);
        }

        public JsonResult GetUserWithSearch(string term)
        {

            List<AdminUser> users;
            users = dbRepo.GetUserWithSearch(term).ToList();
            List<string> returnVals = new List<string>();
            foreach (AdminUser p in users)
            {
                returnVals.Add(p.FirstName + " " + p.LastName);
            }

            return Json(returnVals, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddRemoveRole(string objID, string roleName, string isMember)
        {
            bool bReturn = false;
            string sMsg = "";
            int iId = 0;
            bool bIsMember = false;
            try 
            {
                iId = Int32.Parse(objID);
                bIsMember = Boolean.Parse(isMember);

                bReturn = dbRepo.AddRemoveRole(iId, roleName, bIsMember);
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
        public JsonResult EnableDisableUser(string objID, string isDisabled)
        {
            bool bReturn = false;
            int id = 0;
            bool bIsDisabled = false;
            string sMsg = "";
            try
            {
                id = Int32.Parse(objID);
                bIsDisabled = Boolean.Parse(isDisabled);
                bReturn = dbRepo.EnableDisableUser(id, bIsDisabled);
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

    }
}
