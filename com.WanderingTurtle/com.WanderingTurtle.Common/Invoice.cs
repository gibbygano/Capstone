using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public int InvoiceID { get; set; }
        public int HotelGuestID { get; set; }
        public bool Active { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public Decimal? TotalPaid { get; set; }

        public string GetTotalFormat { get { return String.Format("{0:C}", this.TotalPaid); } }
    }

    /// <summary>
    /// Pat Banks
    /// created on:  2015/02/25
    /// 
    /// Invoice Details inherits from Invoice to hold additional information for better readability.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class InvoiceDetails : Invoice
    {
        public string GuestLastName { get; set; }
        public string GuestFirstName { get; set; }
        public int GuestRoomNum { get; set; }
        public InvoiceDetails() : base() { }
        public string GetFullName { get { return string.Format("{0}, {1}", this.GuestLastName, this.GuestFirstName); } }
    }
}
