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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        List<ListItemObject> myEventList;
        InvoiceDetails inInvoice;
        ProductManager _productManager = new ProductManager();
        EmployeeManager _employeeManager = new EmployeeManager();
        OrderManager _orderManager = new OrderManager();
        //public ItemListing updatedItem;
        public ItemListing originalItem;

        public AddBooking(InvoiceDetails inInvoice)
        {
            this.inInvoice = inInvoice;
            InitializeComponent();

            RefreshListItems();
            lvEventListItems.ItemsSource = myEventList;
            lblAddBookingGuestName.Content = inInvoice.GetFullName;
        }

        private void RefreshListItems()
        {
            myEventList = _orderManager.RetrieveListItemList();

            foreach (ListItemObject lIO in myEventList)
            {
                originalItem = _productManager.RetrieveItemListing(lIO.ItemListID.ToString());

                lIO.QuantityOffered = OrderManager.availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests);
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
            string empID = "101";
            ListItemObject selected;
            DateTime myDate = DateTime.Now;
            string quantity = tbAddBookingQuantity.Text;
            int eID, gID, qID, discount;
            Booking myBooking;
            decimal extendedPrice, totalPrice;

            if (isEmp(empID) == false)
            {
                MessageBox.Show("Please review the Employee ID. A record of this employee is not on file.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }


            if (lvEventListItems.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Please select an event.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }

            selected = getSelectedItem();

            originalItem = _productManager.RetrieveItemListing(selected.ItemListID.ToString());

            int.TryParse(tbAddBookingDiscount.Text, out discount);

            if (tbAddBookingDiscount.Text == null)           
            {
                discount = 0;
            }
            else if (discount > 100 )
            {
                MessageBox.Show("Discount cannot be greater than 100%. \nPlease enter a different discount.");
                tbAddBookingDiscount.Clear();
                tbAddBookingDiscount.Focus();
                return;
            }

            int.TryParse(quantity, out qID);
            //Quantity field on the table needs to be a calculated field in order for this to work.- ItemListing has a max#guest field and a Current#guest field that will be used to calculate quantity
            if (okQuantity(quantity, OrderManager.availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests)) == false || qID <= 0)
            {
                MessageBox.Show("Please review the quantity entered:" +
                    " \nMust be a positive number and cannot excede the quantity available for the event.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            
         try
         {
             extendedPrice = OrderManager.calcExtendedPrice(selected.Price, discount);

             totalPrice = OrderManager.calcTotalPrice(qID, extendedPrice);

             eID = 100;
//TBD SET TO USER TOKEN - eID = (int)Globals.UserToken.EmployeeID;
             gID = inInvoice.HotelGuestID;

             myBooking = new Booking(gID, eID, selected.ItemListID, qID, myDate, selected.Price, extendedPrice, discount, totalPrice);

             //calls to booking manager to add a booking. BookingID is auto-generated in database                
             int result = _orderManager.AddaBooking(myBooking);

             if (result == 1)
             {
                 //change quantity of guests
                 int updatedGuests = originalItem.CurrentNumGuests + qID;
                 int result2 = _orderManager.updateNumberOfGuests(originalItem.ItemListID, originalItem.CurrentNumGuests, updatedGuests);
                 if (result2 == 1)
                 {
                     MessageBox.Show("Numguests changed");                     
                 }

                 MessageBox.Show("The booking has been successfully added.");
                 // closes window after add
                 this.Close();
             }
         } //end try
         catch (Exception ax)
         {
             MessageBox.Show(ax.Message);
         }

        }//end method addBooking()

        /*method to create ListItemObject from listView
         * returns the selected item.
         * Tony Noel 2/18/15
         */
        private ListItemObject getSelectedItem()
        {
            ListItemObject selected = (ListItemObject)lvEventListItems.SelectedItems[0];
            return selected;
        }

        /**
         * validates that a empID entered is an int,
         * parses it into one and passes it through EmployeeHandler to check against database
         * if emp is found, returns true.
         * Else it returns false.
         * Tony Noel- 2/17/15
         */
        private bool isEmp(string emp)
        {
            bool works = false;
            int empID;

            if (emp == "")
            {
                return works;
            }

            try
            {
                Validator.ValidateInt(emp);
                int.TryParse(emp, out empID);
                _employeeManager.FetchEmployee(empID);
                works = true;
                return works;

            }
            catch
            {
                return works;
            }
        }


        /*method to check a quantity
         * takes a string
         * if the string is successfully parsed, and the variable it parses to is less
         * than the myItem.QuantityOffered - return true
         * else return false.
         * Tony Noel 2/18/2015
         */
        private bool okQuantity(string quantity, int available)
        {
            bool works = false;
            int q;
            int.TryParse(quantity, out q);
            ListItemObject myItemObject = getSelectedItem();
            if (Validator.ValidateInt(quantity) == true && q <= available )
            {
                works = true;
                return works;
            }
            else 
            {
                return works;
            }

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
        }
    }
}
