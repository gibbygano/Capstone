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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheListings.xaml
    /// </summary>
    public partial class ListTheListings : UserControl
    {

        private OrderManager ordMan = new OrderManager();
        List<ListItemObject> myListingList;

        public ListTheListings()
        {
            InitializeComponent();
            refreshData();
        }

        private void refreshData()
        {
            try
            {
                myListingList = ordMan.RetrieveListItemList();
                lvEvents.ItemsSource = myListingList;
            }
            catch (Exception)
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
            if (AddItemListings.ShowDialog() == false)
            {
                refreshData();
            }

        }

        // Uses existing selected indeces to create a window that will be filled with the selected objects contents.
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Event EventToEdit = (Event)lvEvents.SelectedItem;
            Window EditEvent = new EditExistingEvent(EventToEdit);
            EditEvent.Show();
        }

        private void btnAddListing_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }




}

