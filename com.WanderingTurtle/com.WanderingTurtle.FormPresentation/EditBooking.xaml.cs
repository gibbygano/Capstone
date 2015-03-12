using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for EditBooking.xaml
    /// </summary>
    public partial class EditBooking : Window
    {
        public InvoiceDetails inInvoice;
        public BookingDetails originalBookingRecord;
        public BookingDetails editedBookingRecord;
        List<BookingDetails> outInvList = new List<BookingDetails>();
        ListItemObject listingToView = new ListItemObject();       

        Booking newBooking;
        OrderManager _orderManager = new OrderManager();

        /// <summary>
        /// Created by Ryan Blake 2015/03/06
        /// Allows user to edit a booking
        /// </summary>
        /// <param name="inInvoice">Invoice info from the view invoice UI</param>
        /// <param name="inBookingDetails">Booking info from the view invoice UI</param>
        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails)
        {
            this.inInvoice = inInvoice;
            originalBookingRecord = inBookingDetails;
            editedBookingRecord = inBookingDetails;

            InitializeComponent();

            outInvList.Add(inBookingDetails);
            listingToView = _orderManager.RetrieveEventListing(inBookingDetails.ItemListID);
            listingToView.QuantityOffered = _orderManager.availableQuantity(listingToView.MaxNumGuests, listingToView.CurrentNumGuests);

            lblEditBookingGuestName.Content = inInvoice.GetFullName;
            udAddBookingQuantity.Value = originalBookingRecord.Quantity;
            udDiscount.Value = originalBookingRecord.Discount;

            lblAvailSeats.Content = listingToView.QuantityOffered;

            lvEditBookingListItems.ItemsSource = outInvList;

            udAddBookingQuantity.Maximum = inBookingDetails.Quantity + listingToView.QuantityOffered;
        }


        /// <summary>
        /// Created by Ryan Blake
        ///
        /// </summary>
        /// <remarks>
        /// Updated- Tony Noel, 2015/03/10 to check if the quantity is going up and see if the booking is already full
        ///and if the booking has occured already, it cannot be changed.
        /// Updated by Pat Banks 2015/03/11 updated for use of up/down controls for quantity and discount
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            int qtyToTry = (int)(udAddBookingQuantity.Value);
            decimal discountToTry = (decimal)(udDiscount.Value);

            int numGuestsDifference=0;

            ListItemObject originalItem = null;

            try
            {
                //A variable to hold the dfference between the number of guests on the original reservation, and the old reservation
                numGuestsDifference = _orderManager.spotsReservedDifference(qtyToTry, originalBookingRecord.Quantity);

                // creates an ItemListing object by retrieving the record of the specific object based on it's ItemListID
                originalItem = _orderManager.RetrieveEventListing(editedBookingRecord.ItemListID);
                
                //assigned the difference of the MaxNumGuests - currentNum of guests
                int quantityOffered = _orderManager.availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests);
                


                //If the quantity offered is 0, and the new quantity is going up from the original amount booked, alerts the staff and returns.
                if (quantityOffered == 0 && (numGuestsDifference > qtyToTry))
                {
                    MessageBox.Show("This event is already full. You cannot add more guests to it.");
                    return;
                }

                //Method to check the number of guests added to a reservation against the available quantity for the event
                if (numGuestsDifference > quantityOffered)
                {
                    MessageBox.Show("You are attempting to add "+ numGuestsDifference+ " guests onto this reservation, however, there are only " + quantityOffered + " spots open for this event. Please alert the guest.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue locating the ItemListID on file.", ex.Message);
            }

            if (qtyToTry == 0)
            {
                MessageBox.Show("Please use cancel instead of setting quantity 0");
                return;
            }
          
            //update edited booking object with new data
            editedBookingRecord.Quantity = qtyToTry;
            editedBookingRecord.Discount = discountToTry;
            editedBookingRecord.TicketPrice = originalItem.Price;
            editedBookingRecord.ExtendedPrice = _orderManager.calcExtendedPrice(editedBookingRecord.TicketPrice, editedBookingRecord.Quantity);
            editedBookingRecord.TotalCharge = _orderManager.calcTotalCharge(editedBookingRecord.Discount, editedBookingRecord.ExtendedPrice);
            
            //send the changes to the database       
            newBooking = (Booking)editedBookingRecord;
            int numRows = _orderManager.EditBooking(newBooking);

            if (numRows == 1)
            {
                MessageBox.Show("Booking changed successfully.");
                
                //change number of seasts available
                ListItemObject originalEventListing = _orderManager.RetrieveEventListing(editedBookingRecord.ItemListID);

                int newNumGuests = originalEventListing.CurrentNumGuests + numGuestsDifference;

                int result1 = _orderManager.updateNumberOfGuests(editedBookingRecord.ItemListID, originalEventListing.CurrentNumGuests, newNumGuests);

                this.Close();
            }
        }
    }
}
