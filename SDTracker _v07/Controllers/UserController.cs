using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.DbImp;

using System.Web.Security;
using DotNetOpenAuth.AspNet;
using SDTracker.Common;

using BusinessLayer.DbInterfaces;


namespace SDTracker.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        private IUserDb dbRepo;
        public UserController(IUserDb dbRepo)
        {
            this.dbRepo = dbRepo;
 
        }

        public ViewResult Index()
        {
            return View("Index");
        }

        public ViewResult Secure()
        {
            return View("Secure");
        }

        [AuthorizerAttribute(Roles = ("HeadEngineer, DesignEngineer"))]
        public ViewResult DesignEngineer()
        {
            return View("DesignEngineer");
        }

        [AuthorizerAttribute(Roles = "Admin"), HandleError]
        public ViewResult Admin()
        {
            return View("Admin");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers


        [AuthorizerAttribute(Roles = "Guest"), HandleError]
        public ViewResult TempErrorPage()
        {
            return View("TempErrorPage");
        }

        
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url == null)
            { 
                return RedirectToAction("Index", "Home");
            }
            else if(Url.IsLocalUrl(returnUrl)){
                return Redirect(returnUrl);
            }
            else{
                return RedirectToAction("TempErrorPage", "User");
            }

        }
        #endregion

        public JsonResult UserNameAlreadyExists(string UserName)
        {
            string msg = String.Format("The username {0} is available", UserName);

            if (dbRepo.UserExists(UserName))
            {
                msg = String.Format("{0} is NOT available.", UserName);
            }
            else 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InitialsAlreadyExists(string Initials)
        {
            string msg = String.Format("The initials {0} are already used by another user.", Initials);

            if (dbRepo.InitialsExixts(Initials))
            {
                msg = String.Format("The initials {0} are already used by another user.", Initials);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public void ChangePassword()
        {
 
        }

        // This action is called via GET
        // To cleare the password from the URL - I beleive ther is a masking attribute/method
        // But for now.. the password will be set to null at the redirection
        // See 
        public ViewResult RegisterInfo(Engineer engineer)
        {
            return View("RegisterInfo", engineer);
        }

        // save field is used in the RegisterInfo View
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


        


    }
}
