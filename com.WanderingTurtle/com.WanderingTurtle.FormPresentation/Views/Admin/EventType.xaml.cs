using com.WanderingTurtle.BusinessLogic;
using System.Windows.Controls;
using com.WanderingTurtle.FormPresentation.Models;
using com.WanderingTurtle.Common;
using System.Collections.Generic;
using MahApps.Metro.Controls.Dialogs;
using System;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation.Views.Admin
{
    /// <summary>
    /// Interaction logic for EventType.xaml
    /// </summary>
    internal partial class EventType : UserControl
    {
        private EventManager _eventManager = new EventManager();
        private EventManager.EventResult _result;

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/26
        /// 
        /// Initializes the eventType admin ui
        /// </summary>
        public EventType()
        {
            InitializeComponent();
            FillComboBox();         
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/26
        /// fills the EventType combo Boxes
        /// </summary>
        private void FillComboBox()
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
        /// Miguel Santana
        /// Created: 2015/04/13
        ///
        /// Show Message Dialog
        /// </summary>
        /// <param name="component"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns>error message</returns>
        private void ShowInputErrorMessage(FrameworkElement component, string message, string title = null)
        {
            throw new InputValidationException(component, message, title);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/13
        ///
        /// Show Message Dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style"></param>
        /// <returns>awaitable Task of MessageDialogResult</returns>
        private async Task<MessageDialogResult> ShowMessage(string message, string title = null, MessageDialogStyle? style = null)
        {
            return await this.ShowMessageDialog(message, title, style);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/04/13
        ///
        /// Show Message Dialog - overloaded
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns>error message</returns>
        private void ShowErrorMessage(string message, string title = null)
        {
            throw new WanderingTurtleException(this, message, title);
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/26
        /// 
        /// Sends the eventType that user wants to archive to the BusinessLogic Layer
        /// Returns an indication of whether or not the transaction was successful
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnArchiveType_Click(object sender, RoutedEventArgs e)
        {
            Common.EventType archiveEventType = (Common.EventType)cboArchiveEvent.SelectedItem;

            try
            {
                _result = _eventManager.ArchiveAnEventType(archiveEventType);

                if (_result.Equals(EventManager.EventResult.Success))
                {
                     await ShowMessage("Your Request was Processed Successfully", "Success");
                     cboArchiveEvent.SelectedIndex = -1;                   
                     FillComboBox();
                }                
                else
                {
                    await ShowMessage("Event type could not be updated.", "Error");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/26
        /// 
        /// Sends the eventType that user wants to edit to the BusinessLogic Layer
        /// Returns an indication of whether or not the transaction was successful
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnEditType_Click(object sender, RoutedEventArgs e)
        {
            if (!Validator.ValidateString(txtEditEventType.Text))
            {
                ShowInputErrorMessage(txtEditEventType, "Please enter a valid Event Type");
            }

            try
            {
                Common.EventType oldEventType = (Common.EventType)cboEditEvent.SelectedItem;
                Common.EventType newEventType = new Common.EventType(oldEventType.EventTypeID, txtEditEventType.Text);

                _result = _eventManager.EditEventType(oldEventType, newEventType);

                if (_result.Equals(EventManager.EventResult.Success))
                {
                     await ShowMessage("Your Request was Processed Successfully", "Success");
                     cboEditEvent.SelectedIndex = -1;
                     txtEditEventType.Text = "";
                     FillComboBox();
                }                
                else
                {
                    await ShowMessage("Event type could not be updated.", "Error");
                }
            }
            catch (Exception)
            {   
                throw;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/26
        /// 
        /// Sends the eventType that user wants to edit to the BusinessLogic Layer
        /// Returns an indication of whether or not the transaction was successful
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAddType_Click(object sender, RoutedEventArgs e)
        {
            if (!txtAddEventType.Text.ValidateString())
            {
                ShowInputErrorMessage(txtAddEventType, "Please enter a valid event type.  No spaces allowed.");
            }
            try
            {
                _result = _eventManager.AddNewEventType(txtAddEventType.Text);

                if (_result.Equals(EventManager.EventResult.Success))
                {
                    await ShowMessage("Your Request was Processed Successfully", "Success");
                    txtAddEventType.Text = "";
                    FillComboBox();
                }
                else
                {
                    await ShowMessage("Event type could not be added.", "Error");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString(), null);
                throw;
            }
        }
    }
}