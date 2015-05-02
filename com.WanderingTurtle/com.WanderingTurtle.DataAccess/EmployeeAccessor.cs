using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    public static class EmployeeAccessor
    {

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method takes in a parameter of newEmployee
        ///     Database is queried using stored procedure and looks for matching
        ///     information from the object passed to the method
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/26
        /// Renamed stored procedure to spInsertEmployee
        /// </remarks>
        /// <param name="newEmployee">Employee object to add to databse</param>
        /// <exception cref="ApplicationException">Exception is thrown if no rows affected were returned</exception>
        /// /// <returns>Number of rows affected</returns>
        public static int AddEmployee(Employee newEmployee)
        {
            int rowsAffected = 0;

            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spInsertEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@firstName", newEmployee.FirstName);
            cmd.Parameters.AddWithValue("@lastName", newEmployee.LastName);
            cmd.Parameters.AddWithValue("@empPassword", newEmployee.Password);
            cmd.Parameters.AddWithValue("@empLevel", newEmployee.Level);
            cmd.Parameters.AddWithValue("@active", newEmployee.Active);

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

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/18
        /// Method takes in an Employee ID,
        ///     submits those variables to the stored procedure spEmployeeGet,
        ///     which will return the specific employee that matches the ID
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/26
        /// Renamed stored procedure to spEmployeeGet
        /// Reconfigured employee object creation from data retrieved from database.
        /// 
        /// Pat Banks
        /// Updated:  2015/04/25
        /// Stored procedure updated
        /// </remarks>
        /// <param name="empID">ID of an employee to search in the database</param>
        /// <exception cref="ApplicationException">Exception is thrown if no employee id was found</exception>
        /// <returns>Returns the Employee found in database by ID</returns>
        public static Employee GetEmployee(int empID)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spSelectEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", empID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return new Employee(
                        reader.GetInt32(0), //EmployeeID
                        reader.GetString(1), //FirstName
                        reader.GetString(2), //LastName
                        reader.GetInt32(3), //Level
                        reader.GetBoolean(4) //Active
                    );
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
        }


        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method creates a connection to the database and calls
        ///     the stored procedure spEmployeeList that querys the database
        ///     for all of the active employees
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/26
        /// Renamed stored procedure to spEmployeeGet.
        /// Reconfigured employee object creation from data retrieved from database.
        /// </remarks>
        /// <exception cref="ApplicationException">Exception is thrown stating that no employees could be found in DB</exception>
        /// <returns>List object employeeList is returned with a list of all the employees in the database</returns>
        public static List<Employee> GetEmployeeList()
        {
            var employeeList = new List<Employee>();

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spSelectEmployee";
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
                        employeeList.Add(
                            new Employee(
                                reader.GetInt32(0), //EmployeeID
                                reader.GetString(1), //FirstName
                                reader.GetString(2), //LastName
                                reader.GetInt32(3), //Level
                                reader.GetBoolean(4) //Active
                            )
                        );
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

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/12
        /// Method takes in originalEmployee and updatedEmployee
        ///     the parameters are inserted into the stored procedure and update the
        ///     information from orignalEmployee with updatedEmployee
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/26
        /// Renamed stored procedure to spEmployeeUpdate
        /// 
        /// Pat Banks
        /// Updated: 2015/04/14
        /// Renamed stored procedure
        /// </remarks>
        /// <param name="originalEmployee">Original employee object, should match information stored in database</param>
        /// <param name="updatedEmployee">Employee object containing new information to update</param>
        /// <exception cref="ApplicationException">Exception is thrown if the original employee object information does not match information stored in database (Concurrency Error)</exception>
        /// <returns>Returns number of rows affected</returns>
        public static int UpdateEmployee(Employee originalEmployee, Employee updatedEmployee)
        {
            int rowsAffected = 0;

            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spUpdateEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@firstName", updatedEmployee.FirstName);
            cmd.Parameters.AddWithValue("@lastName", updatedEmployee.LastName);
            cmd.Parameters.AddWithValue("@password", updatedEmployee.Password);
            cmd.Parameters.AddWithValue("@level", updatedEmployee.Level);
            cmd.Parameters.AddWithValue("@active", updatedEmployee.Active);

            cmd.Parameters.AddWithValue("@original_employeeID", originalEmployee.EmployeeID);
            cmd.Parameters.AddWithValue("@original_firstName", originalEmployee.FirstName);
            cmd.Parameters.AddWithValue("@original_lastName", originalEmployee.LastName);
            cmd.Parameters.AddWithValue("@original_level", originalEmployee.Level);
            cmd.Parameters.AddWithValue("@original_active", originalEmployee.Active);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Employee Update Concurrency Error");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/03/03
        /// Checks an employee's ID and password against the database.
        /// </summary>
        /// <param name="employeeId">The employee's unique ID</param>
        /// <param name="employeePassword">The employee's password</param>
        /// <returns>The employee with the given credentials</returns>
        public static Employee GetEmployeeLogin(int employeeId, string employeePassword)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spSelectEmployeeByIDPassword";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@empPassword", employeePassword);

            Employee tempEmployee = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    tempEmployee = new Employee((int)reader.GetValue(0), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), (int)reader.GetValue(3));
                }
                else
                {
                    throw new ApplicationException("No such login is available.");
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
            return tempEmployee;
        }
    }
}