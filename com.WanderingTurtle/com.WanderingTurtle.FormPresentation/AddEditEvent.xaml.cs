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
        private readonly EventManager _eventManager = new EventManager();

        public Event OriginalEvent { get; private set; }
        private readonly Event _eventToSubmit = new Event();

        /// <summary>
        /// Hunter Lind
        /// Created: 2015/2/23
        /// Initializes the form and populates a combobox with event types.
        /// </summary>
        public AddEditEvent()
        {
            Setup();
            Title = "Add a new Event";
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/2/23
        /// Fills out our form with information from NewEvent.
        /// Also saves an Unrevised version of NewEvent.
        /// </summary>
        /// <param name="eventToEdit">The Event we are going to edit</param>
        /// <param name="readOnly">Make the form ReadOnly.</param>
        /// <exception cref="WanderingTurtleException">Occurrs making components readonly</exception>
        public AddEditEvent(Event eventToEdit, bool readOnly = false)
        {
            OriginalEvent = eventToEdit;

            _eventToSubmit.Active = eventToEdit.Active;
            _eventToSubmit.Description = eventToEdit.Description;
            _eventToSubmit.EventTypeID = eventToEdit.EventTypeID;
            _eventToSubmit.OnSite = eventToEdit.OnSite;
            _eventToSubmit.Transportation = eventToEdit.Transportation;
            _eventToSubmit.EventItemName = eventToEdit.EventItemName;
            _eventToSubmit.EventItemID = eventToEdit.EventItemID;

            Setup();

            Title = "Editing Event: " + OriginalEvent.EventItemName;

            if (readOnly) { (Content as Panel).MakeReadOnly(BtnCancel); }
        }

        /// <summary>
        /// Hunter Lind
        /// Created: 2015/2/23
        /// Creates an event to replace the old version of itself.
        /// </summary>
        private async void AddNewEvent()
        {
            var newEvent = new Event();

            try
            {
                newEvent.EventItemName = TxtEventName.Text;
                // On-site //
                if (RadOnSiteYes.IsChecked == true)
                {
                    newEvent.OnSite = true;
                }
                else if (RadOnSiteNo.IsChecked == true)
                {
                    newEvent.OnSite = false;
                }
                else
                {
                    throw new InputValidationException(RadOnSite, "Please fill in the on site field");
                }

                // Provided transport //
                if (RadTranspNo.IsChecked == true)
                {
                    newEvent.Transportation = false;
                }
                else if (RadTranspYes.IsChecked == true)
                {
                    newEvent.Transportation = true;
                }
                else
                {
                    throw new InputValidationException(RadTransp, "Please fill out the Transportation field");
                }

                newEvent.Description = TxtDescrip.Text;

                if (CboxType.SelectedItem != null)
                {
                    newEvent.EventTypeID = ((EventType)CboxType.SelectedItem).EventTypeID;
                }
                else
                {
                    throw new InputValidationException(CboxType, "Please select an event type!");
                }
                if (String.IsNullOrEmpty(TxtEventName.Text))
                {
                    throw new InputValidationException(TxtEventName, "Please enter an event name.");
                }
                EventManager.EventResult result = _eventManager.AddNewEvent(newEvent);
                if (result == EventManager.EventResult.Success)
                {
                    await this.ShowMessageDialog("Successfully Added Event");
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                if (ex is InputValidationException) { throw new InputValidationException((InputValidationException)ex); }
                throw new WanderingTurtleException(this, ex, "Error adding new event");
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Added button to allow cancel of the form function.  Combined Edit and Add forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Added button to allow Reset of the form fields.  Combined Edit and Add forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Logic rearranged when combining the add and edit forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (OriginalEvent == null) { AddNewEvent(); }
            else { EditExistingEvent(); }
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/2/23
        /// Fills out our form with information from EventToEdit.
        /// Also saves an Unrevised version of EventToEdit.
        /// </summary>
        private async void EditExistingEvent()
        {
            _eventToSubmit.EventItemName = TxtEventName.Text;

            //Checks and instantiates the minimum and maximum guest controlsToKeepEnabled
            _eventToSubmit.EventItemName = TxtEventName.Text;

            try
            {
                // On-site //
                if (RadOnSiteYes.IsChecked == true)
                {
                    _eventToSubmit.OnSite = true;
                }
                else if (RadOnSiteNo.IsChecked == true)
                {
                    _eventToSubmit.OnSite = false;
                }
                else
                {
                    throw new InputValidationException(RadOnSite, "Please fill in the on site field");
                }

                // Provided transport //
                if (RadTranspNo.IsChecked == true)
                {
                    _eventToSubmit.Transportation = false;
                }
                else if (RadTranspYes.IsChecked == true)
                {
                    _eventToSubmit.Transportation = true;
                }
                else
                {
                    throw new InputValidationException(RadTransp, "Please fill out the Transportation field");
                }

                _eventToSubmit.Description = TxtDescrip.Text;

                if (CboxType.SelectedItem != null)
                {
                    _eventToSubmit.EventTypeID = ((EventType)CboxType.SelectedItem).EventTypeID;
                }
                else
                {
                    throw new InputValidationException(CboxType, "Please select an event type!");
                }
                if (String.IsNullOrEmpty(TxtEventName.Text))
                {
                    throw new InputValidationException(TxtEventName, "Please enter an event name.");
                }

                //DialogBox.ShowMessageDialog(Unrevised.EventItemID + Unrevised.EventItemName + Unrevised.EventTypeID + Unrevised.EventTypeName + Unrevised.Description + Unrevised.OnSite + Unrevised.Transportation + "\n" + NewEvent.EventItemID + NewEvent.EventItemName + NewEvent.EventTypeID + NewEvent.EventTypeName + NewEvent.Description + NewEvent.OnSite + NewEvent.Transportation);

                // Submit the events
                var eventManagerResult = _eventManager.EditEvent(OriginalEvent, _eventToSubmit);

                if (eventManagerResult.Equals(EventManager.EventResult.Success))
                {
                    await this.ShowMessageDialog("Event Changed Successfully!");
                    DialogResult = true;
                    Close();
                }
                else { throw new WanderingTurtleException(this, eventManagerResult.ToString()); }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Editing Event");
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Method is used to populate fields when editing the event, otherwise, brings up a blank form.
        /// </summary>
        private void SetFields()
        {
            if (OriginalEvent == null)
            {
                TxtEventName.Text = null;
                CboxType.SelectedItem = null;
                TxtDescrip.Text = null;
                RadTranspYes.IsChecked = false;
                RadTranspNo.IsChecked = false;
                RadOnSiteYes.IsChecked = false;
                RadOnSiteNo.IsChecked = false;
            }
            else
            {
                TxtEventName.Text = OriginalEvent.EventItemName;
                TxtDescrip.Text = OriginalEvent.Description;

                foreach (EventType item in CboxType.Items)
                {
                    if (OriginalEvent.EventTypeID.Equals(item.EventTypeID))
                    { CboxType.SelectedItem = item; }
                }

                if (OriginalEvent.Transportation)
                { RadTranspYes.IsChecked = true; }
                else
                { RadTranspNo.IsChecked = true; }

                if (OriginalEvent.OnSite)
                { RadOnSiteYes.IsChecked = true; }
                else
                { RadOnSiteNo.IsChecked = true; }
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/06
        /// Sets up the ui
        /// </summary>
        private void Setup()
        {
            InitializeComponent();

            try
            {
                CboxType.Items.Clear();
                CboxType.ItemsSource = _eventManager.RetrieveEventTypeList();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error getting Event Types");
            }

            SetFields();
        }
    }
}