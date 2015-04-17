using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking
    {
        private List<ItemListingDetails> myEventList = new List<ItemListingDetails>();
        private InvoiceDetails inInvoice;
        private int eID;
        private BookingManager _bookingManager = new BookingManager();
        public ItemListing originalItem;

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/13
        ///
        /// UI for adding a booking
        /// Access from the View Invoice screen
        /// </summary>
        /// <param name="inInvoice">brings the invoice data from the prior list view</param>
        public AddBooking(InvoiceDetails inInvoice)
        {
            this.inInvoice = inInvoice;

            InitializeComponent();
            RefreshListItems();
            udDiscount.Maximum = .20;

            eID = (int)Globals.UserToken.EmployeeID;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/11
        ///
        /// Extracted method to refresh the list view as needed
        /// </summary>
        private void RefreshListItems()
        {
            lvEventListItems.ItemsPanel.LoadContent();

            try
            {
                myEventList = _bookingManager.RetrieveActiveItemListings();
                lvEventListItems.ItemsSource = myEventList;
                lvEventListItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve Hotel Guest listing from the database.");
            }
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/13
        ///
        /// Handles the add Booking click event
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
        ///
        /// Moved decision logic to Booking Manager
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAddBookingAdd_Click(object sender, RoutedEventArgs e)
        {
            //validates data from form
            if (!Validate()) { return; }

            try
            {
                Booking bookingToAdd = gatherFormInformation();

                //get results of adding booking
                ResultsEdit result = _bookingManager.AddBookingResult(bookingToAdd);

                switch (result)
                {
                    case (ResultsEdit.QuantityZero):
                        throw new WanderingTurtleException(this, "Quantity of tickets must be more than zero.");

                    case (ResultsEdit.DatabaseError):
                        throw new WanderingTurtleException(this, "Booking could not be added due to database malfunction.");

                    case (ResultsEdit.Success):
                        btnAddBookingAdd.IsEnabled = false;
                        await DialogBox.ShowMessageDialog(this, "The booking has been successfully added.");
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2/11/15
        ///
        /// a method to collect all information from the form
        /// Then after taking each variable and testing them in their specific validation method, parses them
        /// into the correct variable needed to be stored as a ItemListingDetails
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/11
        ///
        /// Added up/down controls to allow for easier user data entry
        /// </remarks>
        private Booking gatherFormInformation()
        {
            ItemListingDetails selectedItemListing = getSelectedItem();

            //gets quantity from the up/down quantity field
            int qty = (int)(udAddBookingQuantity.Value);

            //get discount from form
            decimal discount = (decimal)(udDiscount.Value);

            //calculate values for the tickets
            decimal extendedPrice = _bookingManager.calcExtendedPrice(selectedItemListing.Price, qty);
            decimal totalPrice = _bookingManager.calcTotalCharge(discount, extendedPrice);

            Booking bookingToAdd = new Booking(inInvoice.HotelGuestID, eID, selectedItemListing.ItemListID, qty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);
            return bookingToAdd;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/18
        ///
        /// Method to create ItemListingDetails from listView
        /// </summary>
        /// <returns>Returns the selected item.</returns>
        private ItemListingDetails getSelectedItem()
        {
            ItemListingDetails selected = (ItemListingDetails)lvEventListItems.SelectedItem;

            if (selected == null)
            {
                throw new WanderingTurtleException(this, "Please select an event.");
            }
            return selected;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/11
        ///
        /// Updates the total cost with discount
        /// </summary>
        /// <param name="myItemObject"></param>
        private void refreshCostsToDisplay(ItemListingDetails myItemObject)
        {
            //total cost calculations
            if (myItemObject == null) return;

            decimal extendedPrice = _bookingManager.calcExtendedPrice(myItemObject.Price, (int)(udAddBookingQuantity.Value));
            lblTotalWithDiscount.Content = _bookingManager.calcTotalCharge((decimal)(udDiscount.Value), extendedPrice);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/11
        ///
        /// Updates the total cost with discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateTicketPrice_Click(object sender, RoutedEventArgs e)
        {
            ItemListingDetails myItemObject = getSelectedItem();
            refreshCostsToDisplay(myItemObject);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/09
        ///
        /// Adds the event description to the UI when the listView Item changes
        /// Changes up/down quantity value if booking is full
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEventListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemListingDetails myItemObject = getSelectedItem();

            txtEventDescription.Text = myItemObject.EventDescription;
            udAddBookingQuantity.Maximum = myItemObject.QuantityOffered;

            udAddBookingQuantity.Value = myItemObject.QuantityOffered == 0 ? 0 : 1;
            refreshCostsToDisplay(myItemObject);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        ///
        /// Validates user input for quantity and discount %
        /// </summary>
        /// <returns>True or false if valid</returns>
        private bool Validate()
        {
            if (!Validator.ValidateInt(udAddBookingQuantity.Value.ToString()))
            {
                throw new InputValidationException(udAddBookingQuantity, "Value is not an integer.  Please re-enter.");
            }
            if (!Validator.ValidateDecimal(udDiscount.Value.ToString()))
            {
                throw new InputValidationException(udDiscount, "Value is not a percentage.  Please re-enter.");
            }
            return true;
        }
    }
}