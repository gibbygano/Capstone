using com.WanderingTurtle.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using com.WanderingTurtle.DataAccess;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    public class TestCleanupAccessor
    {

        public static void testEmp(Employee testEmp)
        {
            //establish connection
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            //write some query text
            string query = "DELETE FROM Employee WHERE firstName = 'Test' AND lastName = 'Passman'";
            //create a Sql Command
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                //this part must be in the try as it is attempting to establish connection.
                //open connection
                conn.Open();
                //execute the command and capture the results to a SqlDataReader
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();
                }
                /*else
                {
                    var up = new ApplicationException("Your record could not be made.");
                    throw up;
                }*/
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

        public static void testBook(Booking testBook)
        {
            //establish connection
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            //write some query text
            string query = "DELETE FROM Booking WHERE GuestID = 100 AND EmployeeID = 100 AND ItemListID = 100 AND TicketPrice= 1234";
            //create a Sql Command
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                //this part must be in the try as it is attempting to establish connection.
                //open connection
                conn.Open();
                //execute the command and capture the results to a SqlDataReader
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();
                }
                /*else
                {
                    var up = new ApplicationException("Your record could not be made.");
                    throw up;
                }*/
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
        /// Created: Tony Noel 2015/04/24
        /// Uses the stored procedure listed to locate an invoice where the guestID matches the fake
        /// hotel guestID and then removes it from the database.
        /// </summary>
        public static void ClearOutInvoice()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "testSPClearHotelGuest";
            var cmd = new SqlCommand(cmdText, conn);

            //Set command type to stored procedure and add parameters
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                Console.Write("Fail!");
            }
            finally 
            { 
                conn.Close(); 
            }
        }
        /// <summary>
        /// Created: Tony Noel- 2015/04/24, Deletes the dummy Hotel Guest record from the database.
        /// </summary>
        public static void DeleteHotelGuest()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string commandText3 = @"DELETE FROM [dbo].[HotelGuest] WHERE FirstName = 'Fake' AND Address1 = '1111 Fake St.' AND PhoneNumber = '5556667777'";

            var cmd3 = new SqlCommand(commandText3, conn);

            try
            {
                conn.Open();
                cmd3.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                Console.Write("Fail!");
            }
            finally
            {
                conn.Close();
            }
        }
        public static int GetBooking()
        {
            int result;

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string query = "Select BookingID FROM Booking WHERE GuestID = 100 AND EmployeeID = 100 AND ItemListID = 100 AND TicketPrice= 1234";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    result = reader.GetInt32(0);
                }
                else
                {
                    var ex = new ApplicationException("Requested object did not match any records.");
                    throw ex;
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

            return result;
        }
        public static List<Booking> GetAllBookings()
        {
            var result = new List<Booking>();

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string query = "Select BookingID, GuestID, EmployeeID, Booking.ItemlistID, Quantity, DateBooked, Discount, Booking.Active, TicketPrice, ExtendedPrice, TotalCharge FROM Booking, ItemListing Where Booking.ItemListID = ItemListing.ItemListID AND ItemListing.Active = 1";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var myBook = new Booking();
                        myBook.BookingID = reader.GetInt32(0);
                        myBook.GuestID = reader.GetInt32(1);
                        myBook.EmployeeID = reader.GetInt32(2);
                        myBook.ItemListID = reader.GetInt32(3);
                        myBook.Quantity = reader.GetInt32(4);
                        myBook.DateBooked = reader.GetDateTime(5);
                        myBook.Discount = reader.GetDecimal(6);
                        myBook.Active = reader.GetBoolean(7);
                        myBook.TicketPrice = reader.GetDecimal(8);
                        myBook.ExtendedPrice = reader.GetDecimal(9);
                        myBook.TotalCharge = reader.GetDecimal(10);
                        result.Add(myBook);
                    }
                }
                else
                {
                    var ex = new ApplicationException("Requested object did not match any records.");
                    throw ex;
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

            return result;
        }

        public static int GetHotelGuest()
        {
            int result;

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string query = "Select HotelGuestID FROM HotelGuest WHERE FirstName= 'Fake' AND LastName = 'Person' AND Address1 = '1111 Fake St.' AND EmailAddress='fake@gmail.com' ";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    result = reader.GetInt32(0);
                }
                else
                {
                    var ex = new ApplicationException("Requested object did not match any records.");
                    throw ex;
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

            return result;
        }

        public static int resetItemListing100()
        {
            int rowsAffected;
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "UPDATE ItemListing SET CurrentNumberOfGuests = 30 WHERE ItemListID = 100";
            var cmd = new SqlCommand(cmdText, conn);
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

        public static void deleteTestApplication()
        {
            //establish connection
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            //write some query text
            string query = "DELETE FROM SupplierApplication WHERE CompanyName = 'Awsome Tours'";
            //create a Sql Command
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                //this part must be in the try as it is attempting to establish connection.
                //open connection
                conn.Open();
                //execute the command and capture the results to a SqlDataReader
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();
                }
                /*else
                {
                    var up = new ApplicationException("Your record could not be made.");
                    throw up;
                }*/
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

        public static int DeleteTestSupplier(Supplier supplierToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteTestSupplier";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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