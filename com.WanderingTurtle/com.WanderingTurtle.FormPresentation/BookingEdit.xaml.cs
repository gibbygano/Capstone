using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Ryan Blake
    /// Created: 2015/03/06
    ///
    /// Interaction logic for EditBooking.xaml
    /// </summary>
    public partial class EditBooking
    {
        public InvoiceDetails _CurrentInvoice { get; set; }

        public BookingDetails CurrentBookingDetails { get; set; }

        private ItemListingDetails _eventListingToView = new ItemListingDetails();
        private int eID;
        private BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/03/06
        /// Allows user to edit a booking
        /// </summary>
        /// <param name="currentInvoice">Invoice info from the view invoice UI</param>
        /// <param name="inBookingDetails">Booking info from the view invoice UI</param>
        /// <param name="ReadOnly">Make the form ReadOnly.</param>
        /// <exception cref="WanderingTurtleException">Occurrs making components readonly.</exception>
        public EditBooking(InvoiceDetails currentInvoice, BookingDetails inBookingDetails, bool ReadOnly = false)
        {
            _CurrentInvoice = currentInvoice;
            CurrentBookingDetails = inBookingDetails;
            InitializeComponent();
            Title = "Editing Booking: " + CurrentBookingDetails.EventItemName;

            populateTextFields();
            eID = (int)Globals.UserToken.EmployeeID;

            if (ReadOnly) { WindowHelper.MakeReadOnly(Content as Panel, btnCancel); }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015-03-19
        ///
        /// Populates text fields with object data
        /// </summary>
        private void populateTextFields()
        {
            //get latest data on the eventItemListing
            _eventListingToView = _bookingManager.RetrieveItemListingDetailsList(CurrentBookingDetails.ItemListID);
            _eventListingToView.QuantityOffered = _bookingManager.AvailableQuantity(_eventListingToView.MaxNumGuests, _eventListingToView.CurrentNumGuests);

            //populate form fields with object data
            lblEditBookingGuestName.Content = _CurrentInvoice.GetFullName;
            lblEventName.Content = CurrentBookingDetails.EventItemName;
            lblStartDate.Content = CurrentBookingDetails.StartDate;
            lblTicketPrice.Content = CurrentBookingDetails.TicketPrice.ToString("c");
            lblTotalDue.Content = CurrentBookingDetails.TotalCharge.ToString("c");

            udAddBookingQuantity.Value = CurrentBookingDetails.Quantity;
            udDiscount.Value = (double?)CurrentBookingDetails.Discount;

            lblAvailSeats.Content = _eventListingToView.QuantityOffered;

            //calculates the maximum quantity for the u/d
            udAddBookingQuantity.Maximum = CurrentBookingDetails.Quantity + _eventListingToView.QuantityOffered;
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/03/06
        ///
        /// </summary>
        /// <remarks>
        /// Tony Noel
        /// Updated: 2015/03/10
        ///
        /// To check if the quantity is going up and see if the booking is already full
        /// and if the booking has occured already, it cannot be changed.
        /// Pat Banks
        /// Updated: 2015/03/11
        ///
        /// Updated for use of up/down controls for quantity and discount
        /// Pat Banks
        /// Updated: 2015/03/19
        ///
        /// Moved decision logic to booking manager
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmitBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get form info
                Booking editedBookingRecord = gatherFormInformation();

                //get results of adding booking
                ResultsEdit result = _bookingManager.EditBookingResult(CurrentBookingDetails.Quantity, editedBookingRecord);

                switch (result)
                {
                    case (ResultsEdit.QuantityZero):
                        throw new ApplicationException("Please use cancel instead of setting quantity 0.");
                    case (ResultsEdit.Success):
                        await this.ShowMessageDialog(string.Format("The booking has been successfully {0}.", CurrentBookingDetails == null ? "added" : "updated"));
                        DialogResult = true;
                        Close();
                        break;

                    case (ResultsEdit.ListingFull):
                        throw new ApplicationException("This event is already full. You cannot add more guests to it.");
                    case (ResultsEdit.ChangedByOtherUser):
                        throw new ApplicationException("Changed by another user");
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Gathers form data to submit to database for changes
        /// </summary>
        /// <returns>booking of the new information</returns>
        private Booking gatherFormInformation()
        {
            //gets quantity from the up/down quantity field
            int qty = (int)(udAddBookingQuantity.Value);

            //get discount from form
            decimal discount = (decimal)(udDiscount.Value);

            //calculate values for the tickets
            decimal extendedPrice = _bookingManager.CalcExtendedPrice(CurrentBookingDetails.TicketPrice, qty);
            decimal totalPrice = _bookingManager.CalcTotalCharge(discount, extendedPrice);

            Booking editedBooking = new Booking(CurrentBookingDetails.BookingID, CurrentBookingDetails.GuestID, eID, CurrentBookingDetails.ItemListID, qty, DateTime.Now, discount, CurrentBookingDetails.Active, CurrentBookingDetails.TicketPrice, extendedPrice, totalPrice);
            return editedBooking;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/23
        ///
        /// Handles Click event for cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Calculates the adjusted ticket price based on new data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateTicketPrice_Click(object sender, RoutedEventArgs e)
        {
            decimal extendedPrice = _bookingManager.CalcExtendedPrice(CurrentBookingDetails.TicketPrice, (int)(udAddBookingQuantity.Value));
            lblTotalDue.Content = (_bookingManager.CalcTotalCharge((decimal)(udDiscount.Value), extendedPrice)).ToString("c");

            //***********************TBD NEED to look at this - not updating correctly
            lblAvailSeats.Content = _eventListingToView.QuantityOffered - _bookingManager.SpotsReservedDifference((int)(udAddBookingQuantity.Value), _eventListingToView.QuantityOffered);
        }
    }
}