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
            cmd.Parameters.AddWithValue("@zip", newHotelGuest.Zip);
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
        /// Gets a hotel guest by id
        /// </summary>
        /// <param name="hotelGuestID">the id of a hotel guest to retrieve</param>
        /// <returns>HotelGuest object retrieved from database</returns>
        public static HotelGuest HotelGuestGet(int hotelGuestID)
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spHotelGuestGet";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@hotelGuestID", hotelGuestID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return new HotelGuest(
                        reader.GetInt32(0), //HotelGuestID
                        reader.GetString(1), //FirstName
                        reader.GetString(2), //LastName
                        reader.GetString(4), //Address1
                        !reader.IsDBNull(5) ? reader.GetString(5) : null, //Address2
                        reader.GetString(3), //Zip
                        !reader.IsDBNull(6) ? reader.GetString(6) : null, //PhoneNumber
                        !reader.IsDBNull(7) ? reader.GetString(7) : null //EmailAdddress
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

        /// <summary>
        /// Gets a list of all Hotel Guests
        /// </summary>
        /// <returns>List of HotelGuest Objects</returns>
        public static List<HotelGuest> HotelGuestGet()
        {
            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();

            var cmdText = "spHotelGuestGetList";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
                                reader.GetString(4), //Address1
                                !reader.IsDBNull(5) ? reader.GetString(5) : null, //Address2
                                reader.GetString(3), //Zip
                                !reader.IsDBNull(6) ? reader.GetString(6) : null, //PhoneNumber
                                !reader.IsDBNull(7) ? reader.GetString(7) : null //EmailAdddress
                            )
                        );
                    }
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
            return list;
        }

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
            cmd.Parameters.AddWithValue("@zip", newHotelGuest.Zip);
            cmd.Parameters.AddWithValue("@address1", newHotelGuest.Address1);
            cmd.Parameters.AddWithValue("@address2", newHotelGuest.Address2);
            cmd.Parameters.AddWithValue("@phoneNumber", newHotelGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@email", newHotelGuest.EmailAddress);

            cmd.Parameters.AddWithValue("@original_hotelGuestID", oldHotelGuest.HotelGuestID);
            cmd.Parameters.AddWithValue("@original_firstName", oldHotelGuest.FirstName);
            cmd.Parameters.AddWithValue("@original_lastName", oldHotelGuest.LastName);
            cmd.Parameters.AddWithValue("@original_zip", oldHotelGuest.Zip);
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