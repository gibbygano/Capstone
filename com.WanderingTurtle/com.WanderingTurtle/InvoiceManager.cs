using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.BusinessLogic
{
    public class InvoiceManager
    {
        /// <summary>
        /// Created by Pat Banks 2015/02/25
        /// Calls the InvoiceAccessor method that
        /// retrieves a list of bookings for a hotel guest
        /// </summary>
        /// <param name="hotelGuestID">Hotel guest ID</param>
        /// <returns>List of bookings for a hotel guest</returns>
        public List<BookingDetails> RetrieveBookingDetailsList(int hotelGuestID)
        {
            try
            {
                var list = new List<BookingDetails>();
                
                list = InvoiceAccessor.GetInvoiceBookingsByGuest(hotelGuestID);
                
                //get the total price for each item
                foreach (BookingDetails b in list)
                {
                    b.TotalPrice = b.GetTotalCost;
                }
                
                return list;
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
            decimal amt = 0;

            //go through bookings to calculate amount due for a customer
            foreach (BookingDetails b in guestBookings)
            {
                decimal cost = b.Price * b.Quantity;
                amt += cost;
            }

            return amt;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        /// Calls the InvoiceAccessor method that
        /// retrieves a list of invoices that are active
        /// </summary>
        /// <returns>List of all active guest invoices</returns>
        public List<InvoiceDetails> RetrieveAllInvoiceDetails()
        {
            try
            {
                return InvoiceAccessor.GetActiveInvoiceList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Created by Pat Banks 2015/02/2015
        /// Calls the InvoiceAccessor method that
        /// retrieves Invoice information for a selected hotel guest
        /// </summary>
        /// <param name="hotelGuestID">Hotel Guest ID</param>
        /// <returns>Invoice information for a hotel guest</returns>
        public InvoiceDetails RetrieveInvoiceByGuest(int hotelGuestID)
        {
            try
            {
                return InvoiceAccessor.GetInvoiceByGuest(hotelGuestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        /// <summary>
        /// Created by Pat Banks 2015/03/03
        /// Calls the InvoiceAccessor method that
        /// archives invoice information for a selected hotel guest
        /// </summary>
        /// <param name="originalInvoice">invoice that was fetched from database - used to check for concurrency errors</param>
        /// <param name="updatedInvoice">information that needs to be updated in the database</param>
        /// <returns>boolean true if result was successful</returns>
        public bool ArchiveCurrentGuestInvoice(Invoice originalInvoice, Invoice updatedInvoice)
        {
            try
            {
                return InvoiceAccessor.ArchiveGuestInvoice(originalInvoice, updatedInvoice) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
