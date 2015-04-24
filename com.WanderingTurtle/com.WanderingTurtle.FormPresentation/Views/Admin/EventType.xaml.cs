using com.WanderingTurtle.BusinessLogic;
using System.Windows.Controls;
using com.WanderingTurtle.FormPresentation.Models;
using com.WanderingTurtle.Common;
using System.Collections.Generic;
using System;

namespace com.WanderingTurtle.FormPresentation.Views.Admin
{
    /// <summary>
    /// Interaction logic for EventType.xaml
    /// </summary>
    internal partial class EventType : UserControl
    {
        private EventManager _eventManager = new EventManager();
        private EventType updateEventType;


        public EventType()
        {
            InitializeComponent();
            cboArchiveEvent.ItemsSource = DataCache._currentEventTypeList;
            cboEditEvent.ItemsSource = DataCache._currentEventTypeList;
        }

        private void fillComboBox()
        {
            try
            {
                var eventTypeList = DataCache._currentEventTypeList;
                cboArchiveEvent.ItemsSource = eventTypeList;
                cboArchiveEvent.DisplayMemberPath = "EventName";
                cboArchiveEvent.SelectedValuePath = "EventTypeID";

                cboEditEvent.ItemsSource = eventTypeList;
                cboEditEvent.DisplayMemberPath = "EventName";
                cboEditEvent.SelectedValuePath = "EventTypeID";
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Retrieving the EventTypes");
            }
        }

        private void getAddInformation()
        {
            txtAddEventType.Text = updateEventType.Name;

            var list = DataCache._currentEventTypeList;
           // listEventTypes.DataContext = list;

        }

        private void getEditInformation()
        {
            cboEditEvent.Text =
            txtEventTypeEdit.Text = updateEventType.Name;
        }

         private void getArchiveInformation()
        {
            //cboArchiveEvent.SelectedValuePath = updateEventType.EventTypeID;
        }

        private void cboEditEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtAddEventType.IsEnabled = false;
        }

        private void cboArchiveEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Reset_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Submit_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }


    }
}