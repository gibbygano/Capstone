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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        public static AddBooking Instance;
        public OrderManager myManager = new OrderManager();

        public List<com.WanderingTurtle.Common.ListItem> myEventList;
        public AddBooking()
        {
            myEventList = myManager.RetrieveListItemList();
            InitializeComponent();
            Instance = this;
            lvAddBookingListItems.ItemsSource = myEventList;
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
            string guest = tbAddBookingGuestID.Text;
            int itemListID;
            string selected;
            string quantity = tbAddBookingQuantity.Text;
            int eID, gID, qID;
            Booking myBooking;

            if (selectedItem() == false)
            {
                MessageBox.Show("Please select an event!");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            if (!Validator.ValidateInt(empID))
            {
                MessageBox.Show("Please review the Employee ID. Must be a three digit number.");
                btnAddBookingAdd.IsEnabled = true;
            }
            if (!Validator.ValidateInt(guest))
            {
                MessageBox.Show("Please review the Guest ID. Must be a three digit number.");
                btnAddBookingAdd.IsEnabled = true;
            }
            if (!Validator.ValidateInt(quantity))
            {
                MessageBox.Show("Please review the quantity entered. Must be a 2 digit number or less.");
                btnAddBookingAdd.IsEnabled = true;
            }
            else
            {
                selected = lvAddBookingListItems.SelectedItems[0].ToString();

                int.TryParse(empID, out eID);
                int.TryParse(guest, out gID);
                int.TryParse(selected, out itemListID); 
                int.TryParse(quantity, out qID);

                myBooking = new Booking(gID, eID, itemListID, qID);
                //calls to booking manager to add a booking. BookingID is auto-generated in database
                myManager.AddaBooking(myBooking);
         
                MessageBox.Show("The booking has been successfully added.");
                clearFields();
                btnAddBookingAdd.IsEnabled = true;

            }//end else
            

        }//end method addBooking()

        /*attempts to turn selected into a string object.
         * if successful, returns true.
         * if not returns false         */
        private bool selectedItem()
        {
            bool works = false;
            try
            {
                string selected = lvAddBookingListItems.SelectedItems[0].ToString();
                works = true;
                return works;

            }
            catch
            {
                return works;
            }
        }

        /*Sets form fields back to null after an add has been successfully completed
         * Tony Noel-2/11/15
         */

        
        public void clearFields()
        {
            tbAddBookingEmpID.Text = null;
            tbAddBookingGuestID.Text = null;
           
            tbAddBookingQuantity.Text = null;
        }

    }
}
