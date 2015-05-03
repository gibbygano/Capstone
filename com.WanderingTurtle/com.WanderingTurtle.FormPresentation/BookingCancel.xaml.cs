using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Tony Noel
    /// Created: 2015/03/04
    /// Interaction logic for CancelBooking.xaml
    /// </summary>
    public partial class CancelBooking
    {
        private BookingDetails CurrentBooking { get; set; }

        private InvoiceDetails CurrentInvoice { get; set; }

        private decimal _cancelFee;
        private readonly BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        /// A form used to cancel a booking. Displays the information about the booking.
        /// </summary>
        /// <param name="booking">Requires an input of a BookingDetails object
        /// (received from the ViewInvoice form- lvCustomerBookings )
        /// </param>
        /// <param name="invoice">input received from ViewInvoice</param>
        public CancelBooking(BookingDetails booking, InvoiceDetails invoice)
        {
            InitializeComponent();
            CurrentBooking = booking;
            CurrentInvoice = invoice;
            Title = "Canceling Booking: " + CurrentBooking.EventItemName;
            PopulateText();
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        /// Attempts to populate the UI and the Guest labels with text pertaining to the guest booking
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/08
        /// Updated with new fields and formatting;  Moved fee calculation to BLL
        /// </remarks>
        private void PopulateText()
        {
            //populating with data from objects that opened the form
            LblBookingId.Content = CurrentBooking.BookingID;
            LblGuestName.Content = CurrentInvoice.GetFullName;
            LblQuantity.Content = CurrentBooking.Quantity;
            LblEventName.Content = CurrentBooking.EventItemName;
            LblDiscount.Content = CurrentBooking.Discount.ToString("p");
            LblEventTime.Content = CurrentBooking.StartDate;
            LblTicketPrice.Content = CurrentBooking.TicketPrice.ToString("c");
            LblTotalDue.Content = CurrentBooking.TotalCharge.ToString("c");

            _cancelFee = _bookingManager.CalculateCancellationFee(CurrentBooking);
            LblCancelMessage.Content = "A fee of " + _cancelFee.ToString("c") + " will be charged to cancel this booking.";
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        /// A method to cancel a booking.
        /// The object is then sent to the OrderManager-EditBooking method to be processed.
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/08
        /// Updated fields to reflect cancellation of booking
        ///
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved logic to BookingManager - CancelBookingResults
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnConfirmCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the cancellation fee
                CurrentBooking.TotalCharge = _cancelFee;

                //cancel booking and get results
                ResultsEdit result = _bookingManager.CancelBookingResults(CurrentBooking);

                switch (result)
                {
                    case (ResultsEdit.ChangedByOtherUser):
                        throw new ApplicationException("This booking has already been cancelled.");

                    case (ResultsEdit.Success):
                        await this.ShowMessageDialog("Booking successfully cancelled.");
                        DialogResult = true;
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/23
        /// Closes UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}