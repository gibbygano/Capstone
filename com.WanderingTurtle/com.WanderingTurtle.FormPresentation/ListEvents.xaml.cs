using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListEventss.xaml
    /// </summary>
    public partial class ListEvents : UserControl
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
        public ListEvents()
        {
            InitializeComponent();
            Refresh();
        }

        private void ViewEventDetails(Event eventToView, bool ReadOnly = false)
        {
            try
            {
                new AddEditEvent(eventToView, ReadOnly).ShowDialog();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Opens a new AddNewEvent window for the user to interact with.
        /// When the window closes, we refresh our listview.
        /// </summary>
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window AddEvent = new AddEditEvent();

                if (AddEvent.ShowDialog() == false)
                {
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Archives a no longer offered event.
        /// </summary>
        private async void btnArchiveEvent_Click(object sender, RoutedEventArgs e)
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
                        Event EventToDelete = (Event)lvEvents.SelectedItems[0];
                        myMan.ArchiveAnEvent(EventToDelete);
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
        /// Unimplemented delete button code.
        /// Will be implemented by: 2015/2/27
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event EventToDelete = (Event)lvEvents.SelectedItems[0];
                myMan.ArchiveAnEvent(EventToDelete);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event EventToEdit = (Event)lvEvents.SelectedItem;
                var editWindow = new AddEditEvent(EventToEdit);
                if (editWindow.ShowDialog() == false)
                {
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
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

        /// <summary>
        /// This method will sort the listview column in both asending and desending order
        /// Created by Will Fritz 15/2/27
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEventListHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            if (column == null)
            {
                return;
            }

            if (_sortColumn == column)
            {
                // Toggle sorting direction
                _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            string header = string.Empty;

            // if binding is used and property name doesn't match header content
            Binding b = _sortColumn.Column.DisplayMemberBinding as Binding;

            if (b != null)
            {
                header = b.Path.Path;
            }

            try
            {
                ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lvEvents.ItemsSource);
                resultDataView.SortDescriptions.Clear();
                resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There must be data in the list before you can sort it");
            }
        }

        private void lvEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewEventDetails(DataGridHelper.DataGridRow_Click<Event>(sender, e), true);
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
            if (txtSearchInput.Text.Length == 0)
            {
                btnSearch.Content = "Refresh List";
            }
            else
            {
                btnSearch.Content = "Search";
            }
        }
    }
}