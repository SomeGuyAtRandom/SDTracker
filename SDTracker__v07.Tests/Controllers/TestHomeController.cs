using System;
using System.Linq;
using NUnit.Framework;
using SDTracker.Controllers;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DbImp;
using BusinessLayer.Models;
using BusinessLayer.DbInterfaces;


//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Text;
//using System.Collections.Generic;
//using PagedList;
//using PagedList.Mvc;
//using System.Web.Security;


using Moq;
using Rhino.Mocks;

namespace SDTracker.tests.Controllers
{
    [TestFixture]
    public class TestHomeController
    {

        public TestHomeController()
        {
 
        }

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
        public void Register_Returns_ViewResult()
        {
            //Set up
            HomeController controller = new HomeController(null);
            var result = controller.Register();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

 
        }

        


       

        [Test]
        public void Login_Returns_ViewResult()
        {
            // Setup     
            HomeController controller = new HomeController(null);

            // Test Result     
            var result = controller.Login("url stiring");
            var data = result;

            // Assert     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            //Assert.AreEqual("Admin", result.ViewName);

        }

        [Test]
        public void Login_Gets_Correct_Redirect()
        {
            Mock<IHomeDb> mock = new Mock<IHomeDb>();
            var repository = Rhino.Mocks.MockRepository.GenerateStub<IHomeDb>();

            UserLogin user = new UserLogin()
            {
                UserName = "account",
                Password = "Account#@1",
                RememberMe = true
            };

            repository.Stub(r => r.UserIsValid(user)).Return(true);
            repository.Stub(r => r.DoAuthen()).Return(false);
            repository.Stub(r => r.GetRoles("account")).Return(new Role[]
            {
                new Role { Id = 1, RoleType = "Admin"},
                new Role { Id = 2, RoleType = "Guest"},
                new Role { Id = 3, RoleType = "DesignEngineer"},
                new Role { Id = 4, RoleType = "HeadEngineer"}
            }.AsEnumerable());

            //GetRoles


            var controller = new HomeController(repository);
            //controller.Stub(c => c.Url.IsLocalUrl("return url")).Return(true);

            // Test Result
            var result = controller.Login(user, "return url") as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);

        }

    }
}
