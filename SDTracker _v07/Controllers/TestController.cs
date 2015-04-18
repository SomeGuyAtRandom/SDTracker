using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDTracker.Common;

using BusinessLayer.DbImp;
using BusinessLayer.Models;


namespace SDTracker.Controllers
{
    public class TestController : Controller
    {
        public ViewResult Index()
        {
            District district = new District();
            return View("Index", district); 
        }

    }
}
