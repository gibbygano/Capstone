using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace com.WanderingTurtle.BusinessLogic
{
	public class HotelGuestManager
	{
		/// <summary>
		/// Creates a new Hotel Guest in the database
		/// </summary>
		/// <param name="newHotelGuest">Object containing new hotel guest information</param>
		/// <returns>Number of rows effected</returns>
        /// <remarks>
        /// Miguel Santana 2/18/2015
        /// Updated Rose Steffensmeier
        /// </remarks>
		/// 
		public bool AddHotelGuest(HotelGuest newHotelGuest)
		{
            try
            {
                return HotelGuestAccessor.HotelGuestAdd(newHotelGuest) > 0;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
		}

		/// <summary>
		/// Gets a hotel guest by id
		/// </summary>
		/// <param name="hotelGuestId">the id of a hotel guest to retrieve</param>
		/// <returns>HotelGuest object retrieved from database</returns>
		/// Miguel Santana 2/18/2015
		public HotelGuest GetHotelGuest(int hotelGuestId)
		{
            try
            {
                List<HotelGuest> list = HotelGuestAccessor.HotelGuestGet(hotelGuestId);
                return (list.Count == 1) ? list.ElementAt(0) : null;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
		}

		/// <summary>
		/// Gets a list of all Hotel Guests
		/// </summary>
		/// <returns>List of HotelGuest Objects</returns>
		/// Miguel Santana 2/18/2015
		public List<HotelGuest> GetHotelGuestList()
		{
            try
            {
                return HotelGuestAccessor.HotelGuestGet();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
		}

		/// <summary>
		/// Updates a hotel guest with new informatino
		/// </summary>
		/// <param name="oldHotelGuest">Object containing original information about a hotel guest</param>
		/// <param name="newHotelGuest">Object containing new hotel guest information</param>
		/// <returns>Number of rows effected</returns>
		/// Miguel Santana 2/18/2015
		public bool UpdateHotelGuest(HotelGuest oldHotelGuest, HotelGuest newHotelGuest)
		{
            try
            {
                return HotelGuestAccessor.HotelGuestUpdate(oldHotelGuest, newHotelGuest) > 0;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
		}

		/// <summary>
		/// Archives a hotel guest
		/// Created By Rose Steffensmeier 2013/02/26
		/// </summary>
		/// <param name="oldGuest"></param>
		/// <param name="newActive"></param>
		/// <exception cref="Exception">an exception was hit in the HotelGuestAccessor or HotelGuestAccessor can't be found</exception>
		/// <returns>true if rows were affected, false if not</returns>
		public bool ArchiveHotelGuest(HotelGuest oldGuest, bool newActive)
		{
            try
            {
                return HotelGuestAccessor.HotelGuestArchive(oldGuest, newActive) > 0;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
		}
	}
}