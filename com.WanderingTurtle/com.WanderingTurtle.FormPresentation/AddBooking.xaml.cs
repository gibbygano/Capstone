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
using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking 
    {
        List<ListItemObject> myEventList = new List<ListItemObject>();
        InvoiceDetails inInvoice;
        int eID;
        EmployeeManager _employeeManager = new EmployeeManager();
        OrderManager _orderManager = new OrderManager();
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
            eID = (int)Globals.UserToken.EmployeeID;
            lblAddBookingGuestName.Content = inInvoice.GetFullName;
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
                myEventList = _orderManager.RetrieveListItemList();

                //calculating the quantity of available tickets for each listing
                foreach (ListItemObject lIO in myEventList)
                {
                    lIO.QuantityOffered = _orderManager.availableQuantity(lIO.MaxNumGuests, lIO.CurrentNumGuests);
                }
                lvEventListItems.ItemsSource = myEventList;
                lvEventListItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Unable to retrieve Hotel Guest listing from the database. \n" + ex.Message);
            }

        }

        /// <summary>
        /// Created by TOny Noel- 2/11/15
        /// Handles click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBookingAdd_Click(object sender, RoutedEventArgs e)
        {
            addBooking();           
        }

        /// <summary>
        /// Created by TOny Noel- 2/11/15
        /// addBooking()- a method to collect all information from the form
        /// Then after taking each variable and testing them in their specific validation method, parses them 
        /// into the correct variable needed to be stored as a listItemObject
        /// </summary>
        /// <remarks>
        /// Updated by:  Pat Banks 2015/03/11
        /// Added up/down controls to allow for easier user data entry
        /// </remarks>
        public void addBooking()
        {
            ListItemObject selected;
            int gID;
            Booking myBooking;
            decimal extendedPrice, totalPrice, discount;

            //gets quantity from the up/down quantity field
            int qty = (int)(udAddBookingQuantity.Value);      
            
            if (lvEventListItems.SelectedIndex.Equals(-1))
            {
                System.Windows.MessageBox.Show("Please select an event.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }

            selected = getSelectedItem();
            originalItem = _orderManager.RetrieveEventListing(selected.ItemListID);

            if (qty == 0)
            {
                System.Windows.MessageBox.Show("Please enter a quanity greather than 0");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }

            //get discount from form
            discount = (decimal)(udDiscount.Value);

            try
            {
                extendedPrice = _orderManager.calcExtendedPrice(selected.Price, qty);
                totalPrice = _orderManager.calcTotalCharge(discount, extendedPrice);            

                 gID = inInvoice.HotelGuestID;
             
                 DateTime myDate = DateTime.Now;   
                 myBooking = new Booking(gID, eID, selected.ItemListID, qty, myDate, selected.Price, extendedPrice, discount, totalPrice);

                 //calls to booking manager to add a booking. BookingID is auto-generated in database                
                 int result = _orderManager.AddaBooking(myBooking);

                 if (result == 1)
                 {
                     //change quantity of guests
                     int updatedGuests = originalItem.CurrentNumGuests + qty;

                     int result2 = _orderManager.updateNumberOfGuests(originalItem.ItemListID, originalItem.CurrentNumGuests, updatedGuests);
                     if (result2 == 1)
                     {
                         System.Windows.MessageBox.Show("The booking has been successfully added.");                    
                     }
                     // closes window after add
                     this.Close();
                 }
         } //end try
         catch (Exception ax)
         {
             System.Windows.MessageBox.Show(ax.Message);
         }

        }//end method addBooking()


        /// <summary>
        /// Tony Noel 2/18/15
        /// method to create ListItemObject from listView
        /// </summary>
        /// <returns>returns the selected item.</returns>
        private ListItemObject getSelectedItem()
        {
            ListItemObject selected = (ListItemObject)lvEventListItems.SelectedItem;

            if (selected== null)
            {
                System.Windows.MessageBox.Show("Please select an event.");
            }
            return selected;
        }

        /// <summary>
        /// Created by Pat Banks 2015/03/09
        /// 
        /// Adds the event description to the UI when the listView Item changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEventListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListItemObject myItemObject = getSelectedItem();

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
        /// Created by Pat Banks 2015/03/11
        /// 
        /// updates the total cost with discount
        /// </summary>
        /// <param name="myItemObject"></param>
        private void refreshCostsToDisplay(ListItemObject myItemObject)
        {
            //total cost calculations
            if (myItemObject != null)
            {
                decimal extendedPrice = _orderManager.calcExtendedPrice(myItemObject.Price, (int)(udAddBookingQuantity.Value));
                lblTotalWithDiscount.Content = _orderManager.calcTotalCharge((decimal)(udDiscount.Value), extendedPrice);
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
            ListItemObject myItemObject = getSelectedItem();
            refreshCostsToDisplay(myItemObject);
        }

    }
}
