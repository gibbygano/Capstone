using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation.Views.Admin
{
    /// <summary>
    /// Interaction logic for EventType.xaml
    /// </summary>
    internal partial class EventType
    {
        private readonly EventManager _eventManager = new EventManager();
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
                CboArchiveEvent.ItemsSource = _eventManager.RetrieveEventTypeList();
                CboArchiveEvent.DisplayMemberPath = "EventName";
                CboArchiveEvent.SelectedValuePath = "EventTypeID";

                CboEditEvent.ItemsSource = DataCache._currentEventTypeList;
                CboEditEvent.DisplayMemberPath = "EventName";
                CboEditEvent.SelectedValuePath = "EventTypeID";
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
            Common.EventType archiveEventType = (Common.EventType)CboArchiveEvent.SelectedItem;

            _result = _eventManager.ArchiveAnEventType(archiveEventType);

            if (_result.Equals(EventManager.EventResult.Success))
            {
                await ShowMessage("Your Request was Processed Successfully", "Success");
                CboArchiveEvent.SelectedIndex = -1;
                FillComboBox();
            }
            else
            {
                await ShowMessage("Event type could not be updated.", "Error");
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
            if (!TxtEditEventType.Text.ValidateString())
            {
                ShowInputErrorMessage(TxtEditEventType, "Please enter a valid Event Type");
            }

            Common.EventType oldEventType = (Common.EventType)CboEditEvent.SelectedItem;
            Common.EventType newEventType = new Common.EventType(oldEventType.EventTypeID, TxtEditEventType.Text);

            _result = _eventManager.EditEventType(oldEventType, newEventType);

            if (_result.Equals(EventManager.EventResult.Success))
            {
                await ShowMessage("Your Request was Processed Successfully", "Success");
                CboEditEvent.SelectedIndex = -1;
                TxtEditEventType.Text = "";
                FillComboBox();
            }
            else
            {
                await ShowMessage("Event type could not be updated.", "Error");
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
            if (!TxtAddEventType.Text.ValidateString())
            {
                ShowInputErrorMessage(TxtAddEventType, "Please enter a valid event type.  No spaces allowed.");
            }
            try
            {
                _result = _eventManager.AddNewEventType(TxtAddEventType.Text);

                if (_result.Equals(EventManager.EventResult.Success))
                {
                    await ShowMessage("Your Request was Processed Successfully", "Success");
                    TxtAddEventType.Text = "";
                    FillComboBox();
                }
                else
                {
                    await ShowMessage("Event type could not be added.", "Error");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Error Adding Event Type");
            }
        }

        private async void BtnRunXML_Click(object sender, RoutedEventArgs e)
        {
            AccountingManager myMan = new AccountingManager();

            DateTime formStartDate = (DateTime)(DateStart.SelectedDate);
            DateTime formEndDate = (DateTime)(DateEnd.SelectedDate);

            var report = myMan.GetAccountingDetails(formStartDate, formEndDate);

            string filename = "report - " +
                DateTime.Now.ToShortDateString().Replace("/", "-")
                + " at " +
                DateTime.Now.ToShortTimeString().Replace(":", "-") + ".xml";

            if (report.XMLFile(filename))
            {
                await ShowMessage("Your Report has been saved successfully.", "Success");
                DateStart.Text = null;
                DateEnd.Text = null;
            }
            else
            {
                await ShowMessage("There was a problem.  File not written.", "Error");
                DateStart.Text = null;
                DateEnd.Text = null;
            }
        }
    }
}