using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;


namespace BookingTests
{
    [TestClass]
    public class BookingLogicTests
    {
        int guestID = 1;
        int empID = 100;
        int itemID = 100;
        int bQuantity = 100;
        DateTime dateBooked = DateTime.Now;
        decimal ticket = 20m;
        decimal extended = 40m;
        decimal discount = .1m;
        decimal total;
        Booking booking;
        BookingDetails bookingDetails;

        [TestInitialize]
        public void BookingTestSetup()
        {
            bookingDetails = new BookingDetails();

        }

        [TestMethod]
        public void TestBookingConstructor()
        {
            booking = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);
            
        }

        [TestMethod]
        public void TestCancellationFee(BookingDetails testBooking)
        {

        }


    }
}
