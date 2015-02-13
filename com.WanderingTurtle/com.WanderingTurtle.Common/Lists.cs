using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Class that creates a Lists object. Signifies the actual listing of an 
    /// event by a specific supplier at a specific time as they can be "relisted"
    /// 
    /// Created by Matt Lapka 2/8/15
    /// </summary>
    public class Lists
    {
        public int SupplierID { get; set; }
        public int ItemListID { get; set; }
        public DateTime DateListed { get; set; }

        public Lists()
        {
            //default constructor
        }

        public Lists(int supplierID, int itemListID, DateTime dateListed)
        {
            SupplierID = supplierID;
            ItemListID = itemListID;
            DateListed = dateListed;
        }
    }
}
