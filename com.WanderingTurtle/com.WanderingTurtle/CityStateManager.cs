using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    public class CityStateManager
    {
        /// <summary>
        /// Get a list of all CityState records
        /// </summary>
        /// <returns>Return List of CityState Objects for the cache</returns>
        /// Miguel Santana 2/18/2015
        public void PopulateCityStateCache()
        {
            try
            {
                //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                var list = CityStateAccessor.CityStateGet();
                DataCache._currentCityStateList = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}