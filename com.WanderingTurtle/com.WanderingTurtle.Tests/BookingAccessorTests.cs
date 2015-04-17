using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{   ///Created By: Tony Noel- 2015/4/10
    /// <summary>
    /// Booking Accessor Tests- Creates a fake booking record by using a dummy ItemListing from the database 
    /// (ItemListing record 100). Performs actions on this booking based upon Accessor methods.
    /// </summary>
    [TestClass]
    public class BookingAccessorTests
    {
        int BookingID;
        int guestID = 100;
        int empID = 100;
        int itemID = 100;
        int bQuantity = 2;
        DateTime dateBooked = DateTime.Now;
        decimal ticket = 1234m;
        decimal extended = 40m;
        decimal discount = .1m;
        decimal total = 36m;
        Booking booking;

        [TestInitialize]
        public void BookingTestSetup()
        {   //Sets up the booking record and calls for the record to be added
            TestBookingConstructor();
            TestAddBookingAccess();
        }
        [TestMethod]
        public void TestBookingConstructor()
        {
            //booking object created
            booking = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);
            Assert.IsNotNull(guestID);
        }
        [TestMethod]
        public void TestAddBookingAccess()
        {
            //Added to database and checked to see if an int row is returned, then compared to expected result
            int result = BookingAccessor.addBooking(booking);
            int expected = 1;
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestGetEventListingAccess()
        { //A test to retrieve a single listing by ID from the ItemListing table.
            ItemListingDetails details = BookingAccessor.getEventListing(itemID);
            int expected = 1234;
            Assert.AreEqual(expected, details.Price);

        }
        [TestMethod]
        public void TestGetListItemsAccess()
        { //A test to retrieve a listing of ItemListingDetails, checks to ensure that a list of ItemListingDetails
            //is being returned and that the count is greater than 1 record.
            List<ItemListingDetails> details = BookingAccessor.getListItems();
            bool worked = false;
            if (details.Count > 1)
            {
                worked = true;
            }
            Assert.IsTrue(worked);
        
        }
        [TestMethod]
        public void TestGetBookingbyIDAccess()
        {   // Retrieves a booking from the database by ID, first captures the dummy booking from database
            //using a TestAccessor, then uses a real accessor method to be tested.
            BookingID = TestCleanupAccessor.GetBooking();
            Booking booking2 = BookingAccessor.getBooking(BookingID);
            decimal expected = 1234;
            Assert.AreEqual(expected, booking2.TicketPrice);
            
        }
        [TestMethod]
        public void TestUpdateBookingAccess()
        {   // Updates the dummy booking in the database, first captures the dummy bookingID from database
            //using a TestAccessor
            BookingID = TestCleanupAccessor.GetBooking();
            //Assigns one booking object to be the old record and one to be the new record
            Booking old = BookingAccessor.getBooking(BookingID);
            Booking newB = new Booking(guestID, empID, itemID, 3, dateBooked, ticket, extended, discount, total);
            //Updates the old with the new quantity
            int rows = BookingAccessor.updateBooking(old, newB);
            int expected = 3;
            //Grabs the record to test and see if the update went through
            Booking toCheck = BookingAccessor.getBooking(BookingID);
            Assert.AreEqual(expected, toCheck.Quantity);

        }
        [TestCleanup]
        public void CleanupTest()
        {
            TestCleanupAccessor.resetItemListing100();
            TestCleanupAccessor.testBook(booking);

        }
    }
}
