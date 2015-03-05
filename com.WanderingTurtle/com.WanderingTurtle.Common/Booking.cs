using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    ///Created By: Tony Noel - 15/2/3, Updated - Tony Noel 15/3/2
    /// <summary>
    /// Booking- a class used to create a booking object. Has a default constructor, one that takes 5 arguments (which will be used in adds)
    /// and another that takes all 9 arguments
    /// the fields- BookingID, Cancel, Refund, and Active all have defaults in the database upon creation
    /// </summary>
    public class Booking
    {
        public int BookingID { get; set; }
        public int GuestID { get; set; }
        public int EmployeeID { get; set; }
        public int ItemListID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateBooked { get; set; }
        public bool Cancel { get; set; }
        public decimal Refund { get; set; }
        public bool Active { get; set; }

        public Booking()
        {

        }

        /// <summary>
        /// Constructor for a booking object- 5 arguments
        /// </summary>
        /// <param name="guestID"></param>
        /// <param name="empID"></param>
        /// <param name="itemID"></param>
        /// <param name="bQuantity"></param>
        /// <param name="dateBooked"></param>
        public Booking(int guestID, int empID, int itemID, int bQuantity, DateTime dateBooked)
        {

            GuestID = guestID;
            EmployeeID = empID;
            ItemListID = itemID;
            Quantity = bQuantity;
            DateBooked = dateBooked;
        }
        public Booking(int bookingID, int guestID, int empID, int itemID, int bQuantity, DateTime dateBooked, bool cancel, decimal refund, bool active)
        {
            BookingID = bookingID;
            GuestID = guestID;
            EmployeeID = empID;
            ItemListID = itemID;
            Quantity = bQuantity;
            DateBooked = dateBooked;
            Cancel = cancel;
            Refund = refund;
            Active = active;
        }

    }
    /// <summary>
    /// Pat Banks
    /// created on:  2015/02/25
    /// 
    /// Booking Details inherits from a Booking. Object holds additional information for a booking to show on an invoice.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class BookingDetails : Booking
    {
        public decimal Price { get; set; }
        public string EventItemName { get; set; }
        public DateTime StartDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int InvoiceID { get; set; }
        public decimal GetTotalCost { get { return (this.Quantity * this.Price); } }

        public BookingDetails() : base() { }

    }
}