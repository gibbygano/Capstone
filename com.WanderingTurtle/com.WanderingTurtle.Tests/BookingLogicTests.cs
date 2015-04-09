using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.DataAccess;
using System.Collections.Generic;


namespace BookingTests
{
    [TestClass]
    public class BookingLogicTests
    {
        //
        int BookingID;
        int guestID = 0;
        int empID = 100;
        int itemID = 100;
        int bQuantity = 2;
        DateTime dateBooked = DateTime.Now;
        decimal ticket = 1234m;
        decimal extended = 40m;
        decimal discount = .1m;
        decimal total = 36m;
        Booking booking;
        BookingDetails bookingDetails;
        BookingManager myBook = new BookingManager();

        [TestInitialize]
        public void BookingTestSetup()
        {
            TestBookingConstructor();
        }
        [TestMethod]
        public void TestBookingConstructor()
        {
            booking = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);
            Assert.IsNotNull(guestID);
        }
        [TestMethod]
        public void TestAddBookingResult()
        {
            ResultsEdit result = myBook.AddBookingResult(booking);
            ResultsEdit expected = ResultsEdit.Success;
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestEditBooking()
        {
            Booking booking1 = myBook.RetrieveBooking(100);
            int result2 = myBook.EditBooking(booking1);
            int expected = 1;
            Assert.AreEqual(expected, result2);
        }
        [TestMethod]
        public void TestRetrieveActiveItemListings()
        {
            List<ItemListingDetails> activeEventListings = myBook.RetrieveActiveItemListings();
            Assert.IsNotNull(activeEventListings);
        }

        [TestMethod]
        public void TestRetrieveEventListing()
        {
            ItemListingDetails detail = myBook.RetrieveEventListing(itemID);
            Assert.IsNotNull(detail);
        }
        [TestMethod]
        public void TestRetrieveBooking()
        {
            Booking booking = myBook.RetrieveBooking(100);
            Assert.IsNotNull(booking);
        }
        [TestMethod]
        public void TestCalcTotalCharge()
        {
            decimal result = myBook.calcTotalCharge(discount, extended);
            decimal expected = 36m;
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestAvailableQuantity()
        {
            int max = 10;
            int current = 5;
            int quantity = myBook.availableQuantity(max, current);
            int expected = 5;
            Assert.AreEqual(expected, quantity);
        }
        [TestMethod]
        public void TestSpotsReserved()
        {
            int newQuantity = 6;
            int oldQuantity = 4;
            int quantity = myBook.spotsReservedDifference(newQuantity, oldQuantity);
            int expected = 2;
            Assert.AreEqual(expected, quantity);
        }
        [TestMethod]
        public void updateNumberofGuests()
        {
            int rows = myBook.updateNumberOfGuests(booking.ItemListID, 32, 30);
            Assert.IsNotNull(rows);
        }
        [TestMethod]
        public void CheckToEditBooking()
        {
            bookingDetails = new BookingDetails();
            bookingDetails.StartDate = DateTime.Now;
            bookingDetails.Quantity = 2;
            ResultsEdit result = myBook.CheckToEditBooking(bookingDetails);
            Assert.IsNotNull(result);
        }
        //[TestMethod]
        //public void TestCancelBookingResults()
        //{
        //    ResultsEdit result = myBook.AddBookingResult(booking);
        //    bookingDetails = new BookingDetails();
        //    int id = TestCleanupAccessor.GetBooking();
        //    Booking booking1 = myBook.RetrieveBooking(id);
        //    bookingDetails = (BookingDetails)booking1;
        //    bookingDetails.Quantity = 0;
        //    Assert.IsNotNull(booking1);
        //}
        [TestCleanup]
        public void CleanupTest()
        {

            TestCleanupAccessor.testBook(booking);

        }
    }
}
