using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System.Collections.Generic;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class InvoiceManagerTest
    {
        private HotelManagerAccessorTest tester = new HotelManagerAccessorTest();
        private InvoiceManager access = new InvoiceManager();
        List<InvoiceDetails> invoiceList;

        [TestInitialize]
        public void initialize()
        {
            tester.initialize();
        }

        [TestMethod]
        public void InvoiceManagerGetByGuest()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)guest[guest.Count - 1].HotelGuestID;

            Invoice invoice = access.RetrieveInvoiceByGuest(id);

            Assert.AreEqual(id, invoice.HotelGuestID);
        }

        [TestMethod]
        public void InvoiceManagerArchive()
        {
            invoiceList = InvoiceAccessor.GetAllInvoicesList();
            Invoice oldInvoice = invoiceList[invoiceList.Count - 1];

            ResultsArchive result = access.ArchiveCurrentGuestInvoice(oldInvoice);

            Assert.AreEqual(ResultsArchive.Success, result);
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
        public void InvoiceManagerCalculateDue()
        {
            List<HotelGuest> listGuest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)listGuest[1].HotelGuestID;
            List<BookingDetails> guestBookings = access.RetrieveGuestBookingDetailsList(id);
            
            decimal amount = access.CalculateTotalDue(guestBookings);

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
            List<HotelGuest> listGuest = HotelGuestAccessor.HotelGuestGet();
            int id = (int)listGuest[1].HotelGuestID;
            List<BookingDetails> guestBookings = access.RetrieveGuestBookingDetailsList(id);
            List<InvoiceDetails> invoices = access.RetrieveActiveInvoiceDetails();
            InvoiceDetails invoice = null;
            for (int i = 0; i < invoices.Count; i++)
            {
                if (guestBookings[1].GuestID == invoices[i].HotelGuestID)
                {
                    invoice = invoices[i];
                }
            }

            var result = access.CheckToArchiveInvoice(invoice, guestBookings);

            Assert.AreEqual(ResultsArchive.OkToArchive, result);
        }

        [TestCleanup]
        public void cleanup()
        {
            tester.cleanup();
        }
    }
}
