using System;
using System.Windows;
using System.Windows.Controls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;

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
        public InvoiceDetails inInvoice;
        public BookingDetails originalBookingRecord;
        ItemListingDetails eventListingToView = new ItemListingDetails();
        int eID;
        BookingManager _bookingManager = new BookingManager();

        /// <summary>
        /// Ryan Blake 
        /// Created: 2015/03/06
        /// Allows user to edit a booking
        /// </summary>
        /// <param name="inInvoice">Invoice info from the view invoice UI</param>
        /// <param name="inBookingDetails">Booking info from the view invoice UI</param>
        /// <param name="ReadOnly">Make the form ReadOnly.</param>
        /// <exception cref="WanderingTurtleException">Occurrs making components readonly.</exception>
        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails, bool ReadOnly = false)
        {
            this.inInvoice = inInvoice;
            originalBookingRecord = inBookingDetails;
            InitializeComponent();

            populateTextFields();
            eID = (int)Globals.UserToken.EmployeeID;

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { btnCancel }); }
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
            eventListingToView = _bookingManager.RetrieveEventListing(originalBookingRecord.ItemListID);
            eventListingToView.QuantityOffered = _bookingManager.availableQuantity(eventListingToView.MaxNumGuests, eventListingToView.CurrentNumGuests);

            //populate form fields with object data
            lblEditBookingGuestName.Content = inInvoice.GetFullName;
            lblEventName.Content = originalBookingRecord.EventItemName;
            lblStartDate.Content = originalBookingRecord.StartDate;
            lblTicketPrice.Content = originalBookingRecord.TicketPrice.ToString("c");
            lblTotalDue.Content = originalBookingRecord.TotalCharge.ToString("c");

            udAddBookingQuantity.Value = originalBookingRecord.Quantity;
            udDiscount.Value = originalBookingRecord.Discount;

            lblAvailSeats.Content = eventListingToView.QuantityOffered;

            //calculates the maximum quantity for the u/d 
            udAddBookingQuantity.Maximum = originalBookingRecord.Quantity + eventListingToView.QuantityOffered;
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
        private async void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get form info
                Booking editedBookingRecord = gatherFormInformation();

                //get results of adding booking
                ResultsEdit result = _bookingManager.EditBookingResult(originalBookingRecord.Quantity, editedBookingRecord);

                switch (result)
                {
                    case (ResultsEdit.QuantityZero):
                        throw new ApplicationException("Please use cancel instead of setting quantity 0.");
                    case (ResultsEdit.Success):
                        await DialogBox.ShowMessageDialog(this, "The booking has been successfully added.");
                        this.Close();
                        break;
                    case(ResultsEdit.ListingFull):
                        throw new ApplicationException("This event is already full. You cannot add more guests to it.");
                    case(ResultsEdit.ChangedByOtherUser):
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
            decimal extendedPrice, totalPrice, discount;

            //gets quantity from the up/down quantity field
            int qty = (int)(udAddBookingQuantity.Value);

            //get discount from form
            discount = (decimal)(udDiscount.Value);

            //calculate values for the tickets
            extendedPrice = _bookingManager.calcExtendedPrice(originalBookingRecord.TicketPrice, qty);
            totalPrice = _bookingManager.calcTotalCharge(discount, extendedPrice);

            Booking editedBooking = new Booking(originalBookingRecord.BookingID, originalBookingRecord.GuestID, eID, originalBookingRecord.ItemListID, qty, DateTime.Now, discount,originalBookingRecord.Active, originalBookingRecord.TicketPrice, extendedPrice, totalPrice);
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
            this.Close();
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
            decimal extendedPrice = _bookingManager.calcExtendedPrice(originalBookingRecord.TicketPrice, (int)(udAddBookingQuantity.Value));
            lblTotalDue.Content = (_bookingManager.calcTotalCharge((decimal)(udDiscount.Value), extendedPrice)).ToString("c");
           
 //***********************TBD NEED to look at this - not updating correctly
            lblAvailSeats.Content = eventListingToView.QuantityOffered - _bookingManager.spotsReservedDifference((int)(udAddBookingQuantity.Value), eventListingToView.QuantityOffered);

        }
    }
}
