using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListEvents.xaml
    /// </summary>
    public partial class ListEvents : IDataGridContextMenu
    {
        private List<Event> _myEventList;
        private readonly EventManager _myEventManager = new EventManager();
        private readonly ProductManager _myProductManager = new ProductManager();

        /// <summary>
        /// Hunter Lind
        /// Created 2015/2/23
        /// Fills our Listview with events and initializes the window.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <see cref="DataGridContextMenuResult" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DataGridContextMenuResult" /> is not an <see cref="T:System.Enum" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The item to add already has a different logical parent.
        /// </exception>
        /// <exception cref="WanderingTurtleException">Error assigning the context menu to the component</exception>
        public ListEvents()
        {
            InitializeComponent();
            Refresh();

            LvEvents.SetContextMenu();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/15
        /// Logic for context menus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<Event>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenEvent();
                    break;

                case DataGridContextMenuResult.View:
                    OpenEvent(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenEvent(selectedItem);
                    break;

                case DataGridContextMenuResult.Delete:
                    ArchiveEvent(selectedItem);
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/09
        /// Opens the addevent ui with a record to edit or a blank screen
        /// </summary>
        /// <param name="selectedEvent"></param>
        /// <param name="readOnly"></param>
        private void OpenEvent(Event selectedEvent = null, bool readOnly = false)
        {
            try
            {
                if (selectedEvent == null)
                {
                    if (new AddEditEvent().ShowDialog() == false) return;
                    Refresh();
                }
                else
                {
                    if (new AddEditEvent(selectedEvent, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/2/23
        /// Opens a dialog to confirm archival of an event record
        /// List is refreshed after archival
        /// </summary>
        /// <param name="selectedEvent"></param>
        private async void ArchiveEvent(Event selectedEvent)
        {
            ResultsArchive results = _myProductManager.CheckToArchiveEvent(selectedEvent.EventItemID);

            if (results == ResultsArchive.OkToArchive)
            {
                // Configure the message box to be displayed
                string messageBoxText = "Are you sure you want to delete this event?";
                string caption = "Delete Event?";

                // Display message box
                MessageDialogResult result = await this.ShowMessageDialog(messageBoxText, caption, MessageDialogStyle.AffirmativeAndNegative);
                // Process message box results
                switch (result)
                {
                    case MessageDialogResult.Affirmative:
                        try
                        {
                            _myEventManager.ArchiveAnEvent(selectedEvent);
                            Refresh();
                        }
                        catch (Exception ex)
                        {
                            throw new WanderingTurtleException(this, ex);
                        }
                        break;

                    case MessageDialogResult.Negative:
                        // User pressed No button
                        // ...
                        break;
                }
            }
            else
            {
                throw new WanderingTurtleException(this, "Event cannot be archive because it has active listings associated.");
            }
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/2/23
        /// Opens a new AddNewEvent window for the user to interact with.
        /// When the window closes, we refresh our listview.
        /// </summary>
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            OpenEvent();
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/2/23
        /// Archives a no longer offered event.
        /// </summary>
        private void btnArchiveEvent_Click(object sender, RoutedEventArgs e)
        {
            ArchiveEvent(LvEvents.SelectedItem as Event);
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/02/24
        /// Opens the Edit Event ui with the selected event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            OpenEvent(LvEvents.SelectedItem as Event);
        }

        /// <summary>
        /// Justin Pennington
        /// Created: 2015/3/4
        /// Searches through the retrieved Event List (myEventList) and populates the listview with results
        /// that Contain the text in the txtSearchInput (NOT case sensitive)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Event> myTempList = _myEventManager.EventSearch(TxtSearchInput.Text);
            LvEvents.ItemsSource = myTempList;
            TxtSearchInput.Text = "";
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/04
        /// Handles double click for record view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEvent(sender.RowClick<Event>(), true);
        }

        /// <summary>
        /// Hunter Lind
        /// Created:  2015/2/23
        /// Refreshes our Listview, a handy method instead of having to re-type code.
        /// </summary>
        private void Refresh()
        {
            LvEvents.ItemsPanel.LoadContent();

            try
            {
                _myEventList = _myEventManager.RetrieveEventList();
                foreach (Event x in _myEventList)
                {
                    x.setFields();
                }
                LvEvents.ItemsSource = _myEventList;
                LvEvents.Items.Refresh();
            }
            catch (Exception ex)
            {
                LvEvents.ItemsSource = "";
                throw new WanderingTurtleException(this, "Create an event or contact your Systems Administrator", "No data to display from the database.", ex);
            }
        }

        /// <summary>
        /// Justin Pennington
        /// Created:  2015/03/27
        /// Changes the text for the search button depending on what user did.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnSearch.Content = TxtSearchInput.Text.Length == 0 ? "Refresh List" : "Search";
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/05/02
        /// Reloads the events listing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEvents_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}