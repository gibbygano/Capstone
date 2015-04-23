using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.BusinessLogic
{
    public class HotelGuestManager
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
        /// Rose Steffensmeier
        /// Updated: 2015/03/10
        /// Rose Steffensmeier
        /// Updated: 2015/03/12
        ///
        /// Updated 2015/04/13 by Tony Noel -Updated to comply with the ResultsEdit class of error codes.
        ///
        /// Updated try/catch blocks
        /// </remarks>
        public ResultsEdit AddHotelGuest(HotelGuest newHotelGuest)
        {
            try
            {
                bool worked = HotelGuestAccessor.HotelGuestAdd(newHotelGuest) > 0;
                if (worked == true)
                {
                    return ResultsEdit.Success;
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
            return ResultsEdit.DatabaseError;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/12
        ///
        /// Gets a hotel guest by id
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/03/12
        ///
        /// Updated try/catch blocks
        /// </remarks>
        /// <param name="hotelGuestId">the id of a hotel guest to retrieve</param>
        /// <returns>HotelGuest object retrieved from database</returns>
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

        private string RandomString(int Size)
        {
            Random rg = new Random();
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 5; i++)
            {
                ch = input[rg.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }


        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/12
        ///
        /// Updates a hotel guest with new information
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/03/12
        ///
        /// Updated 2015/04/13 by Tony Noel -Updated to comply with the ResultsEdit class of error codes.
        ///
        /// Updated try/catch blocks
        /// </remarks>
        /// <param name="oldHotelGuest">Object containing original information about a hotel guest</param>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
        public ResultsEdit UpdateHotelGuest(HotelGuest oldHotelGuest, HotelGuest newHotelGuest)
        {
            try
            {
                bool worked = HotelGuestAccessor.HotelGuestUpdate(oldHotelGuest, newHotelGuest) > 0;
                if (worked == true)
                {
                    return ResultsEdit.Success;
                }
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
            return ResultsEdit.DatabaseError;
        }

        /// <summary>
        /// Rose Steffensmeier
        /// Created: 2013/02/26
        ///
        /// Archives a hotel guest
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/03/12
        ///
        /// Updated try/catch blocks
        /// </remarks>
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