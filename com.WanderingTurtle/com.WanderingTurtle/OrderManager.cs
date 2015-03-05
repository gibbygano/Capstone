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

        ///Created By: Tony Noel - 15/2/5
        /// <summary>
        /// RetrieveBookingList- a method used to retrieve a list of bookings through the data access layer, from the database
        /// </summary>
        /// <exception cref="ax-ApplicationException">trouble accessing the server.</exception>
        /// <returns>a list of booking objects from database.</returns>
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

        ///Created By: Tony Noel - 15/2/13
        /// <summary>
        /// RetrieveListItemList- a method used to retrieve a list of ListItemObjects (a subclass of Booking) through the data access layer, from the database
        /// The information returned is specifically that human-readable elements needed to make a booking like event name, description, etc
        /// </summary>
        /// <returns>Returns a list of ListItemObject objects from database(From the ItemListing and EventItem tables).</returns>
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

        ///Created By: Tony Noel - 15/2/5,  Updated By: Pat Banks - 2/19/15 exception handling
        /// <summary>
        /// AddaBooking- a method used to send a new booking through the data access layer to be added to the database
        /// </summary>
        /// <param name="newBooking">Takes an input of a booking object</param>
        /// <returns>Returns an int- the number of rows affected, if add is successful</returns>
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

        /// Created By: Tony Noel - 2/5/15
        /// <summary>
        /// EditBooking- a method used to update a booking through the data access layer to be added to the database
        /// As the BookingID number will not change or be updated in the database the method uses the same booking ID number to search
        /// the database through the Retrieve Booking method. This will pull the originally record into an object "oldOne". Then the 
        /// original record and the new booking object that was passed to the method can both be passed to upDateBooking to be updated.
        /// </summary>
        /// <param name="newOne">Takes an input of a booking object</param>
        /// <returns> Returns an int- the number of rows affected, if add is successful</returns>
        public int EditBooking(Booking newOne)
        {
            Booking oldOne = RetrieveBooking(newOne.BookingID);
            var numRows = BookingAccessor.updateBooking(oldOne, newOne);
            return numRows;
        }

        /// Created By: Tony Noel - 15/2/5
        /// <summary>
        /// RetrieveBooking- a method used to request a booking from the data access layer and database
        /// </summary>
        /// <param name="bookingID">Takes an input of an int- the BookingID number to locate the requested record.</param>
        /// <returns>Output is a booking object to hold the booking record.</returns>
        public Booking RetrieveBooking(int bookingID)
        {
            try
            {
                return BookingAccessor.getBooking(bookingID);
            }
            catch (Exception)
            {
                var ax = new ApplicationException("There was a problem accessing your data.");
                throw ax;
            }
        }

    }
}
