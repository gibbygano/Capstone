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
        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/27
        /// 
        /// Helper class to cleanup or select records from the database for test use only.
        /// </summary>
        /// <remarks>
        /// Updated: 2015/04/10
        /// Updated: 2015/05/01
        /// Updated: 2015/05/05
        /// 
        /// Added new method to get fake EmpID
        /// </remarks>
        /// <param name="testEmp">The Employee object to be tested with</param>

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

        /// <summary>
        /// Tony Noel
        /// Created: 2015/05/05
        /// 
        /// A method that will only grab the specified fake employee record from the database. Returns the Employee ID.
        /// </summary>
        /// <returns>The EmployeeID of the fake Employee</returns>
        public static int getTestEmp()
        {
            int result;
            //establish connection
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            //write some query text
            string query = "Select employeeID FROM Employee WHERE firstName = 'Test' AND lastName = 'Passman'";
            //create a Sql Command
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
        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/27
        /// 
        /// Deletes only the dummy booking record from the database for testing.
        /// </summary>
        /// <param name="testBook">The Booking Object used for testing</param>
        public static void testBook(Booking testBook)
        {
            //establish connection
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            //write some query text
            string query = "DELETE FROM Booking WHERE GuestID = 100 AND EmployeeID = 100 AND ItemListID = 100 AND TotalCharge= 36";
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
        /// Tony Noel 
        /// Created: 2015/04/24
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
        /// Tony Noel 
        /// Created: 2015/04/24
        /// 
        /// Deletes the dummy Hotel Guest record from the database.
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
        /// <summary>
        /// Tony Noel
        /// Created: 2015/04/07
        /// 
        /// Grabs the specific dummy record from the database
        /// </summary>
        /// <returns>The ID of the fake Booking added</returns>
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
        /// <summary>
        /// Tony Noel
        /// Created: 2015/05/01
        /// 
        /// Used to grab a list of all bookings where the ItemListID links to a active ItemListing only.
        /// </summary>
        /// <returns>A List object of Booking objects</returns>
        public static List<Booking> GetAllBookings()
        {
            var result = new List<Booking>();

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string query = "Select BookingID, GuestID, EmployeeID, Booking.ItemlistID, Quantity, DateBooked, Discount, Booking.Active, TicketPrice, ExtendedPrice, TotalCharge FROM Booking, ItemListing Where Booking.ItemListID = ItemListing.ItemListID AND ItemListing.Active = 1 AND Booking.Quantity != 0";
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
        /// <summary>
        /// Tony Noel
        /// Created: 2015/04/27
        /// 
        /// Grabs the specific fake hotel guest record added in the test method.
        /// </summary>
        /// <returns>The ID of the fake HotelGuest</returns>
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
        /// <summary>
        /// Tony Noel
        /// Created: 2015/04/07
        /// 
        ///  Resets the dummy record back to 30. Aides in Booking Test methods.
        /// </summary>
        /// <returns>The number of rows affected</returns>
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

        /// <summary>
        /// Hunter Lind
        /// Created: 2015/04/16
        /// 
        /// Deletes Testing data for SupplierApplication
        /// </summary>
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
        /// <summary>
        /// Hunter Lind
        /// Created: 2015/04/16
        /// 
        /// Deletes Test Supplier Data
        /// </summary>
        /// <param name="supplierToDelete">The fake Supplier object to be deleted from the database</param>
        /// <returns>An int reflecting number of rows affected</returns>
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
        /// <summary>
        /// Bryan Hurst
        /// Created: 2015/05/01
        /// 
        /// Deletes the test SupplierLogin from the database
        /// </summary>
        /// <param name="supplierLoginToDelete">The test SupplierLogin object to be deleted</param>
        /// <returns>An int reflecting the number of rows affected</returns>
        public static int DeleteTestSupplierLogin(SupplierLogin supplierLoginToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteTestSupplierLogin";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", supplierLoginToDelete.UserName);

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

        /// <summary>
        /// Bryan Hurst
        /// Created: 2015/05/01
        /// 
        /// Selectes the test archived SupplierLogin
        /// </summary>
        /// <param name="userPassword">The password of the test data</param>
        /// <param name="userName">The username of the test data</param>
        /// <returns>The test SupplierLogin object</returns>
        static public SupplierLogin RetrieveArchivedSupplierLoginTest(string userPassword, string userName)
        {
            SupplierLogin getSupplierInfo = new SupplierLogin();
            var conn = DatabaseConnection.GetDatabaseConnection();
            string query = "spSelectArchivedSupplierLoginTest";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userPassword", userPassword);
            cmd.Parameters.AddWithValue("@userName", userName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();

                    getSupplierInfo.UserID = (int)reader.GetValue(0);
                    getSupplierInfo.UserPassword = reader.GetValue(1).ToString();
                    getSupplierInfo.UserName = reader.GetValue(2).ToString();
                    getSupplierInfo.SupplierID = reader.GetValue(3).ToString();
                    getSupplierInfo.Active = reader.GetBoolean(4);
                }
                else
                    throw new ApplicationException("Incorrect login information. Try again.");

                return getSupplierInfo;
            }
            catch (SqlException)
            {
                throw;
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
        /// Bryan Hurst
        /// Created: 2015/05/02
        /// 
        /// Deletes the test EventType object from the database
        /// </summary>
        /// <param name="TestEventType">The test EventType object added to the database previously</param>
        /// <returns>An int reflecting the number of rows affected</returns>
        public static int DeleteEventTypeTest(EventType TestEventType)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spDeleteTestEventType";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventName", TestEventType.EventName);

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