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
        public BookingDetails inBookingDetails;
        List<BookingDetails> outInvList = new List<BookingDetails>();
        public ProductManager myProd = new ProductManager();
        Booking newBooking;
        OrderManager _orderManager = new OrderManager();

        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails)
        {
            this.inInvoice = inInvoice;
            this.inBookingDetails = inBookingDetails;

            InitializeComponent();

            outInvList.Add(inBookingDetails);

            lblGeneralEditBooking.Content = "Editing Booking #" + inBookingDetails.BookingID;
            lblEditBookingGuestName.Content = inInvoice.GetFullName;
            tbEditBookingQuantity.Text = inBookingDetails.Quantity.ToString();
            tbEditBookingDiscount.Text = inBookingDetails.Discount.ToString();

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

            //calls to the Calculate time method in ordermanager which returns a decimal in the form of 0.0, .5, or 1.0.
            //1.0 in this method means that the startdate of the event is less than a day away, in other words too late to avoid being charged and 
            //too late to be edited or cancelled.
            decimal time = OrderManager.CalculateTime(inBookingDetails);
            if (time == 1.0m)
            {
                MessageBox.Show("Due to the close proximity in time to the start date of this event, the reservation cannot be edited.");
                return;
            }
            if(!int.TryParse(inQuantity, out quantity))
            {
                MessageBox.Show("Please enter a whole number for quantity.");
                return;
            }

            try
            {
                //A variable to hold the dfference between the number of guests on the original reservation, and the old reservation
                int numGuests = OrderManager.spotsReservedDifference(quantity, inBookingDetails.Quantity);

                // creates an ItemListing object by retrieving the record of the specific object based on it's ItemListID
                ItemListing originalItem = myProd.RetrieveItemListing(inBookingDetails.ItemListID.ToString());
                //assigned the difference of the MaxNumGuests - currentNum of guests
                int quantityOffered = OrderManager.availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests);
                
                //If the quantity offered is 0, and the new quantity is going up from the original amount booked, alerts the staff and returns.
                if (quantityOffered == 0 && quantity > inBookingDetails.Quantity)
                {
                    MessageBox.Show("This event is already full. You cannot add more guests to it.");
                    return;
                }
                //Method to check the number of guests added to a reservation against the available quantity for the event
                if (numGuests > quantityOffered)
                {
                    MessageBox.Show("You are attempting to add "+ numGuests+ " guests onto this reservation, however, there are only " + quantityOffered + " spots open for this event. Please alert the guest.");
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
            else if(discount > 100)
            {
                MessageBox.Show("Discount cannot exceed 100.");
                tbEditBookingDiscount.Clear();
                tbEditBookingDiscount.Focus();
                return;
            }


            inBookingDetails.Quantity = quantity;


            inBookingDetails.ExtendedPrice = calcExtendedPrice(inBookingDetails.TicketPrice, discount);
//ProductManager myProdMan = new ProductManager();
//ItemListing originalListItem = myProdMan.RetrieveItemListing(myBooking.ItemListID.ToString());

//int newNumGuests = originalListItem.CurrentNumGuests - myBooking.Quantity;


  //int result1 = OrderManager.updateNumberOfGuests(myBooking.ItemListID, originalListItem.CurrentNumGuests, newNumGuests);

            newBooking = (Booking)inBookingDetails;

            _orderManager.EditBooking(newBooking);

            MessageBox.Show("Booking changed successfully.");

            this.Close();
        }

    }
}
