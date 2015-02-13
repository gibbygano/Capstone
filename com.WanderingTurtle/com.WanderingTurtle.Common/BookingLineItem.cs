using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    public class BookingLineItem : Booking
    {
        public int ItemListID { get; set; }
        public int Quantity { get; set; }
    }
}
