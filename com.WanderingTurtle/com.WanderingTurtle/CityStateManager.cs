using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.BusinessLogic
{
    public class CityStateManager
    {
        /// <summary>
        /// Get a list of all CityState records
        /// </summary>
        /// <returns>Return List of CityState Objects</returns>
        /// Miguel Santana 2/18/2015
        public List<CityState> GetCityStateList()
        {
            try
            {
                return CityStateAccessor.CityStateGetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a CityState record by zip
        /// </summary>
        /// <param name="zip">Specificed Zip to look up</param>
        /// <returns>Return CityState Object</returns>
        /// Miguel Santana 2/18/2015
        public CityState GetCityState(String zip)
        {
            try
            {
                List<CityState> list = CityStateAccessor.CityStateGetList();
                return (list.Count == 1) ? list.ElementAt(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}