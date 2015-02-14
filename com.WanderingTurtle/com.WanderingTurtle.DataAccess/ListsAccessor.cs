using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;
using System.Data;



namespace com.WanderingTurtle.DataAccess
{
    class ListsAccessor
    {
        public static List<Lists> getListsList(string inListName)
        {
            var myLists = new List<Lists>();

            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "SELECT SupplierID, ItemListID, DateListed " +
            "FROM Lists";
            var cmd = new SqlCommand(query, conn);

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

        public static Lists getLists(string inSupplierID, string inItemListID)
        {
            var theLists = new Lists();
            // set up the database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "SELECT SupplierID, ItemListID, DateListed"  +
            "FROM Lists WHERE ItemListID = " + inItemListID + "AND SupplierID = " + inSupplierID;
            var cmd = new SqlCommand(query, conn);


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
