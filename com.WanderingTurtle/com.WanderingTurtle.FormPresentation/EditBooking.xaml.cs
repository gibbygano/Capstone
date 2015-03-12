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

        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails)
        {
            this.inInvoice = inInvoice;
            originalBookingRecord = inBookingDetails;
            editedBookingRecord = inBookingDetails;

            InitializeComponent();

            outInvList.Add(inBookingDetails);
            listingToView = _orderManager.RetrieveEventListing(inBookingDetails.ItemListID);

            lblEditBookingGuestName.Content = inInvoice.GetFullName;
            tbEditBookingQuantity.Text = inBookingDetails.Quantity.ToString();
            tbEditBookingDiscount.Text = inBookingDetails.Discount.ToString();

            lblAvailSeats.Content = listingToView.MaxNumGuests - listingToView.CurrentNumGuests;

            lvEditBookingListItems.ItemsSource = outInvList;
        }

        ///Created by Ryan Blake
        ///Updated- Tony Noel, 2015/03/10 to check if the quantity is going up and see if the booking is already full
        ///and if the booking has occured already, it cannot be changed.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            string inQuantity;
            string inDiscount;
            int discount;

            inQuantity = tbEditBookingQuantity.Text;

            if(!int.TryParse(inQuantity, out quantity))
            {
                MessageBox.Show("Please enter a whole number for quantity.");
                return;
            }

            int numGuestsDifference=0;

            try
            {
                //A variable to hold the dfference between the number of guests on the original reservation, and the old reservation
                numGuestsDifference = _orderManager.spotsReservedDifference(quantity, editedBookingRecord.Quantity);

                // creates an ItemListing object by retrieving the record of the specific object based on it's ItemListID
                ListItemObject originalItem = _orderManager.RetrieveEventListing(editedBookingRecord.ItemListID);

                //assigned the difference of the MaxNumGuests - currentNum of guests
                int quantityOffered = _orderManager.availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests);
                
                //If the quantity offered is 0, and the new quantity is going up from the original amount booked, alerts the staff and returns.
                if (quantityOffered == 0 && quantity > editedBookingRecord.Quantity)
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

            if (quantity < 1)
            {
                MessageBox.Show("Please use cancel instead of setting quantity 0");
                return;
            }

            inDiscount = tbEditBookingDiscount.Text;
            int.TryParse(inDiscount, out discount);

            if (inDiscount == null)
            {
                discount = 0;
            }
            else if(discount > 20)
            {
                MessageBox.Show("Discount cannot exceed 20%");
                tbEditBookingDiscount.Clear();
                tbEditBookingDiscount.Focus();
                return;
            }

            //update edited booking object with new data
            editedBookingRecord.TicketPrice = originalBookingRecord.TicketPrice;
            editedBookingRecord.ExtendedPrice = _orderManager.calcExtendedPrice(editedBookingRecord.TicketPrice, quantity);
            editedBookingRecord.TotalCharge = _orderManager.calcTotalCharge(discount, editedBookingRecord.ExtendedPrice);
            editedBookingRecord.Quantity = quantity;

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
