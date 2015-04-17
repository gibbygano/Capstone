using System;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Pat Banks
    /// Created: 2015/02/24
    ///
    /// Class for the creation of Invoice Objects with set data fields
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class Invoice
    {
        public bool Active { get; set; }

        public DateTime? DateClosed { get; set; }

        public DateTime DateOpened { get; set; }

        public string GetTotalFormat { get { return String.Format("{0:C}", this.TotalPaid); } }

        public int HotelGuestID { get; set; }

        public int InvoiceID { get; set; }

        public Decimal? TotalPaid { get; set; }
    }

    /// <summary>
    /// Pat Banks
    /// Created: 2015/02/25
    ///
    /// Invoice Details inherits from Invoice to hold additional information for better readability.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class InvoiceDetails : Invoice
    {
        public InvoiceDetails()
            : base()
        {
        }

        public string GetFullName { get { return string.Format("{0}, {1}", this.GuestLastName, this.GuestFirstName); } }

        public string GuestFirstName { get; set; }

        public string GuestLastName { get; set; }

        public string GuestRoomNum { get; set; }
    }
}