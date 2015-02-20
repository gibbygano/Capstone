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
using System.Windows.Navigation;
using System.Windows.Shapes;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListBookings.xaml
    /// </summary>
    public partial class ListBookings : UserControl
    {
        private OrderManager myBookings = new OrderManager();
        List<Booking> bookingList;

        public ListBookings()
        {            
            InitializeComponent();
            RefreshBookingsList();
        }

        /*Code to link to the AddBooking form
         * Opens the AddBooking form when the "Add" button on the list screen is selected.
         * Tony Noel- 2/15/15
         */
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            AddBooking myBooking = new AddBooking();

            if (myBooking.ShowDialog() == false)
            {
                RefreshBookingsList();   
            }
        }
        private void btnRefreshList_Click(object sender, RoutedEventArgs e)
        {
            RefreshBookingsList();
        }

        private void RefreshBookingsList()
        {
            try
            {
                bookingList = myBookings.RetrieveBookingList();
                lvBookingList.ItemsSource = bookingList;
                lvBookingList.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve booking list from the database. \n" + ex.Message);
            }

        }

    }
}
