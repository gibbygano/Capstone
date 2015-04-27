﻿using com.WanderingTurtle.Common;
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

        [TestInitialize]
        public void initialize()
        {
            tester.initialize();
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