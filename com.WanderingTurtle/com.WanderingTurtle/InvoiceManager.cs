using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace com.WanderingTurtle.BusinessLogic
{
	public class InvoiceManager
	{
        BookingManager _bookingManager = new BookingManager();
        HotelGuestManager _hotelGuestManager = new HotelGuestManager();

		/// <summary>
		/// Created by Pat Banks 2015/02/25
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
		/// Created by Pat Banks 2015/03/03
		/// Calls the InvoiceAccessor method that
		/// retrieves a list of invoices that are active
		/// </summary>
		/// <returns>List of all active guest invoices</returns>
		public List<InvoiceDetails> RetrieveActiveInvoiceDetails()
        {
            try
            {
                List<InvoiceDetails> activeInvoices = new List<InvoiceDetails>();
                List<InvoiceDetails> allInvoices = InvoiceAccessor.GetAllInvoicesList();
  
                foreach (InvoiceDetails i in allInvoices)
                {
                    if (i.Active == true)
                    {
                        activeInvoices.Add(i);              
                    }
                }
                return activeInvoices;
            }
            catch (Exception)
            {
                throw;
            }
		}

		/// <summary>
		/// Created by Pat Banks 2015/02/2015
		/// Calls the InvoiceAccessor method that
		/// retrieves Invoice information for a selected hotel guest
		/// </summary>
		/// <param name="hotelGuestId">Hotel Guest ID</param>
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
        /// Created by Pat Banks 2015/03/03
        /// Calculates the amount due for a customer's bookings
        /// </summary>
        /// <param name="guestBookings">List of bookings for a guest</param>
        /// <returns>cost of bookings</returns>
        public decimal CalculateTotalDue(List<BookingDetails> guestBookings)
        {
            //go through bookings to calculate amount due for a customer
            return guestBookings.Sum(b => b.TotalCharge);
        }

        /// Created by Pat Banks 2015/03/09
        ///
        /// Checks if a booking is in the future and has tickets booked
        /// If fails, then guest cannot checkout
        /// <remarks>
        /// Updated by Pat Banks 2015/03/19
        /// Moved logic to Business Logic Layer
        /// </remarks>
        public ResultsArchive CheckToArchiveInvoice(InvoiceDetails invoiceToArchive, List<BookingDetails> bookingsToArchive)
        {
            foreach (BookingDetails b in bookingsToArchive)
            {
                if (b.StartDate > DateTime.Now && b.Quantity > 0)
                {
                    return ResultsArchive.CannotArchive;
                }
            }
            return ResultsArchive.OkToArchive;
        }

		/// <summary>
		/// Created by Pat Banks 2015/03/03
		/// Calls the InvoiceAccessor method that
		/// archives invoice information for a selected hotel guest
		/// </summary>
		/// <param name="originalInvoice">invoice that was fetched from database - used to check for concurrency errors</param>
		/// <param name="updatedInvoice">information that needs to be updated in the database</param>
		/// <returns>boolean true if result was successful</returns>
		public ResultsArchive ArchiveCurrentGuestInvoice(Invoice invoiceToTry)
		{
            try
            {
                //get latest bookings from the guest
                List<BookingDetails> bookingsToArchive = RetrieveGuestBookingDetailsList(invoiceToTry.HotelGuestID);

                //archive guest's bookings by changing active field to false
                foreach (BookingDetails b in bookingsToArchive)
                {
                    b.Active = false;

                    int numrows = _bookingManager.EditBooking(b);
                    
                    if (numrows != 1)
                    {
                        return ResultsArchive.ChangedByOtherUser;
                    }
                }

                //Get the latest hotel guest info
                HotelGuest guestToArchive = _hotelGuestManager.GetHotelGuest(invoiceToTry.HotelGuestID);
                bool guestArchive = _hotelGuestManager.ArchiveHotelGuest(guestToArchive, !guestToArchive.Active);

                if(guestArchive == false)
                {
                    return ResultsArchive.ChangedByOtherUser;
                } 
                else
                {
                    Invoice originalInvoice = RetrieveInvoiceByGuest(invoiceToTry.HotelGuestID);
                    
                    //update invoice record with dateClosed and change active status
                    invoiceToTry.DateClosed = DateTime.Now;
                    invoiceToTry.Active = false;

                    int numRows = InvoiceAccessor.ArchiveGuestInvoice(originalInvoice, invoiceToTry);

                    if (numRows != 1)
                    {
                        return ResultsArchive.ChangedByOtherUser;
                    }
                    else
                    {
                        return ResultsArchive.Success; 
                    }
                }
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
	}
}