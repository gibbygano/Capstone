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

        private ProductManager prodMan = new ProductManager();
        List<ItemListing> myListingList;

        public ListTheListings()
        {
            InitializeComponent();
            refreshData();
        }

        private void refreshData()
        {
            try
            {
                myListingList = prodMan.RetrieveItemListingList();
                lvListing.ItemsSource = myListingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No database able to be accessed for Listings");
                //MessageBox.Show(ex.ToString());
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
        }

        private void btnArchiveListing_click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemListing ListingToDelete = (ItemListing)lvListing.SelectedItems[0];
                MessageBox.Show(prodMan.ArchiveItemListing(ListingToDelete).ToString());

                refreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }




}

