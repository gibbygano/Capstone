using com.WanderingTurtle.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    public class SupplierLoginAccessor
    {
        public SupplierLoginAccessor()
        { }

        /// <summary>]
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/04/13
        /// Added new parameter for input, uncommented Exception catch.
        /// </remarks>
        /// <param name="userPassword">The password for the supplier.</param>
        /// <param name="userName">The user name for the supplier.</param>
        /// <exception cref="SqlException">If the database cannot be accessed.</exception>
        /// <exception cref="ApplicationException">If the login information is not found.</exception>
        /// <returns>A SupplierLogin object that contains the information about the supplier.</returns>
        public SupplierLogin RetrieveSupplierLogin(string userPassword, string userName)
        {
            SupplierLogin getSupplierInfo = new SupplierLogin();
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectSupplierLogin";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userPassword", userPassword);
            cmd.Parameters.AddWithValue("@userName", userName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();

                    getSupplierInfo.UserID = (int)reader.GetValue(0);
                    getSupplierInfo.UserPassword = reader.GetValue(1).ToString();
                    getSupplierInfo.UserName = reader.GetValue(2).ToString();
                    getSupplierInfo.SupplierID = reader.GetValue(3).ToString();
                    getSupplierInfo.Active = reader.GetBoolean(4);
                }
                else
                    throw new ApplicationException("Incorrect login information. Try again.");

                return getSupplierInfo;
            }
            catch (SqlException)
            {
                throw;
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
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/04/13
        /// Added new paramenter
        /// </remarks>
        /// <param name="userName">The username the supplier wants.</param>
        /// <exception cref="SqlException">Goes off if unable to connect to the database or the username is already taken.</exception>
        /// <returns>The number of rows that were affected. Should never be greater than one.</returns>
        public int AddSupplierLogin(String userName, int supplierID)
        {
            int rowsAdded = 0;
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spInsertSupplierLogin";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@supplierID", supplierID);

            try
            {
                conn.Open();
                rowsAdded = cmd.ExecuteNonQuery();

                return rowsAdded;
            }
            catch (SqlException)
            {
                throw;
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
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// Updates a supplier's password
        /// </summary>
        /// <param name="newPassword">The string containing the new password for a supplier</param>
        /// <param name="oldLogin">The SupplierLogin object matching the Supplier whose login information is being updated</param>
        /// <returns>number of rows affected</returns>
        public int UpdateSupplierPassword(string newPassword, SupplierLogin oldLogin)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spUpdateSupplierPassword";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Updated Supplier Info:
            cmd.Parameters.AddWithValue("@Password", newPassword);

            //Old Supplier Info
            cmd.Parameters.AddWithValue("@original_UserName", oldLogin.UserName);
            cmd.Parameters.AddWithValue("@original_Password", oldLogin.UserPassword);
            cmd.Parameters.AddWithValue("@original_SupplierID", oldLogin.SupplierID);

            int rowsAffected;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/15
        /// Checks if user name is in use or has ever been in use
        /// </summary>
        /// <param name="userName">The username to check against</param>
        /// <returns>Returns true if the suggested userName is valid; False if the userName is/was in use</returns>
        public bool CheckUserName(string userName)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectSupplierLoginbyUserName";
            bool validName = false;

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userName", userName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();
                    validName = false;
                }
                else
                {
                    validName = true;
                }

                return validName;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/25
        /// Retrieves the supplierLogin information
        /// </summary>
        /// <param name="supplierID">The supplierID matching a record to retrieve</param>
        /// <returns>Username found SupplierLogin table</returns>
        public string RetrieveSupplierUserNameByID(int supplierID)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectSupplierLoginByID";
            string userNameFound;

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@supplierID", supplierID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    userNameFound = reader.GetString(0);
                }
                else
                {
                    throw new ApplicationException("SupplierID not found.");
                }

                return userNameFound;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Bryan Hurst 
        /// Created:  2015/04/23
        /// Method for the deletion of test login records in the database
        /// </summary>
        /// <param name="supplierLoginToDelete">The SupplierLogin object used for testing -- to be deleted</param>
        /// <returns>number of rows affected</returns>
        public static int DeleteTestSupplierLogin(SupplierLogin supplierLoginToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteTestSupplierLogin";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", supplierLoginToDelete.UserName);

            int rowsAffected;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}