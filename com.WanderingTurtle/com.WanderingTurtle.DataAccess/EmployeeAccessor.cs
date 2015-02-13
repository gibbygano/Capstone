using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class EmployeeAccessor
    {
        
        // Ryan Blake
        // February 12, 2015

        // Paramaters: N/A

        // Desc.: Method creates a connection to the database and calls 
        // the stored procedure spEmployeeList that querys the database
        // for all of the active employees

        // Failure: Exception is thrown stating that no employees could be found in DB

        // Success: List object employeeList is returned with a list of all the employees
        // in the database
        public static List<Employee> GetEmployeeList()
        {
            var employeeList = new List<Employee>();

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spEmployeeList";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var inEmployee = new Employee();

                        inEmployee.EmployeeID = (int)reader.GetValue(0);
                        inEmployee.FirstName = reader.GetValue(1).ToString();
                        inEmployee.LastName = reader.GetValue(2).ToString();
                        inEmployee.UserID = (int)reader.GetValue(3);
                        inEmployee.Active = (bool)reader.GetValue(4);

                        employeeList.Add(inEmployee);
                    }
                }
                else
                {
                    var ax = new ApplicationException("No employees found in database.");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return employeeList;
        }

       
        // Ryan Blake
        // February 12, 2015

        // Parameters: firstName || Type: string, lastName || Type: string

        // Desc.: Method takes in an employee first name and last name, submits
        // those variables to the stored procedure spSelect Employee
        // which will return the specific employee that matches the firstName and lastName

        // Failure: Exception is thrown that asks the user to try their search again
        // becuase the employee wasn't found

        // Success: An employee object is returned from the database and from the method
        // as myEmployee
        public static Employee GetEmployee(string firstName, string lastName)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spSelectEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);

            Employee myEmployee = new Employee();

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    myEmployee.EmployeeID = (int)reader.GetValue(0);
                    myEmployee.FirstName = reader.GetValue(1).ToString();
                    myEmployee.LastName = reader.GetValue(2).ToString();
                    myEmployee.UserID = (int)reader.GetValue(3);
                    myEmployee.Active = (bool)reader.GetValue(4);
                }
                else
                {
                    var ax = new ApplicationException("Specific employee not found, check your search parameters and try again.");
                    throw ax;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return myEmployee;
        }

        // Ryan Blake
        // February 15, 2015

        // Parameters: newEmployee || Type: Employee

        // Desc.: Method takes in a parameter of newEmployee
        // Database is queried using stored procedure and looks for matching
        // information from the object passed to the method

        // Success: a count of rows affected is returned

        // Failure: an application exception will be thrown
        public static int AddEmployee(Employee newEmployee)
        {
            int rowsAffected = 0;

            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spAddEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employeeID", newEmployee.EmployeeID);
            cmd.Parameters.AddWithValue("@firstName", newEmployee.FirstName);
            cmd.Parameters.AddWithValue("@lastName", newEmployee.LastName);
            cmd.Parameters.AddWithValue("@userID", newEmployee.UserID);
            cmd.Parameters.AddWithValue("@active", true);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("There was a problem adding the employee to the database. Please try again.");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        // Ryan Blake
        // February 12, 2015

        // Parameters: originalEmployee || Type: Employee, updatedEmployee || Type: Employee

        // Desc.: Method takes in originalEmployee and updatedEmployee
        // the parameters are inserted into the stored procedure and update the 
        // information from orignalEmployee with updatedEmployee

        // Success: a rowcount of rows affected will be returned

        // Failure:  (originalEmployee does not match an employee listing in the database)
        // application exception wil be thrown

        // Note: this method will also serve as the delete as the user will simply 
        // change the employee Active to false, removing them
        // from any listings but still keeping their records in the database

        public static int UpdateEmployee(Employee originalEmployee, Employee updatedEmployee)
        {
            int rowsAffected = 0;

            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spUpdateEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employeeID", updatedEmployee.EmployeeID);
            cmd.Parameters.AddWithValue("@firstName", updatedEmployee.FirstName);
            cmd.Parameters.AddWithValue("@lastName", updatedEmployee.LastName);
            cmd.Parameters.AddWithValue("@userID", updatedEmployee.UserID);
            cmd.Parameters.AddWithValue("@active", updatedEmployee.Active);

            cmd.Parameters.AddWithValue("@original_employeeID", originalEmployee.EmployeeID);
            cmd.Parameters.AddWithValue("@original_firstName", originalEmployee.FirstName);
            cmd.Parameters.AddWithValue("@original_lastName", originalEmployee.LastName);
            cmd.Parameters.AddWithValue("@original_userID", originalEmployee.UserID);
            cmd.Parameters.AddWithValue("@original_active", originalEmployee.Active);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Could not update employee information, please try again.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }
    }
}