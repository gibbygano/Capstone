using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for EditExistingEvent.xaml
    /// </summary>
    public partial class AddEditEvent
    {
        private EventManager _eventManager = new EventManager();

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Initializes the form and populates a combobox with event types.
        /// </summary>
        public AddEditEvent()
        {
            Setup();
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        ///
        /// Fills out our form with information from NewEvent.
        /// Also saves an Unrevised version of NewEvent.
        /// </summary>
        /// <param name="EventToEdit">The Event we are going to edit</param>
        public AddEditEvent(Event EventToEdit, bool ReadOnly = false)
        {
            this.CurrentEvent = EventToEdit;
            Setup();
            if (ReadOnly)
            {
                FrameworkElement[] controlsToKeepEnabled = { BtnCancel };
                WindowHelper.MakeReadOnly(this.Content as Panel, controlsToKeepEnabled);
            }
        }

        public Event CurrentEvent { get; private set; }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        ///
        /// Creates an event to replace the old version of itself.
        /// </summary>
        private void AddNewEvent()
        {
            var NewEvent = new Event();
            NewEvent.EventItemName = txtEventName.Text;

            try
            {
                // On-site //
                if (radOnSiteYes.IsChecked == true)
                {
                    NewEvent.OnSite = true;
                }
                else if (radOnSiteNo.IsChecked == true)
                {
                    NewEvent.OnSite = false;
                }
                else
                {
                    DialogBox.ShowMessageDialog(this, "Please fill in the on site field");
                    return;
                }

                // Provided transport //
                if (radTranspNo.IsChecked == true)
                {
                    NewEvent.Transportation = false;
                }
                else if (radTranspYes.IsChecked == true)
                {
                    NewEvent.Transportation = true;
                }
                else
                {
                    DialogBox.ShowMessageDialog(this, "Please fill out the Transportation field");
                    return;
                }

                NewEvent.Description = txtDescrip.Text;

                if (cboxType.SelectedItem != null)
                {
                    NewEvent.EventTypeID = (cboxType.SelectedItem as EventType).EventTypeID;
                }
                else
                {
                    DialogBox.ShowMessageDialog(this, "Please select an event type!");
                    return;
                }
                if (String.IsNullOrEmpty(txtEventName.Text))
                {
                    DialogBox.ShowMessageDialog(this, "Please enter an event name.");
                    return;
                }
                EventManager.EventResult result = _eventManager.AddNewEvent(NewEvent);
                if (result == EventManager.EventResult.Success)
                {
                    DialogBox.ShowMessageDialog(this, "Successfully Added Event");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "Error adding new event");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEvent == null) { AddNewEvent(); }
            else { EditExistingEvent(); }
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        ///
        /// Fills out our form with information from EventToEdit.
        /// Also saves an Unrevised version of EventToEdit.
        /// </summary>
        private void EditExistingEvent()
        {
            var NewEvent = new Event();
            NewEvent.EventItemName = txtEventName.Text;

            //Checks and instantiates the minimum and maximum guest controlsToKeepEnabled
            NewEvent.EventItemName = txtEventName.Text;

            try
            {
                // On-site //
                if (radOnSiteYes.IsChecked == true)
                {
                    NewEvent.OnSite = true;
                }
                else if (radOnSiteNo.IsChecked == true)
                {
                    NewEvent.OnSite = false;
                }
                else
                {
                    DialogBox.ShowMessageDialog(this, "Please fill in the on site field");
                    return;
                }

                // Provided transport //
                if (radTranspNo.IsChecked == true)
                {
                    NewEvent.Transportation = false;
                }
                else if (radTranspYes.IsChecked == true)
                {
                    NewEvent.Transportation = true;
                }
                else
                {
                    DialogBox.ShowMessageDialog(this, "Please fill out the Transportation field");
                    return;
                }

                NewEvent.Description = txtDescrip.Text;

                if (cboxType.SelectedItem != null)
                {
                    NewEvent.EventTypeID = (cboxType.SelectedItem as EventType).EventTypeID;
                }
                else
                {
                    DialogBox.ShowMessageDialog(this, "Please select an event type!");
                    return;
                }
                if (String.IsNullOrEmpty(txtEventName.Text))
                {
                    DialogBox.ShowMessageDialog(this, "Please enter an event name.");
                    return;
                }

                //DialogBox.ShowMessageDialog(Unrevised.EventItemID + Unrevised.EventItemName + Unrevised.EventTypeID + Unrevised.EventTypeName + Unrevised.Description + Unrevised.OnSite + Unrevised.Transportation + "\n" + NewEvent.EventItemID + NewEvent.EventItemName + NewEvent.EventTypeID + NewEvent.EventTypeName + NewEvent.Description + NewEvent.OnSite + NewEvent.Transportation);

                // Submit the events
                var EventManagerResult = _eventManager.EditEvent(CurrentEvent, NewEvent);
                if (EventManagerResult.Equals(EventManager.EventResult.Success))
                {
                    DialogBox.ShowMessageDialog(this, "Event Changed Successfully!");
                    this.Close();
                }
                else { throw new Exception(EventManagerResult.ToString()); }
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "Error Editing Event");
            }
        }

        private void SetFields()
        {
            if (CurrentEvent == null)
            {
                txtEventName.Text = null;
                cboxType.SelectedItem = null;
                txtDescrip.Text = null;
                radTranspYes.IsChecked = false;
                radTranspNo.IsChecked = false;
                radOnSiteYes.IsChecked = false;
                radOnSiteNo.IsChecked = false;
            }
            else
            {
                txtEventName.Text = CurrentEvent.EventItemName;
                txtDescrip.Text = CurrentEvent.Description;

                foreach (EventType item in cboxType.Items)
                {
                    if (CurrentEvent.EventTypeID.Equals(item.EventTypeID))
                    { cboxType.SelectedItem = item; }
                }

                if (CurrentEvent.Transportation == true)
                { radTranspYes.IsChecked = true; }
                else
                { radTranspNo.IsChecked = true; }

                if (CurrentEvent.OnSite == true)
                { radOnSiteYes.IsChecked = true; }
                else
                { radOnSiteNo.IsChecked = true; }
            }
        }

        private void Setup()
        {
            InitializeComponent();

            try
            {
                cboxType.Items.Clear();
                cboxType.ItemsSource = _eventManager.RetrieveEventTypeList();
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "Error getting Event Types");
            }

            SetFields();
        }
    }
}