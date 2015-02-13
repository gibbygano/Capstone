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
        /// Inserts a new Supplier Application Record into the Database
        /// </summary>
        /// <param name="supplierApplicationToAdd">A Supplier Application Object that contains all the information to be added</param>
        /// <returns>int # of rows affected</returns>
        /// Created by Matt Lapka 2/8/15
        public static int AddSupplierApplication(SupplierApplication supplierApplicationToAdd)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            
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
            cmd.Parameters.AddWithValue("@Approved", supplierApplicationToAdd.Approved);
            cmd.Parameters.AddWithValue("@ApprovalDate", supplierApplicationToAdd.ApprovalDate);

            var rowsAffected = 0;
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
        /// Updates an ecisting Supplier Application Record already in the Database
        /// </summary>
        /// <param name="oldApplication">A SupplierApplication Object that contains all the information of the record to be changed</param>
        /// <param name="oldApplication">A SupplierApplication Object that contains all the information to change in the record</param>
        /// <returns>int # of rows affected</returns>
        /// Created by Matt Lapka 2/8/15
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
            cmd.Parameters.AddWithValue("@Approved", newApplication.Approved);
            cmd.Parameters.AddWithValue("@ApprovalDate", newApplication.ApprovalDate);

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
            cmd.Parameters.AddWithValue("@originalApproved", oldApplication.Approved);
            cmd.Parameters.AddWithValue("@originalApprovalDate", oldApplication.ApprovalDate);
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
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
            return rowsAffected; 
        }
        /// <summary>
        /// Retrieves a list of all Supplier Application Records from the Database
        /// </summary>
        /// <returns>List of SupplierApplication objects</returns>
        /// Created by Matt Lapka 2/8/15
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
                        currentSupplierApplication.CompanyDescription = reader.GetValue(2).ToString();
                        currentSupplierApplication.FirstName = reader.GetValue(3).ToString();
                        currentSupplierApplication.LastName= reader.GetValue(4).ToString();
                        currentSupplierApplication.Address1 = reader.GetValue(5).ToString();
                        currentSupplierApplication.Address2 = reader.GetValue(6).ToString();
                        currentSupplierApplication.Zip = reader.GetValue(7).ToString();
                        currentSupplierApplication.PhoneNumber = reader.GetValue(8).ToString();
                        currentSupplierApplication.EmailAddress = reader.GetValue(9).ToString();
                        currentSupplierApplication.ApplicationDate = (DateTime)reader.GetValue(10);
                        currentSupplierApplication.Approved = reader.GetBoolean(11);
                        currentSupplierApplication.ApprovalDate = (DateTime)reader.GetValue(12);
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
        /// Retrieves a signle Supplier Application Records from the Database
        /// </summary>
        /// <returns>SupplierApplication object</returns>
        /// Created by Matt Lapka 2/8/15
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
                    currentSupplierApplication.ApplicationID = (int)reader.GetValue(0);
                    currentSupplierApplication.CompanyName = reader.GetValue(1).ToString();
                    currentSupplierApplication.CompanyDescription = reader.GetValue(2).ToString();
                    currentSupplierApplication.FirstName = reader.GetValue(3).ToString();
                    currentSupplierApplication.LastName = reader.GetValue(4).ToString();
                    currentSupplierApplication.Address1 = reader.GetValue(5).ToString();
                    currentSupplierApplication.Address2 = reader.GetValue(6).ToString();
                    currentSupplierApplication.Zip = reader.GetValue(7).ToString();
                    currentSupplierApplication.PhoneNumber = reader.GetValue(8).ToString();
                    currentSupplierApplication.EmailAddress = reader.GetValue(9).ToString();
                    currentSupplierApplication.ApplicationDate = (DateTime)reader.GetValue(10);
                    currentSupplierApplication.Approved = reader.GetBoolean(11);
                    currentSupplierApplication.ApprovalDate = (DateTime)reader.GetValue(12);
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
    }
}


