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
    /// Interaction logic for AddItemListings.xaml
    /// </summary>
    public partial class AddItemListing : Window
    {
        EventManager myMan = new EventManager();

        ProductManager prodMan = new ProductManager();

        public AddItemListing()
        {
            
            InitializeComponent();
        }

        public AddItemListing(Event EventToAddListingOf)
        {
            InitializeComponent();
            txtEventName.Text = EventToAddListingOf.EventItemName;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemListing newListing = new ItemListing();

            try
            {
                // Tries to validate information to put into the newListing object.
                if (!Validator.ValidateDateTime(DateStart.Text + txtStartTime.Text) || !Validator.ValidateDateTime(dateEnd.Text + txtEndTime.Text))
                {
                    throw new Exception("Your dates are wrong");
                }
                else
                {
                    newListing.StartDate = DateTime.Parse(DateStart.Text + txtStartTime.Text);
                    newListing.EndDate = DateTime.Parse(dateEnd.Text + txtEndTime.Text);
                }

                if(!Validator.ValidateDecimal(txtPrice.Text))
                {
                    throw new Exception("Your price is not formatted correctly. Please use the ##.## format.");
                }
                else
                {
                    newListing.Price = Decimal.Parse(txtPrice.Text);
                }

                if(!Validator.ValidateInt(txtSeats.Text))
                {
                    throw new Exception("The number of seats available at your event is incorrectly formatted");
                }
                else
                {
                    newListing.QuantityOffered = int.Parse(txtSeats.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            //BLL: As of 2/14/15 this method is missing. Must be added. 
            // Should accept Param: ItemListing
            // Adds ItemListing to the DB

            // Hunter Lind 2/14/15
            prodMan.AddItemListing(newListing);
        }
    }
}
