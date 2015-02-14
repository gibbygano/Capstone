using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    /* Booking- a class used to create a booking object
         * Has a default constructor and a constructor that takes 3 int arguments and a DateTime argument
         * Created By: Tony Noel - 2/3/15
         * */
    public class Booking
    {
        public int BookingID { get; set; }
        public int GuestID { get; set; }
        public int EmployeeID { get; set; }
        public int ItemListID { get; set; }
        public int BookingQuantity { get; set; }
        public DateTime DateBooked { get; set; }

        public Booking()
        {

        }
        public Booking(int guestID, int empID, DateTime dateBooked)
        {
            GuestID = guestID;
            EmployeeID = empID;
            DateBooked = dateBooked;
        }
        public Booking(int bookingID, int guestID, int empID, int itemID, int bQuantity, DateTime dateBooked)
        {
            BookingID = bookingID;
            GuestID = guestID;
            EmployeeID = empID;
            DateBooked = dateBooked;
        }
    }
}
