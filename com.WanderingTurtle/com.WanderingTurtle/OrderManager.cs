using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.BusinessLogic
{
	public class OrderManager
	{
		///Created By: Tony Noel - 15/2/5
		/// <summary>
		/// RetrieveBookingList- a method used to retrieve a list of bookings through the data access layer, from the database
		/// </summary>
		/// <exception cref="ApplicationException">trouble accessing the server.</exception>
		/// <returns>a list of booking objects from database.</returns>
		public List<Booking> RetrieveBookingList()
		{
			return BookingAccessor.getBookingList();
		}

		///Created By: Tony Noel - 15/2/13
		/// <summary>
		/// RetrieveListItemList- a method used to retrieve a list of ListItemObjects (a subclass of Booking) through the data access layer, from the database
		/// The information returned is specifically that human-readable elements needed to make a booking like event name, description, etc
		/// </summary>
		/// <returns>Returns a list of ListItemObject objects from database(From the ItemListing and EventItem tables).</returns>
		public List<ListItemObject> RetrieveListItemList()
		{
			return BookingAccessor.getListItems();
		}

		///Created By: Tony Noel - 15/2/5,  Updated By: Pat Banks - 2/19/15 exception handling
		/// <summary>
		/// AddaBooking- a method used to send a new booking through the data access layer to be added to the database
		/// </summary>
		/// <param name="newBooking">Takes an input of a booking object</param>
		/// <returns>Returns an int- the number of rows affected, if add is successful</returns>
		public int AddaBooking(Booking newBooking)
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
		/// <param name="bookingId">Takes an input of an int- the BookingID number to locate the requested record.</param>
		/// <returns>Output is a booking object to hold the booking record.</returns>
		public Booking RetrieveBooking(int bookingId)
		{
			return BookingAccessor.getBooking(bookingId);
		}

        ///Updated by: Tony Noel 2015/03/10
       /// <summary>
       /// Method to calculate the cancellation fee using the CalculateTime method * TotalCharge
       /// </summary>
       /// <param name="bookingToCancel"></param>
       /// <returns></returns>
        public static decimal CalculateCancellationFee(BookingDetails bookingToCancel)
        {
            decimal feePercent = CalculateTime(bookingToCancel);
            if (feePercent > 1.0m)
            {
                feePercent = 1.0m;
            }
            return feePercent * bookingToCancel.TotalCharge;
        }
        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A method to compare two different dates and determine a refund amount.
        /// Stores today's date, then subtracts todays date from the start date of the event-
        /// this information stored on the BookingDetails myBooking object
        /// Uses a TimeSpan object which represents an interval of time and is able to perform calculations on time.
        /// The difference of days is stored on an double and used to test conditions.
        /// </summary>
        /// <remarks>
        /// Updated by Pat Banks 2015/03/07, Updated Tony Noel 2015/03/10
        /// </remarks>
        /// <returns>decimal containing the total cancellation fee amount</returns>
        public static decimal CalculateTime(BookingDetails bookingStartTime)
        {
            decimal feePercent;
            //TimeSpan is used to calculate date differences
            TimeSpan ts = bookingStartTime.StartDate - DateTime.Now;
            //The .Days gets the amount of days inbetween returning an int.
            double difference = ts.TotalDays;

            if (difference >= 3)
            {
                feePercent = 0.0m;
            }
            if (difference < 3 && difference > 1)
            {
                feePercent = 0.5m;
            }
            if (difference < 0)
            {
                //this is returned if the booking startdate is past today's date
                feePercent = 2.0m;
            }
            else
            {
                feePercent = 1.0m;
            }
            return feePercent;
        }


        /// Created by: 
        /// <summary>
        /// Updated- Tony Noel 2015/03/10 - moved to OrderManager as method does calculations. Changed to a public static method.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static decimal calcExtendedPrice(decimal price, decimal discount)
        {
            decimal extendedPrice;

            extendedPrice = ((100 - discount) / 100) * price;

            return extendedPrice;
        }
        ///Updated by: Tony Noel 2015/03/10, moved to Order manager, made public static method as it does a calculation.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="extendedPrice"></param>
        /// <returns></returns>
        public static decimal calcTotalPrice(int quantity, decimal extendedPrice)
        {
            return (decimal)quantity * extendedPrice;
        }
        /// <summary>
        /// Updated by: Tony Noel, 2015/03/10, moved to ordermanager as it does a calculation.
        /// </summary>
        /// <param name="maxQuantity"></param>
        /// <param name="currentQuantity"></param>
        /// <returns></returns>
        public static int availableQuantity(int maxQuantity, int currentQuantity)
        {
            int availableQuantity;

            availableQuantity = maxQuantity - currentQuantity;

            return availableQuantity;
        }
        ///Created By: Tony Noel- 2015/03/10
        /// <summary>
        /// A helper method to calculate the quantity of guests being added onto a booking compared to the original
        /// amount reserved for the booking. Returns the difference between the two.
        /// </summary>
        /// <param name="maxQuantity"></param>
        /// <param name="currentQuantity"></param>
        /// <returns></returns>
        public static int spotsReservedDifference(int newQuantity, int currentQuantity)
        {
           int quantity = newQuantity - currentQuantity;

            return quantity;
        }
        public static int updateNumberOfGuests(int itemID, int oldNumGuests, int newNumGuests)
		{
            return BookingAccessor.updateNumberOfGuests(itemID, oldNumGuests, newNumGuests);
		}
	}
}