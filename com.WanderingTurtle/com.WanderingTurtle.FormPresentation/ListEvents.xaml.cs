using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private GridViewColumnHeader _sortColumn;

        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;

        private List<Event> myEventList;
        private EventManager myMan = new EventManager();

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Fills our Listview with events and initializes the window.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="(DataGridContextMenuResult)" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="(DataGridContextMenuResult)" /> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        public ListEvents()
        {
            InitializeComponent();
            Refresh();

            lvEvents.SetContextMenu(this);
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = DataGridHelper.ContextMenuClick<Event>(sender, out command);
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

        private async void ArchiveEvent(Event selectedEvent)
        {
            // Configure the message box to be displayed
            string messageBoxText = "Are you sure you want to delete this event?";
            string caption = "Delete Event?";

            // Display message box
            MessageDialogResult result = await DialogBox.ShowMessageDialog(this, messageBoxText, caption, MessageDialogStyle.AffirmativeAndNegative);

            // Process message box results
            switch (result)
            {
                case MessageDialogResult.Affirmative:
                    try
                    {
                        myMan.ArchiveAnEvent(selectedEvent);
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

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Opens a new AddNewEvent window for the user to interact with.
        /// When the window closes, we refresh our listview.
        /// </summary>
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            OpenEvent();
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Archives a no longer offered event.
        /// </summary>
        private void btnArchiveEvent_Click(object sender, RoutedEventArgs e)
        {
            ArchiveEvent(lvEvents.SelectedItem as Event);
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            OpenEvent(lvEvents.SelectedItem as Event);
        }

        /// <summary>
        /// Justin Pennington
        /// Created: 2015/3/4
        ///
        /// Searches through the retrieved Event List (myEventList) and populates the listview with results
        /// that Contain the text in the txtSearchInput (NOT case sensitive)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Event> myTempList = myMan.EventSearch(txtSearchInput.Text);
            lvEvents.ItemsSource = myTempList;
            txtSearchInput.Text = "";
        }

        private void lvEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEvent(DataGridHelper.RowClick<Event>(sender), true);
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Refreshes our Listview, a handy method instead of having to re-type code.
        /// </summary>
        private void Refresh()
        {
            try
            {
                myEventList = myMan.RetrieveEventList();
                foreach (Event x in myEventList)
                {
                    x.setFields();
                }
                lvEvents.ItemsSource = myEventList;
            }
            catch (Exception ex)
            {
                lvEvents.ItemsSource = "";
                throw new WanderingTurtleException(this, "Create an event or contact your Systems Administrator", "No data to display from the database.", ex);
            }
        }

        private void txtSearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSearch.Content = txtSearchInput.Text.Length == 0 ? "Refresh List" : "Search";
        }
    }
}