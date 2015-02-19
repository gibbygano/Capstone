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
        public CityState(string zip, string city, string state)
        {
            this.Zip = zip;
            this.City = city;
            this.State = state;
        }

        public string City { get; set; }

        public string GetZipStateCity { get { return String.Format("{1}{0}{2}{0}{3}", " - ", this.Zip, this.State, this.City); } }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}