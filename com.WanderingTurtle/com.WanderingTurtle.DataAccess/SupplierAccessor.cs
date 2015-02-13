using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class SupplierAccessor
    {
        public static Supplier GetSupplier(string supplierID)
        {
            /*
             * Retrieves Supplier information from database based on SupplierID and returns a Supplier Object.
             *
             * Created by Tyler Collins 02/03/15
             */

            Supplier supplierToRetrieve = new Supplier();
            string query = "Select SupplierID, CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, SupplierType, ApplicationID, UserID "
                + "From Supplier Where SupplierID = '" + supplierID + "' and Active=1";
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            SqlCommand cmd = new SqlCommand(query, conn);

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
                    supplierToRetrieve.SupplierTypeID = reader.GetInt32(9);
                    supplierToRetrieve.ApplicationID = reader.GetInt32(10);
                    supplierToRetrieve.UserID = reader.GetInt32(11);
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
        //    SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

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

        public static List<Supplier> GetSupplierList()
        {
            /*
             * Retrieves Supplier information from database based on SupplierID and creates a Supplier Object.
             * Adds Supplier Object to a List of Supplier Objects.
             * Repeats for every supplier in the table.
             * Returns List of Supplier Objects
             *
             * Created by Tyler Collins 02/03/15
             */
            List<Supplier> supplierList = new List<Supplier>();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "SELECT SupplierID, CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, SupplierTypeID, ApplicationID, UserID "
                + "FROM Suppliers WHERE Active=1";
            var cmd = new SqlCommand(query, conn);

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
                        currentSupplier.SupplierTypeID = reader.GetInt32(9);
                        currentSupplier.ApplicationID = reader.GetInt32(10);
                        currentSupplier.UserID = reader.GetInt32(11);

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

        public static int AddSupplier(Supplier supplierToAdd)
        {
            /*
             * Takes a Supplier Object to INSERT a Supplier into the database.
             *
             * Created by Tyler Collins 02/04/15
             */
            var conn = DatabaseConnection.GetDatabaseConnection();

            string storedProcedure = "spInsertSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);

            cmd.Parameters.AddWithValue("@CompanyName", supplierToAdd.CompanyName);
            cmd.Parameters.AddWithValue("@FirstName", supplierToAdd.FirstName);
            cmd.Parameters.AddWithValue("@LastName", supplierToAdd.LastName);
            cmd.Parameters.AddWithValue("@Address1", supplierToAdd.Address1);
            cmd.Parameters.AddWithValue("@Address2", supplierToAdd.Address2);
            cmd.Parameters.AddWithValue("@Zip", supplierToAdd.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", supplierToAdd.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", supplierToAdd.EmailAddress);
            cmd.Parameters.AddWithValue("@SupplierTypeID", supplierToAdd.SupplierTypeID);
            cmd.Parameters.AddWithValue("@ApplicationID", supplierToAdd.ApplicationID);
            cmd.Parameters.AddWithValue("@UserID", supplierToAdd.UserID);

            int numRows;
            try
            {
                conn.Open();
                numRows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return numRows;
        }

        public static int UpdateSupplier(Supplier newSupplierInfo)
        {
            /*
             * Takes a Supplier Object.
             * Retrieves Supplier data based on passed Supplier Object.
             * Creates duplicate Supplier Object with original data.
             * Updates database with passed Supplier Object data WHERE database data = original Supplier Object data.
             *
             * Created by Tyler Collins 02/04/15
             */
            Supplier oldSupplierInfo = new Supplier();
            oldSupplierInfo = GetSupplier((newSupplierInfo.SupplierID).ToString());

            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spUpdateSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);

            //Need to look at spUpdateSupplier Stored Procedure
            //Updated Supplier Info:
            cmd.Parameters.AddWithValue("@CompanyName", newSupplierInfo.CompanyName);
            cmd.Parameters.AddWithValue("@FirstName", newSupplierInfo.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newSupplierInfo.LastName);
            cmd.Parameters.AddWithValue("@Address1", newSupplierInfo.Address1);
            cmd.Parameters.AddWithValue("@Address2", newSupplierInfo.Address2);
            cmd.Parameters.AddWithValue("@Zip", newSupplierInfo.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", newSupplierInfo.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", newSupplierInfo.EmailAddress);
            cmd.Parameters.AddWithValue("@SupplierTypeID", newSupplierInfo.SupplierTypeID);
            cmd.Parameters.AddWithValue("@ApplicationID", newSupplierInfo.ApplicationID);
            cmd.Parameters.AddWithValue("@UserID", newSupplierInfo.UserID);

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
            cmd.Parameters.AddWithValue("@originalSupplierTypeID", oldSupplierInfo.SupplierTypeID);
            cmd.Parameters.AddWithValue("@originalApplicationID", oldSupplierInfo.ApplicationID);
            cmd.Parameters.AddWithValue("@originalUserID", oldSupplierInfo.UserID);

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

        public static int DeleteSupplier(Supplier supplierToDelete)
        {
            /*
             * Takes a Supplier Object to match to a record in the database.
             * Updates a Supplier record in database to set Active field to false.
             *
             * Created by Tyler Collins 02/05/15
             */
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);

            cmd.Parameters.AddWithValue("@CompanyName", supplierToDelete.CompanyName);
            cmd.Parameters.AddWithValue("@FirstName", supplierToDelete.FirstName);
            cmd.Parameters.AddWithValue("@LastName", supplierToDelete.LastName);
            cmd.Parameters.AddWithValue("@Address1", supplierToDelete.Address1);
            cmd.Parameters.AddWithValue("@Address2", supplierToDelete.Address2);
            cmd.Parameters.AddWithValue("@Zip", supplierToDelete.Zip);
            cmd.Parameters.AddWithValue("@PhoneNumber", supplierToDelete.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", supplierToDelete.EmailAddress);
            cmd.Parameters.AddWithValue("@SupplierTypeID", supplierToDelete.SupplierTypeID);
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
    }
}