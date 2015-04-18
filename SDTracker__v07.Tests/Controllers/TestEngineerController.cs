using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using SDTracker.Controllers;
using BusinessLayer.Models;
using Moq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

using BusinessLayer.DbInterfaces;

namespace SDTracker.tests.Controllers
{
    /// <summary>
    /// Summary description for EngineerControllerTest
    /// </summary>
    [TestFixture]
    public class TestEngineerController
    {
        private Mock<IEngineerDb> mockGetEngineerById;

        public TestEngineerController()
        {
            this.mockGetEngineerById = new Mock<IEngineerDb>();
            this.mockGetEngineerById.Setup(m => m.getEngineerUserId(1)).Returns(new Engineer
            {
                Id = 1,
                UserName = "XX",
                FirstName = "XX",
                LastName = "XX",
                Initials = "XX",
                Email = "1@1.com",
                Phone = "(818)555-0001",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            });

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
        public void EditProfile_Returns_ViewResult_and_Engineer()
        {
            // Setup
            EngineerController controller = new EngineerController(mockGetEngineerById.Object);

            // Test Result
            var result = controller.EditProfile(1);
            var model = result.Model;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("EditProfile", result.ViewName);

            Assert.IsInstanceOf<Engineer>(model);
        }

        
    
    
    }
}
