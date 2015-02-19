using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.DataAccess
{
    public class HotelGuestAccessor
    {
        /// <summary>
        /// Creates a new Hotel Guest in the database
        /// </summary>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        public static int HotelGuestAdd(NewHotelGuest newHotelGuest)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spHotelGuestAdd";
            var cmd = new SqlCommand(cmdText, conn);
            var numRows = 0;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstName", newHotelGuest.FirstName);
            cmd.Parameters.AddWithValue("@lastName", newHotelGuest.LastName);
            cmd.Parameters.AddWithValue("@zip", newHotelGuest.CityState.Zip);
            cmd.Parameters.AddWithValue("@address1", newHotelGuest.Address1);
            cmd.Parameters.AddWithValue("@address2", newHotelGuest.Address2);
            cmd.Parameters.AddWithValue("@phoneNumber", newHotelGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@email", newHotelGuest.EmailAddress);

            try
            {
                conn.Open();
                numRows = cmd.ExecuteNonQuery();

                if (numRows == 0)
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

            return numRows;
        }

        /// <summary>
        /// Get a list of all HotelGuest Objects
        /// </summary>
        /// <param name="hotelGuestID">Optional Parameter to specify a hotel gust to look up</param>
        /// <returns></returns>
        public static List<HotelGuest> HotelGuestGet(int? hotelGuestID = null)
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spHotelGuestGet";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@hotelGuestID", hotelGuestID);

            List<HotelGuest> list = new List<HotelGuest>();
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new HotelGuest(
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
                                !reader.IsDBNull(9) ? reader.GetString(9) : null //EmailAdddress
                            )
                        );
                    }
                }
                else
                {
                    throw new ApplicationException(hotelGuestID == null ? "Could not find any Hotel Guests" : "Hotel Guest ID number did not match any records.");
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
            return list;
        }

        /********************  Methods not used in Sprint 1 ************************************************/

        /// <summary>
        /// Updates a hotel guest with new information
        /// </summary>
        /// <param name="oldHotelGuest">Object containing original information about a hotel guest</param>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        public static int HotelGuestUpdate(HotelGuest oldHotelGuest, NewHotelGuest newHotelGuest)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spHotelGuestUpdate";
            var cmd = new SqlCommand(cmdText, conn);
            var numRows = 0;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstName", newHotelGuest.FirstName);
            cmd.Parameters.AddWithValue("@lastName", newHotelGuest.LastName);
            cmd.Parameters.AddWithValue("@zip", newHotelGuest.CityState.Zip);
            cmd.Parameters.AddWithValue("@address1", newHotelGuest.Address1);
            cmd.Parameters.AddWithValue("@address2", newHotelGuest.Address2);
            cmd.Parameters.AddWithValue("@phoneNumber", newHotelGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@email", newHotelGuest.EmailAddress);

            cmd.Parameters.AddWithValue("@original_hotelGuestID", oldHotelGuest.HotelGuestID);
            cmd.Parameters.AddWithValue("@original_firstName", oldHotelGuest.FirstName);
            cmd.Parameters.AddWithValue("@original_lastName", oldHotelGuest.LastName);
            cmd.Parameters.AddWithValue("@original_zip", oldHotelGuest.CityState.Zip);
            cmd.Parameters.AddWithValue("@original_address1", oldHotelGuest.Address1);
            cmd.Parameters.AddWithValue("@original_address2", oldHotelGuest.Address2);
            cmd.Parameters.AddWithValue("@original_phoneNumber", oldHotelGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@original_email", oldHotelGuest.EmailAddress);

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




    }
}