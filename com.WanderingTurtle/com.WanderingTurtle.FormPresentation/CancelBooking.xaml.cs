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
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for CancelBooking.xaml
    /// </summary>
    public partial class CancelBooking : Window
    {
        private BookingDetails myBooking;
        private InvoiceDetails myInvoice;
        private decimal _cancelFee = 0m;

        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A form used to cancel a booking. Displays the information about the booking.
        /// </summary>
        /// <param name="booking">Requires an input of a BookingDetails object (received from the ViewInvoice form- lvCustomerBookings )</param>
        public CancelBooking(BookingDetails booking, InvoiceDetails invoice)
        {
            InitializeComponent();
            myBooking = booking;
            myInvoice = invoice;
            populateText();
  
        }
        /// <summary>
        /// Attempts to populate the textblock and the Guest labels with text pertaining to the guest booking
        /// </summary>
        public void populateText()
        {
            try
            {
                lblBookingID.Content = myBooking.BookingID;
                lblGuestName.Content = myInvoice.GetFullName;
                lblQuantity.Content = myBooking.Quantity;
                lblEventName.Content = myBooking.EventItemName;
                lblDiscount.Content = myBooking.Discount;
                lblEventTime.Content = myBooking.StartDate;
                lblTicketPrice.Content = myBooking.TicketPrice;
                lblTotalDue.Content = myBooking.TotalCharge;

                _cancelFee = OrderManager.CalculateCancellationFee(myBooking);
                lblCancelMessage.Content = "A fee of " + _cancelFee + " will be charged to cancel this booking.";
            }
            catch (Exception ax)
            {
                MessageBox.Show("Hotel Guest information was not found. ", ax.Message);
            }

        }

        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A method to cancel a booking. First creates a booking object by searching for the original booking.
        /// Obtains this information, then creates a new booking object using the old booking information, and the 
        /// 3 updated fields that complete a cancel- cancel, refund, and active.
        /// The object is then sent to the OrderManager-EditBooking method to be processed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmCancel_Click(object sender, RoutedEventArgs e)
        {
            Booking oldBook;
            //Needs to be retooled to include the required fields to complete a cancellation- including a refund calculation and extended price/total charge calculations
            try
             {
                 //Grabbing old booking information
                 oldBook = OrderManager.RetrieveBooking(myBooking.BookingID);
                
                 //New booking object created with original fields and the three updated fields.
                 //Booking toCancel = new Booking(oldBook.BookingID, oldBook.GuestID, oldBook.EmployeeID, oldBook.ItemListID, oldBook.Quantity, oldBook.DateBooked, refund, active);
                 //int result = OrderManager.EditBooking(toCancel);
                 //if (result == 1)
                 //{
                 //    MessageBox.Show("The booking has been cancelled.");
                 //    // closes window after cancel
                 //    this.Close();
                 //}
                
             }
             catch (Exception ex)
             {
                 MessageBox.Show("An issue occured while attempting to cancel this booking.", ex.Message);
             }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}