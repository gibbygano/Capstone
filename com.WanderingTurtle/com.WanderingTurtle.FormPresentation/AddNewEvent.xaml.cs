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
    /// Interaction logic for AddNewEvent.xaml
    /// </summary>
    public partial class AddNewEvent : Window
    {
        EventManager myMan = new EventManager();
        public AddNewEvent()
        {

            var TempList = myMan.RetrieveEventTypeList();

            cboxType.ItemsSource = TempList;



            InitializeComponent();
        }

        /// <summary>
        // Constructs and submits a new event using a form filled out by a user
        //
        // Pre requesits: Form is completely filled out. 
        // Post requesits: Using user inputted information, we submit an event to the database for storage.
        /// </summary>

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var eventToSubmit = new Event();


            eventToSubmit.EventItemName = txtEventName.Text;

            //Checks and instantiates the minimum and maximum guest numbers
            try
            {

                if (!Validator.ValidateInt(txtMaxGuest.Text) || !Validator.ValidateInt(txtMinGuest.Text))
                {
                    throw new Exception("Not a valid amount of guests");

                }
                else
                {
                    int x;
                    int y;
                    int.TryParse(txtMaxGuest.Text, out x);
                    int.TryParse(txtMaxGuest.Text, out y);

                    eventToSubmit.MaxNumGuests = x;
                    eventToSubmit.MinNumGuests = y;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            // Checks and instantiates the Price of tickets.
            try
            {

                if (!Validator.ValidateDecimal(this.txtPrice.Text))
                {
                    throw new Exception("Not a valid Price");
                }
                else
                {
                    eventToSubmit.PricePerPerson = Convert.ToDecimal(txtPrice);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
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
                    eventToSubmit.EventStartDate = DateTime.Parse(DateStart.Text + txtStartTime.Text);
                    eventToSubmit.EventEndDate = DateTime.Parse(dateEnd.Text + txtEndTime.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            // Checks the radio buttons for on site
            try
            {
                if (radOnSiteYes.IsChecked == true)
                {
                    eventToSubmit.OnSite = true;
                }
                else if (radOnSiteNo.IsChecked == true)
                {
                    eventToSubmit.OnSite = false;
                }
                else
                {
                    throw new Exception("Please fill in the on site field");
                }
            }
            catch (Exception tf)
            {
                throw tf;
            }

            // Checks the radio buttons for transportation
            try
            {

                if (radTranspNo.IsChecked == true)
                {
                    eventToSubmit.Transportation = false;
                }
                else if (radTranspYes.IsChecked == true)
                {
                    eventToSubmit.Transportation = true;
                }
                else
                {
                    throw new Exception("Please fill out the Transportation field");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            int TypeSelected = cboxType.SelectedIndex;

            eventToSubmit.EventTypeID = (int)cboxType.SelectedValue;

            // BLL SubmitEvent here.
                // case here for eventTypes

            myMan.AddNewEvent(eventToSubmit); 

        }
            

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
             
    }
}
