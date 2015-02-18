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
using System.Windows.Shapes;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ViewList.xaml
    /// </summary>
    public partial class ViewList : Window
    {
        private EventManager myMan = new EventManager();
        List<Event> myEventList;

        /// <summary>
        /// Instantiates this form and attempts to populate the Event Listview
        /// </summary>
        public ViewList()
        {
            InitializeComponent();
            try
            {
                myEventList = myMan.RetrieveEventList();
                lvEvents.ItemsSource = myEventList;
            }
            catch(Exception ex)
            {
                MessageBox.Show("No events in the database exist");
            }
        }

        /// <summary>
        /// Uses the EventManager to archive a completed / no longer offered event.
        /// It uses the selected event to delete a specific item.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Event EventToDelete = (Event)lvEvents.SelectedItems[0];
            myMan.ArchiveAnEvent(EventToDelete);
        }

        /// <summary>
        /// Opens the AddEventWindow so the user can input new event information
        /// </summary>
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Window AddEvent = new AddNewEvent();
            AddEvent.Show();
        }

        /// <summary>
        /// Opens the EditEvent form using the selected event in the eventlist to autopopulate said form. 
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window EditEvent = new EditExistingEvent();
            EditEvent.Show();
        }
    }

}
