using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle
{
    public class HotelGuestManager
    {
        /// <summary>
        /// Creates a new Hotel Guest in the database
        /// </summary>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        public int AddHotelGuest(NewHotelGuest newHotelGuest)
        {
            try
            {
                return HotelGuestAccessor.HotelGuestAdd(newHotelGuest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a hotel guest by id
        /// </summary>
        /// <param name="hotelGuestID">the id of a hotel guest to retrieve</param>
        /// <returns>HotelGuest object retrieved from database</returns>
        public HotelGuest GetHotelGuest(int hotelGuestID)
        {
            try
            {
                return HotelGuestAccessor.HotelGuestGet(hotelGuestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of all Hotel Guests
        /// </summary>
        /// <returns>List of HotelGuest Objects</returns>
        public List<HotelGuest> GetHotelGuestList()
        {
            try
            {
                return HotelGuestAccessor.HotelGuestGetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates a hotel guest with new informatino
        /// </summary>
        /// <param name="oldHotelGuest">Object containing original information about a hotel guest</param>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateHotelGuest(HotelGuest oldHotelGuest, NewHotelGuest newHotelGuest)
        {
            try
            {
                return HotelGuestAccessor.HotelGuestUpdate(oldHotelGuest, newHotelGuest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}