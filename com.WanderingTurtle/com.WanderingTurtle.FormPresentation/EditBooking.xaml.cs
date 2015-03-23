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
using com.WanderingTurtle.FormPresentation.Models;
using System.Data.SqlClient;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
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
        /// Created by Ryan Blake 2015/03/06
        /// Allows user to edit a booking
        /// </summary>
        /// <param name="inInvoice">Invoice info from the view invoice UI</param>
        /// <param name="inBookingDetails">Booking info from the view invoice UI</param>
        public EditBooking(InvoiceDetails inInvoice, BookingDetails inBookingDetails)
        {
            this.inInvoice = inInvoice;
            originalBookingRecord = inBookingDetails;
            InitializeComponent();

            populateTextFields();
            eID = (int)com.WanderingTurtle.FormPresentation.Models.Globals.UserToken.EmployeeID;
        }


        /// <summary>
        /// created by Pat Banks 2015-03-19
        /// 
        /// populates text fields with object data
        /// </summary>
        /// <param name="inInvoice"></param>
        /// <param name="inBookingDetails"></param>
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
        /// Created by Ryan Blake 2015/03/06
        ///
        /// </summary>
        /// <remarks>
        /// Updated- Tony Noel, 2015/03/10 to check if the quantity is going up and see if the booking is already full
        ///and if the booking has occured already, it cannot be changed.
        /// Updated by Pat Banks 2015/03/11 updated for use of up/down controls for quantity and discount
        /// UPdated by Pat Banks 2015/03/19 moved decision logic to booking manager 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
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
                        DialogBox.ShowMessageDialog(this, "Please use cancel instead of setting quantity 0.");
                        break;
                    case (ResultsEdit.Success):
                        DialogBox.ShowMessageDialog(this, "The booking has been successfully added.");
                        this.Close();
                        break;
                    case(ResultsEdit.ListingFull):
                        DialogBox.ShowMessageDialog(this,"This event is already full. You cannot add more guests to it.");
                        break;
                    case(ResultsEdit.ChangedByOtherUser):
                        DialogBox.ShowMessageDialog(this, "Changed by another user");
                        break;
                }
            }
            catch (ApplicationException ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
            }
            catch (SqlException ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
            }
        }


        /// <summary>
        /// Created by Pat Banks 2015/03/19
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
        /// Handles Click event for cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/19
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
