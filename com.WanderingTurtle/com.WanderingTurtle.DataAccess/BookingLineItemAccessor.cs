using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.DataAccess
{
    public class BookingLineItemAccessor
    {
        /// <summary>
        /// Returns a BookingLineItem based on input parameters
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="ItemListID"></param>
        /// <returns>
        /// bliToReturn
        /// </returns>
        public static BookingLineItem getBookingLineItem(int bookingID, int ItemListID)
        {
            var bliToReturn = new BookingLineItem();

            var conn = DatabaseConnection.GetDatabaseConnection();

            string sql = @"spBookingLineItemSelectSingle";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@BookingID", bookingID);
            command.Parameters.AddWithValue("@ItemListID", ItemListID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    bliToReturn.BookingID = (int)reader.GetValue(0);
                    bliToReturn.ItemListID = (int)reader.GetValue(1);
                    bliToReturn.Quantity = (int)reader.GetValue(2);
                }
                else
                {
                    throw new ApplicationException("No such BookingLineItem Exists.");
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


            return bliToReturn;
        }

        /// <summary>
        /// Booking Line Item list "getter"
        /// Author: Arik Chadima, 2/6/15
        /// </summary>
        /// <returns>
        /// Returns a list of Booking Line Items.
        /// </returns>
        public static List<BookingLineItem> getBookingLineItemList()
        {
            var bliList = new List<BookingLineItem>();

            var conn = DatabaseConnection.GetDatabaseConnection();

            string sql = @"spBookingLineItemSelectAll";
            SqlCommand command = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var bliToReturn = new BookingLineItem();
                        bliToReturn.BookingID = (int)reader.GetValue(0);
                        bliToReturn.ItemListID = (int)reader.GetValue(1);
                        bliToReturn.Quantity = (int)reader.GetValue(2);
                        bliList.Add(bliToReturn);
                    }
                }
                else
                {
                    throw new ApplicationException("No booking line items found.");
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



            return bliList;
        }

        /// <summary>
        /// Marks passed BookingLineItem as inactive
        /// </summary>
        /// <param name="itemToDelete"></param>
        /// <returns>
        /// Execution command
        /// </returns>
        public static int deleteBookingLineItem(BookingLineItem itemToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();

            string sql = @"spBookingLineItemSelectSingle";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@BookingID", itemToDelete.BookingID);
            command.Parameters.AddWithValue("@ItemListID", itemToDelete.ItemListID);
            command.Parameters.AddWithValue("@Quantity", itemToDelete.Quantity);
            try
            {
                conn.Open();

                return command.ExecuteNonQuery();
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
        /// Adds new BookingLineItem object
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns>
        /// Execution command
        /// </returns>
        public static int addBookingLineItem(BookingLineItem itemToAdd)
        {

            var conn = DatabaseConnection.GetDatabaseConnection();

            string sql = @"spBookingLineItemInsert";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@BookingID", itemToAdd.BookingID);
            command.Parameters.AddWithValue("@ItemListID", itemToAdd.ItemListID);
            command.Parameters.AddWithValue("@Quantity", itemToAdd.Quantity);
            try
            {
                conn.Open();

                return command.ExecuteNonQuery();
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
        /// Updates database information and compares old information with current database values
        /// to ensure that the database has not been modified in the meantime
        /// </summary>
        /// <param name="oldList">Old values to ensure data has not been modified</param>
        /// <param name="newList">New values to be written to database</param>
        /// <returns>
        /// Execution command
        /// </returns>
        public static int updateBookingLineItem(BookingLineItem newItem, BookingLineItem oldItem)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();

            string sql = @"spBookingLineItemInsert";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@BookingID", newItem.BookingID);
            command.Parameters.AddWithValue("@ItemListID", newItem.ItemListID);
            command.Parameters.AddWithValue("@Quantity", newItem.Quantity);
            command.Parameters.AddWithValue("@original_BookingID", oldItem.BookingID);
            command.Parameters.AddWithValue("@original_ItemListID", oldItem.ItemListID);
            command.Parameters.AddWithValue("@original_Quantity", oldItem.Quantity);
            try
            {
                conn.Open();

                return command.ExecuteNonQuery();
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
    }
}
