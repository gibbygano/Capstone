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
            ReloadComboBox();           
        }

        private void fillComboBox()
        {
            try
            {
                cboArchiveEvent.ItemsSource = _eventManager.RetrieveEventTypeList();
                cboArchiveEvent.DisplayMemberPath = "EventName";
                cboArchiveEvent.SelectedValuePath = "EventTypeID";

                cboEditEvent.ItemsSource = DataCache._currentEventTypeList;
                cboEditEvent.DisplayMemberPath = "EventName";
                cboEditEvent.SelectedValuePath = "EventTypeID";
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Retrieving the EventTypes");
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/25
        ///
        /// Defines what the user wants to do for the event type
        /// </summary>
        /// <remarks>
        /// </remarks>
        private IEnumerable<ChangeFunction> GetFunctionList { get { return new List<ChangeFunction>(Enum.GetValues(typeof(ChangeFunction)) as IEnumerable<ChangeFunction>); } }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/11
        ///
        /// Reloads the combobox with values
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            cboFunctionChoice.ItemsSource = GetFunctionList;
        }


        private void cboFunctionChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                if (cboFunctionChoice.ToString().Equals(ChangeFunction.Add.ToString()))
                {
                    txtAddEventType.Text = "";
                    txtAddEventType.IsEnabled = true;
                    
                    cboEditEvent.IsEnabled = false;
                    txtEventTypeEdit.IsEnabled = false;
                    txtEventTypeEdit.Text = "";

                    cboArchiveEvent.IsEnabled = false;
                }
                else if (cboFunctionChoice.ToString().Equals(ChangeFunction.Edit.ToString()))
                {
                    fillComboBox();
                    cboEditEvent.IsEnabled = true;
                    cboEditEvent.SelectedIndex = -1;
                    txtEventTypeEdit.IsEnabled = true;
                    txtEventTypeEdit.Text = "";
                    txtAddEventType.IsEnabled = false;
                    cboArchiveEvent.IsEnabled = false;
                }
                else
                {
                    fillComboBox();
                    cboArchiveEvent.IsEnabled = true;
                    cboArchiveEvent.SelectedIndex = -1;
                    txtAddEventType.IsEnabled = false;

                    txtEventTypeEdit.IsEnabled = false;
                    txtEventTypeEdit.Text = "";

                }

        }


        private void Reset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            cboFunctionChoice.SelectedIndex = -1;

            txtAddEventType.IsEnabled = false;

            txtEventTypeEdit.IsEnabled = false;
            txtEventTypeEdit.Text = "";

            cboArchiveEvent.IsEnabled = false;
        }

        private void Submit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //cboArchiveEvent.SelectedValuePath = updateEventType.EventTypeID;
        }
    }
}