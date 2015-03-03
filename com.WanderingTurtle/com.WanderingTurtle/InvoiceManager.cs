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
        /// Retrieves booking information for a hotel guest
        /// </summary>
        /// <param name="hotelGuestID">Hotel guest ID</param>
        /// <returns>List of bookings for a hotel guest</returns>
        public List<BookingDetails> RetrieveBookingDetailsList(int hotelGuestID)
        {
            try
            {
                return InvoiceAccessor.getInvoiceBookingsByGuest(hotelGuestID);            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Created by Pat Banks 2015/02/2015
        /// Retrieves Invoice information for a hotel guest
        /// </summary>
        /// <param name="hotelGuestID">Hotel Guest ID</param>
        /// <returns>Invoice information for a hotel guest</returns>
        public InvoiceDetails RetrieveInvoiceByGuest(int hotelGuestID)
        {
            try
            {
                return InvoiceAccessor.getInvoiceByGuest(hotelGuestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
