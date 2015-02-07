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
        // getEmployeeList() takes in no parameters and returns a full
        // list of all of the employeesin the database(employeeList)
        // objects are added to list object collection
        // if no employee database listings are found an
        // applicaiton exception is thrown
        // Ryan Blake February 5th

        public static List<Employee> getEmployeeList()
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

        // getEmployee takes in two parameters of firstName and lastName and
        // then queries the database with a string that returns
        // the results to an employee object if they match the parameters
        // employee first name and last name must be correct in databae or
        // an Application exception will be returned
        // if successful, an Employee type object named myEmployee will be returned holding all of the employee information
        // Ryan Blake, February 5th

        public static Employee getEmployee(string firstName, string lastName)
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

        // AddEmployee() takes in a parameter of newEmployee type Employee
        // Database is queried using stored procedure and looks for matching
        // information from the object passed to the method
        // if successful a count of rows affected is returned
        // if unsuccssful an application exception will be thrown
        // Ryan Blake, February 5th

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

        // UpdateEmployee takes in two parameters of Employee object type (originalEmployee, updatedEmployee)
        // the parameters are inserted into the stored procedure and update the information from orignalEmployee with updatedEmployee
        // upon success a rowcount of rows affected will be returned
        // if unsuccessful (originalEmployee does not match an employee listing in the database)
        // Application exception wil be thrown
        // this method will also serve as the delete as the user will simply change the employee Active to false, removing them
        // from any listings but still keeping their records in the database
        // Ryan Blake, February 5th

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