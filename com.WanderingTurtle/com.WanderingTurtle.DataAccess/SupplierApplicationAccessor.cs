using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;


namespace com.WanderingTurtle.DataAccess
{
    public class SupplierApplicationAccessor
    {

        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Inserts a new Supplier Application Record into the Database
        /// </summary>
        /// <param name="supplierApplicationToAdd">A Supplier Application Object that contains all the information to be added</param>
        /// <returns>int # of rows affected</returns>
        public static int AddSupplierApplication(SupplierApplication supplierApplicationToAdd)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();

            int newApplicationID;
            string cmdtext = "spInsertSupplierApplication";
            var cmd = new SqlCommand(cmdtext, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyName", supplierApplicationToAdd.CompanyName);
            cmd.Parameters.AddWithValue("@CompanyDescription", supplierApplicationToAdd.CompanyDescription);
            cmd.Parameters.AddWithValue("@FirstName", supplierApplicationToAdd.FirstName);
            cmd.Parameters.AddWithValue("@LastName", supplierApplicationToAdd.LastName);
            cmd.Parameters.AddWithValue("@Address1", supplierApplicationToAdd.Address1);
            cmd.Parameters.AddWithValue("@Address2", supplierApplicationToAdd.Address2);
            cmd.Parameters.AddWithValue("@Zip", supplierApplicationToAdd.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", supplierApplicationToAdd.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", supplierApplicationToAdd.EmailAddress);
            cmd.Parameters.AddWithValue("@ApplicationDate", supplierApplicationToAdd.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationStatus", supplierApplicationToAdd.ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", supplierApplicationToAdd.LastStatusDate);
            cmd.Parameters.AddWithValue("@Remarks", supplierApplicationToAdd.Remarks);

            var rowsAffected = 0;
          
            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();
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
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Updates an existing Supplier Application Record already in the Database
        /// </summary>
        /// <param name="oldApplication">A SupplierApplication Object that contains all the information of the record to be changed</param>
        /// <param name="newApplication">A SupplierApplication Object that contains all the information to change in the record</param>
        /// <returns>int # of rows affected</returns>
        public static int UpdateSupplierApplication(SupplierApplication oldApplication, SupplierApplication newApplication)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateSupplierApplication";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyName", newApplication.CompanyName);
            cmd.Parameters.AddWithValue("@CompanyDescription", newApplication.CompanyDescription);
            cmd.Parameters.AddWithValue("@FirstName", newApplication.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newApplication.LastName);
            cmd.Parameters.AddWithValue("@Address1", newApplication.Address1);
            cmd.Parameters.AddWithValue("@Address2", newApplication.Address2);
            cmd.Parameters.AddWithValue("@Zip", newApplication.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", newApplication.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", newApplication.EmailAddress);
            cmd.Parameters.AddWithValue("@ApplicationDate", newApplication.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationStatus", newApplication.ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", newApplication.LastStatusDate);
            cmd.Parameters.AddWithValue("@Remarks", newApplication.Remarks);

            cmd.Parameters.AddWithValue("@originalApplicationID", oldApplication.ApplicationID);

            cmd.Parameters.AddWithValue("@originalCompanyName", oldApplication.CompanyName);
            cmd.Parameters.AddWithValue("@originalCompanyDescription", oldApplication.CompanyDescription);
            cmd.Parameters.AddWithValue("@originalFirstName", oldApplication.FirstName);
            cmd.Parameters.AddWithValue("@originalLastName", oldApplication.LastName);
            cmd.Parameters.AddWithValue("@originalAddress1", oldApplication.Address1);
            cmd.Parameters.AddWithValue("@originalAddress2", oldApplication.Address2);
            cmd.Parameters.AddWithValue("@originalZip", oldApplication.Zip);
            cmd.Parameters.AddWithValue("@originalPhoneNumber", oldApplication.PhoneNumber);
            cmd.Parameters.AddWithValue("@originalEmailAddress", oldApplication.EmailAddress);
            cmd.Parameters.AddWithValue("@originalApplicationDate", oldApplication.ApplicationDate);
            cmd.Parameters.AddWithValue("@originalApplicationStatus", oldApplication.ApplicationStatus);
            cmd.Parameters.AddWithValue("@originalLastStatusDate", oldApplication.LastStatusDate);
            cmd.Parameters.AddWithValue("@originalRemarks", oldApplication.Remarks);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                               
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected; 
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Retrieves a list of all Supplier Application Records from the Database
        /// </summary>
        /// <remarks>
        /// Edited by Rose Steffensmeier 2015/04/03
        /// added param to input so that stored procedure will work, param does not affect the actual 
        /// </remarks>
        /// <returns>List of SupplierApplication objects</returns>
        public static List<SupplierApplication> GetSupplierApplicationList()
        {
            var ApplicationList = new List<SupplierApplication>();
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spSelectAllSupplierApplication";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var currentSupplierApplication = new SupplierApplication();

                        currentSupplierApplication.ApplicationID = (int)reader.GetValue(0);
                        currentSupplierApplication.CompanyName = reader.GetValue(1).ToString();
                        currentSupplierApplication.CompanyDescription = !reader.IsDBNull(2) ? currentSupplierApplication.CompanyDescription = reader.GetValue(2).ToString() : null;
                        currentSupplierApplication.FirstName = reader.GetValue(3).ToString();
                        currentSupplierApplication.LastName= reader.GetValue(4).ToString();
                        currentSupplierApplication.Address1 = reader.GetValue(5).ToString();
                        currentSupplierApplication.Address2 = reader.GetValue(6).ToString();
                        currentSupplierApplication.Zip = reader.GetValue(7).ToString();
                        currentSupplierApplication.PhoneNumber = reader.GetValue(8).ToString();
                        currentSupplierApplication.EmailAddress = reader.GetValue(9).ToString();
                        currentSupplierApplication.ApplicationDate = (DateTime)reader.GetValue(10);
                        currentSupplierApplication.ApplicationStatus = reader.GetValue(11).ToString();
                        currentSupplierApplication.LastStatusDate = reader.GetDateTime(12);
                        currentSupplierApplication.Remarks = !reader.IsDBNull(13) ? currentSupplierApplication.Remarks = reader.GetValue(13).ToString() : null;

                        ApplicationList.Add(currentSupplierApplication);
                    }
                }
                else
                {
                    var ex = new ApplicationException("No Supplier Applications Found!");
                    throw ex;
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
            return ApplicationList;
        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/02/08
        /// Retrieves a single Supplier Application Records from the Database
        /// </summary>
        /// <param name="SupplierApplicationID">A string of the SupplierApplicationID of the Supplier Application to be fetched</param>
        /// <returns>SupplierApplication object</returns>
        public static SupplierApplication GetSupplierApplication(String SupplierApplicationID)
        {
            var currentSupplierApplication = new SupplierApplication();
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spSelectSupplierApplication";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApplicationID", SupplierApplicationID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    currentSupplierApplication.ApplicationID = reader.GetInt32(0);
                    currentSupplierApplication.CompanyName = reader.GetValue(1).ToString();
                    currentSupplierApplication.CompanyDescription = !reader.IsDBNull(2) ? currentSupplierApplication.CompanyDescription = reader.GetString(2): null;
                    currentSupplierApplication.FirstName = reader.GetValue(3).ToString();
                    currentSupplierApplication.LastName = reader.GetValue(4).ToString();
                    currentSupplierApplication.Address1 = reader.GetValue(5).ToString();
                    currentSupplierApplication.Address2 = reader.GetValue(6).ToString();
                    currentSupplierApplication.Zip = reader.GetValue(7).ToString();
                    currentSupplierApplication.PhoneNumber = reader.GetValue(8).ToString();
                    currentSupplierApplication.EmailAddress = reader.GetValue(9).ToString();
                    currentSupplierApplication.ApplicationDate = reader.GetDateTime(10);
                    currentSupplierApplication.ApplicationStatus = reader.GetValue(11).ToString();
                    currentSupplierApplication.LastStatusDate = reader.GetDateTime(12);
                    currentSupplierApplication.Remarks = !reader.IsDBNull(13) ? currentSupplierApplication.Remarks = reader.GetValue(13).ToString() : null;
                }
                else
                {
                    var ax = new ApplicationException("Supplier Application Not Found!");
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
            return currentSupplierApplication;
        }

        public static int UpdateSupplierApplication(SupplierApplication oldApplication, SupplierApplication newApplication, string userName, decimal supplyCost)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateApplicationAddSupplierAddLogin";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@SupplyCost", supplyCost);

            cmd.Parameters.AddWithValue("@CompanyName", newApplication.CompanyName);
            cmd.Parameters.AddWithValue("@CompanyDescription", newApplication.CompanyDescription);
            cmd.Parameters.AddWithValue("@FirstName", newApplication.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newApplication.LastName);
            cmd.Parameters.AddWithValue("@Address1", newApplication.Address1);
            cmd.Parameters.AddWithValue("@Address2", newApplication.Address2);
            cmd.Parameters.AddWithValue("@Zip", newApplication.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", newApplication.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", newApplication.EmailAddress);
            cmd.Parameters.AddWithValue("@ApplicationDate", newApplication.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationStatus", newApplication.ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", newApplication.LastStatusDate);
            cmd.Parameters.AddWithValue("@Remarks", newApplication.Remarks);

            cmd.Parameters.AddWithValue("@originalApplicationID", oldApplication.ApplicationID);

            cmd.Parameters.AddWithValue("@originalCompanyName", oldApplication.CompanyName);
            cmd.Parameters.AddWithValue("@originalCompanyDescription", oldApplication.CompanyDescription);
            cmd.Parameters.AddWithValue("@originalFirstName", oldApplication.FirstName);
            cmd.Parameters.AddWithValue("@originalLastName", oldApplication.LastName);
            cmd.Parameters.AddWithValue("@originalAddress1", oldApplication.Address1);
            cmd.Parameters.AddWithValue("@originalAddress2", oldApplication.Address2);
            cmd.Parameters.AddWithValue("@originalZip", oldApplication.Zip);
            cmd.Parameters.AddWithValue("@originalPhoneNumber", oldApplication.PhoneNumber);
            cmd.Parameters.AddWithValue("@originalEmailAddress", oldApplication.EmailAddress);
            cmd.Parameters.AddWithValue("@originalApplicationDate", oldApplication.ApplicationDate);
            cmd.Parameters.AddWithValue("@originalApplicationStatus", oldApplication.ApplicationStatus);
            cmd.Parameters.AddWithValue("@originalLastStatusDate", oldApplication.LastStatusDate);
            cmd.Parameters.AddWithValue("@originalRemarks", oldApplication.Remarks);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();


                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected; 
        }
    }
}