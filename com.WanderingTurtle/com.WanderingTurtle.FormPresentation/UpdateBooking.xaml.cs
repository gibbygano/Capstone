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
    /// Interaction logic for UpdateBooking.xaml
    /// </summary>
    public partial class UpdateBooking : Window
    { /*  
        //public static UpdateBooking Instance;
        //OrderManager up = new OrderManager();
        //string id;
        //int counter;

        //public UpdateBooking(string id)
        //{
        //    InitializeComponent();
        //    Instance = this;
        //    this.id = id;
        //    load();
        //}

        //private void UpdateBooking1_Loaded(object sender, RoutedEventArgs e)
        //{

            
        //}
        ///*Method to set the fields to the records initial content
        // * Uses the string id passed to the form, tryparses an int and uses the OrderManager 
        // * to locate a booking and BookinglineItem record, then places the string information in the corresponding fields
        // * Tony Noel- 2/12/15
        // */
        //private void load()
        //{
        //    int id;
        //    int.TryParse(this.id, out id);
        //    var editBooking = up.RetrieveBooking(id);
        //    lblUpBookingBookingID.Content = editBooking.BookingID.ToString();
        //    tbUpBookingEmpID.Text = editBooking.EmployeeID.ToString();
        //    tbUpBookingGuestID.Text = editBooking.GuestID.ToString();
        //    lblUpBookingDate1.Content = editBooking.DateBooked.ToString();

        //    /*var editBookingLI= update.RetrieveBookingLineItem(id);
        //     * lblUpBookingItemID1.Content = editBookingLI.ItemID.toString();
        //     * tbUpBookingQuantity.Text = editBookingLI.Quantity.toString();
        //     * 
        //     */
        //    comboUpBookingItem.IsEnabled = false;
        //    dtpUpBooking.IsEnabled = false;
        //    checkUpItemID.IsChecked = false;
        //    checkUpDate.IsChecked = false;
          
        //}
        ///*Event handler for when the checkUpItemID is checked
        // * will enable the combobox to select the new item and hide the label containing the old information 
        // * uses a counter to prevent the list from being loaded more than once
        // * Tony Noel- 2/12/15
        // */
        //private void checkUpItemID_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (checkUpItemID.IsChecked == true)
        //    {
        //        comboUpBookingItem.IsEnabled = true;
        //        lblUpBookingItemID1.Opacity = 0;
        //        if (counter == 0)
        //        {
        //            loadItems();
        //            counter++;
        //        }
        //    }
        //    else
        //    {
        //        comboUpBookingItem.IsEnabled = false;
        //        lblUpBookingItemID1.Opacity = 100;
        //    }
        //}

        //private void loadItems()
        //{
        //    //code here would load lineitem- itemID's, need order manager lineItem access

        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    string bookingID = lblUpBookingBookingID.Content.ToString();
        //    string empID = tbUpBookingEmpID.Text;
        //    string guest = tbUpBookingGuestID.Text;
        //    string itemID = lblUpBookingItemID1.Content.ToString();
        //    string quantity = tbUpBookingQuantity.Text;
        //    string date = lblUpBookingDate1.Content.ToString();

        //    if (checkUpItemID.IsChecked == true)
        //    {
        //        itemID = comboUpBookingItem.Text;
        //    }
        //    if (checkUpDate.IsChecked == true)
        //    {
        //        date = dtpUpBooking.Text;
        //    }
            
        //    int bID, eID, gID, iID, qID;
        //    DateTime d;
        //    Booking myBooking;

        //    btnUpBooking.IsEnabled = false;
        //    if (!Validator.ValidateInt(empID))
        //    {
        //        MessageBox.Show("Please review the Employee ID. Must be a three digit number.");
        //        btnUpBooking.IsEnabled = true;
        //    }
        //    if (!Validator.ValidateInt(guest))
        //    {
        //        MessageBox.Show("Please review the Guest ID. Must be a three digit number.");
        //        btnUpBooking.IsEnabled = true;
        //    }
        //    if (!Validator.ValidateInt(itemID))
        //    {
        //        MessageBox.Show("Please review the Item ID.");
        //        btnUpBooking.IsEnabled = true;
        //    }
        //    if (!Validator.ValidateInt(quantity))
        //    {
        //        MessageBox.Show("Please review the quantity entered. Must be a 2 digit number or less.");
        //        btnUpBooking.IsEnabled = true;
        //    }
        //    if (!Validator.ValidateDateTime(date))
        //    {
        //        MessageBox.Show("Please review the date.");
        //        btnUpBooking.IsEnabled = true;
        //    }
        //    else
        //    {
        //        int.TryParse(bookingID, out bID);
        //        int.TryParse(empID, out eID);
        //        int.TryParse(guest, out gID);
        //        int.TryParse(itemID, out iID);
        //        int.TryParse(quantity, out qID);
        //        DateTime.TryParse(date, out d);
        //        myBooking = new Booking(bID, gID, eID, d);
        //        //calls to booking manager to update a booking. 
        //        up.EditBooking(myBooking);
                
        //        //needs bookingline to be added to the orderManager- would pass the three ints to manager to be
        //        //turned into a bookingLineItem object and edited in database.
        //        //ex: up.EditBookingLineItem(bID, iID, qID);

        //        //Need LineItems to be added to order manager.
        //        MessageBox.Show("The booking has been successfully edited.");
        //        btnUpBooking.IsEnabled = true;
                
                

        //    }//end else
        //}
    }
}
