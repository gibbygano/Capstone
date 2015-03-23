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
using com.WanderingTurtle;
using com.WanderingTurtle.BusinessLogic;
using Xceed.Wpf.Toolkit;
using System.Data.SqlClient;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Threading.Tasks;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking 
    {
        List<ItemListingDetails> myEventList = new List<ItemListingDetails>();
        InvoiceDetails inInvoice;
        int eID;
        BookingManager _bookingManager = new BookingManager();
        public ItemListing originalItem;

        /// <summary>
        /// Created by Tony Noel 2015/02/13
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
            eID = (int)com.WanderingTurtle.FormPresentation.Models.Globals.UserToken.EmployeeID;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/11
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
                DialogBox.ShowMessageDialog(this, ex.Message, "Unable to retrieve Hotel Guest listing from the database.");
            }

        }

        /// <summary>
        /// Created by Tony Noel 2015/02/13
        /// Handles the add Booking click event
        /// </summary>
        /// <remarks>
        /// updated by Pat Banks 2015/03/19
        /// Moved decision logic to Booking Manager
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBookingAdd_Click(object sender, RoutedEventArgs e)
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
                        DialogBox.ShowMessageDialog(this, "Quantity of tickets must be more than zero.");
                        break;

                    case(ResultsEdit.DatabaseError):
                        DialogBox.ShowMessageDialog(this, "Booking could not be added due to database malfunction.");
                        break;

                    case (ResultsEdit.Success):
                        DialogBox.ShowMessageDialog(this, "The booking has been successfully added."); 
                        this.Close();
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
        /// Created by TOny Noel- 2/11/15
        /// a method to collect all information from the form
        /// Then after taking each variable and testing them in their specific validation method, parses them 
        /// into the correct variable needed to be stored as a ItemListingDetails
        /// </summary>
        /// <remarks>
        /// Updated by:  Pat Banks 2015/03/11
        /// Added up/down controls to allow for easier user data entry
        /// </remarks>
        private Booking gatherFormInformation()
        {
            decimal extendedPrice, totalPrice, discount;
            ItemListingDetails selectedItemListing = getSelectedItem();

            //gets quantity from the up/down quantity field
            int qty = (int)(udAddBookingQuantity.Value);

            //get discount from form
            discount = (decimal)(udDiscount.Value);

            //calculate values for the tickets
            extendedPrice = _bookingManager.calcExtendedPrice(selectedItemListing.Price, qty);
            totalPrice = _bookingManager.calcTotalCharge(discount, extendedPrice);

            Booking bookingToAdd = new Booking(inInvoice.HotelGuestID, eID, selectedItemListing.ItemListID, qty, DateTime.Now, selectedItemListing.Price, extendedPrice, discount, totalPrice);
            return bookingToAdd;
        }

        /// <summary>
        /// Tony Noel 2/18/15
        /// method to create ItemListingDetails from listView
        /// </summary>
        /// <returns>returns the selected item.</returns>
        private ItemListingDetails getSelectedItem()
        {
            ItemListingDetails selected = (ItemListingDetails)lvEventListItems.SelectedItem;

            if (selected== null)
            {
                DialogBox.ShowMessageDialog(this, "Please select an event.");
            }
            return selected;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/11
        /// 
        /// updates the total cost with discount
        /// </summary>
        /// <param name="myItemObject"></param>
        private void refreshCostsToDisplay(ItemListingDetails myItemObject)
        {
            //total cost calculations
            if (myItemObject != null)
            {
                decimal extendedPrice = _bookingManager.calcExtendedPrice(myItemObject.Price, (int)(udAddBookingQuantity.Value));
                lblTotalWithDiscount.Content = _bookingManager.calcTotalCharge((decimal)(udDiscount.Value), extendedPrice);
            }
            return;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/11
        /// 
        /// updates the total cost with discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateTicketPrice_Click(object sender, RoutedEventArgs e)
        {
            ItemListingDetails myItemObject = getSelectedItem();
            refreshCostsToDisplay(myItemObject);
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/09
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

            if (myItemObject.QuantityOffered == 0)
            {
                udAddBookingQuantity.Value = 0;
            }
            else
            {
                udAddBookingQuantity.Value = 1;
            }
            refreshCostsToDisplay(myItemObject);
        }

        /// <summary>
        /// Created By Pat Banks 2015/03/19
        /// validates user input for quantity and discount %
        /// </summary>
        /// <returns>True or false if valid</returns>
        private bool Validate()
        {
            if (!Validator.ValidateInt(udAddBookingQuantity.Value.ToString()))
            {
                DialogBox.ShowMessageDialog(this, "Value is not an integer.  Please re-enter.");
                udAddBookingQuantity.Focus();
                return false;
            }
            if (!Validator.ValidateDecimal(udDiscount.Value.ToString()))
            {
                DialogBox.ShowMessageDialog(this, "Value is not a percentage.  Please re-enter.");
                udDiscount.Focus();
                return false;
            }
            return true;
        }
    }
}