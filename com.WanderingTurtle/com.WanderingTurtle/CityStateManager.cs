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
        /// Miguel Santana 
        /// Created: 2015/02/18
        /// 
        /// Get a list of all CityState records
        /// </summary>
        /// <returns>Return List of CityState Objects for the cache</returns>
        public void PopulateCityStateCache()
        {
            //need to check if it has already been populated first
            if (DataCache._currentCityStateList == null)
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
}