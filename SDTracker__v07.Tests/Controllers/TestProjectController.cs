using System;
// using Microsoft.VisualStudio.TestTools.UnitTesting;

using NUnit.Framework;
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
    [TestFixture]
    public class TestProjectController
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [Test]
        public void Delete_RedirectsToAction_Table()
        {
            // Setup  

            Mock<IProjectDb> mockDeleteProject = new Mock<IProjectDb>();
            mockDeleteProject.Setup(m => m.DeleteProject(1));
            ProjectController controller = new ProjectController(mockDeleteProject.Object);

            // Test Result  
            var result = controller.Delete(1) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Table", result.RouteValues["Action"]);
            Assert.AreEqual("Project", result.RouteValues["controller"]);

        }

        [Test]
        public void Table_Returns_ViewResult_And_IPagedList()
        {

            //// Setup
            //Mock<IProjectDb> mockProjects = new Mock<IProjectDb>();
            //mockProjects.Setup(m => m.Projects()).Returns(new Project[]
            //{
            //    new Project { Id = 1, AllCDs=null, DateUpdated = DateTime.Now, CDs="", CouncilDistricts=null,CurrentComment="",CurrRemark="",DateAssigned= DateTime.Now,DateCreated= DateTime.Now,DesignEngineer=null, DesignEngineerId=0,District=null, DistrictId=0, FiveDigit="",HeadEngineer=null, HeadEngineerId=0,JobType=null,JobTypeId=0,Location="",ProjNo="", Requirements=null, Rush=false, SelectedCDs= null, StartDate= DateTime.Now },
            //    new Project { Id = 2, AllCDs=null, DateUpdated = DateTime.Now, CDs="", CouncilDistricts=null,CurrentComment="",CurrRemark="",DateAssigned= DateTime.Now,DateCreated= DateTime.Now,DesignEngineer=null, DesignEngineerId=0,District=null, DistrictId=0, FiveDigit="",HeadEngineer=null, HeadEngineerId=0,JobType=null,JobTypeId=0,Location="",ProjNo="", Requirements=null, Rush=false, SelectedCDs= null, StartDate= DateTime.Now },
            //    new Project { Id = 3, AllCDs=null, DateUpdated = DateTime.Now, CDs="", CouncilDistricts=null,CurrentComment="",CurrRemark="",DateAssigned= DateTime.Now,DateCreated= DateTime.Now,DesignEngineer=null, DesignEngineerId=0,District=null, DistrictId=0, FiveDigit="",HeadEngineer=null, HeadEngineerId=0,JobType=null,JobTypeId=0,Location="",ProjNo="", Requirements=null, Rush=false, SelectedCDs= null, StartDate= DateTime.Now }
            //}.AsEnumerable());


            //ProjectController controller = new ProjectController(mockProjects.Object);

            //// Test Result
            //var result = controller.Table(1);
            //var model = result.Model;

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.IsInstanceOf<ViewResult>(result);
            //Assert.AreEqual("Table", result.ViewName);
            //Assert.IsInstanceOf<IPagedList<Project>>(model);

            //Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["Districts"]);
            //Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["JobTypes"]);
            //Assert.IsInstanceOf<List<SelectListItem>>(result.ViewData["Field"]);

           
 
        }

        [Test]
        public void ViewBag_Is_()
        {
            // Setup

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
            //var ar = controller.About() as ViewResult;


            

            //var ar = controller.About() as ViewResult;


        }
    


    }
}
