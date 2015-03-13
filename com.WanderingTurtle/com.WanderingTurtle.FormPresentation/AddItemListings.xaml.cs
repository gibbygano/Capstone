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
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

// Worked on by:
///Hunter
////Fritz
/////Matthew 10:15
namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddItemListings.xaml
    /// </summary>
    public partial class AddItemListing 
    {
        EventManager myMan = new EventManager();

        SupplierManager mySupMan = new SupplierManager();

        ProductManager prodMan = new ProductManager();
        
        //populates our Combo box for the user to pick from
        public AddItemListing()
        {

            InitializeComponent();
            List<Event> myList = myMan.RetrieveEventList();
            try
            {
                eventCbox.Items.Clear();
                eventCbox.ItemsSource = myList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            List<Supplier> mySupplierList = mySupMan.RetrieveSupplierList();
            try
            {
                supplierCbox.Items.Clear();
                supplierCbox.ItemsSource = mySupplierList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Now);
            dateStart.BlackoutDates.Add(cdr);
            dateEnd.BlackoutDates.Add(cdr);
        }

        /// <summary>
        /// Creates a new ItemListing to send to the database using user inputted values on this form.
        /// </summary>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            addItemListing();

        }

        private void addItemListing()
        {
            ItemListing newListing = null;

            if (eventCbox.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Please select an Event to List!");
                return;
            }

            if (supplierCbox.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Please select a supplier!");
                return;
            }

            if (dateStart.Text == null || dateEnd.Text == null)
            {
                MessageBox.Show("Please select a date");
                return;
            }

            if (tpStartTime.Value == null || tpEndTime.Value == null)
            {
                MessageBox.Show("Please select a time");
                return;
            }

            if (udPrice.Value == 0)
            {
                MessageBox.Show("Please indicate a price for tickets");
                return;
            }

            if (udSeats.Value == 0)
            {
                MessageBox.Show("Please indicate number of seats for the event");
                return;
            }

            try
            {
                DateTime formStartDate = (DateTime)(dateStart.SelectedDate);
                DateTime formEndDate = (DateTime)(dateEnd.SelectedDate);

                DateTime formStartTime = (DateTime)(tpStartTime.Value);
                DateTime formEndTime = (DateTime)(tpEndTime.Value);

                newListing.EventID = Int32.Parse(eventCbox.SelectedValue.ToString());
                newListing.SupplierID = Int32.Parse(supplierCbox.SelectedValue.ToString());

                //date is your existing Date object, time is the nullable DateTime object from your TimePicker
                newListing.StartDate = DateTime.Parse(string.Format("{0} {1}", formStartDate.ToShortDateString(), formStartTime.ToLongTimeString()));
                newListing.EndDate = DateTime.Parse(string.Format("{0} {1}", formEndDate.ToShortDateString(), formEndTime.ToLongTimeString()));

                newListing.Price = (decimal)(udPrice.Value);
                newListing.MaxNumGuests = (int)(udSeats.Value);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                prodMan.AddItemListing(newListing);
                MessageBox.Show("Listing successfully added!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error adding the Item Listing.");
            }
        }
    }
}
