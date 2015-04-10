using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
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
        string FirstName = "Test";
        string LastName = "Passman";
        string Password = "passman_1";
        RoleData Level = (RoleData)2;
        bool Active = true;

        Employee testEmp;


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
        [TestMethod]
        public void TestMethodFetchEmployeeAccess()
        {
            //Fetches the fake employee record
            bool worked = false;
            Employee testEmp2 = EmployeeAccessor.GetEmployee(FirstName, LastName);
            if (testEmp2.FirstName == "Test")
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }

        [TestMethod]
        public void TestMethodFetchEmployeeByIDAccess()
        {
            //Fetches Fake Record
            Employee testEmp1 = EmployeeAccessor.GetEmployee(FirstName, LastName);
            Employee testEmp2 = EmployeeAccessor.GetEmployee((int)testEmp1.EmployeeID);
            Assert.AreEqual(testEmp1.FirstName, testEmp2.FirstName);
        }
        [TestMethod]
        public void TestMethodFetchEmpListAccess()
        {
            //Fetches list
            List<Employee> myList = new List<Employee>();
            myList = EmployeeAccessor.GetEmployeeList();

            Assert.IsNotNull(myList);
        }
        [TestMethod]
        public void TestMethodGetEmpLoginAccess()
        {
            Employee testEmp1 = EmployeeAccessor.GetEmployee(FirstName, LastName);
            Employee testEmp2 = EmployeeAccessor.GetEmployeeLogin((int)testEmp1.EmployeeID, Password);
            bool worked = false;
            if (testEmp1.FirstName == testEmp2.FirstName)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }
        [TestMethod]
        public void TestMethodEditEmployeeAccess()
        {
            Employee testEmp1 = EmployeeAccessor.GetEmployee(FirstName, LastName);
            Employee testEmp2 = new Employee(testEmp1.FirstName, LastName, "pass123", (int)testEmp1.Level, testEmp1.Active);
            bool worked = false;
            int rows = EmployeeAccessor.UpdateEmployee(testEmp1, testEmp2);
            if (rows == 1)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }

        [TestCleanup]
        public void TestMethodDeleteEmpRecordAccess()
        {
            TestCleanupAccessor.testEmp(testEmp);

        }
    }
}
