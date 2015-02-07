using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class HotelGuestAccessor
    {
        public static HotelGuest SelectHotelGuest(int hotelGuestID)
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string query = @"SELECT [HotelGuestID],[FirstName],[LastName],[Zip],[Address1],[Address2],[PhoneNumber],[EmailAddress],[HotelGuestPIN]
                            FROM [dbo].[HotelGuest]
                            WHERE [HotelGuestID] = @hotelGuestID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@hotelGuestID", hotelGuestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command and capture the results to a SqlDataReader
                SqlDataReader reader = cmd.ExecuteReader();

                // catch anything?
                if (reader.HasRows)
                {
                    reader.Read();

                    return new HotelGuest(
                        reader.GetInt32(0), // HotelGuestID
                        reader.GetString(1), //FirstName
                        reader.GetString(2), //LastName
                        reader.GetString(3), //Zip
                        reader.GetString(4), //Address1
                        reader.GetString(5), //Address2
                        reader.GetString(6), //PhoneNumber
                        reader.GetString(7), //EmailAdddress
                        reader.GetInt32(8) //HotelGuestPIN
                   );
                }
                else
                {
                    throw new ApplicationException("Hotel Guest ID number did not match any records.");
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
        }
    }
}