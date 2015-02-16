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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        public static AddBooking Instance;
        public OrderManager myManager = new OrderManager();
        public AddBooking()
        {
            InitializeComponent();
            Instance = this;
            loadTypes();
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
            string quantity = tbAddBookingQuantity.Text;
            string date = dtpAddBooking.Text;
            int eID, gID, qID;
            DateTime d;
            Booking myBooking;

            if (comboAddBookingItem.Text != "")
            {
                itemListID = ((ListItem)comboAddBookingItem.SelectedItem).ItemListID;
            }
            else
            {
                MessageBox.Show("Please select an event from the drop down list.");
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
            if (!Validator.ValidateDateTime(date))
            {
                MessageBox.Show("Please review the date.");
                btnAddBookingAdd.IsEnabled = true;
            }
            else
            {
                int.TryParse(empID, out eID);
                int.TryParse(guest, out gID);
                int.TryParse(quantity, out qID);
                DateTime.TryParse(date, out d);
                myBooking = new Booking(gID, eID, d);
                //calls to booking manager to add a booking. BookingID is auto-generated in database
                myManager.AddaBooking(myBooking);
                //allows for retrieval of the booking just made inorder to collect the auto-generated BookingID 
                var getBooking = myManager.getSearchBooking(myBooking);

                //needs bookingline to be added to the orderManager- would pass the three ints to manager to be
                //turned into a bookingLineItem object and added to database.
                //ex: myManager.AddBookingLineItem(getBooking.BookingID, itemListID, qID);

                //Need LineItems to be added to order manager.
                MessageBox.Show("The booking has been successfully added.");
                clearFields();
                btnAddBookingAdd.IsEnabled = true;

            }//end else
            

        }//end method addBooking()

        /*Sets form fields back to null after an add has been successfully completed
         * Tony Noel-2/11/15
         */
        public void clearFields()
        {
            tbAddBookingEmpID.Text = null;
            tbAddBookingGuestID.Text = null;
            comboAddBookingItem.Text = null;
            tbAddBookingQuantity.Text = null;
        }

        private void loadTypes()
        {
            var bType = myManager.RetrieveBookingOpsList();
            comboAddBookingItem.DisplayMemberPath = "EventName";
            comboAddBookingItem.SelectedValue = "ItemListID";  //to get this int later use ((LaptopTypes)cbEditsType.SelectedItem).TypesID;
            for (int i = 0; i < bType.Count; i++)
            {
                this.comboAddBookingItem.Items.Add(bType[i]);
            }
        }
    }
}
