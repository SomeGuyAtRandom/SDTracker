using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;

using NUnit.Framework;
using SDTracker.Controllers;
using Moq;
using BusinessLayer.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using BusinessLayer.DbInterfaces;


namespace SDTracker.tests.Controllers
{
    /// <summary>
    /// Summary description for TestAdminController
    /// </summary>
    [TestFixture]
    public class TestAdminController
    {
        private Mock<IAdminDb> mockGetAdminUserById;
        private Mock<IAdminDb> mockGetUsersWithSearch;

        public TestAdminController()
        {
            this.mockGetAdminUserById = new Mock<IAdminDb>();
            this.mockGetAdminUserById.Setup(m => m.getAdminUserById(1)).Returns(new AdminUser
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

            this.mockGetUsersWithSearch = new Mock<IAdminDb>();
            this.mockGetUsersWithSearch.Setup(m => m.GetUserWithSearch("")).Returns(new AdminUser[]
            {
                new AdminUser { Id = 1, UserName = "XX", FirstName="XX", LastName="XX", Initials="XX", Email= "1@1.com", Phone="(818)555-0001", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
                new AdminUser { Id = 2, UserName = "YY", FirstName="YY", LastName="YY", Initials="YY", Email= "2@2.com", Phone="(818)555-0002", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
                new AdminUser { Id = 3, UserName = "ZZ", FirstName="ZZ", LastName="ZZ", Initials="ZZ", Email= "3@3.com", Phone="(818)555-0003", DateCreated = DateTime.Now, DateUpdated = DateTime.Now }
            }.AsEnumerable());
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
        public void UserList_Returns_ViewResult_and_ToPagedList()
        {
            AdminController controller = new AdminController(this.mockGetUsersWithSearch.Object);
            // Test Result
            var result = controller.UserList(1);
            var model = result.Model;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("UserList", result.ViewName);
            Assert.IsInstanceOf<PagedList<AdminUser>>(model);

        }

        [Test]
        public void GetUserWithSearch_Returns_JsonResult()
        {

            // Setup     
            AdminController controller = new AdminController(mockGetUsersWithSearch.Object);

            // Test Result     
            var result = controller.GetUserWithSearch("") as JsonResult;

            var data = result.Data;
            var type = data.GetType();
            var countPropertyInfo = type.GetProperty("Count");
            var expectedCount = countPropertyInfo.GetValue(data, null);

            // Assert     
            Assert.IsInstanceOf<JsonResult>(result);
            Assert.AreEqual(3, expectedCount);

        }


        [Test]
        public void UserList_with_Search_Returns_ViewResult_and_ToPagedList()
        {
            // Setup
            AdminController controller = new AdminController(mockGetAdminUserById.Object);

            // Test Result
            var result = controller.UserList("Some search Term", 1);
            var model = result.Model;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("UserList", result.ViewName);
            Assert.IsInstanceOf<PagedList<AdminUser>>(model);
        }

        [Test]
        public void EditProfile_Returns_ViewResult()
        {
            AdminController controller = new AdminController(mockGetAdminUserById.Object);

            // Test Result
            var result = controller.EditProfile(1);
            var model = result.Model;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("EditProfile", result.ViewName);

            Assert.IsInstanceOf<AdminUser>(model);

        }

        [Test]
        public void AddRemoveRole_Returns_JsonResult()
        {
            // Setup     
            AdminController controller = new AdminController(null);
 
        }
    }
}
