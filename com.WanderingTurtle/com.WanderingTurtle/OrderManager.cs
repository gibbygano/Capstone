using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.BusinessLogic
{
	public class OrderManager
	{
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

        /// <summary>
        /// Created by Pat Banks 2015/03/11
        /// 
        /// Retrieves Event Listing information for the one selected item listing
        /// Information is human readable with data from joined tables
        /// </summary>
        /// <param name="itemListID">ItemList ID</param>
        /// <returns>a ListItemObject containing the item listing information</returns>
		public ListItemObject RetrieveEventListing(int itemListID)
		{
			return BookingAccessor.getEventListing(itemListID);
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
       /// <returns>the actual fee in $ that will be charged for cancelling a booking</returns>
        public decimal CalculateCancellationFee(BookingDetails bookingToCancel)
        {
            decimal feePercent = CalculateTime(bookingToCancel);

            return feePercent * bookingToCancel.TotalCharge;
        }

        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A method to compare two different dates and determine a cancellation fee amount.
        /// Stores today's date, then subtracts todays date from the start date of the event
        /// Uses a TimeSpan object which represents an interval of time and is able to perform calculations on time.
        /// The difference of days is stored on an double and used to test conditions.
        /// </summary>
        /// <remarks>
        /// Updated by Pat Banks 2015/03/07, Updated Tony Noel 2015/03/10
        /// </remarks>
        /// <returns>decimal containing the total cancellation fee % amount</returns>
        public decimal CalculateTime(BookingDetails bookingStartTime)
        {
            decimal feePercent;
            //TimeSpan is used to calculate date differences

            TimeSpan ts = bookingStartTime.StartDate - DateTime.Now;

            //The .TotalDays gets the amount of days inbetween returns a double to account for partial days

            double difference = ts.TotalDays;

            //if event is more than 3 days away, there is no fee charged
            if (difference >= 3)
            {
                feePercent = 0m;
            }
            //if event is between 1 and 3 days, 1/2 the total price is charged
            else if (difference < 3 && difference > 1)
            {
                feePercent = 0.5m;
            }

            //if event is less than 1 day away, guest pays for entire amount
            else
            {
                feePercent = 1.0m;
            }

            return feePercent;
        }

        /// <summary>
        /// Created by Tony Noel 2015/03/10 - 
        /// 
        /// Calculates the extended price of for the order of quantity of tickets multiplied by the unit price
        /// </summary>
        /// <param name="price">price of one ticket</param>
        /// <param name="quantity">number of tickets</param>
        /// <returns>the extended price</returns>
        public decimal calcExtendedPrice(decimal price, int quantity)
        {
            return quantity * price;            
        }

        /// <summary>
        /// Created by: Tony Noel 2015/03/10, moved to Order manager
        /// 
        /// Calculates the total charge of discount * Extended price
        /// </summary>
        /// <param name="discount">percentage from form</param>
        /// <param name="extendedPrice">ticket price * quantity</param>
        /// <returns></returns>
        public decimal calcTotalCharge(decimal discount, decimal extendedPrice)
        {
            decimal amtToPayPercent = (decimal)(1 - discount);

            return amtToPayPercent * extendedPrice;
        }


        /// <summary>
        /// Created by Pat Banks 2015/03/11
        /// 
        /// Calculates the cost of one ticket with the discount given
        /// </summary>
        /// <param name="discount">% discount</param>
        /// <param name="TicketPrice">cost of one ticket</param>
        /// <returns>cost of one ticket with a discount</returns>
        public decimal calcTicketWithDiscount(decimal discount, decimal TicketPrice)
        {
            decimal amtToPayPercent = (decimal)(1 - discount);

            return amtToPayPercent * TicketPrice;
        }

        /// <summary>
        /// Updated by: Tony Noel, 2015/03/10, moved to ordermanager as it does a calculation.
        /// </summary>
        /// <param name="maxQuantity"></param>
        /// <param name="currentQuantity"></param>
        /// <returns></returns>
        public int availableQuantity(int maxQuantity, int currentQuantity)
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
        /// <param name="newQuantity">updated number of people attending</param>
        /// <param name="currentQuantity">current quantity of people attending</param>
        /// <returns>number of spots different</returns>
        public int spotsReservedDifference(int newQuantity, int currentQuantity)
        {
           int quantity = newQuantity - currentQuantity;

            return quantity;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/11
        /// 
        /// Calls the Booking Accessor to update the number of guests attending an event
        /// Needed after a booking is added, edited or cancelled
        /// </summary>
        /// <param name="itemID">id of the eventListing</param>
        /// <param name="oldNumGuests">eventListing number of guests that were attending</param>
        /// <param name="newNumGuests">new number of guests</param>
        /// <returns>number of rows affected</returns>
        public int updateNumberOfGuests(int itemID, int oldNumGuests, int newNumGuests)
		{
            return BookingAccessor.updateNumberOfGuests(itemID, oldNumGuests, newNumGuests);
		}
	}
}