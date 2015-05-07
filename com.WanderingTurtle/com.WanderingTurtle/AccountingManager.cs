using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.WanderingTurtle.BusinessLogic
{
    public class AccountingManager
    {
        /// <summary>
        /// Arik Chadima
        /// Created: 2015/5/1
        /// Assembles and returns an AccountingDetails Object with booking details for invoices and supplier listings for closed out invoices within the start and end params
        /// </summary>
        /// <param name="start">start date of invoices</param>
        /// <param name="end">end date of invoices</param>
        /// <returns>AccountingDetails with object data as requested by the params</returns>
        /// <remarks>
        /// Arik Chadima
        /// Updated: 2015/05/01
        /// Implemented method from just a stub to complete.
        /// Arik Chadima
        /// Updated 2015/05/05
        /// Added try-catch blocks for "dangerous" code.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="match" /> is null.</exception>
        public AccountingDetails GetAccountingDetails(DateTime start, DateTime end)
        {
            AccountingDetails details = new AccountingDetails
            {
                StartDate = start,
                EndDate = end
            };
            InvoiceManager im = new InvoiceManager();
            BookingManager bm = new BookingManager();
            List<ItemListing> listings = ItemListingAccessor.GetAllItemListingList();
            List<InvoiceDetails> inactiveInvoices = InvoiceAccessor.GetAllInvoicesList().FindAll(i => i.Active == false && i.DateOpened >= start && i.DateClosed <= end);
            List<BookingDetails> bookings = new List<BookingDetails>();
            List<int> listingIDs = new List<int>();

            foreach (InvoiceDetails i in inactiveInvoices)
            {
                var guestBookings = im.RetrieveGuestBookingDetailsList(i.HotelGuestID);
                details.Invoices.Add(new AccountingInvoiceDetails { InvoiceInformation = i, Bookings = guestBookings }); //translations into a "lower" subset.

                foreach (BookingDetails bd in guestBookings)
                {
                    bookings.Add(bd);
                    if (!listingIDs.Contains(bd.ItemListID))
                    {
                        listingIDs.Add(bd.ItemListID);
                    }
                }
            }

            var suppliers = SupplierAccessor.GetSupplierList();

            foreach (Supplier s in suppliers)
            {
                IEnumerable<int> itemIDs = listings.FindAll(l => listingIDs.Contains(l.ItemListID)).Select(l => l.ItemListID);
                var iDs = itemIDs as IList<int> ?? itemIDs.ToList();
                List<ItemListingDetails> items = iDs.Select(i => bm.RetrieveItemListingDetailsList(i)).ToList();

                //probably too condensed, but it compiles everyting necessary for stuffs.
                details.SupplierListings.Add(new AccountingSupplierListingDetails { Vendor = s, Items = items, Bookings = bookings.FindAll(b => iDs.Contains(b.ItemListID)) });
            }

            return details;
        }
    }
}