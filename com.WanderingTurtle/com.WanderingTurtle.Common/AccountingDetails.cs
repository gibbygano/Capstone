using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.Common
{
    public class AccountingDetails
    {
        /// <summary>
        /// Class combines data from Invoices and Suppliers for XML
        /// </summary>
        /// <remarks>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// </remarks>
        public AccountingDetails()
        {
            Invoices = new List<AccountingInvoiceDetails>();
            SupplierListings = new List<AccountingSupplierListingDetails>();
        }

        public DateTime EndDate { get; set; }

        public List<AccountingInvoiceDetails> Invoices { get; private set; }

        public DateTime StartDate { get; set; }

        public List<AccountingSupplierListingDetails> SupplierListings { get; private set; }
    }

    /// <summary>
    /// Arik Chadima
    /// Created: 2015/4/30
    /// </summary>
    public class AccountingInvoiceDetails
    {
        public AccountingInvoiceDetails()
        {
            Bookings = new List<BookingDetails>();
        }

        public List<BookingDetails> Bookings { get; set; }

        public InvoiceDetails InvoiceInformation { get; set; }
    }

    /// <summary>
    /// Arik Chadima
    /// Created: 2015/4/30
    /// </summary>
    public class AccountingSupplierListingDetails
    {
        public AccountingSupplierListingDetails()
        {
            Items = new List<ItemListingDetails>();
            Bookings = new List<BookingDetails>();
        }

        public List<BookingDetails> Bookings { get; set; }

        public List<ItemListingDetails> Items { get; set; }

        public Supplier Vendor { get; set; }
    }
}