using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class BookingAccessor
    {
        /*Creates a list of options, has an ItemListID, Quantity, and some event info
         * to help populate drop downs/ lists for Add and update Bookings
         *Returns a list of BookingOptions objects 
         * 
         * Tony Noel- 2/13/15
         */
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

                        currentBook.ItemListID = reader.GetInt32(0);
                        currentBook.QuantityOffered = reader.GetInt32(1);
                        currentBook.StartDate = reader.GetDateTime(2);
                        currentBook.EndDate = reader.GetDateTime(3);
                        currentBook.EventID = reader.GetInt32(4);
                        currentBook.EventName = reader.GetString(5);
                        currentBook.EventDescription = reader.GetString(6);

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

        /* getBookingList- a method used to collect a list of bookings from the database
         * Output is a list of booking objects to hold the booking records.
         * Specific Exception thrown is if the booking data cannot be found.
         * Created By: Tony Noel - 2/3/15
         * */

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
                        currentBook.Cancel = reader.GetBoolean(6);
                        currentBook.Refund = reader.GetDecimal(7);
                        currentBook.Active = reader.GetBoolean(8);

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

        /* AddBooking- a method used to insert a booking into the database
         * inputs a Booking object to be inserted
         * Output is the number of rows affected by the insert
         * Created By: Tony Noel - 2/3/15
         * Updated By:  Pat Banks - 2/19/15 (exception  handling if add wasn't successful)
         * */

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

        /* getBooking- a method used to select a booking record from the database
            * Takes an input of an int- the BookingID number to locate the requested record.
            * Output is a booking object to hold the booking record.
            * Specific Exception thrown is if the BookingID isn't on file.
            * Created By: Tony Noel - 2/3/15
            * */

        public static Booking getBooking(int BookingID)
        {
            Booking BookingToGet = new Booking();

            //establish connection
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectBooking";
            //create a Sql Command
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@bookingID", BookingID); //This is the parameter passing portion of the code.

            try
            {
                //open connection
                conn.Open();
                //execute the command and capture the results to a SqlDataReader
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();

                    BookingToGet.BookingID = reader.GetInt32(0);
                    BookingToGet.GuestID = reader.GetInt32(1);
                    if (!reader.IsDBNull(2)) BookingToGet.EmployeeID = reader.GetInt32(2);
                    BookingToGet.ItemListID = reader.GetInt32(3);
                    BookingToGet.Quantity = reader.GetInt32(4);
                    BookingToGet.DateBooked = reader.GetDateTime(5);
                    BookingToGet.Cancel = reader.GetBoolean(6);
                    BookingToGet.Refund = reader.GetDecimal(7);
                    BookingToGet.Active = reader.GetBoolean(8);
                }
                else
                {
                    var up = new ApplicationException("The BookingID provided does not match any records on file.");
                    throw up;
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

        /* UpdateBooking- a method used to update a booking in the database
     * inputs are the original Booking object along with a booking object to update
     * Output is the rows affected by the update
     * Created By: Tony Noel - 2/3/15
         * Updated - TOny Noel 15/3/2
     * */

        public static int updateBooking(Booking oldOne, Booking toUpdate)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spUpdateBooking";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Quantity", toUpdate.Quantity);
            cmd.Parameters.AddWithValue("@Refund", toUpdate.Refund);
            cmd.Parameters.AddWithValue("@Cancel", toUpdate.Cancel);
            cmd.Parameters.AddWithValue("@Active", toUpdate.Active);

            cmd.Parameters.AddWithValue("@original_BookingID", oldOne.BookingID);
            cmd.Parameters.AddWithValue("@original_GuestID", oldOne.GuestID);
            cmd.Parameters.AddWithValue("@original_EmployeeID", oldOne.EmployeeID);
            cmd.Parameters.AddWithValue("@original_ItemListID", oldOne.ItemListID);
            cmd.Parameters.AddWithValue("@original_Quantity", oldOne.Quantity);
            cmd.Parameters.AddWithValue("@original_DateBooked", oldOne.DateBooked);
            cmd.Parameters.AddWithValue("@original_Refund", oldOne.Refund);
            cmd.Parameters.AddWithValue("@original_Cancel", oldOne.Cancel);
            cmd.Parameters.AddWithValue("@original_Active", oldOne.Active);

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