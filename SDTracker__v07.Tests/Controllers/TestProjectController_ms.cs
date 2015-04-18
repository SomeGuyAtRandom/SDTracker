using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SDTracker.Controllers;
using Moq;
using BusinessLayer.Models;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PagedList;
using PagedList.Mvc;

using BusinessLayer.DbInterfaces;


namespace SDTracker.tests.Controllers
{
    [TestClass]
    public class TestProjectController_ms
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IProjectDb> mockDistrict = new Mock<IProjectDb>();
            mockDistrict.Setup(m => m.Districts()).Returns(new District[]
            {
                new District { Id = 1, Code="w", DateUpdated = DateTime.Now, Name="ww" },
                new District { Id = 2, Code="x", DateUpdated = DateTime.Now, Name="xx" },
                new District { Id = 3, Code="y", DateUpdated = DateTime.Now, Name="yy" },
                new District { Id = 4, Code="z", DateUpdated = DateTime.Now, Name="xx" }
            }.AsEnumerable().ToList());

            //List<SelectListItem> 

            var controller = new ProjectController(mockDistrict.Object);
            var ar = controller.Table(1) as ViewResult;
        }
    }
}
