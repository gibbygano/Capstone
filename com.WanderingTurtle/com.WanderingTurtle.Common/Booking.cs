using System;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Tony Noel
    /// Created: 2015/02/03
    ///
    /// Booking- a class used to create a booking object. Has a default constructor, one that takes 5 arguments (which will be used in adds)
    /// and another that takes all 9 arguments
    /// the fields- BookingID, Cancel, Refund, and Active all have defaults in the database upon creation
    /// </summary>
    /// <remarks>
    /// Tony Noel
    /// Updated: 2015/03/02
    /// Tony Noel
    /// Updated: 2015/03/06
    ///
    /// Updated with new fields for prices
    /// </remarks>
    public class Booking
    {
        public Booking()
        {
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/02/06
        ///
        /// Constructor for a booking object- 8 arguments
        /// </summary>
        /// <remarks>
        /// Ryan Blake
        /// Updated: 2015/03/09
        /// </remarks>
        /// <param name="guestID"></param>
        /// <param name="empID"></param>
        /// <param name="itemID"></param>
        /// <param name="bQuantity"></param>
        /// <param name="dateBooked"></param>
        public Booking(int guestID, int empID, int itemID, int bQuantity, DateTime dateBooked, decimal ticket, decimal extended, decimal discount, decimal total)
        {
            GuestID = guestID;
            EmployeeID = empID;
            ItemListID = itemID;
            Quantity = bQuantity;
            DateBooked = dateBooked;
            TicketPrice = ticket;
            ExtendedPrice = extended;
            Discount = discount;
            TotalCharge = total;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/06
        ///
        /// Full constructor to take all arguments for Booking
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="guestID"></param>
        /// <param name="empID"></param>
        /// <param name="itemID"></param>
        /// <param name="bQuantity"></param>
        /// <param name="dateBooked"></param>
        /// <param name="refund"></param>
        /// <param name="active"></param>
        /// <param name="ticket"></param>
        /// <param name="extended"></param>
        /// <param name="total"></param>
        public Booking(int bookingID, int guestID, int empID, int itemID, int bQuantity, DateTime dateBooked, decimal discount, bool active, decimal ticket, decimal extended, decimal total)
        {
            BookingID = bookingID;
            GuestID = guestID;
            EmployeeID = empID;
            ItemListID = itemID;
            Quantity = bQuantity;
            DateBooked = dateBooked;
            Discount = discount;
            Active = active;
            TicketPrice = ticket;
            ExtendedPrice = extended;
            TotalCharge = total;
        }

        public bool Active { get; set; }

        public int BookingID { get; set; }

        public DateTime DateBooked { get; set; }

        public decimal Discount { get; set; }

        public int EmployeeID { get; set; }

        public decimal ExtendedPrice { get; set; }

        public int GuestID { get; set; }

        public int ItemListID { get; set; }

        public int Quantity { get; set; }

        public decimal TicketPrice { get; set; }

        public decimal TotalCharge { get; set; }
    }

    /// <summary>
    /// Pat Banks
    /// Created: on: 2015/02/25
    ///
    /// Booking Details inherits from a Booking. Object holds additional information for a booking to show on an invoice.
    /// </summary>
    /// <remarks>
    /// Pat Banks
    /// Updated: 2015/03/07
    /// Deleted Price and total price that are no longer needed
    /// </remarks>
    public class BookingDetails : Booking
    {
        public BookingDetails()
            : base()
        {
        }

        public string EventItemName { get; set; }

        public int InvoiceID { get; set; }

        public DateTime StartDate { get; set; }
    }
}