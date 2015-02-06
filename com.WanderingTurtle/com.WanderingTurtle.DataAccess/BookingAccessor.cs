using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.DataAccess
{
    public class BookingAccessor
    {
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
            SqlConnection conn = GenericDatabase.GetConnection();
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
                    BookingToGet.DateBooked = reader.GetDateTime(3);
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


        /* getBookingList- a method used to collect a list of bookings from the database
         * Output is a list of booking objects to hold the booking records.
         * Specific Exception thrown is if the booking data cannot be found.
         * Created By: Tony Noel - 2/3/15
         * */
        public static List<Booking> getBookingList()
        {
            var BookingList = new List<Booking>();
            //Set up database call
            var conn = GenericDatabase.GetConnection();
            string query = "SELECT * FROM Booking";
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
                        currentBook.DateBooked = reader.GetDateTime(3);

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
         * */
        public static int addBooking(Booking toAdd)
        {
            var conn = GenericDatabase.GetConnection();
            var cmdText = "spAddBooking";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookingID", toAdd.BookingID);
            cmd.Parameters.AddWithValue("@GuestID", toAdd.GuestID);
            cmd.Parameters.AddWithValue("@EmployeeID", toAdd.EmployeeID);
            cmd.Parameters.AddWithValue("@DateBooked", toAdd.DateBooked);
            
            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();
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



        /* UpdateBooking- a method used to update a booking in the database
         * inputs are the original Booking object along with a booking object to update
         * Output is the rows affected by the update
         * Created By: Tony Noel - 2/3/15
         * */
        public static int updateBooking(Booking oldOne, Booking toUpdate)
        {
            var conn = GenericDatabase.GetConnection();
            var cmdText = "spUpdateBooking";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", toUpdate.GuestID);
            cmd.Parameters.AddWithValue("@EmployeeID", toUpdate.EmployeeID);
            cmd.Parameters.AddWithValue("@DateBooked", toUpdate.DateBooked);
            
            cmd.Parameters.AddWithValue("@original_BookingID", oldOne.BookingID);
            cmd.Parameters.AddWithValue("@original_GuestID", oldOne.GuestID);
            cmd.Parameters.AddWithValue("@original_EmployeeID", oldOne.EmployeeID);
            cmd.Parameters.AddWithValue("@original_DateBooked", oldOne.DateBooked);

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


        /* DeleteBooking- a method used to delete a booking in the database
         * inputs are the Booking object to delete
         * Output is the rows affected by the update
         * Created By: Tony Noel - 2/3/15
         * */
        public static int deleteBooking(Booking toDelete)
        {
            var conn = GenericDatabase.GetConnection();
            var cmdText = "spDeleteBooking";
            var cmd = new SqlCommand(cmdText, conn);
            int rowsAffected = 0;

            //Set command type to stored procedure and add parameters
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookingID", toDelete.BookingID);
            cmd.Parameters.AddWithValue("@original_GuestID", toDelete.GuestID);
            cmd.Parameters.AddWithValue("@original_EmployeeID", toDelete.EmployeeID);
            cmd.Parameters.AddWithValue("@original_DateBooked", toDelete.DateBooked);
            
            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();
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
