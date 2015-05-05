using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    public class InvoiceManager
    {
        private BookingManager _bookingManager = new BookingManager();
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private InvoiceAccessor _invoiceAccessor = new InvoiceAccessor();

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/25
        /// Calls the InvoiceAccessor method that
        /// retrieves a list of bookings for a hotel guest
        /// </summary>
        /// <param name="hotelGuestId">Hotel guest ID</param>
        /// <returns>List of bookings for a hotel guest</returns>
        public List<BookingDetails> RetrieveGuestBookingDetailsList(int hotelGuestId)
        {
            try
            {
                return InvoiceAccessor.GetInvoiceBookingsByGuest(hotelGuestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// Calls the InvoiceAccessor method that
        /// retrieves a list of invoices that are active
        /// </summary>
        /// <returns>List of all active guest invoices</returns>
        public List<InvoiceDetails> RetrieveActiveInvoiceDetails()
        {
            try
            {
                return InvoiceAccessor.GetAllInvoicesList().Where(i => i.Active).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/2015
        /// Calls the InvoiceAccessor method that
        /// retrieves Invoice information for a selected hotel guest
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/02/27
        /// </remarks>
        /// <param name="hotelGuestId">Hotel Guest ID matching a HotelGuest object/record</param>
        /// <returns>Invoice information for a hotel guest</returns>
        public InvoiceDetails RetrieveInvoiceByGuest(int hotelGuestId)
        {
            try
            {
                return InvoiceAccessor.GetInvoiceByGuest(hotelGuestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// Calculates the amount due for a customer's bookings
        /// </summary>
        /// <param name="guestBookings">List of bookings for a guest</param>
        /// <returns>cost of bookings</returns>
        public decimal CalculateTotalDue(List<BookingDetails> guestBookings)
        {
            //go through bookings to calculate amount due for a customer
            return guestBookings.Sum(b => b.TotalCharge);
        }

        /// Pat Banks
        /// Created: 2015/03/09
        /// Checks if a booking is in the future and has tickets booked
        /// If fails, then guest cannot checkout
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved logic to Business Logic Layer
        /// </remarks>
        public ResultsArchive CheckToArchiveInvoice(InvoiceDetails invoiceToArchive, List<BookingDetails> bookingsToArchive)
        {
            return bookingsToArchive.Any(b => b.StartDate > DateTime.Now.AddHours(6) && b.Quantity > 0) ? ResultsArchive.CannotArchive : ResultsArchive.OkToArchive;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/03
        /// Calls the InvoiceAccessor method that
        /// archives invoice information for a selected hotel guest
        /// </summary>
        /// <param name="originalInvoice">invoice that was fetched from database - used to check for concurrency errors</param>
        /// <param name="updatedInvoice">information that needs to be updated in the database</param>
        /// <returns>boolean true if result was successful</returns>
        public ResultsArchive ArchiveGuestInvoice(int GuestID)
        {
            try
            {
                int numRows = _invoiceAccessor.ArchiveGuestInvoice(GuestID);

                return numRows >= 2 ? ResultsArchive.Success : ResultsArchive.ChangedByOtherUser;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Created: on 2015/04/10
        /// Justin Pennington
        /// Searches for a string that the user inputs
        /// </summary>
        /// <param name="inSearch">The string to search for</param>
        /// <returns>
        /// A List object containing InvoiceDetails objects which contain the string passed 
        /// or an empty list if no matches are found.
        /// </returns>
        public List<InvoiceDetails> InvoiceDetailsSearch(string inSearch)
        {
            //List<Event> myTempList = new List<Event>();
            if (!inSearch.Equals("") && !inSearch.Equals(null))
            {
                //Lambda Version
                //return myTempList.AddRange(DataCache._currentEventList.Where(s => s.EventItemName.ToUpper().Contains(inSearch.ToUpper())).Select(s => s));
                //LINQ version
                List<InvoiceDetails> SearchList = RetrieveActiveInvoiceDetails();
                List<InvoiceDetails> myTempList = new List<InvoiceDetails>();
                myTempList.AddRange(
                  from inGuest in SearchList
                  where inGuest.GuestFirstName.ToUpper().Contains(inSearch.ToUpper()) || inGuest.GuestLastName.ToUpper().Contains(inSearch.ToUpper())
                  select inGuest);
                return myTempList;

                //Will empty the search list if nothing is found so they will get feedback for typing something incorrectly
            }
            return RetrieveActiveInvoiceDetails();
        }
    }
}