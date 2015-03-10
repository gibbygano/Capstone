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
        }

        /// <summary>
        /// Creates a new ItemListing to send to the database using user inputted values on this form.
        /// </summary>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (eventCbox.SelectedIndex > -1)
            {
                ItemListing newListing = new ItemListing();

                newListing.EventID = Int32.Parse(eventCbox.SelectedValue.ToString());
                newListing.SupplierID = Int32.Parse(supplierCbox.SelectedValue.ToString());
                // Tries to validate information to put into the newListing object.
                if (!Validator.ValidateDateTime(DateStart.Text + " " + txtStartTime.Text) || !Validator.ValidateDateTime(dateEnd.Text + " " + txtEndTime.Text) || (txtEndTime.Text=="00:00" && txtStartTime.Text =="00:00"))
                {
                    MessageBox.Show("Your dates and/or times are wrong");
                    return;
                }
                else
                {
                    newListing.StartDate = DateTime.Parse(DateStart.Text + " " + txtStartTime.Text);
                    newListing.EndDate = DateTime.Parse(DateStart.Text + " " + txtEndTime.Text);
                }

                if (!Validator.ValidateDecimal(txtPrice.Text) || txtPrice.Text=="00.00")
                {
                    MessageBox.Show("Your price is not formatted correctly. Please use the ##.## format.");
                    return;
                }
                else
                {
                    newListing.Price = Decimal.Parse(txtPrice.Text);
                }

                if (!Validator.ValidateInt(txtSeats.Text) || Int32.Parse(txtSeats.Text) < 0)
                {
                        MessageBox.Show("The number of seats available at your event is incorrectly formatted");
                        return;
                }
                else
                {
                    newListing.MaxNumGuests = Int32.Parse(txtSeats.Text);
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
            else
            {
                MessageBox.Show("Please select an Event to List!");
            }
        }
    }
}
