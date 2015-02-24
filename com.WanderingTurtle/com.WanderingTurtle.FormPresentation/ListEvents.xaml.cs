using System;
using System.Collections.Generic;
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
                lvEvents.ItemsSource = myEventList;
            }
            catch (Exception ex)
            {
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
            Event EventToDelete = (Event)lvEvents.SelectedItems[0];
            myMan.ArchiveAnEvent(EventToDelete);
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
                temp.Show();
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
                        Event EventToDelete = (Event)lvEvents.SelectedItem;
                        myMan.ArchiveAnEvent(EventToDelete);
                        Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No Event selected, please select an Event and try again.");
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
                EditExistingEvent temp = new EditExistingEvent(EventToEdit);
                temp.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select an event to edit");
            }
        }
    }
}