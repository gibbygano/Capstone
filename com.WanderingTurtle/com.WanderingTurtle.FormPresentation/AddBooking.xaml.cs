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
        public List<ListItemObject> myEventList;
        public InvoiceDetails inInvoice;
        public ProductManager addBookingProdManager = new ProductManager();
        public ItemListing updatedItem;
        public ItemListing originalItem;

        public AddBooking(InvoiceDetails inInvoice)
        {


            this.inInvoice = inInvoice;

            myEventList = OrderManager.RetrieveListItemList();

            InitializeComponent();

            foreach (ListItemObject lIO in myEventList)
            {
                originalItem = addBookingProdManager.RetrieveItemListing(lIO.ItemListID.ToString());

                lIO.QuantityOffered = availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests);
            }

            lvAddBookingListItems.ItemsSource = myEventList;

            lblAddBookingGuestName.Content = inInvoice.GetFullName;

            
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
            string empID = tbAddBookingEmpID.Text;
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


            if (lvAddBookingListItems.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Please select an event.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }

            selected = getSelectedItem();

            originalItem = addBookingProdManager.RetrieveItemListing(selected.ItemListID.ToString());

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
            if (okQuantity(quantity, availableQuantity(originalItem.MaxNumGuests, originalItem.CurrentNumGuests)) == false || qID <= 0)
            {
                MessageBox.Show("Please review the quantity entered:" +
                    " \nMust be a positive number and cannot excede the quantity available for the event.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            
         try
         {
             extendedPrice = calcExtendedPrice(selected.Price, discount);

             totalPrice = calcTotalPrice(qID, extendedPrice); 
                
             int.TryParse(empID, out eID);
             //eID = (int)Globals.UserToken.EmployeeID;
             gID = inInvoice.HotelGuestID;
             //This method call needs to be updated to include a calculated extended price and total charge to be added to the database.
             myBooking = new Booking(gID, eID, selected.ItemListID, qID, myDate, selected.Price, extendedPrice, discount, totalPrice);

             updatedItem = originalItem;

             updatedItem.CurrentNumGuests = originalItem.CurrentNumGuests + qID;

             //addBookingProdManager.EditItemListing(originalItem, updatedItem);

             //calls to booking manager to add a booking. BookingID is auto-generated in database                
            int result = OrderManager.AddaBooking(myBooking);

             if (result == 1)
             {
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
            ListItemObject selected = (ListItemObject)lvAddBookingListItems.SelectedItems[0];
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
                EmployeeManager.FetchEmployee(empID);
                works = true;
                return works;

            }
            catch
            {
                return works;
            }
        }

        private decimal calcExtendedPrice(decimal price, decimal discount)
        {
            decimal extendedPrice;

            extendedPrice = ((100 - discount) / 100) * price;

            return extendedPrice;
        }

        private decimal calcTotalPrice(int quantity, decimal extendedPrice)
        {
            return (decimal)quantity * extendedPrice;
        }

        private int availableQuantity(int maxQuantity, int currentQuantity)
        {
            int availableQuantity;

            availableQuantity = maxQuantity - currentQuantity;

            return availableQuantity;
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

//        /*Sets form fields back to null after an add has been successfully completed
//         * Tony Noel-2/11/15
//         */
        
//        public void clearFields()
//        {
//            tbAddBookingEmpID.Text = null;
////tbAddBookingGuestID.Text = null;
           
//            tbAddBookingQuantity.Text = null;
//        }

        // Pat Banks - February 19, 2015
        // Parameters: returns list data
        // Desc.: Defines employee roles for the combo box
        // Failure: none
        // Success: box is filled and available for use on the form

        private List<HotelGuest> RetrieveGuestList()
        {
            List<HotelGuest> dropDownData = new List<HotelGuest>();
            dropDownData = HotelGuestManager.GetHotelGuestList();

            return dropDownData;
        }
    }
}
