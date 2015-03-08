using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
	public static class InvoiceManager
	{
		/// <summary>
		/// Created by Pat Banks 2015/02/25
		/// Calls the InvoiceAccessor method that
		/// retrieves a list of bookings for a hotel guest
		/// </summary>
		/// <param name="hotelGuestId">Hotel guest ID</param>
		/// <returns>List of bookings for a hotel guest</returns>
		public static List<BookingDetails> RetrieveBookingDetailsList(int hotelGuestId)
		{
			return InvoiceAccessor.GetInvoiceBookingsByGuest(hotelGuestId);
		}

		/// <summary>
		/// Created by Pat Banks 2015/03/03
		/// Calculates the amount due for a customer's bookings
		/// </summary>
		/// <param name="guestBookings">List of bookings for a guest</param>
		/// <returns>cost of bookings</returns>
		public static decimal CalculateTotalDue(List<BookingDetails> guestBookings)
		{
			//go through bookings to calculate amount due for a customer
			return guestBookings.Sum(b => b.TotalCharge);
		}

		/// <summary>
		/// Created by Pat Banks 2015/03/03
		/// Calls the InvoiceAccessor method that
		/// retrieves a list of invoices that are active
		/// </summary>
		/// <returns>List of all active guest invoices</returns>
		public static List<InvoiceDetails> RetrieveAllInvoiceDetails()
		{
			return InvoiceAccessor.GetActiveInvoiceList();
		}

		/// <summary>
		/// Created by Pat Banks 2015/02/2015
		/// Calls the InvoiceAccessor method that
		/// retrieves Invoice information for a selected hotel guest
		/// </summary>
		/// <param name="hotelGuestId">Hotel Guest ID</param>
		/// <returns>Invoice information for a hotel guest</returns>
		public static InvoiceDetails RetrieveInvoiceByGuest(int hotelGuestId)
		{
			return InvoiceAccessor.GetInvoiceByGuest(hotelGuestId);
		}

		/// <summary>
		/// Created by Pat Banks 2015/03/03
		/// Calls the InvoiceAccessor method that
		/// archives invoice information for a selected hotel guest
		/// </summary>
		/// <param name="originalInvoice">invoice that was fetched from database - used to check for concurrency errors</param>
		/// <param name="updatedInvoice">information that needs to be updated in the database</param>
		/// <returns>boolean true if result was successful</returns>
		public static bool ArchiveCurrentGuestInvoice(Invoice originalInvoice, Invoice updatedInvoice)
		{
			return InvoiceAccessor.ArchiveGuestInvoice(originalInvoice, updatedInvoice) > 0;
		}
	}
}