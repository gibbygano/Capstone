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
using com.WanderingTurtle.Common;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for EditExistingEvent.xaml
    /// </summary>
    public partial class EditExistingEvent
    {
        Event Unrevised = new Event();
        Event eventToSubmit = new Event();

        EventManager myMan = new EventManager();
        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// 
        /// Fills out our form with information from EventToEdit.
        /// Also saves an Unrevised version of EventToEdit.
        /// </summary>
        /// <param name="EventToEdit">The Event we are going to edit</param>
        public EditExistingEvent(Event EventToEdit)
        {
            InitializeComponent();
            Unrevised = EventToEdit;

            eventToSubmit.Active = EventToEdit.Active;
            eventToSubmit.Description = EventToEdit.Description;
            eventToSubmit.EventTypeID = EventToEdit.EventTypeID;
            eventToSubmit.OnSite = EventToEdit.OnSite;
            eventToSubmit.Transportation = EventToEdit.Transportation;
            eventToSubmit.EventItemName = EventToEdit.EventItemName;
            eventToSubmit.EventItemID = EventToEdit.EventItemID;

            List<EventType> myList;
            try
            {
                myList = myMan.RetrieveEventTypeList();
                cboxType.Items.Clear();
                cboxType.ItemsSource = myList;
                cboxType.DisplayMemberPath = "EventName";
                cboxType.SelectedValuePath = "EventTypeID";
                for (int i = 0; i < myList.Count; i++)
                {
                    if (myList[i].EventTypeID == EventToEdit.EventTypeID)
                    {
                        cboxType.SelectedValue = myList[i].EventTypeID;
                    }
                }
                //cboxType.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            txtEventName.Text = EventToEdit.EventItemName;
            txtDescrip.AppendText(EventToEdit.Description);

            if (EventToEdit.Transportation == true)
            {
                radTranspYes.IsChecked = true;
            }
            else
            {
                radTranspNo.IsChecked = true;
            }

            if (EventToEdit.OnSite == true)
            {
                radOnSiteYes.IsChecked = true;
            }
            else
            {
                radOnSiteNo.IsChecked = true;
            }

        }


        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// 
        /// Creates an event to replace the old version of itself.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            eventToSubmit.EventItemName = txtEventName.Text;

            //Checks and instantiates the minimum and maximum guest numbers
            eventToSubmit.EventItemName = txtEventName.Text;

            try
            {
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
                    int TypeSelected = (int)cboxType.SelectedValue;
                }
                catch (Exception)
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
                if (String.IsNullOrEmpty(txtEventName.Text))
                {
                    MessageBox.Show("Please enter an event name.");
                    return;
                }

                //MessageBox.Show(Unrevised.EventItemID + Unrevised.EventItemName + Unrevised.EventTypeID + Unrevised.EventTypeName + Unrevised.Description + Unrevised.OnSite + Unrevised.Transportation + "\n" + eventToSubmit.EventItemID + eventToSubmit.EventItemName + eventToSubmit.EventTypeID + eventToSubmit.EventTypeName + eventToSubmit.Description + eventToSubmit.OnSite + eventToSubmit.Transportation);
                // Submit the events
                if (myMan.EditEvent(Unrevised, eventToSubmit)==1)
                {
                    MessageBox.Show("Event Changed Successfully!");
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
