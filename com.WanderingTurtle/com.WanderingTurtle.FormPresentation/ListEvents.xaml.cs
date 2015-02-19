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
        
        // This is where we instantiate our window and populate the EventList with "myEventList" items.
        public ListEvents()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            try
            {
                myEventList = myMan.RetrieveEventList();
                lvEvents.ItemsSource = myEventList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No database able to be accessed for event list");
            }
        }

         private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Calls the delete method from the BLL to archive an event.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Event EventToDelete = (Event)lvEvents.SelectedItems[0];
            myMan.ArchiveAnEvent(EventToDelete);
        }

        // Calls the AddEvent window for the user to input information into
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Window AddEvent = new AddNewEvent();

            if (AddEvent.ShowDialog() == false)
            {
                Refresh();
            }
        }

        // Uses existing selected indeces to create a window that will be filled with the selected objects contents.



        /// <summary>
        /// Uses the EventManager to archive a completed / no longer offered event.
        /// It uses the selected event to delete a specific item.
        /// </summary>
        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            int x = lvEvents.SelectedIndex;
            Event EventToDelete = (Event)lvEvents.Items[x];
            myMan.ArchiveAnEvent(EventToDelete);
            Refresh();
        }
    }

}