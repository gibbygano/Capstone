using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    ///Created By: Tony Noel- 2015/4/10, Updated 2015/5/05
    /// <summary>
    /// A complete listing of Employee Accessor tests. Creates a dummy employee record and performs actions on it
    /// based on the Accessor methods.
    /// Updates: Added new test methods. Updated old ones to match Accessor. Commented each method.
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
            addEMP();
        }
        public void addEMP()
        {
            //Adds the fake record.
            int rows = EmployeeAccessor.AddEmployee(testEmp);
        }
        [TestMethod]
        public void EmployeeAddEmployeeAccess()
        {
            //Adds fake employee to Data base
            int rows = EmployeeAccessor.AddEmployee(testEmp);
            //Asserts that one row was affected.
            Assert.AreEqual(1, rows);
        }
        [TestMethod]
        public void EmployeeGetEmployeeByIDAccess()
        { 
            //Grab the fake employeeID number
            int ID = TestCleanupAccessor.getTestEmp();
            //Callls to Employee Accessor to grab an employee record by id
            Employee fake = EmployeeAccessor.GetEmployee(ID);
            Assert.AreEqual("Test", fake.FirstName);
        }
        [TestMethod]
        public void EmployeeFetchEmpListAccess()
        {
            //Fetches list
            List<Employee> myList = new List<Employee>();
            myList = EmployeeAccessor.GetEmployeeList();
            bool worked = false;
            //Tests that a list greater than one is being returned.
            if (myList.Count > 1)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        }
        [TestMethod]
        public void EmployeeUpdateEmployeeAccess()
        {   //Grabs the fake emp ID
            int ID = TestCleanupAccessor.getTestEmp();
            //Grabs the entire Employee record from Accessor
            Employee orig = EmployeeAccessor.GetEmployee(ID);
            //Creates the new employee record with all of the original, changes the active property to false.
            Employee newEmp = new Employee(orig.FirstName, orig.LastName, orig.Password, (int)orig.Level, false);
            int changed = EmployeeAccessor.UpdateEmployee(orig, newEmp);
            //Asserts that the update went through with one row affected.
            Assert.AreEqual(1, changed);
        }
        [TestMethod]
        public void EmployeeGetEmployeeLoginAccess()
        {
            //Grabs the fake emp ID
            int ID = TestCleanupAccessor.getTestEmp();
            Employee fake = EmployeeAccessor.GetEmployeeLogin(ID, Password);
            //Asserts that the employee record being returned matches the fake employee record in the setup
            Assert.AreEqual("Test", fake.FirstName);
        }
        [TestCleanup]
        public void EmployeeDeleteEmpRecordAccess()
        {
            TestCleanupAccessor.testEmp(testEmp);
        }
    }
}