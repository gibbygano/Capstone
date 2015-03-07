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
            txtBlock_cancel.Text = null;
            populateText();

        }
        /// <summary>
        /// Attempts to populate the textblock and the Guest labels with text pertaining to the guest booking
        /// </summary>
        public void populateText()
        {
            try
            {
                lblCancelGuestInfo.Content = "Guest: " + myInvoice.HotelGuestID + " " + myInvoice.GetFullName;
                txtBlock_cancel.Text = "Booking ID: " + myBooking.BookingID + "\n Event Name: " + myBooking.EventItemName
                    + "\n Start Date: " + myBooking.StartDate + "\n Quantity: " + myBooking.Quantity + "\n Ticket Price: " + myBooking.Price.ToString("c") + "\n Total: " + myBooking.TotalPrice.ToString("c");
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
            /* try
             {
                 //Grabbing old booking information
                 oldBook = OrderManager.RetrieveBooking(myBooking.BookingID);
                 //fields to be updated 
                
                 bool cancel = true;
                 bool active = false;
                 decimal refund = refundAmount();
                 MessageBox.Show("Here's the refund it calculated: " + refund);
                
                 //New booking object created with original fields and the three updated fields.
                 Booking toCancel = new Booking(oldBook.BookingID, oldBook.GuestID, oldBook.EmployeeID, oldBook.ItemListID, oldBook.Quantity, oldBook.DateBooked, refund, active);
                 int result = OrderManager.EditBooking(toCancel);
                 if (result == 1)
                 {
                     MessageBox.Show("The booking has been cancelled.");
                     // closes window after cancel
                     this.Close();
                 }
                
             }
             catch (Exception ex)
             {
                 MessageBox.Show("An issue occured while attempting to cancel this booking.", ex.Message);
             } */

        }
        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A method to compare two different dates and determine a refund amount.
        /// Stores today's date, then subtracts todays date from the start date of the event-
        /// this information stored on the BookingDetails myBooking object
        /// Uses a TimeSpan object which represents an interval of time and is able to perform calculations on time.
        /// The difference of days is stored on an int and used to test conditions.
        /// </summary>
        /// <returns>decimal containing the refund amount</returns>
        private decimal refundAmount()
        {
            decimal refund;
            DateTime today = DateTime.Now;
            //TimeSpan is used to calculate date differences
            TimeSpan ts = myBooking.StartDate - today;
            //The .Days gets the amount of days inbetween returning an int.
            int difference = ts.Days;

            if (difference >= 3)
            {
                refund = 1.0m;
                return refund;
            }
            if (difference < 3 && difference > 1)
            {
                refund = 0.5m;
                return refund;
            }
            else
            {
                refund = 0.0m;
                return refund;
            }


        }
    }
}