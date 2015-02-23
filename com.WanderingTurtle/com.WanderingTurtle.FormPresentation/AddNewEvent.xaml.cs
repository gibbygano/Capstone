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
        private EventManager myMan = new EventManager();

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Initializes the form and populates a combobox with event types. 
        /// </summary>
        public AddNewEvent()
        {
            InitializeComponent();
            List<EventType> TempList = myMan.RetrieveEventTypeList();

            try
            {
                cboxType.Items.Clear();
                cboxType.ItemsSource = TempList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Constructs an Event to add to the database, based on validated user input.
        /// Updated to no longer include Dates for events.
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
                    MessageBox.Show("Not a valid amount of guests");
                    return;
                }

                else if (Int32.Parse(txtMaxGuest.Text) > Int32.Parse(txtMinGuest.Text))
                {
                    if (Int32.Parse(txtMinGuest.Text) > 0)
                    {
                        int x;
                        int y;
                        int.TryParse(txtMaxGuest.Text, out x);
                        int.TryParse(txtMinGuest.Text, out y);
                        eventToSubmit.MaxNumGuests = x;
                        eventToSubmit.MinNumGuests = y;
                    }
                    else
                    {
                        MessageBox.Show("Your minimum guests don't make sense!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Your minimum and maximum guests don't make sense!");
                    return;
                }

                // Price //
                if (!Validator.ValidateDecimal(this.txtPrice.Text))
                {
                    MessageBox.Show("Not a valid Price");
                    return;
                }
                else
                {
                    eventToSubmit.PricePerPerson = Convert.ToDecimal(txtPrice.Text);
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
                    MessageBox.Show("Please fill in the on site field");
                    return;
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
                    MessageBox.Show("Please fill out the Transportation field");
                    return;
                }

                eventToSubmit.Description = txtDescrip.Text;

                try
                {
                    int TypeSelected = cboxType.SelectedIndex;
                }
                catch(Exception)
                {
                    MessageBox.Show("Please select an event type.");
                }
                if (cboxType.SelectedIndex > -1)
                {
                    eventToSubmit.EventTypeID = (int)cboxType.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Please select an event type!");
                    return;
                }
                if(String.IsNullOrEmpty(txtEventName.Text))
                {
                    MessageBox.Show("Please enter an event name.");
                    return;
                }

                if(myMan.AddNewEvent(eventToSubmit) == 1)
                {
                    MessageBox.Show("Successfully Added Event");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Clears text fields when they are selected by the mouse.
        /// </summary>
        private void clicktxtField(object sender, MouseEventArgs e)
        {
            var newTxtBox = (TextBox)sender;
            newTxtBox.Text = "";
            sender = newTxtBox;
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Clears text fields when they become focused by the Keyboard.
        /// </summary>
        private void GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var newTxtBox = (TextBox)sender;
            newTxtBox.Text = "";
            sender = newTxtBox;
        }
    }
}
