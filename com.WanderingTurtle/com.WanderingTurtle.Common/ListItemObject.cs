using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    public class ListItemObject : Booking
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int QuantityOffered { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ListItemObject()
        {

        }

        public ListItemObject(int itemListID, int eventID, string eventName, string eventDescription, int qOffered, DateTime start, DateTime end)
        {
            ItemListID = itemListID;
            EventID = eventID;
            EventName = eventName;
            EventDescription = eventDescription;
            QuantityOffered = qOffered;
            StartDate = start;
            EndDate = end;
        }
    }
}
