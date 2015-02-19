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
        public EmployeeManager myEmp = new EmployeeManager();
        public HotelGuestManager myGuest = new HotelGuestManager();

        public List<ListItemObject> myEventList;
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
            ListItemObject selected;
            DateTime myDate = DateTime.Now;
            string quantity = tbAddBookingQuantity.Text;
            int eID, gID, qID;
            Booking myBooking;

            if (selectedItem() == false)
            {
                MessageBox.Show("Please select an event!");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            if (isEmp(empID) == false)
            {
                MessageBox.Show("Please review the Employee ID. A record of this employee is not on file.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            if (isGuest(guest) == false)
            {
                MessageBox.Show("Please review the Guest ID. A record of this guest is not on file.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            if (okQuantity(quantity) == false)
            {
                MessageBox.Show("Please review the quantity entered. Must be a number and cannot excede the quantity offered for the event.");
                btnAddBookingAdd.IsEnabled = true;
                return;
            }
            else
            {
                selected = getSelectedItem();

                int.TryParse(empID, out eID);
                int.TryParse(guest, out gID);
                int.TryParse(quantity, out qID);
        
                myBooking = new Booking(gID, eID, selected.ItemListID, qID, myDate);
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
                getSelectedItem();
                works = true;
                return works;

            }
            catch
            {
                return works;
            }
        }
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
         * validates that a guest entered is an int,
         * parses it into one and passes it through HotelGuestHandler to check against database
         * if guest is found, returns true.
         * Else it returns false.
         * Tony Noel- 2/17/15
         */
        private bool isGuest(string guest)
        {
            bool works = false;
            int guestID;
            if (guest == "")
            {
                return works;
            }

            try
            {
                Validator.ValidateInt(guest);
                int.TryParse(guest, out guestID);
                myGuest.GetHotelGuest(guestID);
                works = true;
                return works;

            }
            catch
            {
                return works;
            }
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
                myEmp.FetchEmployee(empID);
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
        private bool okQuantity(string quantity)
        {
            bool works = false;
            int q;
            int.TryParse(quantity, out q);
            ListItemObject myItem = getSelectedItem();
            if (Validator.ValidateInt(quantity) == true && q <= myItem.QuantityOffered )
            {
                works = true;
                return works;
            }
            else 
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

        private void AddBooking1_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }

    }
}
