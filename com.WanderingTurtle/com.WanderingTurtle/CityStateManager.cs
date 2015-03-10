﻿using com.WanderingTurtle.Common;
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
			return CityStateAccessor.CityStateGet();
        }

/********************  Methods not used in Sprint 1 ************************************************/
        /// <summary>
        /// Get a CityState record by zip
        /// </summary>
        /// <param name="zip">Specificed Zip to look up</param>
        /// <returns>Return CityState Object</returns>
        /// Miguel Santana 2/18/2015
        public CityState GetCityState(String zip)
        {
			List<CityState> list = CityStateAccessor.CityStateGet(zip);
            return (list.Count == 1) ? list.ElementAt(0) : null;
        }
    }
}