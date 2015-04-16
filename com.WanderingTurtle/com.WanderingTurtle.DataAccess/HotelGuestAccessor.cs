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
        /// Miguel Santana
        /// Created: 2015/02/12
        /// 
        /// Creates a new Hotel Guest in the database
        /// </summary>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        /// <remarks>
        /// Pat Banks 
        /// Updated: 2015/02/27
        /// 
        /// Stored Procedure updated to create an invoice record automatically when adding a hotel guest
        /// Updated Rose Steffensmeier 2015/03/12
        /// Updated by Pat Banks 2015/04/03
        /// Added GuestPIN field
        /// Rose Steffensmeier 
        /// Updated: 2015/03/12

        /// </remarks>
        public static int HotelGuestAdd(HotelGuest newHotelGuest)
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
            cmd.Parameters.AddWithValue("@room", newHotelGuest.Room);
            cmd.Parameters.AddWithValue("@guestPIN", newHotelGuest.GuestPIN);

            try
            {
                conn.Open();
                numRows = cmd.ExecuteNonQuery();
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

            return numRows;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/12
        /// 
        /// Get a list of all HotelGuest Objects
        /// </summary>
        ///<remarks>
        /// Rose Steffensmeier 
        /// Updated: 2015/03/12
        /// </remarks>
        /// <param name="hotelGuestID">Optional Parameter to specify a hotel guest to look up</param>
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
                                !reader.IsDBNull(9) ? reader.GetString(9) : null, //EmailAdddress
                                !reader.IsDBNull(10) ? reader.GetString(10) : null, //Room
                                !reader.IsDBNull(11) ? reader.GetString(11) : null, // PIN
                                reader.GetBoolean(12) //Active
                            )
                        );
                    }
                }
                else
                {
                    throw new ApplicationException(hotelGuestID == null ? "Could not find any Hotel Guests" : "Hotel Guest ID number did not match any records.");
                }
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
            return list;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/12
        /// 
        /// Updates a hotel guest with new information
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier 
        /// Updated: 02/23/2015
        /// </remarks>
        /// <param name="oldHotelGuest">Object containing original information about a hotel guest</param>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        public static int HotelGuestUpdate(HotelGuest oldHotelGuest, HotelGuest newHotelGuest)
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
            cmd.Parameters.AddWithValue("@room", newHotelGuest.Room);
            cmd.Parameters.AddWithValue("@active", newHotelGuest.Active);
            cmd.Parameters.AddWithValue("@guestpin", newHotelGuest.GuestPIN);

            cmd.Parameters.AddWithValue("@original_hotelGuestID", oldHotelGuest.HotelGuestID);
            cmd.Parameters.AddWithValue("@original_firstName", oldHotelGuest.FirstName);
            cmd.Parameters.AddWithValue("@original_lastName", oldHotelGuest.LastName);
            cmd.Parameters.AddWithValue("@original_zip", oldHotelGuest.CityState.Zip);
            cmd.Parameters.AddWithValue("@original_address1", oldHotelGuest.Address1);
            cmd.Parameters.AddWithValue("@original_address2", oldHotelGuest.Address2);
            cmd.Parameters.AddWithValue("@original_phoneNumber", oldHotelGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@original_email", oldHotelGuest.EmailAddress);
            cmd.Parameters.AddWithValue("@original_room", oldHotelGuest.Room);
            cmd.Parameters.AddWithValue("@original_active", oldHotelGuest.Active);
            cmd.Parameters.AddWithValue("@original_guestpin", oldHotelGuest.GuestPIN);

            try
            {
                conn.Open();
                numRows = cmd.ExecuteNonQuery();

                if (numRows == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
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

            return numRows;
        }

        /// <summary>
        /// Rose Steffensmeier
        /// Created: 2015/02/26
        /// 
        /// Archives a hotel guest
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier 
        /// Updated: 2015/03/12
        /// </remarks>
        /// <param name="oldHotelGuestID"></param>
        /// <param name="newHotelGuestID"></param>
        /// <param name="oldActive"></param>
        /// <param name="newActive"></param>
        /// <exception cref="ApplicationException">the hotel guest doesn't exist or couldn't be found</exception>
        /// <exception cref="SqlException">the connection didn't happen or there is something wrong with the stored procedure</exception>
        /// <exception cref="Exception">an exception that wasn't expected happens</exception>
        /// <returns>rows edited</returns>
        public static int HotelGuestArchive(HotelGuest oldGuest, bool newActive)
        {
            int rowsAffected = 0;

            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spHotelGuestArchive";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@active", newActive);
            cmd.Parameters.AddWithValue("@original_hotelGuestID", oldGuest.HotelGuestID);
            cmd.Parameters.AddWithValue("@original_firstName", oldGuest.FirstName);
            cmd.Parameters.AddWithValue("@original_lastName", oldGuest.LastName);
            cmd.Parameters.AddWithValue("@original_zip", oldGuest.CityState.Zip);
            cmd.Parameters.AddWithValue("@original_address1", oldGuest.Address1);
            cmd.Parameters.AddWithValue("@original_address2", oldGuest.Address2);
            cmd.Parameters.AddWithValue("@original_phoneNumber", oldGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@original_emailAddress", oldGuest.EmailAddress);
            cmd.Parameters.AddWithValue("@original_room", oldGuest.Room);
            cmd.Parameters.AddWithValue("@original_active", oldGuest.Active);
            cmd.Parameters.AddWithValue("@original_guestpin", oldGuest.GuestPIN);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
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
            return rowsAffected;
        }
    }
}