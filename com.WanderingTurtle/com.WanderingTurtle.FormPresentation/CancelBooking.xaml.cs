using System;
using System.Windows;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;

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
        private BookingDetails myBooking;
        private InvoiceDetails myInvoice;
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
            myBooking = booking;
            myInvoice = invoice;
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
            lblBookingID.Content = myBooking.BookingID;
            lblGuestName.Content = myInvoice.GetFullName;
            lblQuantity.Content = myBooking.Quantity;
            lblEventName.Content = myBooking.EventItemName;
            lblDiscount.Content = myBooking.Discount.ToString("p");
            lblEventTime.Content = myBooking.StartDate;
            lblTicketPrice.Content = myBooking.TicketPrice.ToString("c");
            lblTotalDue.Content = myBooking.TotalCharge.ToString("c");

            cancelFee = _bookingManager.CalculateCancellationFee(myBooking);
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
                myBooking.TotalCharge = cancelFee;

                //cancel booking and get results
                ResultsEdit result = _bookingManager.CancelBookingResults(myBooking);

                switch (result)
                {
                    case (ResultsEdit.ChangedByOtherUser):
                        throw new Exception("This booking has already been cancelled.");

                    case (ResultsEdit.Success):
                        await DialogBox.ShowMessageDialog(this, "Booking successfully cancelled.");
                        this.Close();
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