using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    /// Updated by: Tony Noel, 2015/05/01, rewrote failed tests. Updated 2015/05/04
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class InvoiceAccessorTest
    {
        private InvoiceAccessor access = new InvoiceAccessor();
        private HotelGuestAccessor access2 = new HotelGuestAccessor();
        private HotelGuestManager hgm = new HotelGuestManager();
        HotelGuest TestGuest;
        [TestInitialize]
        public void initialize()
        {
            TestGuest = new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", "234234", "3453", true);
            hgm.AddHotelGuest(TestGuest);
        }

        [TestMethod]
        public void InvoiceAccessorGet()
        {//updated
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(100);
            int id = (int)guest[guest.Count - 1].HotelGuestID;

            Invoice invoice = InvoiceAccessor.GetInvoiceByGuest(id);

            Assert.AreEqual(id, invoice.HotelGuestID);
        }

        [TestMethod]
        public void InvoiceAccessorGetAll()
        {
            List<InvoiceDetails> invoiceList = null;

            invoiceList = InvoiceAccessor.GetAllInvoicesList();

            Assert.IsNotNull(invoiceList);
        }

        [TestMethod]
        public void InvoiceAccessorGuestBooking()
        {
            List<BookingDetails> guestBookings = null;
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)guest[guest.Count - 1].HotelGuestID;

            guestBookings = InvoiceAccessor.GetInvoiceBookingsByGuest(id);

            Assert.AreNotEqual(null, guestBookings);
        }
        [TestMethod]
        public void InvoiceAccessorArchiveGuestInvoice()
        {   //Archives the fake guest invoice
            int id = TestCleanupAccessor.GetHotelGuest();
            int worked = access.ArchiveGuestInvoice(id);
            Assert.AreEqual(2, worked);
        }
        [TestCleanup]
        public void cleanup()
        {
            TestCleanupAccessor.ClearOutInvoice();
            TestCleanupAccessor.DeleteHotelGuest();
        }
    }
}