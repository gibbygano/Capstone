using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;

namespace com.WanderingTurtle.DataAccess
{
    public class SupplierAccessor
    {
        /// <summary>
        /// Retrieves Supplier information from the Database using a Stored Procedure.
        /// Creates a Supplier object.
        /// 
        /// Created by Tyler Collins 02/03/15
        /// </summary>
        /// <param name="supplierID">Requires a SupplierID to SELECT the correct Supplier record.</param>
        /// <returns>Supplier object</returns>
        public static Supplier GetSupplier(string supplierID)
        {
            Supplier supplierToRetrieve = new Supplier();

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spSelectSupplier";
            SqlCommand cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    supplierToRetrieve.SupplierID = reader.GetInt32(0);
                    supplierToRetrieve.CompanyName = reader.GetString(1);
                    supplierToRetrieve.FirstName = reader.GetString(2);
                    supplierToRetrieve.LastName = reader.GetString(3);
                    supplierToRetrieve.Address1 = reader.GetString(4);
                    supplierToRetrieve.Address2 = !reader.IsDBNull(5) ? supplierToRetrieve.Address2 = reader.GetString(5) : null;
                    supplierToRetrieve.Zip = reader.GetString(6);
                    supplierToRetrieve.PhoneNumber = reader.GetString(7);
                    supplierToRetrieve.EmailAddress = reader.GetString(8);
                    supplierToRetrieve.ApplicationID = reader.GetInt32(9);
                    supplierToRetrieve.SupplyCost = reader.GetDecimal(11);
                    supplierToRetrieve.UserID = reader.GetInt32(10);
                }
                else
                {
                    var pokeball = new ApplicationException("Requested ID did not match any records.");
                    throw pokeball;
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

            return supplierToRetrieve;
        }

        //public static Supplier GetSupplier(string searchParameter, bool isSupplierID)
        //{
        //    /*
        //     * Retrieves Supplier information from database based on SupplierID or CompanyName and returns a Supplier Object.
        //     * 
        //     * Created by Tyler Collins 02/03/15 
        //     */

        //    Supplier supplierToRetrieve = new Supplier();
        //    string query;
        //    SqlConnection conn = DatabaseConnection.GetConnection();

        //    if (isSupplierID)
        //    {
        //        query = "Select SupplierID, CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, SupplierType, ApplicationID, UserID "
        //        + "From Supplier Where SupplierID = '" + searchParameter + "' and Active=1";
        //    }
        //    else
        //    {
        //        query = "Select SupplierID, CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, SupplierType, ApplicationID, UserID "
        //        + "From Supplier Where CompanyName = '" + searchParameter + "' and Active=1";
        //    }
        //    SqlCommand cmd = new SqlCommand(query, conn);

        //    try
        //    {
        //        conn.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            reader.Read();

        //            supplierToRetrieve.SupplierID = reader.GetInt32(0);
        //            supplierToRetrieve.CompanyName = reader.GetString(1);
        //            supplierToRetrieve.FirstName = reader.GetString(2);
        //            supplierToRetrieve.LastName = reader.GetString(3);
        //            supplierToRetrieve.Address1 = reader.GetString(4);
        //            supplierToRetrieve.Address2 = !reader.IsDBNull(5) ? supplierToRetrieve.Address2 = reader.GetString(5) : null;
        //            supplierToRetrieve.Zip = reader.GetString(6);
        //            supplierToRetrieve.PhoneNumber = reader.GetString(7);
        //            supplierToRetrieve.EmailAddress = reader.GetString(8);
        //            supplierToRetrieve.SupplierType = reader.GetInt32(9);
        //            supplierToRetrieve.ApplicationID = reader.GetInt32(10);
        //            supplierToRetrieve.UserID = reader.GetInt32(11);

        //        }
        //        else
        //        {
        //            var pokeball = new ApplicationException("Requested ID did not match any records.");
        //            throw pokeball;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return supplierToRetrieve;
        //}

        /// <summary>
        /// Retrieves all Supplier data from the Database using a Stored Procedure.
        /// Creates a Supplier object from retrieved data.
        /// Adds Supplier object to List of Supplier objects.
        /// 
        /// Created by Tyler Collins 02/03/2015
        /// </summary>
        /// <returns>List of Supplier objects</returns>
        public static List<Supplier> GetSupplierList()
        {
            List<Supplier> supplierList = new List<Supplier>();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spSelectAllSuppliers";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var currentSupplier = new Supplier();

                        currentSupplier.SupplierID = reader.GetInt32(0);
                        currentSupplier.CompanyName = reader.GetString(1);
                        currentSupplier.FirstName = reader.GetString(2);
                        currentSupplier.LastName = reader.GetString(3);
                        currentSupplier.Address1 = reader.GetString(4);
                        currentSupplier.Address2 = !reader.IsDBNull(5) ? currentSupplier.Address2 = reader.GetString(5) : null;
                        currentSupplier.Zip = reader.GetString(6);
                        currentSupplier.PhoneNumber = reader.GetString(7);
                        currentSupplier.EmailAddress = reader.GetString(8);
                        currentSupplier.ApplicationID = reader.GetInt32(9);
                        currentSupplier.UserID = reader.GetInt32(10);
                        currentSupplier.SupplyCost = reader.GetDecimal(11);

                        supplierList.Add(currentSupplier);
                    }
                }
                else
                {
                    var pokeball = new ApplicationException("Data Not Found!");
                    throw pokeball;
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

            return supplierList;
        }
        /// <summary>
        /// INSERTs a Supplier into the Database using a Stored Procedure.
        /// 
        /// Created by Tyler Collins 02/04/2015
        /// </summary>
        /// <param name="supplierToAdd">Requires a Supplier object to INSERT</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int AddSupplier(Supplier supplierToAdd)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spInsertSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyName", supplierToAdd.CompanyName);
            cmd.Parameters.AddWithValue("@FirstName", supplierToAdd.FirstName);
            cmd.Parameters.AddWithValue("@LastName", supplierToAdd.LastName);
            cmd.Parameters.AddWithValue("@Address1", supplierToAdd.Address1);
            cmd.Parameters.AddWithValue("@Address2", supplierToAdd.Address2);
            cmd.Parameters.AddWithValue("@Zip", supplierToAdd.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", supplierToAdd.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", supplierToAdd.EmailAddress);
            cmd.Parameters.AddWithValue("@ApplicationID", supplierToAdd.ApplicationID);
            cmd.Parameters.AddWithValue("@UserID", supplierToAdd.UserID);
            //cmd.Parameters.AddWithValue("@SupplyCost", supplierToAdd.SupplyCost);

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
        /// UPDATEs a Supplier record in the Database with new using a Stored Procedure.
        /// 
        /// Created by Tyler Collins 02/04/2015
        /// Updated by Tyler Collins 02/11/2015
        /// </summary>
        /// <param name="newSupplierInfo">Requires the Supplier object containing the new information</param>
        /// <param name="oldSupplierInfo">Requires the Supplier object to replace that matches the record in the Database</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int UpdateSupplier(Supplier newSupplierInfo, Supplier oldSupplierInfo)
        {

            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spUpdateSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Updated Supplier Info:
            cmd.Parameters.AddWithValue("@CompanyName", newSupplierInfo.CompanyName);
            cmd.Parameters.AddWithValue("@FirstName", newSupplierInfo.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newSupplierInfo.LastName);
            cmd.Parameters.AddWithValue("@Address1", newSupplierInfo.Address1);
            cmd.Parameters.AddWithValue("@Address2", newSupplierInfo.Address2);
            cmd.Parameters.AddWithValue("@Zip", newSupplierInfo.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", newSupplierInfo.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", newSupplierInfo.EmailAddress);
            cmd.Parameters.AddWithValue("@ApplicationID", newSupplierInfo.ApplicationID);
            cmd.Parameters.AddWithValue("@UserID", newSupplierInfo.UserID);
            cmd.Parameters.AddWithValue("@SupplyCost", newSupplierInfo.SupplyCost);


            //Old Supplier Info
            cmd.Parameters.AddWithValue("@SupplierID", oldSupplierInfo.SupplierID);
            cmd.Parameters.AddWithValue("@originalCompanyName", oldSupplierInfo.CompanyName);
            cmd.Parameters.AddWithValue("@originalFirstName", oldSupplierInfo.FirstName);
            cmd.Parameters.AddWithValue("@originalLastName", oldSupplierInfo.LastName);
            cmd.Parameters.AddWithValue("@originalAddress1", oldSupplierInfo.Address1);
            cmd.Parameters.AddWithValue("@originalAddress2", oldSupplierInfo.Address2);
            cmd.Parameters.AddWithValue("@originalZip", oldSupplierInfo.Zip);
            cmd.Parameters.AddWithValue("@originalPhoneNumber", oldSupplierInfo.PhoneNumber);
            cmd.Parameters.AddWithValue("@originalEmailAddress", oldSupplierInfo.EmailAddress);
            cmd.Parameters.AddWithValue("@originalApplicationID", oldSupplierInfo.ApplicationID);
            cmd.Parameters.AddWithValue("@originalUserID", oldSupplierInfo.UserID);
            cmd.Parameters.AddWithValue("@originalSupplyCost", oldSupplierInfo.SupplyCost);

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
        /// DELETEs (Sets Boolean Active field to false) a Supplier record in Database using a Stored Procedure.
        /// 
        /// Created by Tyler Collins 02/05/2015
        /// </summary>
        /// <param name="supplierToDelete">Requires the Supplier object which matches the record to be DELETED in the Database.</param>
        /// <returns>Returns the number of rows affected</returns>
        public static int DeleteSupplier(Supplier supplierToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyName", supplierToDelete.CompanyName);
            cmd.Parameters.AddWithValue("@FirstName", supplierToDelete.FirstName);
            cmd.Parameters.AddWithValue("@LastName", supplierToDelete.LastName);
            cmd.Parameters.AddWithValue("@Address1", supplierToDelete.Address1);
            cmd.Parameters.AddWithValue("@Address2", supplierToDelete.Address2);
            cmd.Parameters.AddWithValue("@Zip", supplierToDelete.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", supplierToDelete.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", supplierToDelete.EmailAddress);
            cmd.Parameters.AddWithValue("@ApplicationID", supplierToDelete.ApplicationID);
            cmd.Parameters.AddWithValue("@UserID", supplierToDelete.UserID);
            cmd.Parameters.AddWithValue("@SupplierID", supplierToDelete.SupplierID);

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

        public static int DeleteTestSupplier(Supplier supplierToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteSupplier_1";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierID", supplierToDelete.SupplierID);

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
