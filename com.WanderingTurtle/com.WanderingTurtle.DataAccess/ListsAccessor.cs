//Justin Pennington 2/14/15
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;



namespace com.WanderingTurtle.DataAccess
{
    public class ListsAccessor
    {

        //Justin Pennington 2/14/15
        /// <summary>
        /// Returns a list of all listings
        /// </summary>
        /// <returns>
        /// myLists
        /// </returns>
        public static List<Lists> GetListsList()
        {
            var myLists = new List<Lists>();

            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectAllLists";
            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentList = new Lists();

                        currentList.SupplierID = reader.GetInt32(0);
                        currentList.ItemListID = reader.GetInt32(1);
                        currentList.DateListed = (DateTime)reader.GetValue(3);
                        
                        myLists.Add(currentList);
                    }
                }
                else
                {
                    var ax = new ApplicationException("Data not found!");
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
            return myLists;
        }

        //Justin Pennington 2/14/15
        /// <summary>
        /// Returns a list of listing objects based on the parameters sent with the method call
        /// </summary>
        /// <param name="inSupplierID">Object holding SupplierID</param>
        /// <param name="inItemListID">Object holding ItemListID</param>
        /// <returns>
        /// theLists
        /// </returns>
        public static Lists GetLists(string inSupplierID, string inItemListID)
        {
            var theLists = new Lists();
            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string cmdText = "spSelectLists";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@SupplierID", inSupplierID);
            cmd.Parameters.AddWithValue("@ItemListID", inItemListID);
            //Put retrieved data into objects
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    theLists.SupplierID = reader.GetInt32(0);
                    theLists.ItemListID = reader.GetInt32(1);
                    theLists.DateListed = (DateTime)reader.GetValue(3);                   
                }
                else
                {
                    var ax = new ApplicationException("Data not found!");
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
            return theLists;
        }

        //Justin Pennington 2/14/15
        /// <summary>
        /// Adds new lists object
        /// </summary>
        /// <param name="newLists">Object holding new listing data</param>
        /// <returns>
        /// rowsAffected
        /// </returns>
        public static int AddLists(Lists newLists)
        {
            //Connect to Database
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spInsertListsItem";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            //Set up Parameters for the Stored Procedures
            cmd.Parameters.AddWithValue("@SupplierID", newLists.SupplierID);
            cmd.Parameters.AddWithValue("@ItemListID", newLists.ItemListID);
            cmd.Parameters.AddWithValue("@DateListed", newLists.DateListed);
            
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

        //Justin Pennington 2/14/15
        /// <summary>
        /// Updates database information and compares old information with current database values
        /// to ensure that the database has not been modified in the meantime
        /// </summary>
        /// <param name="oldList">Old values to ensure data has not been modified</param>
        /// <param name="newList">New values to be written to database</param>
        /// <returns>
        /// rowsAffected
        /// </returns>
        public static int UpdateLists(Lists oldList, Lists newList)
        {
            //connect to Database
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateLists";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            // set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierID", newList.SupplierID);
            cmd.Parameters.AddWithValue("@ItemListID", newList.ItemListID);
            cmd.Parameters.AddWithValue("@DateListed", newList.DateListed);
           

            cmd.Parameters.AddWithValue("@original_SupplierID", oldList.SupplierID);
            cmd.Parameters.AddWithValue("@original_ItemListID", oldList.ItemListID);
            cmd.Parameters.AddWithValue("@original_DateListed", oldList.DateListed);
            
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
            return rowsAffected;  // needs to be rows affected
        }


        //Justin Pennington 2/14/15
        /// <summary>
        /// Marks passed Lists as inactive
        /// </summary>
        /// <param name="inLists">List to be deactivated</param>
        /// <returns>
        /// rowsAffected
        /// </returns>
        public static int DeleteLists(Lists inLists)
        {
            //make connection to Database
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spDeleteLists";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            //set up parameters for the stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@original_SupplierID", inLists.SupplierID);
            cmd.Parameters.AddWithValue("@original_ItemListID", inLists.ItemListID);
            cmd.Parameters.AddWithValue("@original_DateListed", inLists.DateListed);
            
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
            return rowsAffected;  // needs to be rows affected           
        }
    }
}
