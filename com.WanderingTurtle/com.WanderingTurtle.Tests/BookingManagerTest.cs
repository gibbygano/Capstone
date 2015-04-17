using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Summary description for BookingManagerTest
    /// </summary>
    [TestClass]
    public class BookingManagerTest
    {
        /* RefreshItemDetailsListCacheData is Private so the root result cannot be called to check against RetrieveActiveItemListing()
        [TestMethod]
        public void RetrieveActiveItemListing_Valid()
        {
            BookingManager bkManager = new BookingManager();
            bkManager.RefreshItemDetailsListCacheData();
            var expected = DataCache._currentItemListingDetailsList;

            var actual = bkManager.RetrieveActiveItemListings();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void RetrieveActiveItemListing_Invalid()
        {
            BookingManager bkManager = new BookingManager();
            bkManager.RefreshItemDetailsListCacheData();
        }

         RefreshItemDetailsListCacheData() is private

         */

        [TestMethod]
        public void RetrieveEventListing_Valid()
        {
            BookingManager bkManager = new BookingManager();
            var expected = BookingAccessor.getEventListing(100);

            var actual = bkManager.RetrieveEventListing(100);

            Assert.AreEqual(expected.EventID, actual.EventID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void RetrieveEventListing_NoSuchEvent()
        {
            BookingManager bkManager = new BookingManager();
            var actual = bkManager.RetrieveEventListing(9999);
        }

        private int guestID = 8;
        private int empID = 101;
        private int itemID = 103;
        private int bQuantity = 5;
        private DateTime dateBooked = new DateTime(2015, 3, 28, 8, 30, 0);
        private decimal ticket = 10;
        private decimal extended = 50;
        private decimal discount = 0;
        private decimal total = 50;

        [TestMethod]
        public void AddBookingResult_Valid()
        {
            BookingManager bkManager = new BookingManager();
            Booking bookTest = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);

            var actual = bkManager.AddBookingResult(bookTest);

            var expected = ResultsEdit.Success;

            Assert.AreEqual(expected, actual);
        }

        /* Unsure how to trigger
        [TestMethod]
        public void AddBookingResult_FailedToAdd()
        {
            BookingManager bkManager = new BookingManager();
            Booking bookTest = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);

            var actual = bkManager.AddBookingResult(bookTest);

            var expected = ResultsEdit.DatabaseError;

            Assert.AreEqual(expected, actual);
        }*/

        [TestMethod]
        public void AddBookingResult_NothingToAdd()
        {
            bQuantity = 0;
            BookingManager bkManager = new BookingManager();
            Booking bookTest = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);

            var actual = bkManager.AddBookingResult(bookTest);

            var expected = ResultsEdit.QuantityZero;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EditBooking_Valid()
        {
            BookingManager bkManager = new BookingManager();
            discount = 5;
            Booking newBook = new Booking(guestID, empID, itemID, bQuantity, dateBooked, ticket, extended, discount, total);
            newBook.BookingID = 106;

            int expected = 1;

            int actual = bkManager.EditBooking(newBook);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RetrieveBooking_Valid()
        {
            BookingManager bkManager = new BookingManager();

            Booking expected = BookingAccessor.getBooking(100);

            Booking actual = bkManager.RetrieveBooking(100);

            Assert.AreEqual(expected.BookingID, actual.BookingID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void RetrieveBooking_Invalid()
        {
            BookingManager bkManager = new BookingManager();

            Booking actual = bkManager.RetrieveBooking(9999);
        }

        /*
        [TestMethod] Unsure about syntax
        public void CalculateCancellationFee_Valid()
        {
            BookingManager bkManager = new BookingManager();

            string EventItemName;
            DateTime StartDate;
            int InvoiceID;
        }

        [TestMethod]
        public void CalculateTime_Valid()
        {
        }*/

        [TestMethod]
        public void calcTotalCharge_MathCheck()
        {
            BookingManager bkManager = new BookingManager();

            decimal expected = 50;

            decimal actual = bkManager.calcTotalCharge(discount, extended);

            Assert.AreEqual(expected, actual);
        }

        /*
        Didn't see any max/current quantity in DB
        [TestMethod]
        public void availableQuantity_MathCheck()
        {
            BookingManager bkManager = new BookingManager();
        }*/

        [TestMethod]
        public void spotsReservedDifference_MathCheck()
        {
            BookingManager bkManager = new BookingManager();

            int expected = bkManager.spotsReservedDifference(5, 1);

            int actual = 4;

            Assert.AreEqual(expected, actual);
        }

        /*
        ItemID?
        [TestMethod]
        public void updateNumberOfGuests_Valid()
        {
            BookingManager bkManager = new BookingManager();

            int actual = bkManager.updateNumberOfGuests()
        }*/

        /*
        I don't understand booking details
        [TestMethod]
        public void CheckToEditBooking_Valid()
        {
            BookingManager bkManager = new BookingManager();
        }

        [TestMethod]
        public void CancelBookingResults_Valid()
        {
        }

        [TestMethod]
        public void EditBookingResult_Valid()
        {
            BookingManager bkManager = new BookingManager();
        }*/
    }
}