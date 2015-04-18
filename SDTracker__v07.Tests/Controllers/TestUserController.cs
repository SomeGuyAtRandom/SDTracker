using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


using NUnit.Framework;
using SDTracker.Controllers;

using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BusinessLayer.DbImp;
using BusinessLayer.Models;
using System.Web.Security;

using Moq;
//using Rhino.Mocks;

namespace SDTracker.tests.Controllers
{
    /// <summary>
    /// Summary description for TestUserControlle
    /// </summary>
    /// 

    

    [TestFixture]
    public class TestUserController
    {
        public TestUserController()
        {
            //
            // TODO: Add constructor logic here
            //
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test]
        public void Index_Returns_ViewResult()
        {

            // Setup     
            UserController controller = new UserController(null);

            // Test Result     
            var result = controller.Index() as ViewResult;
            var data = result;

            // Assert     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("Index", result.ViewName);
            
        }

        [Test]
        public void Secure_Returns_ViewResult()
        {
            // Setup     
            UserController controller = new UserController(null);

            // Test Result     
            var result = controller.Secure();
            var data = result;

            // Assert     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("Secure", result.ViewName);
 
        }

        [Test]
        public void DesignEngineer_Returns_ViewResult()
        {
            // Setup     
            UserController controller = new UserController(null);

            // Test Result     
            var result = controller.DesignEngineer();
            var data = result;

            // Assert     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("DesignEngineer", result.ViewName);
 
        }

        [Test]
        public void Admin_Returns_ViewResult()
        {
            // Setup     
            UserController controller = new UserController(null);

            // Test Result     
            var result = controller.Admin();
            var data = result;

            // Assert     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("Admin", result.ViewName);

        }


        [Test]
        public void Login_IsValid_And_RedirectToLocal_Url()
        {
            

        }



    }
}
