using System;
using System.Collections.Generic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    public class EmployeeManager
    {
        // Success: An int value is returned to the method to show rows affected
        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method takes in newEmployee and passes it as a parameter into the AddEmployee method of the EmployeeAccessor class
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <exception cref="Exception">Exception is thrown if database is not available or new employee cannot be created in the database for any reason</exception>
        /// <returns>An int value is returned to the method to show rows affected</returns>
        public int AddNewEmployee(Employee newEmployee)
        {
            return EmployeeAccessor.AddEmployee(newEmployee);
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method takes in new and old employee parameters and then submits them to the Data Access Layer method to update the employee record for oldEmploy
        /// </summary>
        /// <remarks>
        /// This will also take the place of a 'Delete' method
        ///     The user will simply mark the employee inactive which will change the
        ///     bit value in the table to a 0 to represent false
        /// </remarks>
        /// <param name="oldEmployee"></param>
        /// <param name="newEmployee"></param>
        /// <exception cref="Exception">EmployeeAccessor method will throw exception to Manager saying that the employee could not be found to edit</exception>
        /// <returns>Employee information is updatd in the table and an integer is returned to represent rows affected</returns>
        public int EditCurrentEmployee(Employee oldEmployee, Employee newEmployee)
        {
            return EmployeeAccessor.UpdateEmployee(oldEmployee, newEmployee);
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method takes in two parameters that will hold the employee's
        ///     first and last name. This information is passed to the access layer
        ///     where it is used to find the employee in question and return that
        ///     employee's information to the method and then to the presentation
        ///     layer calling method
        /// </summary>
        /// <param name="empID"></param>
        /// <exception cref="Exception">An exception is thrown from the Access Layer asking the user to try their search again</exception>
        /// <returns>The employee object is returned to the method successfully</returns>
        public Employee FetchEmployee(int empID)
        {
            return EmployeeAccessor.GetEmployee(empID);
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method takes in new and old employee parameters and then submits them to the
        ///     Data Access Layer method to update the employee record for oldEmployee
        ///     with the the information held in newEmployee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public Employee FetchEmployee(string firstName, string lastName)
        {
			return EmployeeAccessor.GetEmployee(firstName, lastName);
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method makes a call to getEmployeeList method from the EmployeeAccessor to retrieve a list of all active employees
        /// </summary>
        /// <exception cref="Exception">Exception is thrown from Accessor that states that employee could not be found in the database</exception>
        /// <returns>The employee list is retrieved and returned up to the presentation layer (calling method)</returns>
        public List<Employee> FetchListEmployees()
        {
			return EmployeeAccessor.GetEmployeeList();
        }

        /// <summary>
        /// Arik Chadima
        /// 2015/03/03
        /// 
        /// Attempts to fetch an employee with the given credentials from the access layer.
        /// Failure: ApplicationException if the login was bad, and SqlException of some kind if it's a connection issue.
        /// </summary>
        /// <param name="empId">The employee's ID</param>
        /// <param name="empPassword">the employee's Password</param>
        /// <returns>The employee object with the given credentials.</returns>
        public Employee GetEmployeeLogin(int empId, string empPassword)
        {
			return EmployeeAccessor.GetEmployeeLogin(empId, empPassword);
        }
    }
}