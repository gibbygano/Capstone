using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    public class EmployeeManager
    {
        // Success: A ResultsEdit.Success value is returned
        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        ///
        /// <remarks>
        /// Updated 2015/04/13 by Tony Noel -Updated to comply with the ResultsEdit class of error codes.
        /// </remarks>
        /// Method takes in newEmployee and passes it as a parameter into the AddEmployee method of the EmployeeAccessor class
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <exception cref="Exception">Exception is thrown if database is not available or new employee cannot be created in the database for any reason</exception>
        /// <returns>An int value is returned to the method to show rows affected</returns>
        public ResultsEdit AddNewEmployee(Employee newEmployee)
        {
            try
            {
                int worked = EmployeeAccessor.AddEmployee(newEmployee);
                if (worked == 1)
                {
                    return ResultsEdit.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ResultsEdit.DatabaseError;
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        ///
        ///
        /// Method takes in new and old employee parameters and then submits them to the Data Access Layer method to update the employee record for oldEmploy
        /// </summary>
        /// <remarks>
        /// This will also take the place of a 'Delete' method
        ///     The user will simply mark the employee inactive which will change the
        ///     bit value in the table to a 0 to represent false
        /// </remarks>
        ///
        /// <remarks>
        ///  Updated 2015/04/13 by Tony Noel -Updated to comply with the ResultsEdit class of error codes.
        /// </remarks>
        /// <param name="oldEmployee"></param>
        /// <param name="newEmployee"></param>
        /// <exception cref="Exception">EmployeeAccessor method will throw exception to Manager saying that the employee could not be found to edit</exception>
        /// <returns>Employee information is updatd in the table and an integer is returned to represent rows affected</returns>
        public ResultsEdit EditCurrentEmployee(Employee oldEmployee, Employee newEmployee)
        {
            try
            {
                int result = EmployeeAccessor.UpdateEmployee(oldEmployee, newEmployee);
                if (result == 1)
                {
                    return ResultsEdit.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ResultsEdit.DatabaseError;
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        ///
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
            try
            {
                return EmployeeAccessor.GetEmployee(empID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        ///
        /// Method takes in new and old employee parameters and then submits them to the
        ///     Data Access Layer method to update the employee record for oldEmployee
        ///     with the the information held in newEmployee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public Employee FetchEmployee(string firstName, string lastName)
        {
            try
            {
                return EmployeeAccessor.GetEmployee(firstName, lastName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        ///
        /// Method makes a call to getEmployeeList method from the EmployeeAccessor to retrieve a list of all active employees
        /// </summary>
        /// <exception cref="Exception">Exception is thrown from Accessor that states that employee could not be found in the database</exception>
        /// <returns>The employee list is retrieved and returned up to the presentation layer (calling method)</returns>
        public List<Employee> FetchListEmployees()
        {
            try
            {
                return EmployeeAccessor.GetEmployeeList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/03/03
        ///
        /// Attempts to fetch an employee with the given credentials from the access layer.
        /// Failure: ApplicationException if the login was bad, and SqlException of some kind if it's a connection issue.
        /// </summary>
        /// <param name="empId">The employee's ID</param>
        /// <param name="empPassword">the employee's Password</param>
        /// <returns>The employee object with the given credentials.</returns>
        public Employee GetEmployeeLogin(int empId, string empPassword)
        {
            try
            {
                return EmployeeAccessor.GetEmployeeLogin(empId, empPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///Justin Pennington
        ///Created: 2015/04/14
        ///
        ///Attempts to retrieve an employee based on First, Last, or job title
        ///Failure: If nothing is found, it will return an empty list
        ///</summary>
        ///<param name = "inSearch">String search is based on</param>
        ///<returns>List of Employee objects.</returns>
        public List<Employee> SearchEmployee(string inSearch)
        {
            if (!inSearch.Equals("") && !inSearch.Equals(null))
            {
                List<Employee> SearchList = FetchListEmployees();
                List<Employee> myTempList = new List<Employee>();
                myTempList.AddRange(
                  from inEmployee in SearchList
                  where inEmployee.FirstName.ToUpper().Contains(inSearch.ToUpper()) || inEmployee.LastName.ToUpper().Contains(inSearch.ToUpper()) || inEmployee.Level.ToString().ToUpper().Contains(inSearch.ToUpper())
                  select inEmployee);
                return myTempList;

                //Will empty the search list if nothing is found so they will get feedback for typing something incorrectly
            }
            else
            {
                return FetchListEmployees();
            }
        }
    }
}