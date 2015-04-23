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
    ///
    /// Interaction logic for CancelBooking.xaml
    /// </summary>
    public partial class CancelBooking
    {
        private BookingDetails _CurrentBooking { get; set; }

        private InvoiceDetails _CurrentInvoice { get; set; }

        private decimal cancelFee = 0m;
        private BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        ///
        /// A form used to cancel a booking. Displays the information about the booking.
        /// </summary>
        /// <param name="booking">Requires an input of a BookingDetails object
        /// (received from the ViewInvoice form- lvCustomerBookings )
        /// </param>
        /// <param name="invoice">input received from ViewInvoice</param>
        public CancelBooking(BookingDetails booking, InvoiceDetails invoice)
        {
            InitializeComponent();
            _CurrentBooking = booking;
            _CurrentInvoice = invoice;
            Title = "Canceling Booking: " + _CurrentBooking.EventItemName;
            populateText();
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        ///
        /// Attempts to populate the UI and the Guest labels with text pertaining to the guest booking
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/08
        ///
        /// Updated with new fields and formatting;  Moved fee calculation to BLL
        /// </remarks>
        public void populateText()
        {
            //populating with data from objects that opened the form
            lblBookingID.Content = _CurrentBooking.BookingID;
            lblGuestName.Content = _CurrentInvoice.GetFullName;
            lblQuantity.Content = _CurrentBooking.Quantity;
            lblEventName.Content = _CurrentBooking.EventItemName;
            lblDiscount.Content = _CurrentBooking.Discount.ToString("p");
            lblEventTime.Content = _CurrentBooking.StartDate;
            lblTicketPrice.Content = _CurrentBooking.TicketPrice.ToString("c");
            lblTotalDue.Content = _CurrentBooking.TotalCharge.ToString("c");

            cancelFee = _bookingManager.CalculateCancellationFee(_CurrentBooking);
            lblCancelMessage.Content = "A fee of " + cancelFee.ToString("c") + " will be charged to cancel this booking.";
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/03/04
        ///
        /// A method to cancel a booking.
        /// The object is then sent to the OrderManager-EditBooking method to be processed.
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/08
        ///
        /// Updated fields to reflect cancellation of booking
        /// Pat Banks
        /// Updated: 2015/03/19
        ///
        /// Moved logic to BookingManager - CancelBookingResults
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnConfirmCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the cancellation fee
                _CurrentBooking.TotalCharge = cancelFee;

                //cancel booking and get results
                ResultsEdit result = _bookingManager.CancelBookingResults(_CurrentBooking);

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
    }
}
