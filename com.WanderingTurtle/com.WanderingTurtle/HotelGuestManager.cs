﻿using com.WanderingTurtle.Common;
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
        /// Creates a new Hotel Guest in the database
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/03/10
        /// 
        /// Rose Steffensmeier
        /// Updated: 2015/03/12
        /// Updated try/catch blocks
        /// 
        /// Tony Noel 
        /// Updated 2015/04/13 by 
        /// Updated to comply with the ResultsEdit class of error codes.
        /// </remarks>
        /// <param name="newHotelGuest">Object containing new hotel guest information</param>
        /// <returns>Number of rows effected</returns>
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

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/25
        /// Generates a random 6 digit pin for the hotel guest to access website
        /// </summary>
        /// <returns>A string with a randomized value</returns>
        public string GenerateRandomPIN()
        {
            Random rg = new Random();
            string numInput = "0123456789";
            string alphaInput = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 3; i++)
            {
                ch = alphaInput[rg.Next(0, alphaInput.Length)];
                builder.Append(ch);
                ch = numInput[rg.Next(0, numInput.Length)];
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
        /// Updated try/catch blocks
        /// 
        /// Tony Noel
        /// Updated 2015/04/13
        /// Updated to comply with the ResultsEdit class of error codes.
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
    }
}