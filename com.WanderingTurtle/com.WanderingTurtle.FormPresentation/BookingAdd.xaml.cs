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
        private InvoiceDetails _CurrentInvoice { get; set; }

        private List<ItemListingDetails> myEventList = new List<ItemListingDetails>();
        private int eID;
        private BookingManager _bookingManager = new BookingManager();
        public ItemListing originalItem;

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/13
        /// UI for adding a booking
        /// Access from the View Invoice screen
        /// </summary>
        /// <param name="inInvoice">brings the invoice data from the prior list view</param>
        public AddBooking(InvoiceDetails inInvoice)
        {
            _CurrentInvoice = inInvoice;

            InitializeComponent();
            RefreshListItems();
            Title = "Add a new Booking";
            udDiscount.Maximum = .20;

            eID = (int)Globals.UserToken.EmployeeID;
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/11
        /// Extracted method to refresh the list view as needed
        /// </summary>
        private void RefreshListItems()
        {
            lvEventListItems.ItemsPanel.LoadContent();

            try
            {
                myEventList = _bookingManager.RetrieveActiveItemListingDetailsList();
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
        /// Handles the add Booking click event
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/19
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
                Booking bookingToAdd = GatherFormInformation();

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
                        await this.ShowMessageDialog("The booking has been successfully added.");
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
        private void btnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/11
        /// a method to collect all information from the form
        /// Then after taking each variable and testing them in their specific validation method, parses them
        /// into the correct variable needed to be stored as a ItemListingDetails
        /// </summary>
        /// <remarks>
        /// Pat Banks
        /// Updated: 2015/03/11
        /// Added up/down controls to allow for easier user data entry
        /// </remarks>
        private Booking GatherFormInformation()
        {
            ItemListingDetails selectedItemListing = GetSelectedItem();

            //gets quantity from the up/down quantity field
            int qty = (int)(udAddBookingQuantity.Value);

            //get discount from form
            decimal discount = (decimal)(udDiscount.Value);

            //calculate values for the tickets
            decimal extendedPrice = _bookingManager.CalcExtendedPrice(selectedItemListing.Price, qty);
            decimal totalPrice = _bookingManager.CalcTotalCharge(discount, extendedPrice);

            Booking bookingToAdd = new Booking(_CurrentInvoice.HotelGuestID, eID, selectedItemListing.ItemListID, qty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);
            return bookingToAdd;
        }

        /// <summary>
        /// Tony Noel
        /// Created: 2015/02/18
        /// Method to create ItemListingDetails from listView
        /// </summary>
        /// <returns>Returns the selected item.</returns>
        private ItemListingDetails GetSelectedItem()
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
        /// Updates the total cost with discount
        /// </summary>
        /// <param name="myItemObject"></param>
        private void RefreshCostsToDisplay(ItemListingDetails myItemObject)
        {
            //total cost calculations
            if (myItemObject == null) return;

            decimal extendedPrice = _bookingManager.CalcExtendedPrice(myItemObject.Price, (int)(udAddBookingQuantity.Value));
            lblTotalWithDiscount.Content = _bookingManager.CalcTotalCharge((decimal)(udDiscount.Value), extendedPrice);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/11
        /// Updates the total cost with discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateTicketPrice_Click(object sender, RoutedEventArgs e)
        {
            ItemListingDetails myItemObject = GetSelectedItem();
            RefreshCostsToDisplay(myItemObject);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/09
        /// Adds the event description to the UI when the listView Item changes
        /// Changes up/down quantity value if booking is full
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEventListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemListingDetails myItemObject = GetSelectedItem();

            txtEventDescription.Text = myItemObject.EventDescription;
            udAddBookingQuantity.Maximum = myItemObject.QuantityOffered;

            udAddBookingQuantity.Value = myItemObject.QuantityOffered == 0 ? 0 : 1;
            RefreshCostsToDisplay(myItemObject);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/03/19
        /// Validates user input for quantity and discount %
        /// </summary>
        /// <returns>True or false if valid</returns>
        private bool Validate()
        {
            if (!udAddBookingQuantity.Value.ToString().ValidateInt())
            {
                throw new InputValidationException(udAddBookingQuantity, "Value is not an integer.  Please re-enter.");
            }
            if (!udDiscount.Value.ToString().ValidateDecimal())
            {
                throw new InputValidationException(udDiscount, "Value is not a percentage.  Please re-enter.");
            }
            return true;
        }
    }
}