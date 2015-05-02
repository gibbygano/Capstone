using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    //markup for data object to bind
    [DataObject(true)]
    public class BookingManager
    {
        /// <summary>
        /// Tony Noel
        /// Created: 15/2/13
        /// RetrieveListItemList- a method used to retrieve a list of ItemListingDetails (a subclass of Booking) through the data access layer, from the database
        /// The information returned is specifically that human-readable elements needed to make a booking like event name, description, etc
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/30
        /// Added DataCache
        /// </remarks>
        /// <returns>Returns a list of ItemListingDetails objects from database(From the ItemListing and EventItem tables).</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ItemListingDetails> RetrieveActiveItemListingDetailsList()
        {
            double cacheExpirationTime = 5; //how long the cache should live (minutes)
            var now = DateTime.Now;

            try
            {
                if (DataCache._currentItemListingDetailsList == null)
                {
                    RefreshItemListingDetailsListCacheData();
                    return DataCache._currentItemListingDetailsList;
                }
                //check time. If less than 5 min, return cache
                if (now > DataCache._ItemListingDetailsListTime.AddMinutes(cacheExpirationTime))
                {
                    RefreshItemListingDetailsListCacheData();
                    return DataCache._currentItemListingDetailsList;
                }
                return DataCache._currentItemListingDetailsList;
            }
            catch (Exception)
            {
                var ax = new ApplicationException("There was a problem accessing the server. \nPlease contact your system administrator.");
                throw ax;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/30
        ///
        /// Refreshes the data cache
        /// </summary>
        private void RefreshItemListingDetailsListCacheData()
        {
            //data hasn't been retrieved yet. get data, set it to the cache and return the result.
            var activeEventListings = BookingAccessor.GetItemListingDetailsList();

            //calculating the quantity of available tickets for each listing
            foreach (ItemListingDetails lIO in activeEventListings)
            {
                lIO.QuantityOffered = AvailableQuantity(lIO.MaxNumGuests, lIO.CurrentNumGuests);
            }

            DataCache._currentItemListingDetailsList = activeEventListings;
            DataCache._ItemListingDetailsListTime = DateTime.Now;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/11
        ///
        /// Retrieves Event Listing information for the one selected item listing
        /// Information is human readable with data from joined tables
        /// </summary>
        /// <param name="itemListID">ItemList ID</param>
        /// <returns>an ItemListingDetails containing the item listing information</returns>
        public ItemListingDetails RetrieveItemListingDetailsList(int itemListID)
        {
            var now = DateTime.Now;
            double cacheExpirationTime = 5;

            try
            {
                if (DataCache._currentItemListingDetailsList == null)
                {
                    return BookingAccessor.GetItemListingDetails(itemListID);
                }
                //check time. If less than 5 min, return event from cache
                if (now > DataCache._ItemListingDetailsListTime.AddMinutes(cacheExpirationTime))
                {
                    //get event from DB
                    var requestedEvent = BookingAccessor.GetItemListingDetails(itemListID);
                    return requestedEvent;
                }
                else
                {
                    //get event from cached list
                    var list = DataCache._currentItemListingDetailsList;
                    ItemListingDetails requestedEvent = new ItemListingDetails();
                    requestedEvent = list.Where(e => e.ItemListID == itemListID).FirstOrDefault();

                    if (requestedEvent != null)
                    {
                        return requestedEvent;
                    }
                    throw new ApplicationException("Event not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Takes data from the presentation layer and determines the results of attempting to add a booking
        /// </summary>
        /// <param name="bookingToAdd">Booking information from presentation Layer form</param>
        /// <returns>Results of adding the booking</returns>
        public ResultsEdit AddBookingResult(Booking bookingToAdd)
        {
            if (bookingToAdd.Quantity == 0)
            {
                return ResultsEdit.QuantityZero;
            }
            try
            {
                //calls method to add a booking and update itemListing Table with current number of guests
                int result = BookingAccessor.AddBooking(bookingToAdd);

                if (result == 2)
                {
                    //update cache
                    RefreshItemListingDetailsListCacheData();

                    return ResultsEdit.Success;
                }
                return ResultsEdit.DatabaseError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/05
        ///
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
            var numRows = BookingAccessor.UpdateBooking(oldOne, newOne);
            return numRows;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/05
        ///
        /// RetrieveBooking- a method used to request a booking from the data access layer and database
        /// </summary>
        /// <param name="bookingId">Takes an input of an int- the BookingID number to locate the requested record.</param>
        /// <returns>Output is a booking object to hold the booking record.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Booking RetrieveBooking(int bookingId)
        {
            try
            {
                return BookingAccessor.GetBooking(bookingId);
            }
            catch (Exception)
            {
                var ax = new ApplicationException("There was a problem accessing your data.");
                throw ax;
            }
        }

        /// <summary>
        /// Tony Noel
        /// Updated: 2015/03/10
        ///
        /// Method to calculate the cancellation fee using the CalculateTime method * TotalCharge
        /// </summary>
        /// <param name="bookingToCancel"></param>
        /// <returns>the actual fee in $ that will be charged for cancelling a booking</returns>
        public decimal CalculateCancellationFee(BookingDetails bookingToCancel)
        {
            decimal feePercent = CalculateTime(bookingToCancel);

            return feePercent * bookingToCancel.TotalCharge;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        ///
        /// A method to compare two different dates and determine a cancellation fee amount.
        /// Stores today's date, then subtracts todays date from the start date of the event
        /// Uses a TimeSpan object which represents an interval of time and is able to perform calculations on time.
        /// The difference of days is stored on an double and used to test conditions.
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/07
        /// Tony Noel
        /// Updated: 2015/03/10
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
        /// Tony Noel
        /// Created: 2015/03/10
        ///
        /// Calculates the extended price of for the order of quantity of tickets multiplied by the unit price
        /// </summary>
        /// <param name="price">price of one ticket</param>
        /// <param name="quantity">number of tickets</param>
        /// <returns>the extended price</returns>
        public decimal CalcExtendedPrice(decimal price, int quantity)
        {
            return quantity * price;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/10 - Moved to Booking Manager
        ///
        /// Calculates the total charge of discount * Extended price
        /// </summary>
        /// <param name="discount">percentage from form</param>
        /// <param name="extendedPrice">ticket price * quantity</param>
        /// <returns></returns>
        public decimal CalcTotalCharge(decimal discount, decimal extendedPrice)
        {
            decimal amtToPayPercent = (decimal)(1 - discount);

            return amtToPayPercent * extendedPrice;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/10 - Moved to Booking Manager, as it does a calculation.
        /// </summary>
        /// <param name="maxQuantity"></param>
        /// <param name="currentQuantity"></param>
        /// <returns></returns>
        public int AvailableQuantity(int maxQuantity, int currentQuantity)
        {
            int availableQuantity;
            availableQuantity = maxQuantity - currentQuantity;

            return availableQuantity;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/10
        ///
        /// A helper method to calculate the quantity of guests being added onto a booking compared to the original
        /// amount reserved for the booking. Returns the difference between the two.
        /// </summary>
        /// <param name="newQuantity">updated number of people attending</param>
        /// <param name="currentQuantity">current quantity of people attending</param>
        /// <returns>number of spots different</returns>
        public int SpotsReservedDifference(int newQuantity, int currentQuantity)
        {
            int quantity = newQuantity - currentQuantity;

            return quantity;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Checks if a booking can be edited by performing logical checks if booking is too old or cancelled
        /// </summary>
        /// <param name="bookingToCheck"></param>
        /// <returns></returns>
        public ResultsEdit CheckToEditBooking(BookingDetails bookingToCheck)
        {
            if (bookingToCheck.StartDate < DateTime.Now)
            {
                return ResultsEdit.CannotEditTooOld;
            }
            return bookingToCheck.Quantity == 0 ? ResultsEdit.Cancelled : ResultsEdit.OkToEdit;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Gives the results of cancelling a booking to the preseantation layer
        /// </summary>
        /// <param name="bookingToCancel"></param>
        /// <returns></returns>
        public ResultsEdit CancelBookingResults(BookingDetails bookingToCancel)
        {
            try
            {
                //update the numbers
                bookingToCancel.Quantity = 0;
                bookingToCancel.TicketPrice = 0;
                bookingToCancel.ExtendedPrice = 0;
                bookingToCancel.Discount = 0;

                int result = EditBooking(bookingToCancel);

                //result should be 2 - one for the booking, one to update the number of guests for the item listing
                if (result == 2)
                {
                    //refresh Data Cache
                    RefreshItemListingDetailsListCacheData();
                    return ResultsEdit.Success;
                }
                return ResultsEdit.ChangedByOtherUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Gives the results of editing a booking to the presentation layer
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/30
        ///
        /// Updated to include data cache refresh
        /// </remarks>
        /// <param name="originalQty"></param>
        /// <param name="editedBooking"></param>
        /// <returns></returns>
        public ResultsEdit EditBookingResult(int originalQty, Booking editedBooking)
        {
            try
            {
                //A variable to hold the difference between the number of guests on the original reservation, and the old reservation
                int numGuestsDifference = SpotsReservedDifference(editedBooking.Quantity, originalQty);

                // creates an ItemListing object by retrieving the record of the specific object based on it's ItemListID
                ItemListingDetails originalItem = RetrieveItemListingDetailsList(editedBooking.ItemListID);

                //assigned the difference of the MaxNumGuests - currentNum of guests
                int quantityOffered = AvailableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests);

                //If the quantity offered is 0, and the new quantity is going up from the original amount booked, alerts the staff and returns.
                if (quantityOffered == 0 && (numGuestsDifference > editedBooking.Quantity))
                {
                    return ResultsEdit.ListingFull;
                }

                //Method to check the number of guests added to a reservation against the available quantity for the event
                if (numGuestsDifference > quantityOffered)
                {
                    return ResultsEdit.ListingFull;
                }

                if (editedBooking.Quantity == 0)
                {
                    return ResultsEdit.QuantityZero;
                }

                //send the changes to the database
                int numRows = EditBooking(editedBooking);

                if (numRows == 2)
                {
                    //refresh Data Cache
                    RefreshItemListingDetailsListCacheData();
                    return ResultsEdit.Success;
                }
                return ResultsEdit.ChangedByOtherUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/30
        ///
        /// Passes data to the Accessor to verify that the pin is not being used.
        /// </summary>
        /// <param name="inPIN"></param>
        /// <returns></returns>
        public HotelGuest CheckValidPIN(string inPIN)
        {
            try
            {
                //retrieve guest
                return BookingAccessor.VerifyHotelGuestPin(inPIN);
            }
            catch (Exception ax)
            {
                throw ax;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/14
        /// Retrieves numbers from a specific event listing
        /// Not cached since it will differ each time
        /// </summary>
        /// <param name="itemListID">id of event listing</param>
        /// <returns>names of hotel guests, room #s, and quantities of tickets for each booking related to that item listing</returns>
        public List<BookingNumbers> RetrieveBookingNumbers(int itemListID)
        {
            try
            {
                return BookingAccessor.GetBookingNumbers(itemListID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/26
        /// </summary>
        /// <param name="itemListID"></param>
        /// <returns></returns>
        public ResultsArchive CheckListingArchive(int itemListID)
        {
            var bookings = RetrieveBookingNumbers(itemListID);
            var numbers = bookings.Sum(booking => booking.Quantity);

            return numbers > 0 ? ResultsArchive.CannotArchive : ResultsArchive.OkToArchive;
        }
    }
}