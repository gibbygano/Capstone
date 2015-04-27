using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    ///Created By: Tony Noel- 2015/4/10
    /// <summary>
    /// A complete listing of Employee Accessor tests. Creates a dummy employee record and performs actions on it
    /// based on the Accessor methods.
    /// </summary>
    [TestClass]
    public class EmployeeAccessorTests
    {
        //Test values
        private string FirstName = "Test";

        private string LastName = "Passman";
        private string Password = "passman_1";
        private RoleData Level = (RoleData)2;
        private bool Active = true;

        private Employee testEmp;

        [TestInitialize]
        public void EmployeeAccessTestSetup()
        {
            //creating new dummy Employee object
            testEmp = new Employee(FirstName, LastName, Password, (int)Level, Active);
            TestMethodAddEmployeeAccess();
        }

        [TestMethod]
        public void TestMethodAddEmployeeAccess()
        {
            //Adds fake employee to Data base
            bool worked = false;
            int rows = EmployeeAccessor.AddEmployee(testEmp);
            if (rows == 1)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }

        //[TestMethod]
        //public void TestMethodFetchEmployeeAccess()
        //{
        //    //Fetches the fake employee record
        //    bool worked = false;
        //    Employee testEmp2 = EmployeeAccessor.GetEmployee(FirstName, LastName);
        //    if (testEmp2.FirstName == "Test")
        //    {
        //        worked = true;
        //    }
        //    Assert.IsTrue(worked);
        //}

        //[TestMethod]
        //public void TestMethodFetchEmployeeByIDAccess()
        //{
        //    //Fetches Fake Record
        //    Employee testEmp1 = EmployeeAccessor.GetEmployee(FirstName, LastName);
        //    Employee testEmp2 = EmployeeAccessor.GetEmployee((int)testEmp1.EmployeeID);
        //    Assert.AreEqual(testEmp1.FirstName, testEmp2.FirstName);
        //}

        [TestMethod]
        public void TestMethodFetchEmpListAccess()
        {
            //Fetches list
            List<Employee> myList = new List<Employee>();
            myList = EmployeeAccessor.GetEmployeeList();

            Assert.IsNotNull(myList);
        }

        [TestCleanup]
        public void TestMethodDeleteEmpRecordAccess()
        {
            TestCleanupAccessor.testEmp(testEmp);
        }
    }
}