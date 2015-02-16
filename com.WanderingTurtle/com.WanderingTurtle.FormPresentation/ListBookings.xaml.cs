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
        public ListBookings()
        {
            InitializeComponent();
        }
        /*Code to link to the AddBooking form
         * Opens the AddBooking form when the "Add" button on the list screen is selected.
         * Tony Noel- 2/15/15
         */
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            AddBooking myBooking;

            if (AddBooking.Instance == null)
            {
                myBooking = new AddBooking();
                myBooking.Show();
            }
            else
            {
                myBooking = AddBooking.Instance;
                myBooking.Activate();

                //Creates a sound effect through the System.Media and  flash from accessibility

                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void lvEmployeesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void btnUpdateBooking_Click(object sender, RoutedEventArgs e)
        //{
        //    UpdateBooking myBooking;
        //    string id = lvBookingList.SelectedItems[0].ToString();
        //    if (UpdateBooking.Instance == null)
        //    {
        //        myBooking = new UpdateBooking(id);
        //        myBooking.Show();
        //    }
        //    else
        //    {
        //        myBooking = UpdateBooking.Instance;
        //        myBooking.BringToFront();

        //        //Creates a sound effect through the System.Media and  flash from accessibility

        //        System.Media.SystemSounds.Exclamation.Play();
        //    }
        //}
    }
}
