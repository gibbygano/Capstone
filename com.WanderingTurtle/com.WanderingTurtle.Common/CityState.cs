using System;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Matthew Lapka
    /// Created: 2015/02/08
    /// 
    /// City State object populated with data from the CityState lookup table
    /// </summary>
    public class CityState
    {
        public CityState(string zip, string city, string state)
        {
            Zip = zip;
            City = city;
            State = state;
        }

        public string City { get; set; }

        public string GetZipStateCity { get { return String.Format("{1}{0}{2}{0}{3}", " - ", Zip, State, City); } }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}