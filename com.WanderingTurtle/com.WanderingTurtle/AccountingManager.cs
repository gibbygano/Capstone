using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    class AccountingManager
    {
        /// <summary>
        /// Arik Chadima
        /// Created: 2015/5/1
        /// Unimplemented method for assembling required data for an AccountingDetails object.
        /// </summary>
        /// <param name="Start">start date of invoices</param>
        /// <param name="End">end date of invoices</param>
        /// <returns>AccountingDetails with object data as requested by the params</returns>
        public AccountingDetails GetAccountingDetails(DateTime Start, DateTime End)
        {
            AccountingDetails details = new AccountingDetails();
            details.StartDate = Start;
            details.EndDate = End;
            InvoiceManager im = new InvoiceManager();
            BookingManager bm = new BookingManager();
            var listings = ItemListingAccessor.GetAllItemListingList();
            
            
            List<InvoiceDetails> inactiveInvoices = InvoiceAccessor.GetAllInvoicesList().FindAll(i => i.Active == false && i.DateOpened >= Start && i.DateClosed <= End);
            List<BookingDetails> bookings = new List<BookingDetails>();
            List<int> listingIDs = new List<int>();

            foreach (var i in inactiveInvoices)
            {
                var guestBookings = im.RetrieveGuestBookingDetailsList(i.HotelGuestID);
                details.Invoices.Add(new AccountingInvoiceDetails { InvoiceInformation=i,Bookings=guestBookings }); //translations into a "lower" subset.

                foreach (var bd in guestBookings)
                {
                    bookings.Add(bd);
                    if (!listingIDs.Contains(bd.ItemListID))
                    {
                        listingIDs.Add(bd.ItemListID);
                    }
                }
            }

            var suppliers = SupplierAccessor.GetSupplierList();

            foreach (var s in suppliers)
            {
                var itemIDs = listings.FindAll(l => listingIDs.Contains(l.ItemListID)).Select(l=>l.ItemListID);
                var items = new List<ItemListingDetails>();
                foreach (int i in itemIDs)
                {
                    items.Add(bm.RetrieveItemListingDetailsList(i));
                }
                //probably too condensed, but it compiles everyting necessary for stuffs.
                details.SupplierListings.Add(new AccountingSupplierListingDetails { Vendor = s, Items =  items, Bookings=bookings.FindAll(b=>itemIDs.Contains(b.ItemListID))});
            }


            return details;
        }
    }
}
