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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        List<ListItemObject> myEventList = new List<ListItemObject>();
        InvoiceDetails inInvoice;

        EmployeeManager _employeeManager = new EmployeeManager();
        OrderManager _orderManager = new OrderManager();
        //public ItemListing updatedItem;
        public ItemListing originalItem;

        public AddBooking(InvoiceDetails inInvoice)
        {
            this.inInvoice = inInvoice;

            InitializeComponent();
            RefreshListItems();

            lblAddBookingGuestName.Content = inInvoice.GetFullName;
        }

        private void RefreshListItems()
        {
            lvEventListItems.ItemsPanel.LoadContent();

            try
            {
                myEventList = _orderManager.RetrieveListItemList();
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

        private void btnAddBookingAdd_Click(object sender, RoutedEventArgs e)
        {
            addBooking();           
        }

        /*addBooking()- a method to collect all information from the form and turn them into strings
         * Then after taking each variable and testing them in their specific validation method, parses them 
         * into the correct variable needed to be stored as a booking and line item object.
         * 
         * TOny Noel- 2/11/15
         */
        public void addBooking()
        {
            ListItemObject selected;
            int gID;
            Booking myBooking;
            decimal extendedPrice, totalPrice, discount;

            int qty = (int)(udAddBookingQuantity.Value);

            DateTime myDate = DateTime.Now;            
            
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
                System.Windows.MessageBox.Show("This event is full.  Please pick another event");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }

            //get discount from form
            discount = (decimal)(udDiscount.Value);

            try
            {
                extendedPrice = _orderManager.calcExtendedPrice(selected.Price, qty);

                totalPrice = _orderManager.calcTotalCharge(discount, extendedPrice);

int eID = 101;
//TBD SET TO USER TOKEN - eID = (int)Globals.UserToken.EmployeeID;
             gID = inInvoice.HotelGuestID;

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
                     System.Windows.MessageBox.Show("Numguests changed");                     
                 }

                 System.Windows.MessageBox.Show("The booking has been successfully added.");
                 // closes window after add
                 this.Close();
             }
         } //end try
         catch (Exception ax)
         {
             System.Windows.MessageBox.Show(ax.Message);
         }

        }//end method addBooking()

        /*method to create ListItemObject from listView
         * returns the selected item.
         * Tony Noel 2/18/15
         */
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
        /// Adds the event description to the UI
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

            lblTicketWithDiscount.Content = _orderManager.calcTicketWithDiscount((decimal)(udDiscount.Value), myItemObject.Price);
            decimal extendedPrice = _orderManager.calcExtendedPrice(myItemObject.Price, (int)(udAddBookingQuantity.Value));
            lblTotalWithDiscount.Content = _orderManager.calcTotalCharge((decimal)(udDiscount.Value), extendedPrice);
        }

    }
}
