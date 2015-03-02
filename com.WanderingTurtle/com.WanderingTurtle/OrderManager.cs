using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle
{
    public class OrderManager
    {
        public OrderManager()
        { 
        
        }
  

        /* RetrieveBookingList- a method used to retrieve a list of bookings through the data access layer, from
         * the database
        * Takes no inputs
         * Returns a list of booking objects from database.
         * Specific exception: trouble accessing the server.
        * Created By: Tony Noel - 2/5/15
        * */
        public List<Booking> RetrieveBookingList()
        {
            try
            {
                return BookingAccessor.getBookingList();
            }
            catch (Exception)
            {
                var ax = new ApplicationException("There was a problem accessing the server. \nPlease contact your system administrator.");
                throw ax;
            }
        }

        /* RetrieveBookingOpsList- a method used to retrieve a list of booking options through the data access layer, from
         * the database
        * Takes no inputs
         * Returns a list of bookingoptions objects from database.
         * Specific exception: trouble accessing the server.
        * Created By: Tony Noel - 2/13/15
        * */
        public List<ListItemObject> RetrieveListItemList()
        {
            try
            {
                return BookingAccessor.getListItems();
            }
            catch (Exception)
            {
                var ax = new ApplicationException("There was a problem accessing the server. \nPlease contact your system administrator.");
                throw ax;
            }
        }

        /* AddaBooking- a method used to send a new booking through the data access layer 
         * to be added to the database
        * Takes an input of a booking object
         * Returns an int- the number of rows affected.
        * Created By: Tony Noel - 2/5/15
         * Updated By: Pat Banks - 2/19/15 exception handling
        * */
        public int AddaBooking(Booking newBooking)
        {
            try
            {
                var numRows = BookingAccessor.addBooking(newBooking);
                return numRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

             

/********************  Methods not used in Sprint 1 ************************************************/


        /* EditBooking- a method used to update a booking through the data access layer 
         * to be added to the database
        * Takes an input of a booking object
         * As the BookingID number will not change, the method uses the same booking ID number to search
         * the database through the Retrieve Booking method. This will pull the originally record into an object
         * "oldOne". Then the original record and the new booking object that was passed to the method can both
         * be passed to upDateBooking to be updated.
         * Returns an int- the number of rows affected.
        * Created By: Tony Noel - 2/5/15
        * */
        public int EditBooking(Booking newOne)
        {
            Booking oldOne = RetrieveBooking(newOne.BookingID);
            var numRows = BookingAccessor.updateBooking(oldOne, newOne);
            return numRows;
        }


        /* RetrieveBooking- a method used to request a booking from the data access layer and database
  * Takes an input of an int- the BookingID number to locate the requested record.
  * Output is a booking object to hold the booking record.
  * Specific Exception thrown is if there was an issue accessing the data.
  * Created By: Tony Noel - 2/5/15
  * */
        public Booking RetrieveBooking(int bookingID)
        {
            try
            {
                var bookingToGet = BookingAccessor.getBooking(bookingID);
                return bookingToGet;
            }
            catch (Exception)
            {
                var ax = new ApplicationException("There was a problem accessing your data.");
                throw ax;
            }
        }

    }
}
