using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class InvoiceAccessorTest
    {
        private HotelManagerAccessorTest tester = new HotelManagerAccessorTest();
        private InvoiceAccessor access = new InvoiceAccessor();
        private HotelGuestAccessor access2 = new HotelGuestAccessor();
        private List<InvoiceDetails> invoiceList;

        [TestInitialize]
        public void initialize()
        {
            tester.initialize();
            invoiceList = null;
        }

        [TestMethod]
        public void InvoiceAccessorGet()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
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
        public void InvoiceAccessorArchive()
        {
            invoiceList = InvoiceAccessor.GetAllInvoicesList();
            Invoice oldInvoice = invoiceList[invoiceList.Count - 1];
            Invoice newInvoice = new Invoice();
            newInvoice.HotelGuestID = oldInvoice.HotelGuestID;
            newInvoice.Active = false;
            newInvoice.DateOpened = oldInvoice.DateOpened;
            newInvoice.DateClosed = DateTime.Now;
            newInvoice.TotalPaid = 0;

            int count = InvoiceAccessor.ArchiveGuestInvoice(oldInvoice, newInvoice);

            Assert.AreEqual(1, count);
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

        [TestCleanup]
        public void cleanup()
        {
            tester.cleanup();
        }
    }
}