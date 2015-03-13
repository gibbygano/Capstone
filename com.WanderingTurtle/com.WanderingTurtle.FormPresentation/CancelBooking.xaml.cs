using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for CancelBooking.xaml
    /// </summary>
    public partial class CancelBooking
    {
        private BookingDetails myBooking;
        private InvoiceDetails myInvoice;
        private decimal cancelFee = 0m;
        private OrderManager _orderManager = new OrderManager();

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
        /// Created by Tony Noel, 2015/03/04
        /// Attempts to populate the UI and the Guest labels with text pertaining to the guest booking
        /// </summary>
        /// <remarks>
        /// Updated by Pat Banks 2015/03/08
        /// updated with new fields and formatting;  moved fee calculation to BLL
        /// </remarks>
        public void populateText()
        {
            try
            {
                lblBookingID.Content = myBooking.BookingID;
                lblGuestName.Content = myInvoice.GetFullName;
                lblQuantity.Content = myBooking.Quantity;
                lblEventName.Content = myBooking.EventItemName;
                lblDiscount.Content = myBooking.Discount.ToString("p");
                lblEventTime.Content = myBooking.StartDate;
                lblTicketPrice.Content = myBooking.TicketPrice.ToString("c");
                lblTotalDue.Content = myBooking.TotalCharge.ToString("c");

                cancelFee = _orderManager.CalculateCancellationFee(myBooking);
                lblCancelMessage.Content = "A fee of " + cancelFee.ToString("c") + " will be charged to cancel this booking.";
            }
            catch (Exception ax)
            {
                DialogBox.ShowMessageDialog(this, "Hotel Guest information was not found. ", ax.Message);
            }
        }

        ///Created By: Tony Noel, 2015/03/04
        /// <summary>
        /// A method to cancel a booking.
        /// The object is then sent to the OrderManager-EditBooking method to be processed.
        /// </summary>
        /// <remarks>
        /// updated by Pat Banks 2015/03/08
        /// updated fields to reflect cancellation of booking
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListItemObject originalEventListing = _orderManager.RetrieveEventListing(myBooking.ItemListID);

                int newNumGuests = originalEventListing.CurrentNumGuests - myBooking.Quantity;

                int result1 = _orderManager.updateNumberOfGuests(myBooking.ItemListID, originalEventListing.CurrentNumGuests, newNumGuests);

                if (result1 == 1)
                {
                    myBooking.TotalCharge = cancelFee;
                    myBooking.Quantity = 0;
                    myBooking.TicketPrice = 0;
                    myBooking.ExtendedPrice = 0;
                    myBooking.Discount = 0;

                    int result = _orderManager.EditBooking(myBooking);

                    if (result == 1)
                    {
                        DialogBox.ShowMessageDialog(this, "The booking has been cancelled.");
                        // closes window after cancel
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, "An issue occured while attempting to cancel this booking.", ex.Message);
            }
        }
    }
}