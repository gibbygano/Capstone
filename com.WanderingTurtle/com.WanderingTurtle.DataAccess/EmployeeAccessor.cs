using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
	public static class EmployeeAccessor
	{
		// Failure: an application exception will be thrown
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
		/// Renamed stored procedure to spEmployeeAdd
		/// </remarks>
		/// <param name="newEmployee">Employee object to add to databse</param>
		/// <exception cref="ApplicationException">Exception is thrown if no rows affected were returned</exception>
		/// <returns>Number of rows affected</returns>
		public static int AddEmployee(Employee newEmployee)
		{
			var conn = DatabaseConnection.GetDatabaseConnection();

			const string cmdText = "spEmployeeAdd";
			var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@firstName", newEmployee.FirstName);
			cmd.Parameters.AddWithValue("@lastName", newEmployee.LastName);
			cmd.Parameters.AddWithValue("@empPassword", newEmployee.Password);
			cmd.Parameters.AddWithValue("@empLevel", newEmployee.Level);
			cmd.Parameters.AddWithValue("@active", newEmployee.Active);

			conn.Open();

			var rowsAffected = cmd.ExecuteNonQuery();

			if (rowsAffected == 0)
			{
				throw new ApplicationException("There was a problem adding the employee to the database. Please try again.");
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
		/// </remarks>
		/// <param name="empId">ID of an employee to search in the database</param>
		/// <exception cref="ApplicationException">Exception is thrown if no employee id was found</exception>
		/// <returns>Returns the Employee found in database by ID</returns>
		public static Employee GetEmployee(int empId)
		{
			var conn = DatabaseConnection.GetDatabaseConnection();

			const string cmdText = "spEmployeeGet";
			var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@EmployeeID", empId);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();

					return new Employee(
						reader.GetInt32(0),	//EmployeeID
						reader.GetString(1), //FirstName
						reader.GetString(2), //LastName
						null, //Password
						reader.GetInt32(3),	//Level
						reader.GetBoolean(4) //Active
					);
				}
				else
				{
					throw new ApplicationException("Specific employee not found, check your search parameters and try again.");
				}
			}
			finally
			{
				conn.Close();
			}
		}

		/// <summary>
		/// Ryan Blake
		/// Created: 2015/02/12
		/// Method takes in an employee first name and last name,
		///     submits those variables to the stored procedure spEmployeeSelectName,
		///     which will return the specific employee that matches the firstName and lastName
		/// </summary>
		/// <remarks>
		/// Miguel Santana
		/// Updated: 2015/02/26
		/// Renamed stored procedure to spEmployeeSelectName
		/// Reconfigured employee object creation from data retrieved from database
		/// </remarks>
		/// <param name="firstName">First name of employee</param>
		/// <param name="lastName">Last name of employee</param>
		/// <exception cref="ApplicationException">Exception is thrown if no employee was found in database</exception>
		/// <returns>Employee found in database</returns>
		public static Employee GetEmployee(string firstName, string lastName)
		{
			var conn = DatabaseConnection.GetDatabaseConnection();

			const string cmdText = "spEmployeeSelectName";
			var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@firstName", firstName);
			cmd.Parameters.AddWithValue("@lastName", lastName);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();

					return new Employee(
						reader.GetInt32(0),	//EmployeeID
						reader.GetString(1), //FirstName
						reader.GetString(2), //LastName
						null, //Password
						reader.GetInt32(3),	//Level
						reader.GetBoolean(4) //Active
					);
				}
				else
				{
					throw new ApplicationException("Specific employee not found, check your search parameters and try again.");
				}
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

			var conn = DatabaseConnection.GetDatabaseConnection();

			const string cmdText = "spEmployeeGet";
			var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						employeeList.Add(
							new Employee(
								reader.GetInt32(0),	//EmployeeID
								reader.GetString(1), //FirstName
								reader.GetString(2), //LastName
								null, //Password
								reader.GetInt32(3),	//Level
								reader.GetBoolean(4) //Active
							)
						);
					}
				}
				else
				{
					throw new ApplicationException("No employees found in database.");
				}
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
		/// </remarks>
		/// <param name="originalEmployee">Original employee object, should match information stored in database</param>
		/// <param name="updatedEmployee">Employee object containing new information to update</param>
		/// <exception cref="ApplicationException">Exception is thrown if the original employee object information does not match information stored in database (Concurrency Error)</exception>
		/// <returns>Returns number of rows affected</returns>
		public static int UpdateEmployee(Employee originalEmployee, Employee updatedEmployee)
		{
			var conn = DatabaseConnection.GetDatabaseConnection();

			const string cmdText = "spEmployeeUpdate";
			var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

			cmd.Parameters.AddWithValue("@firstName", updatedEmployee.FirstName);
			cmd.Parameters.AddWithValue("@lastName", updatedEmployee.LastName);
			// TODO update stored procedure to compare password
			//cmd.Parameters.AddWithValue("@password", updatedEmployee.Password);
			cmd.Parameters.AddWithValue("@level", updatedEmployee.Level);
			cmd.Parameters.AddWithValue("@active", updatedEmployee.Active);

			cmd.Parameters.AddWithValue("@original_employeeID", originalEmployee.EmployeeId);
			cmd.Parameters.AddWithValue("@original_firstName", originalEmployee.FirstName);
			cmd.Parameters.AddWithValue("@original_lastName", originalEmployee.LastName);
			cmd.Parameters.AddWithValue("@original_level", originalEmployee.Level);
			cmd.Parameters.AddWithValue("@original_active", originalEmployee.Active);

			conn.Open();

			var rowsAffected = cmd.ExecuteNonQuery();

			if (rowsAffected == 0)
			{
				throw new ApplicationException("Employee Update Concurrency Error");
			}
			return rowsAffected;
		}
	}
}