﻿using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace com.WanderingTurtle.DataAccess
{
    public class BookingAccessor
    {

        /// Created by: Tony Noel 15/2/13
        /// <summary>
        /// Creates a list of options, has an ItemListID, Quantity, and some event info
        /// to help populate drop downs/ lists for Add and update Bookings
        /// </summary>
        /// <returns>a list of ListItemObject objects (is created from two tables, ItemListing and Event Item)</returns>
        public static List<ListItemObject> getListItems()
        {
            var BookingOpsList = new List<ListItemObject>();
            //Set up database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectBookingFull";
            var cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentBook = new ListItemObject();
                        //Below are found on the ItemListing table (ItemListID is a foriegn key on booking)
                        currentBook.ItemListID = reader.GetInt32(0);
    //currentBook.MaxNumberOfGuests = reader.GetInt32(1);
    //currentBook.CurrentNumberOfGuests = reader.GetInt32(2);
                        currentBook.StartDate = reader.GetDateTime(3);
                        currentBook.EndDate = reader.GetDateTime(4);
                        //Below are found on the EventItem table
                        currentBook.EventID = reader.GetInt32(5);
                        currentBook.EventName = reader.GetString(6);
                        currentBook.EventDescription = reader.GetString(7);
                        //this is from itemlisting table
                        currentBook.TicketPrice = reader.GetDecimal(8);

                        BookingOpsList.Add(currentBook);
                    }
                }
                else
                {
                    var ax = new ApplicationException("Booking data not found!");
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
            return BookingOpsList;
        }


        ///Created By: Tony Noel - 15/2/3, Updated: 15/3/3 Tony Noel
        /// <summary>
        /// getBookingList- a method used to collect a list of bookings from the database
        /// </summary>
        /// <exception cref="ApplicationException"> Specific Exception thrown is if the booking data cannot be found.</exception>
        /// <returns>Output is a list of booking objects to hold the booking records.</returns>
        public static List<Booking> getBookingList()
        {
            var BookingList = new List<Booking>();
            //Set up database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectAllBookings";
            var cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentBook = new Booking();

                        currentBook.BookingID = reader.GetInt32(0);
                        currentBook.GuestID = reader.GetInt32(1);
                        if (!reader.IsDBNull(2)) currentBook.EmployeeID = reader.GetInt32(2);
                        currentBook.ItemListID = reader.GetInt32(3);
                        currentBook.Quantity = reader.GetInt32(4);
                        currentBook.DateBooked = reader.GetDateTime(5);
                        currentBook.Discount = reader.GetDecimal(6);
                        currentBook.Active = reader.GetBoolean(7);
                        currentBook.TicketPrice = reader.GetDecimal(8);
                        currentBook.ExtendedPrice = reader.GetDecimal(9);
                        currentBook.TotalCharge = reader.GetDecimal(10);

                        BookingList.Add(currentBook);
                    }
                }
                else
                {
                    var ax = new ApplicationException("Booking data not found!");
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
            return BookingList;
        }


        ///Created By: Tony Noel - 15/2/3, Updated By:  Pat Banks - 2/19/15 (exception  handling if add wasn't successful)
        /// <summary>
        /// AddBooking- a method used to insert a booking into the database
        /// </summary>
        /// <param name="toAdd">input- a Booking object to be inserted</param>
        /// <returns>Output is the number of rows affected by the insert</returns>
        public static int addBooking(Booking toAdd)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spAddBooking";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", toAdd.GuestID);
            cmd.Parameters.AddWithValue("@EmployeeID", toAdd.EmployeeID);
            cmd.Parameters.AddWithValue("@ItemListID", toAdd.ItemListID);
            cmd.Parameters.AddWithValue("@Quantity", toAdd.Quantity);
            cmd.Parameters.AddWithValue("@DateBooked", toAdd.DateBooked);
            cmd.Parameters.AddWithValue("@TicketPrice", toAdd.TicketPrice);
            cmd.Parameters.AddWithValue("@ExtendedPrice", toAdd.ExtendedPrice);
            cmd.Parameters.AddWithValue("@TotalCharge", toAdd.TotalCharge);

            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();

                //added exception handling pb 2/19/15
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Error adding new database entry");
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

        /********************  Methods not used in Sprint 1 ************************************************/

        ///Created By: Tony Noel - 15/2/3, Updated: Tony Noel 15/3/3
        /// <summary>
        /// getBooking- a method used to select a specified booking record from the database
        /// </summary>
        /// <param name="BookingID">Takes an input of an int- the BookingID number to locate the requested record.</param>
        /// <returns>Output is a booking object to hold the booking record.</returns>
        public static Booking getBooking(int BookingID)
        {
            //create Booking object to store info
            Booking BookingToGet = new Booking();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectBooking";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@bookingID", BookingID);

            //connect to db
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        BookingToGet.BookingID = reader.GetInt32(0);
                        BookingToGet.GuestID = reader.GetInt32(1);
                        BookingToGet.EmployeeID = reader.GetInt32(2);
                        BookingToGet.ItemListID = reader.GetInt32(3);
                        BookingToGet.Quantity = reader.GetInt32(4);
                        BookingToGet.DateBooked = reader.GetDateTime(5);
                        BookingToGet.Discount = reader.GetDecimal(6);
                        BookingToGet.Active = reader.GetBoolean(7);
                        BookingToGet.TicketPrice = reader.GetDecimal(8);
                        BookingToGet.ExtendedPrice = reader.GetDecimal(9);
                        BookingToGet.TotalCharge = reader.GetDecimal(10);
                    }
                }
                else
                {
                    throw new ApplicationException("BookingID does not match an ID on record.");
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
            return BookingToGet;
        }


        ///Created By: Tony Noel - 15/2/3, Updated - Tony Noel 15/3/2
        /// <summary>
        /// UpdateBooking- a method used to update a booking in the database, allows only four booking fields to be updated:
        /// Quantity, Refund, Cancel, and Active
        /// </summary>
        /// <param name="oldOne">The original Booking object/values</param>
        /// <param name="toUpdate">The new booking object values to replace the old</param>
        /// <returns>Output is the rows affected by the update</returns>
        public static int updateBooking(Booking oldOne, Booking toUpdate)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateBooking";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Quantity", toUpdate.Quantity);
            cmd.Parameters.AddWithValue("@Refund", toUpdate.Discount);
            cmd.Parameters.AddWithValue("@Active", toUpdate.Active);
            cmd.Parameters.AddWithValue("@ExtendedPrice", toUpdate.ExtendedPrice);
            cmd.Parameters.AddWithValue("@TotalCharge", toUpdate.TotalCharge);

            cmd.Parameters.AddWithValue("@original_BookingID", oldOne.BookingID);
            cmd.Parameters.AddWithValue("@original_GuestID", oldOne.GuestID);
            cmd.Parameters.AddWithValue("@original_EmployeeID", oldOne.EmployeeID);
            cmd.Parameters.AddWithValue("@original_ItemListID", oldOne.ItemListID);
            cmd.Parameters.AddWithValue("@original_Quantity", oldOne.Quantity);
            cmd.Parameters.AddWithValue("@original_DateBooked", oldOne.DateBooked);
            cmd.Parameters.AddWithValue("@original_Discount", oldOne.Discount);
            cmd.Parameters.AddWithValue("@original_Active", oldOne.Active);
            cmd.Parameters.AddWithValue("@original_TicketPrice", oldOne.TicketPrice);
            cmd.Parameters.AddWithValue("@original_ExtendedPrice", oldOne.ExtendedPrice);
            cmd.Parameters.AddWithValue("@original_TotalCharge", oldOne.TotalCharge);

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