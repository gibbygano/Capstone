using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.DataAccess;
namespace EmployeeLogicTests
{   ///Created by: Tony Noel, 2015/3/27, Updated 2015/4/10
    /// <summary>
    /// A complete testing of all employee logic methods in the Employee Manager.
    ///The test creates a dummy employee record and performs the various methods in the manager using this record.
    /// </summary>
    [TestClass]
    public class EmployeeTests
    {   //Test values
        string FirstName = "Test";
        string LastName = "Passman";
        string Password = "passman_1";
        RoleData Level = (RoleData)2;
        bool Active = true;

        Employee testEmp;
        EmployeeManager myManager;

        [TestInitialize]
        public void EmployeeTestSetup()
        {
            myManager = new EmployeeManager();
            testEmp = new Employee(FirstName, LastName, Password, (int)Level, Active);
            TestMethodAddEmployee();
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

        [TestMethod]
        public void TestMethodAddEmployee()
        {
            //Adds fake employee to Data base
            bool worked = false;
            ResultsEdit result = myManager.AddNewEmployee(testEmp);
            if (result == ResultsEdit.Success)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }
        [TestMethod]
        public void TestMethodFetchEmployee()
        {
            bool worked = false;
            Employee testEmp2 = myManager.FetchEmployee(FirstName, LastName);
            if (testEmp2.FirstName == "Test")
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }

        [TestMethod]
        public void TestMethodFetchEmployeeByID()
        {
            Employee testEmp1 = myManager.FetchEmployee(FirstName, LastName);
            Employee testEmp2 = myManager.FetchEmployee((int)testEmp1.EmployeeID);
            Assert.AreEqual(testEmp1.FirstName, testEmp2.FirstName);
        }
        [TestMethod]
        public void TestMethodFetchEmpList()
        {
            List<Employee> myList = new List<Employee>();
            myList = myManager.FetchListEmployees();

            Assert.IsNotNull(myList);
        }
        [TestMethod]
        public void TestMethodGetEmpLogin()
        {
            Employee testEmp1 = myManager.FetchEmployee(FirstName, LastName);
            Employee testEmp2 = myManager.GetEmployeeLogin((int)testEmp1.EmployeeID, Password);
            bool worked = false;
            if (testEmp1.FirstName == testEmp2.FirstName)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }
        [TestMethod]
        public void TestMethodEditEmployee()
        {
            Employee testEmp1 = myManager.FetchEmployee(FirstName, LastName);
            Employee testEmp2 = new Employee(testEmp1.FirstName, LastName, "pass123", (int)testEmp1.Level, testEmp1.Active);
            bool worked = false;
            ResultsEdit result = myManager.EditCurrentEmployee(testEmp1, testEmp2);
            if (result == ResultsEdit.Success)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }

        [TestCleanup]
        public void TestMethodDeleteRecord()
        {
            TestCleanupAccessor.testEmp(testEmp);

        }
    }
}
