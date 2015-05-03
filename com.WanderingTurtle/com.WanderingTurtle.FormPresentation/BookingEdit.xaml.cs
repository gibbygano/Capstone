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
    /// Interaction logic for EditBooking.xaml
    /// </summary>
    public partial class EditBooking
    {
        public InvoiceDetails CurrentInvoice { get; set; }

        public BookingDetails CurrentBookingDetails { get; set; }

        private ItemListingDetails _eventListingToView = new ItemListingDetails();
        private readonly int _eId;
        private readonly BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/03/06
        /// Allows user to edit a booking
        /// </summary>
        /// <param name="invoiceToEdit">Invoice info from the view invoice UI</param>
        /// <param name="inBookingDetails">Booking info from the view invoice UI</param>
        /// <param name="readOnly">Make the form ReadOnly.</param>
        /// <exception cref="WanderingTurtleException">Occurs making components readonly.</exception>
        public EditBooking(InvoiceDetails invoiceToEdit, BookingDetails inBookingDetails, bool readOnly = false)
        {
            CurrentInvoice = invoiceToEdit;
            CurrentBookingDetails = inBookingDetails;
            InitializeComponent();
            Title = "Editing Booking: " + CurrentBookingDetails.EventItemName;

            PopulateTextFields();
            _eId = (int)Globals.UserToken.EmployeeID;

            if (readOnly) { WindowHelper.MakeReadOnly(Content as Panel, BtnCancel); }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        /// Populates text fields with object data
        /// </summary>
        private void PopulateTextFields()
        {
            //get latest data on the eventItemListing
            _eventListingToView = _bookingManager.RetrieveItemListingDetailsList(CurrentBookingDetails.ItemListID);
            _eventListingToView.QuantityOffered = _bookingManager.AvailableQuantity(_eventListingToView.MaxNumGuests, _eventListingToView.CurrentNumGuests);

            //populate form fields with object data
            LblEditBookingGuestName.Content = CurrentInvoice.GetFullName;
            LblEventName.Content = CurrentBookingDetails.EventItemName;
            LblStartDate.Content = CurrentBookingDetails.StartDate;
            LblTicketPrice.Content = CurrentBookingDetails.TicketPrice.ToString("c");
            LblTotalDue.Content = CurrentBookingDetails.TotalCharge.ToString("c");

            UdAddBookingQuantity.Value = CurrentBookingDetails.Quantity;
            UdDiscount.Value = (double?)CurrentBookingDetails.Discount;

            LblAvailSeats.Content = _eventListingToView.QuantityOffered;

            //calculates the maximum quantity for the u/d
            UdAddBookingQuantity.Maximum = CurrentBookingDetails.Quantity + _eventListingToView.QuantityOffered;
        }

        /// <summary>
        /// Ryan Blake
        /// Created: 2015/03/06
        /// To check if the quantity is going up and see if the booking is already full
        /// </summary>
        /// <remarks>
        /// Tony Noel
        /// Updated: 2015/03/10
        /// if the booking has occured already, it cannot be changed.
        ///
        /// Pat Banks
        /// Updated: 2015/03/11
        /// up/down controls added for quantity and discount
        ///
        /// Pat Banks
        /// Updated: 2015/03/19
        /// Moved decision logic to booking manager
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmitBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get form info
                Booking editedBookingRecord = GatherFormInformation();

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
        /// Gathers form data to submit to database for changes
        /// </summary>
        /// <returns>booking of the new information</returns>
        private Booking GatherFormInformation()
        {
            //gets quantity from the up/down quantity field
            int qty = (int)(UdAddBookingQuantity.Value);

            //get discount from form
            decimal discount = (decimal)(UdDiscount.Value);

            //calculate values for the tickets
            decimal extendedPrice = _bookingManager.CalcExtendedPrice(CurrentBookingDetails.TicketPrice, qty);
            decimal totalPrice = _bookingManager.CalcTotalCharge(discount, extendedPrice);

            Booking editedBooking = new Booking(CurrentBookingDetails.BookingID, CurrentBookingDetails.GuestID, _eId, CurrentBookingDetails.ItemListID, qty, DateTime.Now, discount, CurrentBookingDetails.Active, CurrentBookingDetails.TicketPrice, extendedPrice, totalPrice);
            return editedBooking;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/23
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
        /// Calculates the adjusted ticket price based on new data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateTicketPrice_Click(object sender, RoutedEventArgs e)
        {
            decimal extendedPrice = _bookingManager.CalcExtendedPrice(CurrentBookingDetails.TicketPrice, (int)(UdAddBookingQuantity.Value));
            LblTotalDue.Content = (_bookingManager.CalcTotalCharge((decimal)(UdDiscount.Value), extendedPrice)).ToString("c");
        }
    }
}