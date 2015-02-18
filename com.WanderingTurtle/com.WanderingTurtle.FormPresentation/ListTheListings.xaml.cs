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
    /// Interaction logic for ListTheListings.xaml
    /// </summary>
    public partial class ListTheListings : UserControl
    {
        private EventManager myMan = new EventManager();
        List<Event> myEventList;

        public ListTheListings()
        {
            InitializeComponent();
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

            private void btnAddListing_Click(object sender, RoutedEventArgs e)
            {
                Window AddItemListings = new AddItemListing();
                AddItemListings.Show();
            }

            // Uses existing selected indeces to create a window that will be filled with the selected objects contents.
            private void btnEdit_Click(object sender, RoutedEventArgs e)
            {
                Window EditEvent = new EditExistingEvent();
                EditEvent.Show();
            }
        }




    }

