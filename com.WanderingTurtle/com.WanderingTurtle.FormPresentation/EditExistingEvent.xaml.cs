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
    /// Interaction logic for EditExistingEvent.xaml
    /// </summary>
    public partial class EditExistingEvent : Window
    {
        public EditExistingEvent()
        {
            InitializeComponent();
        }
        Event Unrevised = new Event();
        EventManager myMan = new EventManager();
        /// <summary>
        /// Populates the forms fields for editing.
        /// </summary>
        /// <param name="EventToEdit">The Event we are going to edit</param>
        public EditExistingEvent(Event EventToEdit)
        {
            txtEventName.Text = EventToEdit.EventItemName;
            InitializeComponent();

            txtMaxGuest.Text = EventToEdit.MaxNumGuests.ToString();
            txtMinGuest.Text = EventToEdit.MinNumGuests.ToString();

            DateStart.Text = EventToEdit.EventStartDate.ToString();
            dateEnd.Text = EventToEdit.EventEndDate.ToString();
            
            txtPrice.Text = EventToEdit.PricePerPerson.ToString();
            rtxtDescrip.AppendText(EventToEdit.Description);


            var TempList = myMan.RetrieveEventTypeList();
            cboxType.ItemsSource = TempList;
            cboxType.SelectedItem = EventToEdit.EventTypeID;


            if(EventToEdit.Transportation == true)
            {
                radTranspYes.IsChecked = true;
            }
            else
            {
                radTranspNo.IsChecked = true;
            }

            if(EventToEdit.OnSite == true)
            {
                radOnSiteYes.IsChecked = true;
            }
            else
            {
                radOnSiteNo.IsChecked =  true;
            }
            
        }
        /// <summary>
        /// Creates a new event to replace the old version of the EventToEdit
        /// </summary>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var eventToSubmit = new Event();
            eventToSubmit.EventItemName = txtEventName.Text;

            //Checks and instantiates the minimum and maximum guest numbers
            try
            {
                // Number Of Guests //
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
                if (!Validator.ValidateDecimal(txtPrice.Text))
                {
                    throw new Exception("Not a valid Price");
                }
                else
                {
                    eventToSubmit.PricePerPerson = Convert.ToDecimal(txtPrice);
                }

                // Date Start + End //
                if (!Validator.ValidateDateTime(DateStart.Text + txtStartTime.Text) || !Validator.ValidateDateTime(dateEnd.Text + txtEndTime.Text))
                {
                    throw new Exception("Your dates are wrong");
                }
                else
                {
                    eventToSubmit.EventStartDate = DateTime.Parse(DateStart.Text + txtStartTime.Text);
                    eventToSubmit.EventEndDate = DateTime.Parse(dateEnd.Text + txtEndTime.Text);
                }

                // On-Site //
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

                // Transportation //
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

                // Submit the events
                myMan.EditEvent(Unrevised, eventToSubmit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
