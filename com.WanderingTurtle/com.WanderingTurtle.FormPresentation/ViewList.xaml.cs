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
        private List<Event> myEventList = myMan.RetrieveEventList();

        public ViewList()
        {
            InitializeComponent();
            /*
            lvEvents.Items.Clear();
            foreach(Event toFill in myEventList)
            {
                lvEvents.Items.Add(toFill);
            }
             * */
            lvEvents.ItemsSource = myEventList;
        }



        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Event EventToDelete = (Event)lvEvents.SelectedItems[0];
            ArchiveAnEvent(EventToDelete);
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Window AddEvent = new AddNewEvent();
            AddEvent.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window EditEvent = new EditExistingEvent();
            EditEvent.Show();
        }
    }

}
