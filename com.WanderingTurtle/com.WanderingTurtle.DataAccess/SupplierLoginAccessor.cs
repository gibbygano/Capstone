using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;

namespace com.WanderingTurtle.DataAccess
{
    public class SupplierLoginAccessor
    {
        public SupplierLoginAccessor()
        { }

        /// <summary>
        /// Created by Rose Steffensmeier 2015/04/03
        /// </summary>
        /// <remarks>
        /// Updated by Rose Steffensmeier 2015/04/13
        /// Added new parameter for input, uncommented Exception catch.
        /// </remarks>
        /// <param name="userPassword">The password for the supplier.</param>
        /// <param name="userName">The user name for the supplier.</param>
        /// <exception cref="SqlException">If the database cannot be accessed.</exception>
        /// <exception cref="ApplicationException">If the login information is not found.</exception>
        /// <returns>A SupplierLogin object that contains the information about the supplier.</returns>
        public SupplierLogin retrieveSupplierLogin(string userPassword, string userName)
        {
            SupplierLogin getSupplierInfo = new SupplierLogin();
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSupplierLoginGet";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
        /// Created by Rose Steffensmeier 2015/04/03
        /// </summary>
        /// <remarks>
        /// Updated by Rose Steffensmeier 2015/04/13
        /// Added new paramenter
        /// </remarks>
        /// <param name="userName">The username the supplier wants.</param>
        /// <exception cref="SqlException">Goes off if unable to connect to the database or the username is already taken.</exception>
        /// <returns>The number of rows that were affected. Should never be greater than one.</returns>
        public int addSupplierLogin(String userName, int supplierID)
        {
            int rowsAdded = 0;
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSupplierLoginAdd";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
        /// Created by Rose Steffensmeier 2015/04/03
        /// </summary>
        /// <remarks>
        /// Updated by Rose Steffensmeier 2015/04/13
        /// added new paramenter, added Exception catch block
        /// </remarks>
        /// <param name="oldSupplierLogin"></param>
        /// <param name="archive"></param>
        /// <returns></returns>
        public int archiveSupplierLogin(SupplierLogin oldSupplierLogin, bool archive)
        {
            int rowsAffected = 0;
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSupplierLoginArchive";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@active", archive);
            cmd.Parameters.AddWithValue("@original_userID", oldSupplierLogin.UserID);
            cmd.Parameters.AddWithValue("@original_userPassword", "Password#1");
            cmd.Parameters.AddWithValue("@original_userName", oldSupplierLogin.UserName);
            cmd.Parameters.AddWithValue("@original_supplierID", oldSupplierLogin.SupplierID);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected;
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


        public int UpdateSupplierLogin(string newUserName, string oldUserName, int oldSupplierID)
        {

            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spSupplierLoginUpdate";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Updated Supplier Info:
            cmd.Parameters.AddWithValue("@UserName", newUserName);

            //Old Supplier Info
            cmd.Parameters.AddWithValue("@original_UserName", oldUserName);
            cmd.Parameters.AddWithValue("@original_SupplierID", oldSupplierID);

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
        public bool checkUserName(string userName)
        {

            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSupplierLoginGetUserName";
            bool validName = false;

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        public string retrieveSupplierUserNameByID(int supplierID)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSupplierLoginGetByID";
            string userNameFound;

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
    }
}
