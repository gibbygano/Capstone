using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// City State object populated with data from the CityState lookup table
    /// 
    /// Created by Matt Lapka - 2/8/15
    /// </summary>
    public class CityState
    {
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public CityState()
        {
            //default constructor
        }

        public CityState(string zip, string city, string state)
        {
            Zip = zip;
            City = city;
            State = state;

        }
    }
}
