using System;
using System.Collections.Generic;
using System.Linq;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

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
            double cacheExpirationTime = 60; //how long the cache should live (minutes)
            var now = DateTime.Now;
            try
            {
                if (DataCache._currentCityStateList == null)
                {
                    //data hasn't been retrieved yet. get data, set it to the cache and return the result.
                    var list = CityStateAccessor.CityStateGet();
                    DataCache._currentCityStateList = list;
                    DataCache._currentCityStateListTime = now;
                    return list;
                }
                else
                {
                    //check time. If less than 60 min, return cache

                    if (now > DataCache._EventListTime.AddMinutes(cacheExpirationTime))
                    {
                        //get new list from DB
                        var list = CityStateAccessor.CityStateGet();
                        //set cache to new list and update time
                        DataCache._currentCityStateList = list;
                        DataCache._currentCityStateListTime = now;
                        return list;
                    }
                    else
                    {
                        return DataCache._currentCityStateList;
                    }
                }
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
                List<CityState> list = CityStateAccessor.CityStateGet(zip);
                return (list.Count == 1) ? list.ElementAt(0) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}