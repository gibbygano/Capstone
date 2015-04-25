﻿using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    public class BookingAccessor
    {
        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/13
        ///
        /// Creates a list of options, has an ItemListID, Quantity, and some event info
        /// to help populate drop downs/ lists for Add Bookings
        /// </summary>
        /// <returns>a list of ItemListingDetails objects (is created from two tables, ItemListing and Event Item)</returns>
        public static List<ItemListingDetails> GetItemListingDetailsList()
        {
            var BookingOpsList = new List<ItemListingDetails>();
            //Set up database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectItemListingDetailsList";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentBook = new ItemListingDetails();
                        //Below are found on the ItemListing table (ItemListID is a foreign key on booking)
                        currentBook.ItemListID = reader.GetInt32(0);
                        currentBook.MaxNumGuests = reader.GetInt32(1);
                        currentBook.CurrentNumGuests = reader.GetInt32(2);
                        currentBook.StartDate = reader.GetDateTime(3);
                        currentBook.EndDate = reader.GetDateTime(4);
                        //Below are found on the EventItem table
                        currentBook.EventID = reader.GetInt32(5);
                        currentBook.EventName = reader.GetString(6);
                        currentBook.EventDescription = reader.GetString(7);
                        //this is from itemlisting table
                        currentBook.Price = reader.GetDecimal(8);

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

        /// <summary>
        /// Created by Pat Banks 2015/03/11
        ///
        /// Retrieves the event listing information to to enable
        /// user to see current number of spots available for a listing
        /// </summary>
        /// <param name="itemListID">Id for the itemListing</param>
        /// <returns></returns>
        public static ItemListingDetails GetItemListingDetails(int itemListID)
        {
            var eventItemListing = new ItemListingDetails();

            //Set up database call
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectItemListingDetailsByID";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@itemListID", itemListID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        //Below are found on the ItemListing table (ItemListID is a foreign key on booking)
                        eventItemListing.ItemListID = reader.GetInt32(0);
                        eventItemListing.MaxNumGuests = reader.GetInt32(1);
                        eventItemListing.CurrentNumGuests = reader.GetInt32(2);
                        eventItemListing.StartDate = reader.GetDateTime(3);
                        eventItemListing.EndDate = reader.GetDateTime(4);
                        //Below are found on the EventItem table
                        eventItemListing.EventID = reader.GetInt32(5);
                        eventItemListing.EventName = reader.GetString(6);
                        eventItemListing.EventDescription = reader.GetString(7);
                        //this is from itemlisting table
                        eventItemListing.Price = reader.GetDecimal(8);
                    }
                }
                else
                {
                    var ax = new ApplicationException("Event data not found!");
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
            return eventItemListing;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/03
        ///
        /// AddBooking- a method used to insert a booking into the database
        /// </summary>
        /// <remarks>
        /// Updated By:  Pat Banks -2015/02/19
        /// exception  handling if add wasn't successful
        /// Pat Banks
        /// Updated:  2015/04/25
        /// SP also updates the Current Number of guests based on the total number of bookings for that Item ID
        /// </remarks>
        /// <param name="toAdd">input- a Booking object to be inserted</param>
        /// <returns>Output is the number of rows affected by the insert</returns>
        public static int AddBooking(Booking toAdd)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spInsertBookingUpdateListingNum";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", toAdd.GuestID);
            cmd.Parameters.AddWithValue("@EmployeeID", toAdd.EmployeeID);
            cmd.Parameters.AddWithValue("@ItemListID", toAdd.ItemListID);
            cmd.Parameters.AddWithValue("@Quantity", toAdd.Quantity);
            cmd.Parameters.AddWithValue("@DateBooked", toAdd.DateBooked);
            cmd.Parameters.AddWithValue("@Discount", toAdd.Discount);
            cmd.Parameters.AddWithValue("@TicketPrice", toAdd.TicketPrice);
            cmd.Parameters.AddWithValue("@ExtendedPrice", toAdd.ExtendedPrice);
            cmd.Parameters.AddWithValue("@TotalCharge", toAdd.TotalCharge);

            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();

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

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/03
        ///
        /// getBooking- a method used to select a specified booking record from the database
        /// </summary>
        /// <remarks>
        /// Tony Noel
        /// Updated: 2015/03/03
        /// </remarks>
        /// <param name="BookingID">Takes an input of an int- the BookingID number to locate the requested record.</param>
        /// <returns>Output is a booking object to hold the booking record.</returns>
        public static Booking GetBooking(int BookingID)
        {
            //create Booking object to store info
            Booking BookingToGet = new Booking();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectBookingByID";

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

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/03
        ///
        /// UpdateBooking- a method used to update a booking in the database, allows only four booking fields to be updated:
        /// Quantity, Refund, Cancel
        /// </summary>
        /// <remarks>
        /// Tony Noel
        /// Updated: 2015/03/02
        /// 
        /// Pat Banks
        /// Updated:  2015/04/25
        /// SP also updates the Current Number of guests based on the total number of bookings for that Item ID
        /// </remarks>
        /// <param name="oldOne">The original Booking object/values</param>
        /// <param name="toUpdate">The new booking object values to replace the old</param>
        /// <returns>Output is the rows affected by the update</returns>
        public static int UpdateBooking(Booking oldOne, Booking toUpdate)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateBookingUpdateListingNum";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Quantity", toUpdate.Quantity);
            cmd.Parameters.AddWithValue("@Discount", toUpdate.Discount);
            cmd.Parameters.AddWithValue("@Active", toUpdate.Active);
            cmd.Parameters.AddWithValue("@TicketPrice", toUpdate.TicketPrice);
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/14
        /// Gets Booking numbers from database for specific event listing
        /// </summary>
        /// <param name="itemListID"></param>
        /// <returns></returns>
        public static List<BookingNumbers> GetBookingNumbers(int itemListID)
        {
            var bookingNumber = new List<BookingNumbers>();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectBookingNumbers";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemListID", itemListID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var myBookingNumber = new BookingNumbers();
                        myBookingNumber.FirstName = reader.GetValue(0).ToString();
                        myBookingNumber.LastName = reader.GetValue(1).ToString();
                        myBookingNumber.Room = reader.GetValue(2).ToString();
                        myBookingNumber.Quantity = (int)reader.GetValue(3);
                        bookingNumber.Add(myBookingNumber);
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
            return bookingNumber;
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/14
        /// 
        /// Verifies the hotel guest pin to sign up for an item listing and create a booking
        /// </summary>
        /// <param name="inPIN"></param>
        /// <returns>Hotel guest that has that pin</returns>
        public static HotelGuest VerifyHotelGuestPin(string inPIN)
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spSelectHotelGuestByPin";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@guestPIN", inPIN);

            HotelGuest foundGuest = null;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foundGuest = new HotelGuest(
                                reader.GetInt32(0), //HotelGuestID
                                reader.GetString(1), //FirstName
                                reader.GetString(2), //LastName
                                reader.GetString(3), //Address1
                                !reader.IsDBNull(4) ? reader.GetString(4) : null, //Address2
                                new CityState(
                                    reader.GetString(5), //Zip
                                    reader.GetString(6), //City
                                    reader.GetString(7) //State
                                ),
                                !reader.IsDBNull(8) ? reader.GetString(8) : null, //PhoneNumber
                                !reader.IsDBNull(9) ? reader.GetString(9) : null, //EmailAdddress
                                !reader.IsDBNull(10) ? reader.GetString(10) : null, //Room
                                !reader.IsDBNull(11) ? reader.GetString(11) : null, // PIN
                                reader.GetBoolean(12) //Active
                        );
                    } // end while
                }
                else
                {
                    var ax = new ApplicationException("PIN not found");
                    throw ax;
                }
            }
            catch (SqlException)
            {
                var ax = new ApplicationException("PIN not found");
                throw ax;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return foundGuest;
        }
    }
}