using System;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLayer.DbImp;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using SDTracker.Common;

//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using WebMatrix.WebData;
using BusinessLayer.DbInterfaces;
using System.Text.RegularExpressions;


namespace SDTracker.Controllers
{
    public class HomeController : Controller
    {
        private IHomeDb dbRepo;
        public HomeController(IHomeDb dbRepo)
        {
            this.dbRepo = dbRepo;
        }


        public ViewResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                if (dbRepo.AddNewUser(user))
                {
                    return RedirectToAction("", "User", user);
                }
            }
            return View("Register");
        }

        public ActionResult RegisterInfo()
        {
            var model = TempData["UserPassword"] as UserDetail;
            return View("RegisterInfo", model);
        }


        public ViewResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");

        }

        public ViewResult NoAccess()
        {
            return View("NoAccess");
        }

        public ActionResult Index()
        {
            return View("Index");
        }
        
        public ActionResult TestPage()
        {
            return View("TestPage");
        }

        [HttpPost]
        public ActionResult Login(UserLogin model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (dbRepo.UserIsValid(model))
                {
                    if (dbRepo.DoAuthen()) { FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe); }
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View("Login", model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("TempErrorPage", "User");
            }

        }


        
        public JsonResult UserNameAlreadyExists(string UserName)
        {
            bool chkUser = false;
            chkUser = dbRepo.UserExists(UserName.ToLower());
            if (chkUser)
            {
                return Json("User name is already registered ", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EmailAlreadyExists(string Email)
        {
            string msg = dbRepo.ValidateEmailSelection(Email);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateUserAtServer(string UserName)   /* ([Bind(Prefix = "UserName ")] string UserName) */
        {
            string msg = dbRepo.ValidateUserNameSelection(UserName);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        


    }
}
