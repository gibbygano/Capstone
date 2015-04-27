﻿using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListEvents.xaml
    /// </summary>
    public partial class ListEvents : IDataGridContextMenu
    {
        private List<Event> _myEventList;
        private com.WanderingTurtle.BusinessLogic.EventManager _myEventManager = new com.WanderingTurtle.BusinessLogic.EventManager();
        private ProductManager _myProductManager = new ProductManager();

        /// <summary>
        /// Hunter Lind
        /// Created 2015/2/23
        /// 
        /// Fills our Listview with events and initializes the window.
        /// </summary>
        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListEvents()
        {
            InitializeComponent();
            Refresh();

            lvEvents.SetContextMenu(this);
        }

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
            ResultsArchive results = _myProductManager.CheckToArchiveEvent(selectedEvent.EventItemID);

            if (results == ResultsArchive.OkToArchive)
            {
                try
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
                catch (Exception)
                {
                    
                    throw;
                }
            }
            else
            {
                throw new WanderingTurtleException(this, "Event cannot be archive because it has active listings associated.");
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
            List<Event> myTempList = _myEventManager.EventSearch(txtSearchInput.Text);
            lvEvents.ItemsSource = myTempList;
            txtSearchInput.Text = "";
        }

        private void lvEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEvent(sender.RowClick<Event>(), true);
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Refreshes our Listview, a handy method instead of having to re-type code.
        /// </summary>
        private void Refresh()
        {
            try
            {
                _myEventList = _myEventManager.RetrieveEventList();
                foreach (Event x in _myEventList)
                {
                    x.setFields();
                }
                lvEvents.ItemsSource = _myEventList;
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