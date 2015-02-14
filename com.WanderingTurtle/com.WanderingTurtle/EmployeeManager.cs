using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle
{
    public class EmployeeManager
    {
        // Ryan Blake 
        // February 12, 2015

        // Parameters: newEmployee  ||  Type: Employee

        // Desc.: Method takes in newEmployee and passes it as a parameter
        // into the AddEmployee method of the EmployeeAccessor class

        // Failure: Exception is thrown if database is not available or 
        // new employee cannot be created in the database for any reason

        // Success: An int value is returned to the method to show rows affected
        public int AddNewEmployee(Employee newEmployee)
        {
            try
            {
                return EmployeeAccessor.AddEmployee(newEmployee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Ryan Blake
        // February 12, 2015

        // Parameters: oldEmployee || Type: Employee, newEmployee || Type: Employee

        // Desc.: Method takes in new and old employee parameters and then submits them to the 
        // Data Access Layer method to update the employee record for oldEmployee
        // with the the information held in newEmployee

        // Failure: EmployeeAccessor method will throw exception to Manager 
        // saying that the employee could not be found to edit

        // Success: Employee information is updatd in the table and 
        // an integer is returned to represent rows affected

        // Additional: This will also take the place of a 'Delete' method
        // The user will simply mark the employee inactive which will change the 
        // bit value in the table to a 0 to represent false
        public int EditCurrentEmployee(Employee oldEmployee, Employee newEmployee)
        {
            try
            {
                return EmployeeAccessor.UpdateEmployee(oldEmployee, newEmployee);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        // Ryan Blake
        // February 12, 2015

        // Parameters: N/A

        // Desc.: Method makes a call to getEmployeeList 
        // method from the EmployeeAccessor to retrieve a 
        // list of all active employees

        // Failure: Exception is thrown from Accessor that states 
        // that employee could not be found in the database

        // Success: The employee list is retrieved and returned 
        // up to the presentation layer (calling method)
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

        // Ryan Blake
        // February 12, 2015

        // Parameters: firstName || Type: string, lastName || Type: string

        // Desc.: Method takes in two parameters that will hold the employee's
        // first and last name. This information is passed to the access layer
        // where it is used to find the employee in question and return that 
        // employee's information to the method and then to the presentation 
        // layer calling method

        // Failure: An exception is thrown from the Access Layer asking the user to 
        // try their search again

        // Success: The employee object is returned to the method successfully
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
    }
}
