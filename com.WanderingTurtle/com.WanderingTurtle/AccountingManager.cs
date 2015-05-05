using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    public class AccountingManager
    {
        /// <summary>
        /// Arik Chadima
        /// Created: 2015/5/1
        /// Assembles and returns an AccountingDetails Object with booking details for invoices and supplier listings for closed out invoices within the start and end params
        /// 
        /// </summary>
        /// <param name="Start">start date of invoices</param>
        /// <param name="End">end date of invoices</param>
        /// <returns>AccountingDetails with object data as requested by the params</returns>
        /// <remarks>
        /// Updated 2015/5/1
        /// Arik Chadima
        /// Implemented method from just a stub to complete.
        /// 
        /// Updated 2015/5/5
        /// Added try-catch blocks for "dangerous" code.
        /// </remarks>
        public AccountingDetails GetAccountingDetails(DateTime Start, DateTime End)
        {
            AccountingDetails details = new AccountingDetails();
            details.StartDate = Start;
            details.EndDate = End;
            InvoiceManager im = new InvoiceManager();
            BookingManager bm = new BookingManager();
            List<ItemListing> listings;
            try
            {
                listings = ItemListingAccessor.GetAllItemListingList();
            }
            catch
            {
                throw;
            }
            List<InvoiceDetails> inactiveInvoices = new List<InvoiceDetails>();
            try
            {
                inactiveInvoices = InvoiceAccessor.GetAllInvoicesList().FindAll(i => i.Active == false && i.DateOpened >= Start && i.DateClosed <= End);
            }
            catch
            {
                throw;
            }
            List<BookingDetails> bookings = new List<BookingDetails>();
            List<int> listingIDs = new List<int>();

            foreach (var i in inactiveInvoices)
            {
                List<BookingDetails> guestBookings;
                try
                {
                    guestBookings = im.RetrieveGuestBookingDetailsList(i.HotelGuestID);
                }
                catch
                {
                    throw;
                }
                details.Invoices.Add(new AccountingInvoiceDetails { InvoiceInformation = i, Bookings = guestBookings }); //translations into a "lower" subset.

                foreach (var bd in guestBookings)
                {
                    bookings.Add(bd);
                    if (!listingIDs.Contains(bd.ItemListID))
                    {
                        listingIDs.Add(bd.ItemListID);
                    }
                }
            }

            List<Supplier> suppliers;
            try
            {
                suppliers = SupplierAccessor.GetSupplierList();
            }
            catch
            {
                throw;
            }

            foreach (var s in suppliers)
            {
                var itemIDs = listings.FindAll(l => listingIDs.Contains(l.ItemListID)).Select(l => l.ItemListID);
                var items = new List<ItemListingDetails>();
                foreach (int i in itemIDs)
                {
                    try
                    {
                        items.Add(bm.RetrieveItemListingDetailsList(i));
                    }
                    catch
                    {
                        throw;
                    }
                }
                //probably too condensed, but it compiles everyting necessary for stuffs.
                details.SupplierListings.Add(new AccountingSupplierListingDetails { Vendor = s, Items = items, Bookings = bookings.FindAll(b => itemIDs.Contains(b.ItemListID)) });
            }


            return details;
        }
    }
}