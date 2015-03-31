using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.BusinessLogic
{
    /// <summary>
    /// Matt Lapka
    /// Created on: 2015/03/26
    /// Holds a cache of data returned from the database that can be returned instead
    /// of a new database query if certain business logic is met (time)
    /// </summary>
    public class DataCache
    {
        /******* EVENT ********/
        public static List<Event> _currentEventList { get; set; }
        public static DateTime _EventListTime { get; set; }

        /******* EVENT TYPE ********/
        public static List<EventType> _currentEventTypeList { get; set; }
        public static DateTime _EventTypeListTime { get; set; }

        /******* ITEM LISTING ********/
        public static List<ItemListing> _currentItemListingList { get; set; }
        public static DateTime _ItemListingListTime { get; set; }

        /******* SUPPLIER ********/
        public static List<Supplier> _currentSupplierList { get; set; }
        public static DateTime _SupplierListTime { get; set; }

        /******* BOOKING DETAILS ********/
        public static List<BookingDetails> _currentBookingDetailsList { get; set; }
        public static DateTime _BookingDetailsListTime { get; set; }

        /******* ItemListingDETAILS ********/
        public static List<ItemListingDetails> _currentItemListingDetailsList { get; set; }
        public static DateTime _ItemListingDetailsListTime { get; set; }

    }
}
