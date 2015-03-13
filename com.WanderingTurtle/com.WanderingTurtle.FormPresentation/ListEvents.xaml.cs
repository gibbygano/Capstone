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
using com.WanderingTurtle.Common;
using EventManager = com.WanderingTurtle.BusinessLogic.EventManager;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListEventss.xaml
    /// </summary>
    public partial class ListEvents : UserControl
    {
        private EventManager myMan = new EventManager();
        List<Event> myEventList;

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Fills our Listview with events and initializes the window.
        /// </summary>
        public ListEvents()
        {
            InitializeComponent();
            Refresh();
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
                foreach(Event x in myEventList)
                {
                    x.setFields();
                }
                lvEvents.ItemsSource = myEventList;
            }
            catch (Exception ex)
            {
                lvEvents.ItemsSource = "";
                MessageBox.Show("No data to display from the database. Create an event or contact your Systems Administrator");
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
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Opens a new AddNewEvent window for the user to interact with. 
        /// When the window closes, we refresh our listview.
        /// </summary>
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Window AddEvent = new AddNewEvent();

            if (AddEvent.ShowDialog() == false)
            {
                Refresh();
            }
        }


        private void btnViewEventDetails(object sender, RoutedEventArgs e)
        {
            try
            {
                Event eventToView = (Event)lvEvents.SelectedItem;
                ViewEventDetails temp = new ViewEventDetails(eventToView);
                temp.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("No Event selected, please select an Event and try again");
            }
        }

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Archives a no longer offered event. 
        /// </summary>
        private void btnArchiveEvent_Click(object sender, RoutedEventArgs e)
        {
            // Configure the message box to be displayed 
            string messageBoxText = "Are you sure you want to delete this event?";
            string caption = "Delete Event?";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results 
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        Event EventToDelete = (Event)lvEvents.SelectedItems[0];
                        myMan.ArchiveAnEvent(EventToDelete);
                        Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    // User pressed No button 
                    // ... 
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button 
                    // ... 
                    break;
            }
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event EventToEdit = (Event)lvEvents.SelectedItem;
                EditExistingEvent editWindow = new EditExistingEvent(EventToEdit);
                if (editWindow.ShowDialog()==false)
                {
                    Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select an event to edit");
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
            //List<Event> myTempList = new List<Event>();
            if (!txtSearchInput.Text.Equals("") && !txtSearchInput.Text.Equals("*"))
            {

                List<Event> myTempList = new List<Event>();
                //Lambda Version
                myTempList.AddRange(myEventList.Where(s => s.EventItemName.ToUpper().Contains(txtSearchInput.Text.ToUpper()))
                    .Select(s => s));
                //LINQ version
                //myTempList.AddRange(
                //        from inEvent in myEventList
                //        where inEvent.EventItemName.ToUpper().Contains(txtSearchInput.Text.ToUpper())
                //        select inEvent); 

                //Will empty the search list if nothing is found so they will get feedback for typing something incorrectly
                lvEvents.ItemsSource = myTempList;
            }
            else if (txtSearchInput.Text.Equals(""))
            {
                lvEvents.ItemsSource = myEventList;
            }
        }

        private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddEvent.IsEnabled = true;
            btnArchiveEvent.IsEnabled = true;
            btnEditEvent.IsEnabled = true;
            btnViewDetails.IsEnabled = true;
        }


        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;
        private GridViewColumnHeader _sortColumn;

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
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There must be data in the list before you can sort it");
            }
        }
    }
}