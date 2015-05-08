using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.WanderingTurtle.Common
{
    [DataContract]
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

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public List<AccountingInvoiceDetails> Invoices { get; private set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public List<AccountingSupplierListingDetails> SupplierListings { get; private set; }
    }

    /// <summary>
    /// Arik Chadima
    /// Created: 2015/4/30
    /// </summary>
    [DataContract]
    public class AccountingInvoiceDetails
    {
        public AccountingInvoiceDetails()
        {
            Bookings = new List<BookingDetails>();
        }

        [DataMember]
        public List<BookingDetails> Bookings { get; set; }

        [DataMember]
        public InvoiceDetails InvoiceInformation { get; set; }
    }

    /// <summary>
    /// Arik Chadima
    /// Created: 2015/4/30
    /// </summary>
    [DataContract]
    public class AccountingSupplierListingDetails
    {
        public AccountingSupplierListingDetails()
        {
            Items = new List<ItemListingDetails>();
            Bookings = new List<BookingDetails>();
        }

        [DataMember]
        public List<BookingDetails> Bookings { get; set; }

        [DataMember]
        public List<ItemListingDetails> Items { get; set; }

        [DataMember]
        public Supplier Vendor { get; set; }
    }
}