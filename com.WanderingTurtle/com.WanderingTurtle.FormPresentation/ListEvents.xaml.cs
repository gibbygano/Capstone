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
            catch (Exception)
            {
                MessageBox.Show("No database able to be accessed for event list");
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

        /// <summary>
        /// Hunter Lind || 2015/2/23
        /// Archives a no longer offered event. 
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