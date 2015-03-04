using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Data;

namespace com.WanderingTurtle.DataAccess
{
    public class InvoiceAccessor
    {
        /// <summary>
        /// Created by Pat Banks 2015/02/25
        /// Creates a connection with database and 
        /// calls the stored procedure spSelectInvoiceBookings 
        /// that querys the database
        /// for all bookings of a specified hotel guest
        /// </summary>
        /// <param name="GuestID">A Hotel Guest's ID</param>
        /// <returns>List of Booking details</returns>
        public static List<BookingDetails> GetInvoiceBookingsByGuest(int guestID)
        {
            //create list of bookingdetails
            List<BookingDetails> guestBookings = new List<BookingDetails>();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string sql = @"spSelectInvoiceBookings";

            SqlCommand command = new SqlCommand(sql, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hotelGuestID", guestID);

            //connect to db and retrieve information
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var details = new BookingDetails();

                        details.BookingID = reader.GetInt32(0);
                        details.GuestID = reader.GetInt32(1);
                        details.ItemListID = reader.GetInt32(2);
                        details.Quantity = reader.GetInt32(3);
                        details.Price = reader.GetDecimal(4);
                        details.StartDate = reader.GetDateTime(5);
                        details.EventItemName = reader.GetValue(6).ToString();

                        guestBookings.Add(details);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return guestBookings;
        }

        /// <summary>
        /// Created by Pat Banks 2015/02/25
        /// Creates a connection with database and 
        /// calls the stored procedure spSelectInvoiceByGuest 
        /// that querys the database for a guest's invoice information
        /// </summary>
        /// <remarks></remarks>
        /// <param name="guestID">Hotel Guest ID</param>
        /// <returns>Invoice information for the guest</returns>
        public static InvoiceDetails GetInvoiceByGuest(int guestID)
        {
            //create invoice object to store the invoice information
            InvoiceDetails guestInvoice = new InvoiceDetails();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectInvoiceByGuest";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@guestID", guestID);

            //connect to db and retrieve information
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    guestInvoice.InvoiceID = reader.GetInt32(0);
                    guestInvoice.HotelGuestID = reader.GetInt32(1);
                    guestInvoice.DateOpened = reader.GetDateTime(2);
                    guestInvoice.Active = reader.GetBoolean(3);
                    guestInvoice.GuestLastName = reader.GetValue(4).ToString();
                    guestInvoice.GuestFirstName = reader.GetValue(5).ToString();
                    guestInvoice.GuestRoomNum = reader.GetInt32(6);
                }
                else
                {
                    throw new ApplicationException("Customer does not have an open invoice.");
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
            return guestInvoice;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        /// Creates a connection with database and 
        /// calls the stored procedure spSelectAllInvoices
        /// that querys the database for a list of all active invoices
        /// </summary>
        /// <remarks></remarks>
        /// <returns>List of InvoiceDetails</returns>
        public static List<InvoiceDetails> GetActiveInvoiceList()
        {
            //create list of InvoiceDetail Objects to store the invoice information
            var guestList = new List<InvoiceDetails>();
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectAllInvoices";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;

            //connect to db and retrieve information
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var details = new InvoiceDetails();

                        details.InvoiceID = reader.GetInt32(0);
                        details.HotelGuestID = reader.GetInt32(1);
                        details.DateOpened = reader.GetDateTime(2);
                        details.Active = reader.GetBoolean(3);
                        details.GuestLastName = reader.GetValue(4).ToString();
                        details.GuestFirstName = reader.GetValue(5).ToString();
                        details.GuestRoomNum = reader.GetInt32(6);

                        if (details.Active == true)
                        {
                            guestList.Add(details);
                        } 
                    }
                }
                else
                {
                    throw new ApplicationException("No open invoices found.");
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
            return guestList;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        /// Creates a connection with database and 
        /// calls the stored procedure spArchiveInvoice
        /// that updates database with information to archive an invoice
        /// </summary>
        /// <param name="originalInvoice">invoice that was fetched from database - used to check for concurrency errors</param>
        /// <param name="updatedInvoice">information that needs to be updated in the database</param>
        /// <returns>Number of rows affected</returns>
        public static int ArchiveGuestInvoice(Invoice originalInvoice, Invoice updatedInvoice)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spArchiveInvoice";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var numRows = 0;

            //parameters for stored procedure
            cmd.Parameters.AddWithValue("@hotelGuestID", updatedInvoice.HotelGuestID);
            cmd.Parameters.AddWithValue("@active", updatedInvoice.Active);
            cmd.Parameters.AddWithValue("@dateOpened", updatedInvoice.DateOpened);
            cmd.Parameters.AddWithValue("@dateClosed", updatedInvoice.DateClosed);
            cmd.Parameters.AddWithValue("@totalPaid", updatedInvoice.TotalPaid);

            cmd.Parameters.AddWithValue("@original_invoiceID", originalInvoice.InvoiceID);
            cmd.Parameters.AddWithValue("@original_hotelGuestID", originalInvoice.HotelGuestID);
            cmd.Parameters.AddWithValue("@original_active", originalInvoice.Active);
            cmd.Parameters.AddWithValue("@original_dateOpened", originalInvoice.DateOpened);

            //connect to db
            try
            {
                conn.Open();
                numRows = cmd.ExecuteNonQuery();

                if (numRows == 0)
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

            return numRows;
        }

    }//end class
} //end namespace