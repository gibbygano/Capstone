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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for SubmitEvent.xaml
    /// </summary>
    public partial class SubmitEvent : Window
    {
        EventManager myMan = new EventManager();
        productManager prodMan = new productManager();

        public SubmitEvent(String s)
        {
            txtEventName.Text = s;
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var ItemListing = new Event();


            ItemListing.EventItemName = txtEventName.Text;

            // Checks and instantiates the Price of tickets.
            try
            {

                if (!Validator.ValidateDecimal(txtPrice.Text))
                {
                    throw new Exception("Not a valid Price");
                }
                else
                {
                    ItemListing.PricePerPerson = Convert.ToDecimal(txtPrice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //Checks and insantiates the Date and Time of the event
            try
            {

                if (!Validator.ValidateDateTime(DateStart.Text + txtStartTime.Text) || !Validator.ValidateDateTime(dateEnd.Text + txtEndTime.Text))
                {
                    throw new Exception("Your dates are wrong");
                }
                else
                {
                    ItemListing.EventStartDate = DateTime.Parse(DateStart.Text + txtStartTime.Text);
                    ItemListing.EventEndDate = DateTime.Parse(dateEnd.Text + txtEndTime.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            // Need a method to submit the EventListing to the DB
            productMan.AddItemListing(ItemListing);

        }
    }
}
