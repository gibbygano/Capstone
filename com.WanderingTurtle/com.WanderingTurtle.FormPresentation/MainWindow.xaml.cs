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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Employee userEmployee = new Employee();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // collect the values from the form
            userEmployee.EmployeeID = int.Parse(txtUserID.Text);
            userEmployee.Password = txtUserPassword.Text;

        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Window AddEmployee = new AddEmployee();
            AddEmployee.Show();
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            Window AddEditHotelGuest = new AddEditHotelGuest();
            AddEditHotelGuest.Show();
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            Window AddBooking = new AddBooking();
            AddBooking.Show();


            //AddBooking myBooking;

            //if (AddBooking.Instance == null)
            //{
            //    myBooking = new AddBooking();
            //    myBooking.Show();
            //}
            //else
            //{
            //    myBooking = AddBooking.Instance;
            //    myBooking.Activate();

            //    //Creates a sound effect through the System.Media and  flash from accessibility

            //    System.Media.SystemSounds.Exclamation.Play();
            //}
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Window AddEvent = new AddNewEvent();
            AddEvent.Show();
        }

        private void btnAddListing_Click(object sender, RoutedEventArgs e)
        {
            Window AddItemListings = new AddItemListing();
            AddItemListings.Show();
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Window AddSupplier = new AddSupplier();
            AddSupplier.Show();
        }

    }
}
