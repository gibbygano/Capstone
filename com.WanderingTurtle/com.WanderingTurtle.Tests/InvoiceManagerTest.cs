using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{   ///Updated by: Tony Noel, 2015/05/01- Rewrote failed tests., Updated 2015/05/04
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class InvoiceManagerTest
    {
        private HotelManagerAccessorTest tester = new HotelManagerAccessorTest();
        private InvoiceManager access = new InvoiceManager();
        private HotelGuestManager hgm = new HotelGuestManager();
        HotelGuest TestGuest;

        [TestInitialize]
        public void initialize()
        {
            TestGuest = new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", "234234", "3453", true);
            hgm.AddHotelGuest(TestGuest);
        }

        [TestMethod]
        public void InvoiceManagerGetByGuest()
        {   //Updated Test- Grabs hotel guest by id
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(100);
            int id = (int)guest[guest.Count - 1].HotelGuestID;
            //Grabs invoice by guest id
            Invoice invoice = access.RetrieveInvoiceByGuest(id);
            Assert.AreEqual(id, invoice.HotelGuestID);
        }

        [TestMethod]
        public void InvoiceManagerGetByBooking()
        {
            List<BookingDetails> guestBookings = null;
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)guest[guest.Count - 1].HotelGuestID;
            guestBookings = access.RetrieveGuestBookingDetailsList(id);
            Assert.AreNotEqual(null, guestBookings);
        }

        [TestMethod]
        public void InvoiceManagerGetAll()
        {
            List<InvoiceDetails> invoiceList = null;
            invoiceList = access.RetrieveActiveInvoiceDetails();
            Assert.IsNotNull(invoiceList);
        }

        [TestMethod]
        public void InvoiceManagerGetGuestBookingDetailsList()
        {
            List<BookingDetails> guestBookings = null;
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)guest[0].HotelGuestID;
            guestBookings = access.RetrieveGuestBookingDetailsList(id);
            Assert.IsNotNull(guestBookings);
        }

        [TestMethod]
        public void InvoiceManagerRetrieveInvoiceDetails()
        {
            List<InvoiceDetails> invoices = null;
            invoices = access.RetrieveActiveInvoiceDetails();
            Assert.IsNotNull(invoices);
        }
        [TestMethod]
        public void InvoiceManagerArchiveGuestInvoice()
        { 
            int guestID = TestCleanupAccessor.GetHotelGuest();
            ResultsArchive result = access.ArchiveGuestInvoice(guestID);
            ResultsArchive expected = ResultsArchive.ChangedByOtherUser;
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void InvoiceManagerCalculateDue()
        {   //Updated: Create a list of booking details
            List<BookingDetails> guestBookings = new List<BookingDetails>();
            //Creates two fake Total Charges to be added to the guestBookings list
            BookingDetails test = new BookingDetails();
            test.TotalCharge = 40m;
            BookingDetails test2 = new BookingDetails();
            test2.TotalCharge = 50m;
            //Fake BookingDetails added
            guestBookings.Add(test);
            guestBookings.Add(test2);
            //Calculates by calling manager method to test
            decimal amount = access.CalculateTotalDue(guestBookings);
            //Asserts that 90 will be returned.
            Assert.AreEqual((decimal)90, amount);
        }

        [TestMethod]
        public void InvoiceManagerCheckArchiveInvoiceFail()
        {
            List<HotelGuest> listGuest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)listGuest[1].HotelGuestID;
            List<BookingDetails> guestBookings = access.RetrieveGuestBookingDetailsList(id);
            var invoices = access.RetrieveActiveInvoiceDetails();
            var invoice = invoices[1];
            var result = access.CheckToArchiveInvoice(invoice, guestBookings);
            Assert.AreEqual(ResultsArchive.CannotArchive, result);
        }

        [TestMethod]
        public void InvoiceManagerCheckArchiveInvoice()
        {
            //Grabs the fake guest ID
            int id = TestCleanupAccessor.GetHotelGuest();
            //Retrieves the invoice for the guest
            Invoice invoice = access.RetrieveInvoiceByGuest(id);
            InvoiceDetails invoice2 = (InvoiceDetails)invoice;
            //creates a list of BookingDetails
            List<BookingDetails> fakeBookings = new List<BookingDetails>();
            //Creates a bookingDetails object and adds a date and quantity to it.
            BookingDetails booking = new BookingDetails();
            DateTime date = new DateTime(2020,05,01);
            booking.StartDate = date;
            booking.Quantity = 0;
            //Adds it to the list
            fakeBookings.Add(booking);
            //checks to archive
            var result = access.CheckToArchiveInvoice(invoice2, fakeBookings);
            //Asserts it will be a success
            Assert.AreEqual(ResultsArchive.OkToArchive, result);
        }
        [TestMethod]
        public void InvoiceManagerInvoiceDetailsSearch()
        {   //Uses the fake name to find the fake guest
            List<InvoiceDetails> details = access.InvoiceDetailsSearch("Fake");
            string expected = "Fake";
            Assert.AreEqual(expected, details[details.Count - 1].GuestFirstName);
        }
        [TestCleanup]
        public void cleanup()
        {
            TestCleanupAccessor.ClearOutInvoice();
            TestCleanupAccessor.DeleteHotelGuest();
        }
    }
}