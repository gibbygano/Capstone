using System;
using System.Data.SqlClient;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.DataAccess
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
            string query = "DELETE FROM Booking WHERE GuestID = 0 AND EmployeeID = 100 AND ItemListID = 100 AND TicketPrice= 1234";
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

    }
}
