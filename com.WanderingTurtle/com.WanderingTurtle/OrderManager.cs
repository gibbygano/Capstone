using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.BusinessLogic
{
	public static class OrderManager
	{
		///Created By: Tony Noel - 15/2/5
		/// <summary>
		/// RetrieveBookingList- a method used to retrieve a list of bookings through the data access layer, from the database
		/// </summary>
		/// <exception cref="ApplicationException">trouble accessing the server.</exception>
		/// <returns>a list of booking objects from database.</returns>
		public static List<Booking> RetrieveBookingList()
		{
			return BookingAccessor.getBookingList();
		}

		///Created By: Tony Noel - 15/2/13
		/// <summary>
		/// RetrieveListItemList- a method used to retrieve a list of ListItemObjects (a subclass of Booking) through the data access layer, from the database
		/// The information returned is specifically that human-readable elements needed to make a booking like event name, description, etc
		/// </summary>
		/// <returns>Returns a list of ListItemObject objects from database(From the ItemListing and EventItem tables).</returns>
		public static List<ListItemObject> RetrieveListItemList()
		{
			return BookingAccessor.getListItems();
		}

		///Created By: Tony Noel - 15/2/5,  Updated By: Pat Banks - 2/19/15 exception handling
		/// <summary>
		/// AddaBooking- a method used to send a new booking through the data access layer to be added to the database
		/// </summary>
		/// <param name="newBooking">Takes an input of a booking object</param>
		/// <returns>Returns an int- the number of rows affected, if add is successful</returns>
		public static int AddaBooking(Booking newBooking)
		{
			return BookingAccessor.addBooking(newBooking);
		}



		/// Created By: Tony Noel - 2/5/15
		/// <summary>
		/// EditBooking- a method used to update a booking through the data access layer to be added to the database
		/// As the BookingID number will not change or be updated in the database the method uses the same booking ID number to search
		/// the database through the Retrieve Booking method. This will pull the originally record into an object "oldOne". Then the
		/// original record and the new booking object that was passed to the method can both be passed to upDateBooking to be updated.
		/// </summary>
		/// <param name="newOne">Takes an input of a booking object</param>
		/// <returns> Returns an int- the number of rows affected, if add is successful</returns>
		public static int EditBooking(Booking newOne)
		{
			Booking oldOne = RetrieveBooking(newOne.BookingID);
			var numRows = BookingAccessor.updateBooking(oldOne, newOne);
			return numRows;
		}

		/// Created By: Tony Noel - 15/2/5
		/// <summary>
		/// RetrieveBooking- a method used to request a booking from the data access layer and database
		/// </summary>
		/// <param name="bookingId">Takes an input of an int- the BookingID number to locate the requested record.</param>
		/// <returns>Output is a booking object to hold the booking record.</returns>
		public static Booking RetrieveBooking(int bookingId)
		{
			return BookingAccessor.getBooking(bookingId);
		}

        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A method to compare two different dates and determine a refund amount.
        /// Stores today's date, then subtracts todays date from the start date of the event-
        /// this information stored on the BookingDetails myBooking object
        /// Uses a TimeSpan object which represents an interval of time and is able to perform calculations on time.
        /// The difference of days is stored on an int and used to test conditions.
        /// </summary>
        /// <remarks>
        /// Updated by Pat Banks 2015/03/07
        /// </remarks>
        /// <returns>decimal containing the total cancellation fee amount</returns>
        public static decimal CalculateCancellationFee(BookingDetails bookingToCancel)
        {
            decimal feePercent;

            //TimeSpan is used to calculate date differences
            TimeSpan ts = bookingToCancel.StartDate - DateTime.Now;
            //The .Days gets the amount of days inbetween returning an int.
            double difference = ts.TotalDays;

            if (difference >= 3)
            {
                feePercent = 0.0m;
            }
            else if (difference < 3 && difference > 1)
            {
                feePercent = 0.5m;
            }
            else
            {
                feePercent = 1.0m;
            }
            return feePercent * bookingToCancel.TotalCharge;
        }

        public static int updateNumberOfGuests(int itemID, int oldNumGuests, int newNumGuests)
		{
            return BookingAccessor.updateNumberOfGuests(itemID, oldNumGuests, newNumGuests);
		}
	}
}