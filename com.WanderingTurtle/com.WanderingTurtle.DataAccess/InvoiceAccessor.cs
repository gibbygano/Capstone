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
        /// Retrieves a list of all open invoices from the database.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>List of open Invoices</returns>
        public static List<InvoiceDetails> getAllInvoiceList()
        {
            var openInvoiceList = new List<InvoiceDetails>();
            var conn = DatabaseConnection.GetDatabaseConnection();

            string query = "spSelectAllInvoices";

            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        var currentOpenInvoice = new InvoiceDetails();

                        currentOpenInvoice.InvoiceID = reader.GetInt32(0);
                        currentOpenInvoice.HotelGuestID = reader.GetInt32(1);
                        currentOpenInvoice.DateOpened = reader.GetDateTime(2);
                        currentOpenInvoice.Active = reader.GetBoolean(3);
                        currentOpenInvoice.GuestLastName = reader.GetValue(4).ToString();
                        currentOpenInvoice.GuestFirstName = reader.GetValue(5).ToString();
                        
                        openInvoiceList.Add(currentOpenInvoice);
                    }
                }
                else
                {
                    var ax = new ApplicationException("Invoice data not found!");
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
            return openInvoiceList;

        }//end getInvoices()


        /// <summary>
        /// Created by Pat Banks 2015/02/25
        /// Retrieves the bookings for a guest
        /// </summary>
        /// <param name="GuestID">A Hotel Guest's ID</param>
        /// <returns>List of Bookings</returns>
        public static List<BookingDetails> getInvoiceBookingsByGuest(int GuestID)
        {
            List< BookingDetails> guestBookings = new List<BookingDetails>();

            var conn = DatabaseConnection.GetDatabaseConnection();

            string sql = @"spSelectInvoiceBookings";

            SqlCommand command = new SqlCommand(sql, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hotelGuestID", GuestID);

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
                        details.TotalPrice = details.Price * details.Quantity;

                        guestBookings.Add(details);
                    }
                }
                else
                {
                    throw new ApplicationException("Customer does not have any bookings.");
                }
            }
            catch (Exception ex)
            {

            }
            return guestBookings;
        }

        /// <summary>
        /// Created by Pat Banks 2015/02/25
        /// Retrieves Guest information for the invoice
        /// </summary>
        /// <param name="guestID">Hotel Guest ID</param>
        /// <returns>Invoice information for the guest</returns>
        public static InvoiceDetails getInvoiceByGuest(int guestID)
        {
            //create invoice object to store the invoice information
            InvoiceDetails guestInvoice = new InvoiceDetails();

            var conn = DatabaseConnection.GetDatabaseConnection();

            string query = "spSelectInvoiceByGuest";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@guestID", guestID);

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


    }//end class
} //end namespace