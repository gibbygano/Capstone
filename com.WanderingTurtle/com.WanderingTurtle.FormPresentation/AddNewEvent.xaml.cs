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
            
            //cboxType.ItemsSource = TempList;
            InitializeComponent();
            cboxType.ItemsSource = TempList;
        }

        /// <summary>
        /// Constructs and submits a new event using a form filled out by a user
        /// </summary>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var eventToSubmit = new Event();
            eventToSubmit.EventItemName = txtEventName.Text;

            try
            {
                // Number of Guests //
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

                // Price //
                if (!Validator.ValidateDecimal(this.txtPrice.Text))
                {
                    throw new Exception("Not a valid Price");
                }
                else
                {
                    eventToSubmit.PricePerPerson = Convert.ToDecimal(txtPrice);
                }
                
                // Start + End date //
                if (!Validator.ValidateDateTime(DateStart.Text + txtStartTime.Text) || !Validator.ValidateDateTime(dateEnd.Text + txtEndTime.Text))
                {
                    throw new Exception("Your dates are wrong");
                }
                else
                {
                    eventToSubmit.EventStartDate = DateTime.Parse(DateStart.Text + txtStartTime.Text);
                    eventToSubmit.EventEndDate = DateTime.Parse(dateEnd.Text + txtEndTime.Text);
                }

                // On-site //
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

                // Provided transport //
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

                int TypeSelected = cboxType.SelectedIndex;

                eventToSubmit.EventTypeID = (int)cboxType.SelectedValue;

                myMan.AddNewEvent(eventToSubmit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
